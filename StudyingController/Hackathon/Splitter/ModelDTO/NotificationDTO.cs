using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ModelDTO
{
    [DataContract]
    [Flags]
    public enum NotificationType
    {
        /// <summary>
        /// Group notification
        /// </summary>
        [EnumMember]
        Group = 1,
        /// <summary>
        /// Event notification
        /// </summary>
        [EnumMember]
        Event = 2
    }

    [DataContract]
    [Flags]
    public enum NotificationStatus
    {
        /// <summary>
        /// Accepted
        /// </summary>
        [EnumMember]
        Accepted = 1,
        /// <summary>
        /// Unaccepted
        /// </summary>
        [EnumMember]
        Unaccepted = 2,
        /// <summary>
        /// Declined
        /// </summary>
        [EnumMember]
        Declined = 4
    }

    [DataContract]
    public class NotificationDTO : BaseDTO
    {
        private NotificationType type;
        [DataMember]
        public NotificationType Type
        {
            get { return type; }
            set { type = value; }
        }

        private string text;
        [DataMember]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private NotificationStatus status;
        [DataMember]
        public NotificationStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        private int userID;
        [DataMember]
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        private int? groupID;
        [DataMember]
        public int? GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        private int? eventID;
        [DataMember]
        public int? EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }

        public NotificationDTO()
            : base()
        
        {
 
        }
    }
}
