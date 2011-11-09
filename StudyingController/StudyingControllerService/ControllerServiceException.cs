using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace StudyingControllerService
{
    [DataContract]
    public class ControllerServiceException
    {

        private string reason;
        [DataMember]
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }

        public ControllerServiceException(string reason)
        {
            this.reason = reason;
        }

    }
}