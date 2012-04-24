using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EntityModel;
using ModelDTO;

namespace SplitterService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class SplitterService : ISplitterService
    {
        #region Fields & Properties

        private static readonly TimeSpan SESSION_TIMEOUT = TimeSpan.FromSeconds(20 * 60); // 20 minutes

        private static readonly int SESSION_POLL_PERIOD = 1000;

        private System.Threading.Timer sessionMonitoringTimer;

        private Dictionary<double, Session> sessions = new Dictionary<double, Session>();

        #endregion
        
        #region Constructors

        public SplitterService()
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
                    throw new Exception("Login denied! Session expired");

                s.LastAccessTime = DateTime.UtcNow;
            }
        }

        #endregion

        private T GetDTO<T>(System.Data.Objects.DataClasses.EntityObject o) where T : BaseDTO
        {
            return (o as IDTOable<T>).ToDTO();
        }

        public Session Login(string login, string password)
        {
            Session session = null;

            try
            {
                using (SplitterDBEntities context = new SplitterDBEntities())
                {
                    SystemUser user = (from u in context.SystemUsers
                                       where u.Login == login.ToLower()
                                       select u).FirstOrDefault();
                    string s = Encoding.UTF8.GetString(user.Password);
                    if (!(user != null && Encoding.UTF8.GetString(user.Password) == password))
                        throw new Exception("Incorrect login or password");

                    session = new Session(GetDTO<SystemUserDTO>(user));

                    lock (sessions)
                        sessions[session.SessionID] = session;

                    return session;
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<SplitterServiceException>(new SplitterServiceException(ex.Message), ex.Message);
            }
        }

        public bool CanRegister(ModelDTO.SystemUserDTO user)
        {
            try
            {
                bool result = false;

                using (SplitterDBEntities context = new SplitterDBEntities())
                {
                    var systemUser = context.SystemUsers.FirstOrDefault(u => u.Login == user.Login);

                    result = systemUser == null;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new FaultException<SplitterServiceException>(new SplitterServiceException(ex.Message), ex.Message);
            }
        }

        public void Register(ModelDTO.SystemUserDTO user)
        {
            try
            {
                using (SplitterDBEntities context = new SplitterDBEntities())
                {
                    context.SystemUsers.AddObject(new SystemUser(user));
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException<SplitterServiceException>(new SplitterServiceException(ex.Message), ex.Message);
            }
        }
    }
}
