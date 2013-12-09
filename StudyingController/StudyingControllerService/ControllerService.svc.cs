using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using StudyingControllerEntityModel;
using EntitiesDTO;
using System.Globalization;

namespace StudyingControllerService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public partial class ControllerService : IControllerService
    {
        #region Fields & Properties

        private static readonly TimeSpan SESSION_TIMEOUT = TimeSpan.FromSeconds(20 * 60); // 20 minutes

        private static readonly int SESSION_POLL_PERIOD = 1000;

        private System.Threading.Timer sessionMonitoringTimer;

        private Dictionary<double, Session> sessions = new Dictionary<double, Session>();

        //private SystemConfiguration configuration;
        //public SystemConfiguration Configuration
        //{
        //    get
        //    {
        //        if (configuration == null)
        //        {
        //            try
        //            {
        //                SystemConfiguration result = null;

        //                using (UniversityEntities context = new UniversityEntities())
        //                {
        //                    result = context.SystemConfigurations.Include("StudyRange").FirstOrDefault();
        //                }

        //                return result;
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
        //            }
        //        }
        //        return configuration;
        //    }
        //}

        #endregion

        #region Constructors

        public ControllerService()
        {
            sessionMonitoringTimer = new System.Threading.Timer(OnPollSessions, null, SESSION_POLL_PERIOD, SESSION_POLL_PERIOD);
        }

        #endregion

        #region Session's stuff

        private void OnPollSessions(object state)
        {
            lock (sessions)
            {
                List<double> removedKeys = new List<double>();

                foreach (KeyValuePair<double, Session> pair in sessions)
                    if (DateTime.UtcNow.Subtract(pair.Value.LastAccessTime) > SESSION_TIMEOUT)
                        removedKeys.Add(pair.Key);

                foreach (double key in removedKeys)
                    sessions.Remove(key);
            }
        }

        private void CheckSession(Session session)
        {
            lock (sessions)
            {
                Session s;
                if (!sessions.TryGetValue(session.SessionID, out s))
                    throw new Exception("Доступ заборонено: сесія неактуальна!");

                s.LastAccessTime = DateTime.UtcNow;
            }
        }

        #endregion

        private T GetDTO<T>(System.Data.Objects.DataClasses.EntityObject o) where T : BaseEntityDTO
        {
            return (o as IDTOable<T>).ToDTO();
        }

        public Session Login(string login, string password)
        {
            Session session = null;

            try
            {
                using (UniversityEntities context = new UniversityEntities())
                {
                    SystemUser user = (from u in context.SystemUsers.Include("UserInformation")
                                       where u.Login == login.ToLower()
                                       select u).FirstOrDefault();

                    if (!(user != null && Encoding.UTF8.GetString(user.Password) == password))
                    {
#if DEBUG
                        if (user == null)
                        {
                            user = new SystemUser();
                            user.Login = login;
                            user.Password = Encoding.UTF8.GetBytes(password);
                            user.iUserRole = 1;
                            var userInformation = new UserInformation()
                            {
                                FirstName = login,
                                LastName = login
                            };
                            context.AddToUserInformations(userInformation);
                            context.SaveChanges();

                            user.UserInformation = userInformation;

                            context.AddToSystemUsers(user);
                            context.SaveChanges();
                        }
#else
                        throw new Exception("У доступі відмовлено!");
#endif
                    }

                    session = new Session(GetSystemUserDTO(user, context));

                    lock (sessions)
                        sessions[session.SessionID] = session;

                    return session;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        private SystemUserDTO GetSystemUserDTO(SystemUser user, UniversityEntities context)
        {
            switch (user.Role)
            {
                case UserRoles.None:
                    throw new Exception("Unknown user's role! This role cannot be stored into DB");
                case UserRoles.InstituteAdmin:
                    return GetDTO<InstituteAdminDTO>(user);
                case UserRoles.FacultyAdmin:
                    return GetDTO<FacultyAdminDTO>(user);
                case UserRoles.InstituteSecretary:
                    return GetDTO<InstituteSecretaryDTO>(user);
                case UserRoles.FacultySecretary:
                    return GetDTO<FacultySecretaryDTO>(user);
                case UserRoles.Student:
                    var student = (user as Student);

                    context.LoadProperty(student, "Groups");
                    var currentGroup = student.Groups.FirstOrDefault();//(g => g.StudyRangeID == Configuration.StudyRangeID);

                    if (currentGroup == null)
                        throw new Exception("Student hasnt a group!");

                    student.CurrentGroupID = currentGroup.ID;

                    return GetDTO<StudentDTO>(student);
                case UserRoles.Teacher:
                    return GetDTO<TeacherDTO>(user);
                default:
                    return GetDTO<SystemUserDTO>(user);
            }
        }

        private SystemUser GetSystemUser(SystemUserDTO user)
        {
            switch (user.Role)
            {
                case UserRoles.None:
                    throw new Exception("What the fuck?! This role cannot be stored into DB");
                case UserRoles.InstituteAdmin:
                    return new InstituteAdmin(user);
                case UserRoles.FacultyAdmin:
                    return new FacultyAdmin(user);
                case UserRoles.InstituteSecretary:
                    return new InstituteSecretary(user);
                case UserRoles.FacultySecretary:
                    return new FacultySecretary(user);
                case UserRoles.Student:
                    return new Student(user);
                case UserRoles.Teacher:
                    return new Teacher(user);
                default:
                    return new SystemUser(user);
            }
        }

        public List<InstituteDTO> GetInstitutes(Session session)
        {
            try
            {
                CheckSession(session);
                List<InstituteDTO> result = new List<InstituteDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    foreach (Institute institute in context.Institutes)
                    {
                        result.Add(GetDTO<InstituteDTO>(institute));
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<FacultyDTO> GetFaculties(Session session, int? instituteID)
        {
            try
            {
                CheckSession(session);
                List<FacultyDTO> result = new List<FacultyDTO>();
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = from f in context.Faculties.Include("Specializations")
                                where f.InstituteID == instituteID || (instituteID == null && f.InstituteID == null)
                                select f;
                    foreach (var faculty in query)
                    {
                        context.LoadProperty(faculty, "Institute");
                        result.Add(GetDTO<FacultyDTO>(faculty));
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<FacultyDTO> GetFaculties(Session session)
        {
            try
            {
                CheckSession(session);
                List<FacultyDTO> result = new List<FacultyDTO>();
                using (UniversityEntities context = new UniversityEntities())
                {
                    foreach (var faculty in context.Faculties)
                        result.Add(GetDTO<FacultyDTO>(faculty));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<CathedraDTO> GetCathedras(Session session, int facultyID)
        {
            try
            {
                CheckSession(session);
                List<CathedraDTO> result = new List<CathedraDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = from c in context.Cathedras.Include("Subjects").Include("Faculty")
                                where c.FacultyID == facultyID
                                select c;
                    foreach (var c in query)
                        result.Add(GetDTO<CathedraDTO>(c));
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<CathedraDTO> GetCathedras(Session session)
        {
            try
            {
                CheckSession(session);
                List<CathedraDTO> result = new List<CathedraDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    foreach (var cathedra in context.Cathedras.Include("Subjects"))
                        result.Add(GetDTO<CathedraDTO>(cathedra));
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public GroupDTO GetGroupByID(Session session, int? groupID)
        {
            try
            {
                CheckSession(session);
                GroupDTO result = new GroupDTO();

                using (UniversityEntities context = new UniversityEntities())
                {
                    Group group = (from g in context.Groups
                                   where g.ID == groupID
                                   select g).FirstOrDefault();

                    result = GetDTO<GroupDTO>(group);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }

        }

        public List<GroupDTO> GetGroups(Session session, int cathedraID)
        {
            try
            {
                CheckSession(session);
                List<GroupDTO> result = new List<GroupDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = from g in context.Groups.Include("Specialization").Include("Cathedra")
                                where g.CathedraID == cathedraID
                                select g;
                    foreach (var group in query)
                        result.Add(GetDTO<GroupDTO>(group));
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<GroupDTO> GetGroups(Session session)
        {
            try
            {
                CheckSession(session);
                List<GroupDTO> result = new List<GroupDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    foreach (var group in context.Groups)
                        result.Add(GetDTO<GroupDTO>(group));
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void SaveInstitute(Session session, InstituteDTO institute)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var item = context.Institutes.FirstOrDefault(inst => inst.ID == institute.ID);
                    if (item == null)
                        context.AddToInstitutes(new Institute(institute));
                    else
                        item.Assign(institute);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void SaveFaculty(Session session, FacultyDTO faculty)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var item = context.Faculties.Include("Specializations").FirstOrDefault(fac => fac.ID == faculty.ID);
                    if (item == null)
                        context.AddToFaculties(new Faculty(faculty));
                    else
                    {
                        item.Assign(faculty);

                        #region Removing

                        var removed =
                               (from entity in item.Specializations
                                where
                                    faculty.Specializations.Find(it => it.ID == entity.ID) == null
                                select entity).ToList();

                        foreach (Specialization entity in removed)
                            context.Specializations.DeleteObject(entity);

                        #endregion
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void SaveCathedra(Session session, CathedraDTO cathedra)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var item = context.Cathedras.Include("Subjects").FirstOrDefault(cath => cath.ID == cathedra.ID);
                    if (item == null)
                        context.AddToCathedras(new Cathedra(cathedra));
                    else
                    {
                        item.Assign(cathedra);

                        #region Removing

                        var removed =
                               (from entity in item.Subjects
                                where
                                    cathedra.Subjects.Find(it => it.ID == entity.ID) == null
                                select entity).ToList();

                        foreach (Subject entity in removed)
                            context.Subjects.DeleteObject(entity);

                        #endregion

                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void SaveGroup(Session session, GroupDTO group)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var item = context.Groups.Include("Specialization").FirstOrDefault(gr => gr.ID == group.ID);
                    if (item == null)
                        context.AddToGroups(new Group(group));
                    else
                        item.Assign(group);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<SystemUserDTO> GetUsers(Session session, UserRoles roles)
        {
            try
            {
                CheckSession(session);

                List<SystemUserDTO> result = new List<SystemUserDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    foreach (SystemUser user in context.SystemUsers.Include("UserInformation"))
                        if (roles.HasFlag(user.Role))
                            result.Add(GetSystemUserDTO(user, context));
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void SaveUser(Session session, SystemUserDTO user)
        {
            try
            {
                CheckSession(session);

                using (UniversityEntities context = new UniversityEntities())
                {
                    var item = context.SystemUsers.Include("UserInformation").FirstOrDefault(gr => gr.ID == user.ID);

                    if (item == null)
                    {
                        SystemUser systemUser = GetSystemUser(user);

                        if (systemUser is Student)
                        {
                            Student student = (systemUser as Student);

                            var group = context.Groups.FirstOrDefault(gr => gr.ID == student.CurrentGroupID);
                            if (group == null)
                                throw new Exception("Group can't be null");

                            student.Groups.Add(group);
                        }

                        context.AddToSystemUsers(systemUser);
                    }
                    else
                    {
                        switch (item.Role)
                        {
                            case UserRoles.InstituteAdmin:
                                (item as IDTOable<InstituteAdminDTO>).Assign(user as InstituteAdminDTO);
                                break;
                            case UserRoles.FacultyAdmin:
                                (item as IDTOable<FacultyAdminDTO>).Assign(user as FacultyAdminDTO);
                                break;
                            case UserRoles.InstituteSecretary:
                                (item as IDTOable<InstituteSecretaryDTO>).Assign(user as InstituteSecretaryDTO);
                                break;
                            case UserRoles.FacultySecretary:
                                (item as IDTOable<FacultySecretaryDTO>).Assign(user as FacultySecretaryDTO);
                                break;
                            case UserRoles.Teacher:
                                (item as IDTOable<TeacherDTO>).Assign(user as TeacherDTO);
                                break;
                            case UserRoles.Student:
                                StudentDTO student = user as StudentDTO;
                                context.LoadProperty(item, "Groups");
                                (item as IDTOable<StudentDTO>).Assign(student);

                                var group = context.Groups.FirstOrDefault(gr => gr.ID == student.GroupID);
                                if (group == null)
                                    throw new NullReferenceException("group must exists in DB");

                                var studentGroup = (item as Student).Groups.FirstOrDefault(gr => gr.ID == student.GroupID);

                                if (studentGroup == null)
                                    (item as Student).Groups.Add(group);
                                break;
                            default:
                                item.Assign(user);
                                break;
                        }
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<SpecializationDTO> GetSpecializations(Session session, int facultyID)
        {
            try
            {
                CheckSession(session);

                List<SpecializationDTO> result = new List<SpecializationDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    var items = from s in context.Specializations
                                where s.FacultyID == facultyID
                                select s;
                    foreach (var specialition in items)
                        result.Add(specialition.ToDTO());
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }



        public SpecializationDTO GetSpecializationByID(Session session, int? specializationID)
        {
            try
            {
                CheckSession(session);
                SpecializationDTO result = new SpecializationDTO();

                using (UniversityEntities context = new UniversityEntities())
                {
                    Specialization specialization = (from s in context.Specializations
                                                     where s.ID == specializationID
                                                     select s).FirstOrDefault();

                    result = GetDTO<SpecializationDTO>(specialization);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void DeleteFaculty(Session session, int facultyID)
        {
            try
            {
                CheckSession(session);

                using (UniversityEntities context = new UniversityEntities())
                {
                    DeleteFaculty(context, facultyID);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void DeleteInstitute(Session session, int instituteID)
        {
            try
            {
                CheckSession(session);

                using (UniversityEntities context = new UniversityEntities())
                {
                    var item = (from f in context.Institutes.Include("Faculties").Include("InstituteAdmins").Include("InstituteSecretaries")
                                where f.ID == instituteID
                                select f).FirstOrDefault();

                    if (item != null)
                    {
                        foreach (var faculty in item.Faculties.ToList())
                            DeleteFaculty(context, faculty.ID);

                        foreach (var user in item.InstituteAdmins.ToList())
                            context.SystemUsers.DeleteObject(user);
                        foreach (var user in item.InstituteSecretaries.ToList())
                            context.SystemUsers.DeleteObject(user);
                        context.Institutes.DeleteObject(item);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        private void DeleteFaculty(UniversityEntities context, int facultyID)
        {
            var item = (from f in context.Faculties.Include("Cathedras").Include("FacultyAdmins").Include("FacultySecretaries")
                        where f.ID == facultyID
                        select f).FirstOrDefault();
            if (item != null)
            {
                foreach (var cathedra in item.Cathedras.ToList())
                    DeleteCathedra(context, cathedra.ID);

                foreach (var user in item.FacultyAdmins.ToList())
                    context.SystemUsers.DeleteObject(user);
                foreach (var user in item.FacultySecretaries.ToList())
                    context.SystemUsers.DeleteObject(user);

                context.Faculties.DeleteObject(item);
            }
        }

        public void DeleteCathedra(Session session, int cathedraID)
        {
            try
            {
                CheckSession(session);

                using (UniversityEntities context = new UniversityEntities())
                {
                    DeleteCathedra(context, cathedraID);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        private void DeleteCathedra(UniversityEntities context, int cathedraID)
        {
            var item = (from f in context.Cathedras.Include("Teachers").Include("Groups").Include("Subjects")
                        where f.ID == cathedraID
                        select f).FirstOrDefault();
            if (item != null)
            {
                foreach (var group in item.Groups.ToList())
                    DeleteGroup(context, group.ID);

                foreach (var subject in item.Subjects.ToList())
                {
                    context.LoadProperty(subject, "Practices");
                    context.LoadProperty(subject, "Lectures");
                    foreach (var pr in subject.Practices.ToList())
                    {
                        context.LoadProperty(pr, "PracticeTeacher");
                        foreach (var pt in pr.PracticeTeacher.ToList())
                        {
                            context.LoadProperty(pt, "Students");
                            context.PracticeTeachers.DeleteObject(pt);
                        }
                        context.Practices.DeleteObject(pr);
                    }
                    foreach (var lecture in subject.Lectures.ToList())
                    {
                        context.LoadProperty(lecture, "Groups");
                        context.Lectures.DeleteObject(lecture);
                    }
                    context.DeleteObject(subject);
                }

                foreach (var user in item.Teachers.ToList())
                {
                    context.LoadProperty(user, "PracticeTeacher");
                    context.LoadProperty(user, "Lectures");
                    foreach (var pt in user.PracticeTeacher.ToList())
                    {
                        context.LoadProperty(pt, "Students");
                        context.PracticeTeachers.DeleteObject(pt);
                    }
                    foreach (var lecture in user.Lectures.ToList())
                    {
                        context.LoadProperty(lecture, "Groups");
                        context.Lectures.DeleteObject(lecture);
                    }
                    context.SystemUsers.DeleteObject(user);
                }
                context.Cathedras.DeleteObject(item);
            }
        }

        public void DeleteGroup(Session session, int groupID)
        {
            try
            {
                CheckSession(session);

                using (UniversityEntities context = new UniversityEntities())
                {
                    DeleteGroup(context, groupID);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        private void DeleteGroup(UniversityEntities context, int groupID)
        {
            var item = (from f in context.Groups.Include("Students")
                        where f.ID == groupID
                        select f).FirstOrDefault();
            if (item != null)
            {
                foreach (var user in item.Students.ToList())
                    context.SystemUsers.DeleteObject(user);

                context.Groups.DeleteObject(item);
            }
        }

        public void DeleteUser(Session session, int userID)
        {
            try
            {
                CheckSession(session);

                using (UniversityEntities context = new UniversityEntities())
                {
                    var item = (from f in context.SystemUsers
                                where f.ID == userID
                                select f).FirstOrDefault();
                    if (item != null)
                        context.SystemUsers.DeleteObject(item);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<LectureDTO> GetLectures(Session session, int teacherID)
        {
            try
            {
                CheckSession(session);

                List<LectureDTO> result = new List<LectureDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = from l in context.Lectures.Include("Groups")
                                where l.TeacherID == teacherID
                                select l;

                    foreach (var lecture in query)
                    {
                        context.LoadProperty(lecture, "Subject");
                        result.Add(lecture.ToDTO());
                    }

                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<GroupDTO> GetLectureGroups(Session session, int lectureID)
        {
            try
            {
                CheckSession(session);

                List<GroupDTO> result = new List<GroupDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    var lecture = context.Lectures.Where(l => l.ID == lectureID).FirstOrDefault();

                    if (lecture != null)
                    {
                        foreach (var item in lecture.Groups)
                            result.Add(item.ToDTO());
                    }

                }

                return result;

            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<TeacherDTO> GetTeachers(Session session, int cathedraID)
        {
            try
            {
                CheckSession(session);

                List<TeacherDTO> result = new List<TeacherDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = context.Cathedras.Include("Teachers").Where(c => c.ID == cathedraID).Select(c => c.Teachers).FirstOrDefault();

                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            context.LoadProperty(item, "UserInformation");
                            context.LoadProperty(item, "Lectures");
                            foreach (var lecture in item.Lectures)
                                context.LoadProperty(lecture, "Subject");

                            result.Add(item.ToDTO());
                        }
                    }

                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<SubjectDTO> GetSubjects(Session session, int cathedraID)
        {
            try
            {
                CheckSession(session);

                List<SubjectDTO> result = new List<SubjectDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = context.Subjects.Where(s => s.CathedraID == cathedraID);

                    if (query != null)
                    {
                        foreach (var item in query)
                            result.Add(item.ToDTO());
                    }

                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void SaveLecture(Session session, LectureDTO lecture)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var item = context.Lectures.Include("Groups").FirstOrDefault(l => l.ID == lecture.ID);
                    if (item == null)
                    {
                        context.AddToLectures(new Lecture(lecture));
                        item = context.Lectures.Include("Groups").FirstOrDefault(l => l.ID == lecture.ID);
                    }
                    else
                        item.Assign(lecture);

                    if (lecture.Groups != null)
                    {
                        foreach (GroupDTO group in lecture.Groups)
                            if (item.Groups.FirstOrDefault(g => g.ID == group.ID) == null)
                            {
                                var g = context.Groups.FirstOrDefault(gr => gr.ID == group.ID);
                                if (g != null)
                                    item.Groups.Add(g);
                            }

                        List<Group> removed = new List<Group>();
                        foreach (Group group in item.Groups)
                            if (lecture.Groups.Find(g => g.ID == group.ID) == null)
                                removed.Add(group);

                        foreach (Group group in removed)
                            item.Groups.Remove(group);
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void SaveTeacherSubjects(Session session, int teacherID, List<SubjectDTO> subjects)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var lectures = context.Lectures.Where(l => l.TeacherID == teacherID).ToList();
                    var query = lectures.Where(l => subjects.Find(s => s.ID == l.SubjectID) == null);

                    foreach (var lecture in query)
                        context.DeleteObject(lecture);

                    var toAdd = subjects.Where(s => lectures.Find(l => l.SubjectID == s.ID) == null);

                    foreach (var subject in toAdd)
                    {
                        var s = context.Subjects.Where(subj => subj.ID == subject.ID).FirstOrDefault();
                        if (s == null)
                            throw new NullReferenceException();

                        context.Lectures.AddObject(new Lecture { Subject = s, TeacherID = teacherID });
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<PracticeTeacherDTO> GetPracticesTeacher(Session session, int teacherID)
        {
            try
            {
                CheckSession(session);

                List<PracticeTeacherDTO> result = new List<PracticeTeacherDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = from pt in context.PracticeTeachers.Include("Practice").Include("Students")
                                where pt.TeacherID == teacherID
                                select pt;
                    foreach (var practiceTeacher in query)
                    {
                        context.LoadProperty(practiceTeacher.Practice, "Subject");

                        foreach (var student in practiceTeacher.Students)
                        {
                            context.LoadProperty(student, "Groups");
                            var currentGroup = student.Groups.FirstOrDefault();//(g => g.StudyRangeID == Configuration.StudyRangeID);

                            if (currentGroup == null)
                                throw new Exception("Student hasnt a group!");

                            student.CurrentGroupID = currentGroup.ID;

                            context.LoadProperty(student, "UserInformation");
                        }
                        result.Add(practiceTeacher.ToDTO());
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<GroupDTO> GetGroupsPractice(Session session, int practiceTeacherID)
        {
            try
            {
                CheckSession(session);
                List<GroupDTO> result = new List<GroupDTO>();
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = from pt in context.PracticeTeachers
                                where pt.ID == practiceTeacherID
                                select pt.Students;

                    foreach (IEnumerable<Student> students in query.ToList())
                    {
                        foreach (Student student in students)
                        {
                            var currentGroup = student.Groups.FirstOrDefault();// (g => g.StudyRangeID == Configuration.StudyRangeID);

                            if (currentGroup == null)
                                throw new Exception("Student hasnt a group!");

                            student.CurrentGroupID = currentGroup.ID;

                            if (result.Find(g => g.ID == currentGroup.ID) == null)
                                result.Add(currentGroup.ToDTO());
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<SubjectDTO> GetTeacherPracticeSubjects(Session session, int teacherID)
        {
            try
            {
                CheckSession(session);
                List<SubjectDTO> result = new List<SubjectDTO>();
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = (from pt in context.PracticeTeachers
                                 where pt.TeacherID == teacherID
                                 select pt.Practice.Subject).ToList();
                    foreach (var s in query)
                        result.Add(s.ToDTO());
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<StudentDTO> GetAllStudents(Session session)
        {
            try
            {
                CheckSession(session);
                List<StudentDTO> result = new List<StudentDTO>();
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = from g in context.Groups
                                select g.Students;
                    foreach (IEnumerable<Student> students in query.ToList())
                    {
                        foreach (Student student in students)
                        {
                            var currentGroup = student.Groups.FirstOrDefault();// (g => g.StudyRangeID == Configuration.StudyRangeID);

                            if (currentGroup == null)
                                throw new Exception("Student hasnt a group!");

                            student.CurrentGroupID = currentGroup.ID;

                            context.LoadProperty(student, "UserInformation");
                            result.Add(student.ToDTO());
                        }
                    }

                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<StudentDTO> GetGroupStudents(Session session, int groupID)
        {
            try
            {
                CheckSession(session);
                List<StudentDTO> result = new List<StudentDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = (from g in context.Groups.Include("Students")
                                 where g.ID == groupID
                                 select g.Students).FirstOrDefault();
                    foreach (var student in query.ToList())
                    {
                        context.LoadProperty(student, "UserInformation");
                        student.CurrentGroupID = groupID;
                        result.Add(student.ToDTO());
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<StudentDTO> GetCathedraStudents(Session session, int cathedraID)
        {
            try
            {
                CheckSession(session);
                List<StudentDTO> result = new List<StudentDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = from g in context.Groups.Include("Students")
                                where g.CathedraID == cathedraID
                                select g.Students;

                    foreach (var students in query.ToList())
                    {
                        foreach (var student in students)
                        {
                            context.LoadProperty(student, "UserInformation");
                            result.Add(student.ToDTO());
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<StudentDTO> GetFacultyStudents(Session session, int facultyID)
        {
            try
            {
                CheckSession(session);
                List<StudentDTO> result = new List<StudentDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = from g in context.Groups.Include("Students")
                                where g.Cathedra.FacultyID == facultyID
                                select g.Students;
                    foreach (var students in query.ToList())
                    {
                        foreach (var student in students)
                        {
                            context.LoadProperty(student, "UserInformation");
                            result.Add(student.ToDTO());
                        }

                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<StudentDTO> GetInstituteStudents(Session session, int instituteID)
        {
            try
            {
                CheckSession(session);
                List<StudentDTO> result = new List<StudentDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = from g in context.Groups.Include("Students")
                                where g.Cathedra.Faculty.InstituteID == instituteID
                                select g.Students;
                    foreach (var students in query.ToList())
                    {
                        foreach (var student in students)
                        {
                            context.LoadProperty(student, "UserInformation");
                            result.Add(student.ToDTO());
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void SavePracticeTeacher(Session session, PracticeTeacherDTO practiceTeacher)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var item = context.PracticeTeachers.Include("Students").Where(p => p.ID == practiceTeacher.ID).FirstOrDefault();
                    if (item == null)
                        throw new NullReferenceException();
                    else
                    {
                        var toAdd = from s in practiceTeacher.Students
                                    where item.Students.ToList().Find(st => st.ID == s.ID) == null
                                    select s;
                        foreach (var student in toAdd)
                        {
                            var group = context.Groups.Include("Students").Where(g => g.ID == student.GroupID).FirstOrDefault();
                            item.Students.Add(group.Students.Where(s => s.ID == student.ID).FirstOrDefault());
                        }

                        var toRemove = from s in item.Students
                                       where practiceTeacher.Students.ToList().Find(st => st.ID == s.ID) == null
                                       select s;
                        foreach (var student in toRemove.ToList())
                            item.Students.Remove(student);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<GroupDTO> GetFacultyGroups(Session session, int facultyID)
        {
            try
            {
                CheckSession(session);
                List<GroupDTO> result = new List<GroupDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = from g in context.Groups
                                where g.Cathedra.FacultyID == facultyID
                                select g;
                    foreach (var group in query.ToList())
                        result.Add(group.ToDTO());
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<GroupDTO> GetInstituteGroups(Session session, int instituteID)
        {
            try
            {
                CheckSession(session);
                List<GroupDTO> result = new List<GroupDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = from g in context.Groups
                                where g.Cathedra.Faculty.InstituteID == instituteID
                                select g;
                    foreach (var group in query.ToList())
                        result.Add(group.ToDTO());
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void SavePracticeTeacherSubjects(Session session, int teacherID, List<SubjectDTO> subjects)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var teacherPractices = context.PracticeTeachers.Include("Practice").Include("Students").Where(p => p.TeacherID == teacherID);

                    var toAdd = from s in subjects
                                where teacherPractices.ToList().Find(t => t.Practice.SubjectID == s.ID) == null
                                select s;

                    foreach (var subject in toAdd.ToList())
                    {
                        var item = context.Practices.Where(p => p.SubjectID == subject.ID).FirstOrDefault();
                        if (item != null)
                            context.PracticeTeachers.AddObject(new PracticeTeacher { Practice = item, TeacherID = teacherID });
                        else
                        {
                            var subj = context.Subjects.Where(s => s.ID == subject.ID).FirstOrDefault();
                            Practice pr = new Practice { Subject = subj };
                            context.Practices.AddObject(pr);
                            context.PracticeTeachers.AddObject(new PracticeTeacher { Practice = pr, TeacherID = teacherID });
                        }
                    }
                    var toRemove = teacherPractices.ToList().Where(t => subjects.Find(s => s.ID == t.Practice.SubjectID) == null);

                    foreach (var pract in toRemove.ToList())
                    {
                        var item = teacherPractices.ToList().Where(t => t.ID == pract.ID).FirstOrDefault();

                        //List<Student> toRemoveStudent = new List<Student>();

                        //foreach (var student in item.Students)
                        //    toRemoveStudent.Add(student);

                        //foreach (var student in toRemoveStudent)
                        //    item.Students.Remove(student);

                        context.PracticeTeachers.DeleteObject(item);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public TeacherDTO GetTeacher(Session session, int teacherID)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = context.Cathedras.Include("Teachers");
                    foreach (var cathedra in query)
                    {
                        var item = cathedra.Teachers.Where(t => t.ID == teacherID).FirstOrDefault();
                        if (item != null)
                        {
                            context.LoadProperty(item, "UserInformation");
                            context.LoadProperty(item, "Cathedra");
                            context.LoadProperty(item.Cathedra, "Faculty");
                            context.LoadProperty(item.Cathedra.Faculty, "Institute");
                            return item.ToDTO();
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public InstituteDTO GetInstituteByID(Session session, int? instituteID)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    if (instituteID != null)
                    {
                        var query = context.Institutes.Where(i => i.ID == instituteID).FirstOrDefault();
                        return query.ToDTO();
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public FacultyDTO GetFacultyByID(Session session, int facultyID)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = context.Faculties.Where(f => f.ID == facultyID).FirstOrDefault();
                    return query.ToDTO();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public CathedraDTO GetCathedraByID(Session session, int cathedraID)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = context.Cathedras.Where(c => c.ID == cathedraID).FirstOrDefault();
                    return query.ToDTO();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<LectureControlDTO> GetLectureControls(Session session, int lectureID)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = context.Lectures.Include("LectureControls").FirstOrDefault(l => l.ID == lectureID);
                    return (query as Lecture).LectureControls.ToDTOList<LectureControlDTO, LectureControl>();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void SaveLectureControls(Session session, int lectureID, List<LectureControlDTO> controls)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    Lecture lecture = context.Lectures.Include("LectureControls").FirstOrDefault(l => l.ID == lectureID);


                    foreach (LectureControlDTO control in controls)
                        if (lecture.LectureControls.FirstOrDefault(lc => lc.ID == control.ID) == null) context.AddToControls(new LectureControl(control));
                        else
                        {
                            var query = context.Controls.FirstOrDefault(c => c.ID == control.ID);
                            if (query != null) (query as Control).Assign(control);
                        }

                    List<LectureControl> toRemove = new List<LectureControl>();
                    foreach (var lectureControl in lecture.LectureControls)
                        if (controls.Find(c => c.ID == lectureControl.ID) == null) toRemove.Add(lectureControl);

                    foreach (var lc in toRemove)
                    {
                        lecture.LectureControls.Remove(lc);
                        context.Controls.DeleteObject(lc);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<PracticeControlDTO> GetPracticeControls(Session session, int practiceID)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = context.PracticeTeachers.Include("PracticeControls").Where(p => p.ID == practiceID).FirstOrDefault();
                    return (query as PracticeTeacher).PracticeControls.ToDTOList<PracticeControlDTO, PracticeControl>();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void SavePracticeControls(Session session, int practiceID, List<PracticeControlDTO> controls)
        {
            try
            {
                CheckSession(session);
                /*                
                using (UniversityEntities context = new UniversityEntities())
                {
                    Lecture lecture = context.Lectures.Include("LectureControls").FirstOrDefault(l => l.ID == lectureID);


                    foreach (LectureControlDTO control in controls)
                        if (lecture.LectureControls.FirstOrDefault(lc => lc.ID == control.ID) == null) context.AddToControls(new LectureControl(control));
                        else
                        {
                            var query = context.Controls.FirstOrDefault(c => c.ID == control.ID);
                            if (query != null) (query as Control).Assign(control);
                        }

                    List<LectureControl> toRemove = new List<LectureControl>();
                    foreach (var lectureControl in lecture.LectureControls)
                        if (controls.Find(c => c.ID == lectureControl.ID) == null) toRemove.Add(lectureControl);

                    foreach (var lc in toRemove)
                    {
                        lecture.LectureControls.Remove(lc);
                        context.Controls.DeleteObject(lc);
                    }
                    context.SaveChanges();
                }
*/
                using (UniversityEntities context = new UniversityEntities())
                {
                    PracticeTeacher practice = context.PracticeTeachers.Include("PracticeControls").FirstOrDefault(p => p.PracticeID == practiceID);

                    foreach (PracticeControlDTO control in controls)
                        if (practice.PracticeControls.FirstOrDefault(pc => pc.ID == control.ID) == null) context.AddToControls(new PracticeControl(control));
                        else
                        {
                            var query = context.Controls.FirstOrDefault(c => c.ID == control.ID);
                            if (query != null) (query as Control).Assign(control);
                        }

                    List<PracticeControl> toRemove = new List<PracticeControl>();

                    foreach (var practiceControl in practice.PracticeControls)
                        if (controls.Find(c => c.ID == practiceControl.ID) == null) toRemove.Add(practiceControl);

                    foreach (var pc in toRemove)
                    {
                        practice.PracticeControls.Remove(pc);
                        context.Controls.DeleteObject(pc);
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<ControlMessageDTO> GetControlMessages(Session session, int controlID)
        {
            try
            {
                CheckSession(session);

                List<ControlMessageDTO> result = new List<ControlMessageDTO>();
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = context.ControlMessages.Include("SystemUser.UserInformation").Include("Control").Where(cm => cm.ControlID == controlID);
                    foreach (var item in query)
                        result.Add(item.ToDTO());
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }


        public void SaveControlMessage(Session session, ControlMessageDTO message)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var item = context.ControlMessages.FirstOrDefault(cm => cm.ID == message.ID);
                    if (item == null)
                        context.ControlMessages.AddObject(new ControlMessage(message));
                    else
                        item.Assign(message);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void SaveLectureControl(Session session, LectureControlDTO control)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var item = context.Controls.FirstOrDefault(c => c.ID == control.ID);
                    if (item == null)
                        context.AddToControls(new LectureControl(control));
                    else
                        item.Assign(control);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<LectureDTO> GetStudentLectures(Session session, int studentID)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    List<LectureDTO> result = new List<LectureDTO>();

                    Student student = context.SystemUsers.FirstOrDefault(u => u.ID == studentID) as Student;
                    context.LoadProperty(student, "Groups");
                    var currentGroup = student.Groups.FirstOrDefault();// (g => g.StudyRangeID == Configuration.StudyRangeID);

                    if (currentGroup == null)
                        throw new Exception("Student hasnt a group!");

                    context.LoadProperty(currentGroup, "Lectures");
                    foreach (var lecture in currentGroup.Lectures)
                    {
                        context.LoadProperty(lecture, "Subject");
                        result.Add(lecture.ToDTO());
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<AttachmentDTO> GetAttachments(Session session, int teacherID)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = context.Attachments.Where(a => a.TeacherID == teacherID);
                    List<AttachmentDTO> result = new List<AttachmentDTO>();
                    foreach (var att in query.ToList())
                        result.Add(att.ToDTO());
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void SaveAttachments(Session session, int userID, List<AttachmentDTO> attachments)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {

                    var userAttachments = (from a in context.Attachments
                                           where a.TeacherID == userID
                                           select a).ToList();
                    var toAdd = (from a in attachments
                                 where userAttachments.Find(ua => ua.ID == a.ID) == null
                                 select a).ToList();
                    var toRemove = (from ua in userAttachments
                                    where attachments.Find(a => a.ID == ua.ID) == null
                                    select ua).ToList();
                    foreach (var r in toRemove)
                        context.DeleteObject(r);
                    foreach (var a in toAdd)
                        context.Attachments.AddObject(new Attachment(a));
                    foreach (var at in userAttachments)
                        if (attachments.Find(a => a.ID == at.ID) != null)
                            at.Assign(attachments.Find(a => a.ID == at.ID));
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void DeleteAttachment(Session session, int attachmentID)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = (from a in context.Attachments
                                 where a.ID == attachmentID
                                 select a).FirstOrDefault();
                    context.DeleteObject(query);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public decimal GetLectureMark(Session session, int studentID, int controlID)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = context.Controls.FirstOrDefault(p => p.ID == controlID);
                    if (query != null)
                    {
                        LectureControl control = query as LectureControl;
                        context.LoadProperty(control, "LectureControlMarks");

                        LectureControlMark mark = control.LectureControlMarks.FirstOrDefault(m => m.StudentID == studentID);

                        if (mark != null) return mark.MarkValue;
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public decimal GetPracticeMark(Session session, int studentID, int controlID)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = context.Controls.FirstOrDefault(p => p.ID == controlID);
                    if (query != null)
                    {
                        PracticeControl control = query as PracticeControl;
                        context.LoadProperty(control, "PracticeControlMarks");

                        PracticeControlMark mark = control.PracticeControlMarks.FirstOrDefault(m => m.StudentID == studentID);

                        if (mark != null) return mark.MarkValue;
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }

        }

        public void SavePracticeControl(Session session, PracticeControlDTO control)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var item = context.Controls.FirstOrDefault(c => c.ID == control.ID);
                    if (item == null)
                    {
                        context.AddToControls(new PracticeControl(control));
                    }
                    else
                        item.Assign(control);

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<PracticeTeacherDTO> GetStudentPractices(Session session, int studentID)
        {
            try
            {
                CheckSession(session);
                List<PracticeTeacherDTO> result = new List<PracticeTeacherDTO>();
                using (UniversityEntities context = new UniversityEntities())
                {
                    foreach (var practice in context.PracticeTeachers.Include("Practice").Include("Students").Include("Teacher"))
                        if (practice.Students.FirstOrDefault(s => s.ID == studentID) != null)
                        {
                            context.LoadProperty(practice.Practice, "Subject");

                            foreach (var student in practice.Students)
                            {
                                context.LoadProperty(student, "UserInformation");

                                context.LoadProperty(student, "Groups");
                                var currentGroup = student.Groups.FirstOrDefault();// (g => g.StudyRangeID == Configuration.StudyRangeID);

                                if (currentGroup == null)
                                    throw new Exception("Student hasnt a group!");

                                student.CurrentGroupID = currentGroup.ID;
                            }

                            result.Add(practice.ToDTO());
                        }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }


        public List<MarkDTO> GetMarks(Session session, ControlDTO control)
        {
            try
            {
                CheckSession(session);
                List<MarkDTO> result = new List<MarkDTO>();
                using (UniversityEntities context = new UniversityEntities())
                {
                    var q = context.Controls.FirstOrDefault(p => p.ID == control.ID);
                    if (q != null)
                    {
                        if (control is PracticeControlDTO)
                        {
                            PracticeControl pc = q as PracticeControl;
                            context.LoadProperty(pc, "Practice_Teacher");
                            context.LoadProperty(pc, "PracticeControlMarks");
                            foreach (var m in pc.PracticeControlMarks)
                            {
                                context.LoadProperty(m, "Student");
                                context.LoadProperty(m.Student, "UserInformation");
                                result.Add(m.ToDTO());
                            }
                            context.LoadProperty(pc.Practice_Teacher, "Students");
                            foreach (Student s in pc.Practice_Teacher.Students)
                            {
                                var query = pc.PracticeControlMarks.Where(m => m.StudentID == s.ID).FirstOrDefault();
                                if (query == null)
                                {
                                    context.LoadProperty(s, "UserInformation");
                                    result.Add(new PracticeControlMarkDTO()
                                    {
                                        PracticeControlID = pc.ID,
                                        MarkValue = 0,
                                        StudentID = s.ID,
                                        Student = s.ToDTO()
                                    });
                                }
                            }
                        }
                        else
                        {
                            LectureControl lc = q as LectureControl;

                            context.LoadProperty(lc, "Lecture");
                            context.LoadProperty(lc, "LectureControlMarks");
                            foreach (var m in lc.LectureControlMarks)
                            {
                                context.LoadProperty(m, "Student");
                                context.LoadProperty(m.Student, "UserInformation");
                                result.Add(m.ToDTO());
                            }
                            context.LoadProperty(lc.Lecture, "Groups");
                            foreach (Group g in lc.Lecture.Groups)
                            {
                                context.LoadProperty(g, "Students");
                                foreach (Student s in g.Students)
                                {
                                    var query = lc.LectureControlMarks.Where(m => m.StudentID == s.ID).FirstOrDefault();
                                    if (query == null)
                                    {
                                        context.LoadProperty(s, "UserInformation");
                                        result.Add(new LectureControlMarkDTO()
                                        {
                                            LectureControlID = lc.ID,
                                            MarkValue = 0,
                                            StudentID = s.ID,
                                            Student = s.ToDTO()
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void SaveMarks(Session session, ControlDTO control, List<MarkDTO> marks)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var q = context.Controls.FirstOrDefault(p => p.ID == control.ID);
                    if (q != null)
                    {
                        var toEdit = new List<MarkDTO>();
                        var toAdd = new List<MarkDTO>();
                        if (control is PracticeControlDTO)
                        {
                            PracticeControl pc = q as PracticeControl;
                            context.LoadProperty(pc, "Practice_Teacher");
                            context.LoadProperty(pc.Practice_Teacher, "Students");
                            context.LoadProperty(pc, "PracticeControlMarks");

                            toAdd = marks.Where(m => pc.PracticeControlMarks.ToList().Find(p => p.StudentID == m.StudentID) == null).ToList();
                            toEdit = marks.Where(m => pc.PracticeControlMarks.ToList().Find(p => p.StudentID == m.StudentID && p.MarkValue != m.MarkValue) != null).ToList();
                            foreach (var a in toAdd)
                            {
                                PracticeControlMark pcm = new PracticeControlMark(a as PracticeControlMarkDTO);
                                context.Marks.AddObject(pcm);
                            }
                            foreach (var e in toEdit)
                            {
                                var elem = context.Marks.Where(m => m.ID == e.ID).FirstOrDefault();
                                if (elem == null)
                                {
                                    PracticeControlMark pcm = new PracticeControlMark(e as PracticeControlMarkDTO);
                                    context.Marks.AddObject(pcm);
                                }
                                else
                                    elem.Assign(e);
                            }
                        }
                        else
                        {
                            LectureControl lc = q as LectureControl;
                            context.LoadProperty(lc, "Lecture");
                            context.LoadProperty(lc.Lecture, "Groups");
                            context.LoadProperty(lc, "LectureControlMarks");

                            toAdd = marks.Where(m => lc.LectureControlMarks.ToList().Find(l => l.StudentID == m.StudentID) == null).ToList();
                            toEdit = marks.Where(m => lc.LectureControlMarks.ToList().Find(l => l.StudentID == m.StudentID) != null).ToList();

                            foreach (var a in toAdd)
                            {
                                LectureControlMark lcm = new LectureControlMark(a as LectureControlMarkDTO);
                                context.Marks.AddObject(lcm);
                            }
                            foreach (var e in toEdit)
                            {
                                var elem = context.Marks.Where(m => m.ID == e.ID).FirstOrDefault();
                                if (elem == null)
                                {
                                    LectureControlMark lcm = new LectureControlMark(e as LectureControlMarkDTO);
                                    context.Marks.AddObject(lcm);
                                }
                                else
                                    elem.Assign(e);
                            }
                        }

                        List<MarkDTO> toNotify = toAdd.Count == 0 ? toEdit : toAdd;
                        foreach (var mark in toNotify)
                        {
                            var notification = new NotificationDTO();
                            var subjectName = string.Empty;
                            if (control is LectureControlDTO)
                            {
                                var lectureID = (control as LectureControlDTO).LectureID;
                                var lecture = context.Lectures.Where(l => l.ID == lectureID).First();
                                context.LoadProperty(lecture, "Subject");
                                subjectName = lecture.Subject.Name;
                            }

                            if (control is PracticeControlDTO)
                            {
                                var practiceID = (control as PracticeControlDTO).PracticeID;
                                var practice = context.Practices.Where(p => p.ID == practiceID).First();
                                context.LoadProperty(practice, "Subject");
                                subjectName = practice.Subject.Name;
                            }

                            notification.Message = string.Format(
                                StudyingControllerService.Resource.ResourceManager.GetString("NewMarkNotificationMessage"),
                                mark.MarkValue.ToString("0.00"),
                                control.Name,
                                subjectName);

                            notification.UserID = mark.StudentID;
                            notification.Date = DateTime.Now;

                            context.Notifications.AddObject(new Notification(notification));
                        }
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<UserRateItemDTO> GetStudentRateList(Session session, BaseEntityDTO universityStructureItem)
        {
            try
            {
                CheckSession(session);
                List<UserRateItemDTO> result = new List<UserRateItemDTO>();
                using (UniversityEntities context = new UniversityEntities())
                {
                    var students = new List<Student>();
                    var groupIDs = new List<int>();

                    if (universityStructureItem is InstituteDTO)
                    {
                        groupIDs.AddRange(context.Institutes.Where(i => i.ID == universityStructureItem.ID).AsEnumerable().Select(i => 
                        {
                            context.LoadProperty(i, "Faculties");
                            return i;
                        }).First().Faculties.SelectMany(f =>
                        {
                            context.LoadProperty(f, "Cathedras");
                            return f.Cathedras;
                        }).SelectMany(c =>
                        {
                            context.LoadProperty(c, "Groups");
                            return c.Groups.Select(g => g.ID);
                        }));
                    }
                    else if (universityStructureItem is FacultyDTO)
                    {
                        groupIDs.AddRange(context.Faculties.Where(f => f.ID == universityStructureItem.ID).AsEnumerable().Select(f => 
                        { 
                            context.LoadProperty(f, "Cathedras"); 
                            return f; 
                        }).First().Cathedras.SelectMany(c => 
                        { 
                            context.LoadProperty(c, "Groups"); 
                            return c.Groups.Select(g => g.ID); 
                        }));
                    }
                    else if (universityStructureItem is CathedraDTO)
                    {
                        groupIDs.AddRange(context.Cathedras.Where(c => c.ID == universityStructureItem.ID).AsEnumerable().SelectMany(c =>
                        {
                            context.LoadProperty(c, "Groups");
                            return c.Groups.Select(g => g.ID);
                        }));
                    }
                    else if (universityStructureItem is GroupDTO)
                    {
                        groupIDs.Add(universityStructureItem.ID);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                    foreach (var groupID in groupIDs)
                    {
                        var group = context.Groups.Where(g => g.ID == groupID).First();
                        if (group != null)
                        {
                            context.LoadProperty(group, "Students");
                            context.LoadProperty(group, "Lectures");
                            
                            students.AddRange(group.Students);
                        }
                    }

                    result = students.Select<Student, UserRateItemDTO>(student => 
                    {
                        context.LoadProperty(student, "Groups");
                        var lectureControls = student.Groups.SelectMany(g =>
                            {
                                context.LoadProperty(g, "Lectures");
                                return g.Lectures.SelectMany(l => 
                                {
                                    context.LoadProperty(l, "LectureControls");
                                    return l.LectureControls;
                                });
                            });

                        context.LoadProperty(student, "Practice_Teacher");

                        var practiceControls = student.Practice_Teacher.SelectMany(pt =>
                            {
                                context.LoadProperty(pt, "Practice");
                                return pt.Practice.PracticeControls;
                            });

                        var lectureMarksSum = lectureControls.Sum(lc =>
                        {
                            context.LoadProperty(lc, "LectureControlMarks");
                            var mark = lc.LectureControlMarks.FirstOrDefault(lcm => lcm.StudentID == student.ID);
                            return mark != null ? mark.MarkValue : 0;
                        });

                        var lectureMaxSum = lectureControls.Sum(lc => lc.MaxMark);

                        var practiceMarksSum = practiceControls.Sum(pc =>
                        {
                            context.LoadProperty(pc, "PracticeControlMarks");
                            var mark = pc.PracticeControlMarks.FirstOrDefault(pcm => pcm.StudentID == student.ID);
                            return mark != null ? mark.MarkValue : 0;
                        });

                        var practiceMaxSum = practiceControls.Sum(pc => pc.MaxMark);

                        context.LoadProperty(student, "UserInformation");
                        return new UserRateItemDTO()
                        {
                            Rate = (lectureControls.Any() || practiceControls.Any()) ? Convert.ToDouble((lectureMarksSum + practiceMarksSum) / (lectureMaxSum + practiceMaxSum)) : 0,
                            Student = student.ToDTO()
                        };
                    }).OrderByDescending(elem => elem.Rate).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<NotificationDTO> GetNotifications(Session session, int userID)
        {
            try
            {
                this.CheckSession(session);
                var resultList = new List<NotificationDTO>();
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = (from notification 
                                      in context.Notifications 
                                      where notification.UserID == userID
                                      orderby notification.Date descending
                                  select notification).Take(10);
                    
                    foreach (var n in query.ToList())
                    {
                        resultList.Add(n.ToDTO());
                    }
                }
                return resultList;
            }
                catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public GroupDTO GetGroup(Session session, int id)
        {
            try
            {
                this.CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = (from item
                                      in context.Groups
                                 where item.ID == id
                                 select item).FirstOrDefault();
                    return query == null ? null : query.ToDTO();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public SpecializationDTO GetSpecialization(Session session, int id)
        {
            try
            {
                this.CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = (from item
                                      in context.Specializations
                                 where item.ID == id
                                 select item).FirstOrDefault();
                    return query == null ? null : query.ToDTO();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public CathedraDTO GetCathedra(Session session, int id)
        {
            try
            {
                this.CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = (from item
                                      in context.Cathedras
                                 where item.ID == id
                                 select item).FirstOrDefault();
                    return query == null ? null : query.ToDTO();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public FacultyDTO GetFaculty(Session session, int id)
        {
            try
            {
                this.CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = (from item
                                      in context.Faculties
                                 where item.ID == id
                                 select item).FirstOrDefault();
                    return query == null ? null : query.ToDTO();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public InstituteDTO GetInstitute(Session session, int id)
        {
            try
            {
                this.CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = (from item
                                      in context.Institutes
                                 where item.ID == id
                                 select item).FirstOrDefault();
                    return query == null? null : query.ToDTO();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }


    }
}
