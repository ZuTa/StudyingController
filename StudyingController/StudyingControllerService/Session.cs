using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using EntitiesDTO;

namespace StudyingControllerService
{
    [DataContract]
    [KnownType(typeof(InstituteAdminDTO))]
    [KnownType(typeof(FacultyAdminDTO))]
    public class Session
    {
        #region Fields & Properties

        private static Random random = new Random();
        private static int counter = 0;

        private SystemUserDTO user;
        [DataMember]
        public SystemUserDTO User
        {
            get { return user; }
            set { user = value; }
        }

        public DateTime LastAccessTime { get; set; }

        [DataMember]
        public double SessionID { get; set; }

        #endregion

        #region Constructors

        public Session(SystemUserDTO user)
        {
            this.user = user;

            SessionID = random.NextDouble();
            SessionID += System.Threading.Interlocked.Increment(ref counter);
            LastAccessTime = DateTime.UtcNow;
        }

        #endregion
    }
}