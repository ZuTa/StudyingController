using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace SplitterService
{
    [DataContract]
    public class SplitterServiceException
    {
        private string reason;
        [DataMember]
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }

        public SplitterServiceException(string reason)
        {
            this.reason = reason;
        }
    }
}