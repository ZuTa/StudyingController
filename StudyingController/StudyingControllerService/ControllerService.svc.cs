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

        private static readonly TimeSpan SESSION_TIMEOUT = TimeSpan.FromSeconds(5 * 60); // 5 minutes

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
                        result.Add(GetDTO<FacultyDTO>(faculty));
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
                    var query = from c in context.Cathedras
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
                    foreach (var cathedra in context.Cathedras)
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
                    var query = from g in context.Groups.Include("Specialization")
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
                     var item = context.Cathedras.FirstOrDefault(cath => cath.ID == cathedra.ID);
                     if (item == null)
                         context.AddToCathedras(new Cathedra(cathedra));
                     else
                         item.Assign(cathedra);

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
            var item = (from f in context.Cathedras.Include("Teachers").Include("Groups")
                        where f.ID == cathedraID
                        select f).FirstOrDefault();
            if (item != null)
            {
                foreach (var group in item.Groups.ToList())
                    DeleteCathedra(context, group.ID);

                foreach (var user in item.Teachers.ToList())
                    context.SystemUsers.DeleteObject(user);

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
    }
}