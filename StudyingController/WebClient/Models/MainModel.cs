using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThinClient.SCS;

namespace ThinClient.Models
{
    public class MainModel
    {
        public string Name
        {
            get;
            set;
        }

        public string AdditionalInfo
        {
            get;
            set;
        }

        public string Role
        {
            get;
            set;
        }

        public string Faculty
        {
            get;
            set;
        }

        public List<NotificationDTO> Notifications
        {
            get;
            set;
        }
    }
}