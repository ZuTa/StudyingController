using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace StudyingControllerService
{
    [DataContract]
    public class Session
    {
        private static Random random = new Random();
        private static int counter = 0;

        public Session()
        {
            SessionID = random.NextDouble();
            SessionID += System.Threading.Interlocked.Increment(ref counter);
            LastAccessTime = DateTime.UtcNow;
        }

        public DateTime LastAccessTime { get; set; }

        [DataMember]
        public double SessionID { get; set; }
    }
}