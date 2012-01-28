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
                    //return new Student(user);
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
                    var query = from f in context.Faculties
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

        public List<GroupDTO> GetGroups(Session session, int cathedraID)
        {
            try
            {
                CheckSession(session);
                List<GroupDTO> result = new List<GroupDTO>();

                using (UniversityEntities context = new UniversityEntities())
                {
                    var query = from g in context.Groups
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
                        item.UpdateData(institute);

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
                     var item = context.Faculties.FirstOrDefault(fac => fac.ID == faculty.ID);
                     if (item == null)
                         context.AddToFaculties(new Faculty(faculty));
                     else
                         item.UpdateData(faculty);

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
                         item.UpdateData(cathedra);

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
                    var item = context.Groups.FirstOrDefault(gr => gr.ID == group.ID);
                    if (item == null)
                        context.AddToGroups(new Group(group));
                    else
                        item.UpdateData(group);

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
                                (item as IDTOable<InstituteAdminDTO>).UpdateData(user as InstituteAdminDTO);
                                break;
                            case UserRoles.FacultyAdmin:
                                (item as IDTOable<FacultyAdminDTO>).UpdateData(user as FacultyAdminDTO);
                                break;
                            case UserRoles.InstituteSecretary:
                                (item as IDTOable<InstituteSecretaryDTO>).UpdateData(user as InstituteSecretaryDTO);
                                break;
                            case UserRoles.FacultySecretary:
                                (item as IDTOable<FacultySecretaryDTO>).UpdateData(user as FacultySecretaryDTO);
                                break;
                            case UserRoles.Teacher:
                                (item as IDTOable<TeacherDTO>).UpdateData(user as TeacherDTO);
                                break;
                            case UserRoles.Student:
                                (item as IDTOable<StudentDTO>).UpdateData(user as StudentDTO);
                                break;
                            default:
                                item.UpdateData(user);
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
    }
}