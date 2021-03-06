﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using StudyingControllerEntityModel;
using EntitiesDTO;
using System.Globalization;
using Common;

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
                    SystemUser user = (from u in context.SystemUsers
                                       where u.Login == login.ToLower()
                                       select u).FirstOrDefault();

                    if (!(user != null /*&& Encoding.UTF8.GetString(user.Password) == password*/))
                    {
                        //#if DEBUG
                        //if (user == null)
                        //{
                        //    user = new SystemUser();
                        //    user.Login = login;
                        //    user.Password = Encoding.UTF8.GetBytes(password);
                        //    user.iUserRole = 1;
                        //    var userInformation = new UserInformation()
                        //    {
                        //        FirstName = login,
                        //        LastName = login
                        //    };
                        //    context.AddToUserInformations(userInformation);
                        //    context.SaveChanges();

                        //    user.UserInformation = userInformation;

                        //    context.AddToSystemUsers(user);
                        //    context.SaveChanges();
                        //}
                        //#else
                        throw new Exception("У доступі відмовлено!");
                        //#endif
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
                using (UniversityEntities context = new UniversityEntities())
                {
                    return InternalGetInstitutes(context).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<FacultyDTO> GetFaculties(Session session, InstituteRef institute)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    return InternalGetFaculties(context, institute).ToList();
                }
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

        public List<CathedraDTO> GetCathedras(Session session, FacultyRef faculty)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    return InternalGetCathedras(context, faculty).ToList();
                }
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

        public List<GroupDTO> GetGroups(Session session, CathedraRef cathedra)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    return InternalGetGroups(context, cathedra).ToList();
                }
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
                    foreach (SystemUser user in context.SystemUsers)
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
                    var item = context.SystemUsers.FirstOrDefault(gr => gr.ID == user.ID);

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

        public List<LectureDTO> GetLectures(Session session, TeacherRef teacher)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    return InternalGetLectures(context, teacher).ToList();
                }
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

        public List<TeacherDTO> GetTeachers(Session session, CathedraRef cathedra)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    return InternalGetTeachers(context, cathedra).ToList();
                }
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

        public List<PracticeTeacherDTO> GetPracticesTeacher(Session session, TeacherRef teacher)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                   return InternalGetPracticeTeachers(context, teacher).ToList();
                }
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
                    var query = context.ControlMessages.Include("Control").Where(cm => cm.ControlID == controlID);
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
                    {
                        item = new LectureControl(control);
                        context.Controls.AddObject(item);
                        context.SaveChanges();

                        int lectureID = (item as LectureControl).LectureID;
                        Lecture lecture = context.Lectures.Include("Groups").Include("Subject").FirstOrDefault(lect => lect.ID == lectureID);
                        Teacher teacher = context.SystemUsers.First(teach => teach.ID == lecture.TeacherID) as Teacher;

                        foreach (var group in lecture.Groups)
                        {
                            foreach (var student in context.Groups.Include("Students").First(gr => gr.ID == group.ID).Students)
                            {
                                context.AddToNotifications(new Notification(new NotificationDTO
                                {
                                    Date = DateTime.Now,
                                    Message = string.Format("Новий контроль ({5}) з предмету \"{0}\" - Л (викладач - {1}) відбудеться {2} об {3}. Максимальна оцінка - {4}.",
                                           lecture.Subject.Name,
                                           teacher.ShortName,
                                           item.Date.ToShortDateString(),
                                           item.Date.ToShortTimeString(),
                                           item.MaxMark,
                                           item.Name),
                                    UserID = student.ID
                                }));
                            }
                        }
                        context.SaveChanges();
                    }
                    else
                    {
                        item.Assign(control);
                        context.SaveChanges();

                        int lectureID = (item as LectureControl).LectureID;
                        Lecture lecture = context.Lectures.Include("Groups").Include("Subject").FirstOrDefault(lect => lect.ID == lectureID);
                        Teacher teacher = context.SystemUsers.First(teach => teach.ID == lecture.TeacherID) as Teacher;

                        foreach (var group in lecture.Groups)
                        {
                            foreach (var student in context.Groups.Include("Students").First(gr => gr.ID == group.ID).Students)
                            {
                                context.AddToNotifications(new Notification(new NotificationDTO
                                {
                                    Date = DateTime.Now,
                                    Message = string.Format("Внесено зміни! Контроль ({5}) з предмету \"{0}\" - Л (викладач - {1}) відбудеться {2} об {3}. Максимальна оцінка - {4}.",
                                           lecture.Subject.Name,
                                           teacher.ShortName,
                                           item.Date.ToShortDateString(),
                                           item.Date.ToShortTimeString(),
                                           item.MaxMark,
                                           item.Name),
                                    UserID = student.ID
                                }));
                            }
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

        public List<LectureDTO> GetStudentLectures(Session session, StudentRef student)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    return InternalGetStudentLectures(context, student).ToList();
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
                        item = new PracticeControl(control);
                        context.Controls.AddObject(item);
                        context.SaveChanges();

                        int practiceID = (item as PracticeControl).PracticeID;
                        Practice practice = context.Practices.Include("Subject").FirstOrDefault(prac => prac.ID == practiceID);

                        PracticeControl practiceItem = item as PracticeControl;
                        context.LoadProperty(practiceItem, "Practice_Teacher");

                        int practiceTeacherID = (item as PracticeControl).Practice_Teacher.ID;
                        PracticeTeacher practiceTeacher = context.PracticeTeachers.Include("Students").First(pt => pt.ID == practiceTeacherID);
                        Teacher teacher = context.SystemUsers.First(teach => teach.ID == practiceItem.Practice_Teacher.TeacherID) as Teacher;

                        foreach (var student in practiceTeacher.Students)
                        {
                            context.AddToNotifications(new Notification(new NotificationDTO
                            {
                                Date = DateTime.Now,
                                Message = string.Format("Новий контроль ({5}) з предмету \"{0}\" - П (викладач - {1}) відбудеться {2} об {3}. Максимальна оцінка - {4}.",
                                       practice.Subject.Name,
                                       teacher.ShortName,
                                       practiceItem.Date.ToShortDateString(),
                                       practiceItem.Date.ToShortTimeString(),
                                       practiceItem.MaxMark,
                                       practiceItem.Name),
                                UserID = student.ID
                            }));
                        }
                        context.SaveChanges();
                    }
                    else
                    {
                        item.Assign(control);
                        context.SaveChanges();

                        int practiceID = (item as PracticeControl).PracticeID;
                        Practice practice = context.Practices.Include("Subject").FirstOrDefault(prac => prac.ID == practiceID);

                        PracticeControl practiceItem = item as PracticeControl;
                        context.LoadProperty(practiceItem, "Practice_Teacher");

                        int practiceTeacherID = (item as PracticeControl).Practice_Teacher.ID;
                        PracticeTeacher practiceTeacher = context.PracticeTeachers.Include("Students").First(pt => pt.ID == practiceTeacherID);
                        Teacher teacher = context.SystemUsers.First(teach => teach.ID == practiceItem.Practice_Teacher.TeacherID) as Teacher;

                        foreach (var student in practiceTeacher.Students)
                        {
                            context.AddToNotifications(new Notification(new NotificationDTO
                            {
                                Date = DateTime.Now,
                                Message = string.Format("Внесено зміни! Контроль ({5}) з предмету \"{0}\" - П (викладач - {1}) відбудеться {2} об {3}. Максимальна оцінка - {4}.",
                                       practice.Subject.Name,
                                       teacher.ShortName,
                                       practiceItem.Date.ToShortDateString(),
                                       practiceItem.Date.ToShortTimeString(),
                                       practiceItem.MaxMark,
                                       practiceItem.Name),
                                UserID = student.ID
                            }));
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

        public List<PracticeTeacherDTO> GetStudentPractices(Session session, StudentRef student)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    return InternalGetStudentPracticeTeachers(context, student).ToList();
                }
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
                                context.LoadProperty(m.Student, "Groups");
                                result.Add(m.ToDTO());
                            }
                            context.LoadProperty(pc.Practice_Teacher, "Students");
                            foreach (Student s in pc.Practice_Teacher.Students)
                            {
                                var query = pc.PracticeControlMarks.Where(m => m.StudentID == s.ID).FirstOrDefault();
                                if (query == null)
                                {
                                    context.LoadProperty(s, "Groups");
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
                                context.LoadProperty(m.Student, "Groups");
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

                        List<MarkDTO> toNotify = toAdd.Concat(toEdit).ToList();
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

        public PracticeDTO GetPractice(Session session, int id)
        {
            try
            {
                this.CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = (from item
                                      in context.Practices
                                 where item.ID == id
                                 select item).FirstOrDefault();
                    if (query != null)
                        context.LoadProperty(query, "Subject");
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
                    return query == null ? null : query.ToDTO();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public StudentDTO GetStudent(Session session, int id)
        {
            try
            {
                this.CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = (from item
                                      in context.SystemUsers
                                 where item.ID == id
                                 select item).FirstOrDefault();
                    if (query != null)
                        context.LoadProperty(query as Student, "Groups");
                    return query == null ? null : (query as Student).ToDTO();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public ControlDTO GetControlById(Session session, int id)
        {
            try
            {
                this.CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = (from item in context.Controls
                                 where item.ID == id
                                 select item).FirstOrDefault();

                    if (query != null && query is LectureControl)
                    {
                        return (query as LectureControl).ToDTO();
                    }

                    if (query != null && query is PracticeControl)
                    {
                        return (query as PracticeControl).ToDTO();
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<VisitingsDTO> GetVisitingsForLecture(Session session, LectureRef lecRef)
        {
            try
            {
                CheckSession(session);

                List<VisitingsDTO> result = new List<VisitingsDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    Lecture lecture = context.Lectures.First(c => c.ID == lecRef.ID) as Lecture;

                    context.LoadProperty(lecture, "Groups");
                    foreach (Group g in lecture.Groups)
                    {
                        context.LoadProperty(g, "Students");
                        foreach (Student s in g.Students)
                        {
                            result.Add(new VisitingsDTO
                            {
                                Lecture = new LectureRef { ID = lecture.ID },
                                Student = new StudentRef { ID = s.ID, Name = s.Name }
                            });
                        }
                    }

                    context.LoadProperty(lecture, "Visitings");
                    var dates = lecture.Visitings.Select(v => v.Date).Distinct();

                    foreach (var res in result)
                    {
                        res.Visitings = new List<VisitingDTO>();
                        foreach (var dat in dates)
                        {
                            Visiting visiting = lecture.Visitings.FirstOrDefault(v => v.Date == dat && v.StudentID == res.Student.ID);
                            if (visiting == null)
                            {
                                visiting = new Visiting
                                {
                                    Date = dat,
                                    Lecture = lecture,
                                    StudentID = res.Student.ID,
                                };

                                context.Visitings.AddObject(visiting);
                                context.SaveChanges();
                            }

                            res.Visitings.Add(visiting.ToDTO());
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

        public void SaveVisitingsForLecture(Session session, LectureRef lecRef, List<VisitingDTO> visitings)
        {
            try
            {
                CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var lecture = context.Lectures.FirstOrDefault(l => l.ID == lecRef.ID);
                    if (lecture != null)
                    {
                        var toEdit = new List<VisitingDTO>();
                        var toAdd = new List<VisitingDTO>();

                        context.LoadProperty(lecture, "Groups");
                        context.LoadProperty(lecture, "Visitings");

                        toAdd = visitings.Where(v => lecture.Visitings.ToList().Find(lv => lv.StudentID == v.Student.ID) == null).ToList();
                        toEdit = visitings.Where(v => lecture.Visitings.ToList().Find(lv => lv.StudentID == v.Student.ID) != null).ToList();

                        foreach (var a in toAdd)
                        {
                            Visiting vis = new Visiting(a as VisitingDTO);
                            vis.Lecture = lecture;
                            context.Visitings.AddObject(vis);
                        }

                        foreach (var e in toEdit)
                        {
                            var elem = context.Visitings.Where(v => v.ID == e.ID).FirstOrDefault();
                            if (elem == null)
                            {
                                Visiting vis = new Visiting(e as VisitingDTO);
                                vis.Lecture = lecture;
                                context.Visitings.AddObject(vis);
                            }
                            else
                            {
                                //elem.Assign(e);
                                //elem.Lecture = lecture;
                                elem.Value = (int)e.Value;
                                elem.Description = e.Description;
                                elem.Date = e.Date;
                            }
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

        public void UpdateMarkValue(Session session, MarkRef mark)
        {
            try
            {
                this.CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var existingMark = context.Marks.Where(em => em.ID == mark.ID).FirstOrDefault();
                    existingMark.MarkValue = mark.Value;

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<BaseEntityDTO> GetLessonTree(Session session, SystemUserRef userRef)
        {
            try
            {
                this.CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    List<BaseEntityDTO> result = new List<BaseEntityDTO>();

                    List<InstituteDTO> institutes = new List<InstituteDTO>();
                    List<FacultyDTO> faculties = new List<FacultyDTO>();
                    List<CathedraDTO> cathedras = new List<CathedraDTO>();
                    List<TeacherDTO> teachers = new List<TeacherDTO>();
                    List<PracticeTeacherDTO> practices = new List<PracticeTeacherDTO>();
                    List<LectureDTO> lectures = new List<LectureDTO>();

                    var user = context.SystemUsers.First(s => s.ID == userRef.ID);

                    switch (user.Role)
                    {
                        case UserRoles.MainSecretary:
                        case UserRoles.MainAdmin:
                            institutes = InternalGetInstitutes(context).ToList();

                            foreach (var ins in institutes)
                                faculties.AddRange(InternalGetFaculties(context, ins.CopyTo(() => new InstituteRef())));
                            faculties.AddRange(InternalGetFaculties(context, null));

                            foreach (var fac in faculties)
                                cathedras.AddRange(InternalGetCathedras(context, fac.CopyTo(() => new FacultyRef())));

                            foreach (var cath in cathedras)
                                teachers.AddRange(InternalGetTeachers(context, cath.CopyTo(() => new CathedraRef())));

                            foreach (var teach in teachers)
                            {
                                practices.AddRange(InternalGetPracticeTeachers(context, teach.CopyTo(() => new TeacherRef())));
                                lectures.AddRange(InternalGetLectures(context, teach.CopyTo(() => new TeacherRef())));
                            }
                            break;
                        case UserRoles.InstituteAdmin:
                        case UserRoles.InstituteSecretary:
                            var instituteId = (user as IInstituteable).InstituteID;

                            faculties.AddRange(InternalGetFaculties(context, new InstituteRef { ID = instituteId }));

                            foreach (var fac in faculties)
                                cathedras.AddRange(InternalGetCathedras(context, fac.CopyTo(() => new FacultyRef())));

                            foreach (var cath in cathedras)
                                teachers.AddRange(InternalGetTeachers(context, cath.CopyTo(() => new CathedraRef())));

                            foreach (var teach in teachers)
                            {
                                practices.AddRange(InternalGetPracticeTeachers(context, teach.CopyTo(() => new TeacherRef())));
                                lectures.AddRange(InternalGetLectures(context, teach.CopyTo(() => new TeacherRef())));
                            }
                            break;
                        case UserRoles.FacultyAdmin:
                        case UserRoles.FacultySecretary:
                            var facultyId = (user as IFacultyable).FacultyID;

                            cathedras.AddRange(InternalGetCathedras(context, new FacultyRef { ID = facultyId }));

                            foreach (var cath in cathedras)
                                teachers.AddRange(InternalGetTeachers(context, cath.CopyTo(() => new CathedraRef())));

                            foreach (var teach in teachers)
                            {
                                practices.AddRange(InternalGetPracticeTeachers(context, teach.CopyTo(() => new TeacherRef())));
                                lectures.AddRange(InternalGetLectures(context, teach.CopyTo(() => new TeacherRef())));
                            }
                            break;
                        case UserRoles.Student:
                            //practices.AddRange(InternalGetStudentPracticeTeachers(context, user.CopyTo(() => new StudentRef())));
                            //lectures.AddRange(InternalGetStudentLectures(context, user.CopyTo(() => new StudentRef())));
                            break;
                        case UserRoles.Teacher:
                            practices.AddRange(InternalGetPracticeTeachers(context, user.CopyTo(() => new TeacherRef())));
                            lectures.AddRange(InternalGetLectures(context, user.CopyTo(() => new TeacherRef())));
                            break;
                        default:
                            throw new NotImplementedException("Unknown user's role!");
                    }
                    result.AddRange(institutes);
                    result.AddRange(faculties);
                    result.AddRange(cathedras);
                    result.AddRange(teachers);
                    result.AddRange(practices);
                    result.AddRange(lectures);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public List<BaseEntityDTO> GetUniversityTree(Session session, SystemUserRef userRef)
        {
            try
            {
                this.CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    List<BaseEntityDTO> result = new List<BaseEntityDTO>();

                    List<InstituteDTO> institutes = new List<InstituteDTO>();
                    List<FacultyDTO> faculties = new List<FacultyDTO>();
                    List<CathedraDTO> cathedras = new List<CathedraDTO>();
                    List<GroupDTO> groups = new List<GroupDTO>();

                    var user = context.SystemUsers.First(s => s.ID == userRef.ID);

                    switch (user.Role)
                    {
                        case UserRoles.MainSecretary:
                        case UserRoles.MainAdmin:
                            institutes = InternalGetInstitutes(context).ToList();

                            foreach (var ins in institutes)
                                faculties.AddRange(InternalGetFaculties(context, ins.CopyTo(() => new InstituteRef())));
                            faculties.AddRange(InternalGetFaculties(context, null));

                            foreach (var fac in faculties)
                                cathedras.AddRange(InternalGetCathedras(context, fac.CopyTo(() => new FacultyRef())));

                            foreach (var cath in cathedras)
                                groups.AddRange(InternalGetGroups(context, cath.CopyTo(() => new CathedraRef())));
                            break;
                        case UserRoles.InstituteAdmin:
                        case UserRoles.InstituteSecretary:
                            var instituteId = (user as IInstituteable).InstituteID;

                            faculties.AddRange(InternalGetFaculties(context, new InstituteRef { ID = instituteId }));

                            foreach (var fac in faculties)
                                cathedras.AddRange(InternalGetCathedras(context, fac.CopyTo(() => new FacultyRef())));

                            foreach (var cath in cathedras)
                                groups.AddRange(InternalGetGroups(context, cath.CopyTo(() => new CathedraRef())));
                            break;
                        case UserRoles.FacultyAdmin:
                        case UserRoles.FacultySecretary:
                            var facultyId = (user as IFacultyable).FacultyID;

                            cathedras.AddRange(InternalGetCathedras(context, new FacultyRef { ID = facultyId }));

                            foreach (var cath in cathedras)
                                groups.AddRange(InternalGetGroups(context, cath.CopyTo(() => new CathedraRef())));
                            break;
                        default:
                            throw new NotImplementedException("Unknown user's role!");
                    }
                    result.AddRange(institutes);
                    result.AddRange(faculties);
                    result.AddRange(cathedras);

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        private IEnumerable<InstituteDTO> InternalGetInstitutes(UniversityEntities context)
        {
            foreach (Institute institute in context.Institutes)
            {
                yield return institute.ToDTO();
            }
        }

        private IEnumerable<FacultyDTO> InternalGetFaculties(UniversityEntities context, InstituteRef instituteRef)
        {
            int? institute = instituteRef == null? null as int?: instituteRef.ID;
            var query = context.Faculties.Include("Specializations")
                        .Where(f => (institute != null && f.InstituteID == institute)
                        || (institute == null && f.InstituteID == null)).ToList();
            foreach (var faculty in query)
            {
                context.LoadProperty(faculty, "Institute");
                yield return faculty.ToDTO();
            }
        }

        private IEnumerable<CathedraDTO> InternalGetCathedras(UniversityEntities context, FacultyRef facultyRef)
        {
            int faculty = facultyRef.ID;
            var query = from c in context.Cathedras.Include("Subjects").Include("Faculty")
                        where c.FacultyID == faculty
                        select c;
            foreach (var c in query)
                yield return c.ToDTO();
        }

        private IEnumerable<TeacherDTO> InternalGetTeachers(UniversityEntities context, CathedraRef cathedraRef)
        {
            int cathedra = cathedraRef.ID;
            var query = context.Cathedras.Include("Teachers").Where(c => c.ID == cathedra).Select(c => c.Teachers).FirstOrDefault();

            if (query != null)
            {
                foreach (var item in query)
                {
                    context.LoadProperty(item, "Lectures");
                    foreach (var lecture in item.Lectures)
                        context.LoadProperty(lecture, "Subject");

                    yield return item.ToDTO();
                }
            }
        }

        private IEnumerable<PracticeTeacherDTO> InternalGetPracticeTeachers(UniversityEntities context, TeacherRef teacherRef)
        {
            var teacher = teacherRef.ID;
            var query = from pt in context.PracticeTeachers.Include("Practice")
                        where pt.TeacherID == teacher
                        select pt;
            foreach (var practiceTeacher in query)
            {
                context.LoadProperty(practiceTeacher.Practice, "Subject");
                context.LoadProperty(practiceTeacher, "Teacher");
                context.LoadProperty(practiceTeacher, "Students");
                foreach (var student in practiceTeacher.Students)
                {
                    context.LoadProperty(student, "Groups");
                    var currentGroup = student.Groups.FirstOrDefault();//(g => g.StudyRangeID == Configuration.StudyRangeID);

                    if (currentGroup == null)
                        throw new Exception("Student hasnt a group!");

                    student.CurrentGroupID = currentGroup.ID;
                }
                yield return practiceTeacher.ToDTO();
            }
        }

        private IEnumerable<LectureDTO> InternalGetLectures(UniversityEntities context, TeacherRef teacherRef)
        {
            int teacher = teacherRef.ID;

            var query = from l in context.Lectures.Include("Groups")
                        where l.TeacherID == teacher
                        select l;

            foreach (var lecture in query)
            {
                context.LoadProperty(lecture, "Subject");
                yield return lecture.ToDTO();
            }
        }

        private IEnumerable<PracticeTeacherDTO> InternalGetStudentPracticeTeachers(UniversityEntities context, StudentRef studentRef)
        {
            int student = studentRef.ID;

            foreach (var practice in context.PracticeTeachers.Include("Practice").Include("Students").Include("Teacher"))
                if (practice.Students.FirstOrDefault(s => s.ID == student) != null)
                {
                    context.LoadProperty(practice.Practice, "Subject");

                    foreach (var stud in practice.Students)
                    {
                        context.LoadProperty(stud, "Groups");
                        var currentGroup = stud.Groups.FirstOrDefault();// (g => g.StudyRangeID == Configuration.StudyRangeID);

                        if (currentGroup == null)
                            throw new Exception("Student hasnt a group!");

                        stud.CurrentGroupID = currentGroup.ID;
                    }

                    yield return practice.ToDTO();
                }
        }

        private IEnumerable<LectureDTO> InternalGetStudentLectures(UniversityEntities context, StudentRef studentRef)
        {
            int studentId = studentRef.ID;
            Student student = context.SystemUsers.FirstOrDefault(u => u.ID == studentId) as Student;
            context.LoadProperty(student, "Groups");
            var currentGroup = student.Groups.FirstOrDefault();// (g => g.StudyRangeID == Configuration.StudyRangeID);

            if (currentGroup == null)
                throw new Exception("Student hasnt a group!");

            context.LoadProperty(currentGroup, "Lectures");
            foreach (var lecture in currentGroup.Lectures)
            {
                context.LoadProperty(lecture, "Subject");
                yield return lecture.ToDTO();
            }
        }

        private IEnumerable<GroupDTO> InternalGetGroups(UniversityEntities context, CathedraRef cathedraRef)
        {
            int cathedra = cathedraRef.ID;
            var query = from g in context.Groups.Include("Specialization").Include("Cathedra")
                        where g.CathedraID == cathedra
                        select g;
            foreach (var group in query)
                yield return GetDTO<GroupDTO>(group);
        }

        #region IRefService methods

        public IEnumerable<LectureRef> GetLectureRefsByTeacherId(Session session, int teacherId) 
        {
            try
            {
                this.CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var resultList = new List<LectureRef>();

                    var lectures = context.Lectures.Where(l => l.TeacherID == teacherId);
                    foreach (var l in lectures)
                    {
                        context.LoadProperty(l, "Subject");
                        resultList.Add(new LectureRef()
                        {
                            ID = l.ID,
                            Subject = new SubjectRef { ID = l.Subject.ID, Name = l.Subject.Name }
                        });
                    }

                    return resultList;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        public IEnumerable<PracticeTeacherRef> GetPracticeTeacherRefsByTeacherId(Session session, int teacherId)
        {
            try
            {
                this.CheckSession(session);
                using (UniversityEntities context = new UniversityEntities())
                {
                    var resultList = new List<PracticeTeacherRef>();

                    var practiceTeachers = context.PracticeTeachers.Where(pt => pt.TeacherID == teacherId);
                    foreach (var pt in practiceTeachers)
                    {
                        context.LoadProperty(pt, "Practice");
                        context.LoadProperty(pt.Practice, "Subject");
                        resultList.Add(new PracticeTeacherRef
                        {
                            ID = pt.ID,
                            Name = pt.Practice.Subject.Name
                        });
                    }

                    return resultList;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<ControllerServiceException>(new ControllerServiceException(ex.Message), ex.Message);
            }
        }

        #endregion
    }
}
