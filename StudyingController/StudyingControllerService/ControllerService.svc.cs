using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using StudyingControllerEntityModel;
using EntitiesDTO;

namespace StudyingControllerService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class ControllerService : IControllerService
    {
        #region Fields & Properties

        private static readonly TimeSpan SESSION_TIMEOUT = TimeSpan.FromSeconds(20 * 60); // 20 minutes

        private static readonly int SESSION_POLL_PERIOD = 1000;

        private System.Threading.Timer sessionMonitoringTimer;

        private Dictionary<double, Session> sessions = new Dictionary<double, Session>();

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
                        throw new Exception("У доступі відмовлено!");

                    session = new Session(GetSystemUserDTO(user));

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

        private SystemUserDTO GetSystemUserDTO(SystemUser user)
        {
            switch (user.Role)
            {
                case UserRoles.None:
                    throw new Exception("What the fuck?! This role cannot be stored into DB");
                case UserRoles.InstituteAdmin:
                    return GetDTO<InstituteAdminDTO>(user);
                case UserRoles.FacultyAdmin:
                    return GetDTO<FacultyAdminDTO>(user);
                case UserRoles.InstituteSecretary:
                    return GetDTO<InstituteSecretaryDTO>(user);
                case UserRoles.FacultySecretary:
                    return GetDTO<FacultySecretaryDTO>(user);
                case UserRoles.Student:
                    return GetDTO<StudentDTO>(user);
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
                                where g.CathedraID== cathedraID
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
                            result.Add(GetSystemUserDTO(user));
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
                        context.AddToSystemUsers(GetSystemUser(user));
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
                                (item as IDTOable<StudentDTO>).Assign(user as StudentDTO);
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
                    var lectures = context.Lectures.Where(l=>l.TeacherID==teacherID).ToList();
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
                            context.LoadProperty(student, "Group");
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
                            context.LoadProperty(student, "Group");
                            if (result.Find(g => g.ID == student.GroupID) == null)
                                result.Add(student.Group.ToDTO());
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
                using(UniversityEntities context = new UniversityEntities())
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
                            var subj = context.Subjects.Where(s=>s.ID == subject.ID).FirstOrDefault();
                            Practice pr = new Practice { Subject = subj };
                            context.Practices.AddObject(pr);
                            context.PracticeTeachers.AddObject(new PracticeTeacher { Practice = pr, TeacherID = teacherID });
                        }
                    }
                    var toRemove = teacherPractices.ToList().Where(t => subjects.Find(s => s.ID == t.Practice.SubjectID) == null);

                    foreach (var pract in toRemove.ToList())
                    {
                        var item = teacherPractices.ToList().Where(t=>t.ID == pract.ID).FirstOrDefault();
                        
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

        public List<ControlDTO> GetLectureControls(Session session, int lectureID)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = context.Lectures.Include("LectureControls").FirstOrDefault(l => l.ID == lectureID);
                    return (query as Lecture).LectureControls.ToDTOList<ControlDTO, LectureControl>();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void SaveLectureControls(Session session, int lectureID, List<ControlDTO> controls)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    Lecture lecture = context.Lectures.Include("LectureControls").FirstOrDefault(l => l.ID == lectureID);

                    foreach (ControlDTO control in controls)
                        if (lecture.LectureControls.FirstOrDefault(lc => lc.ID == control.ID) == null) context.AddToControls(new LectureControl(control) { LectureID = lectureID });
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

        public List<ControlDTO> GetPracticeControls(Session session, int practiceID)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = context.Practices.Include("PracticeControls").Where(p => p.ID == practiceID).FirstOrDefault();
                    return (query as Practice).PracticeControls.ToDTOList<ControlDTO, PracticeControl>();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public void SavePracticeControls(Session session, int practiceID, List<ControlDTO> controls)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    Practice practice = context.Practices.Include("PracticeControls").FirstOrDefault(p => p.ID == practiceID);

                    foreach (ControlDTO control in controls)
                        if (practice.PracticeControls.FirstOrDefault(pc => pc.ID == control.ID) == null) context.AddToControls(new PracticeControl(control) { PracticeID = practiceID });
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

    }
}