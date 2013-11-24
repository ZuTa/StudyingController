using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EntitiesDTO
{
    public class NotificationDTO : BaseEntityDTO
    {
        private string message;
        [DataMember]
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        private int userID;
        [DataMember]
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        private DateTime date;
        [DataMember]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
    }
}
