using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ModelDTO
{
    [DataContract]
    [Flags]
    public enum GroupRoles
    {
        /// <summary>
        /// Participant
        /// </summary>
        [EnumMember]
        Participant = 1,
        /// <summary>
        /// Appraiser
        /// </summary>
        [EnumMember]
        Appraiser = 2,
        /// <summary>
        /// Creator
        /// </summary>
        [EnumMember]
        Creator = 4
    }

    [DataContract]
    public class GroupUserDTO : BaseDTO
    {
        private int userID;
        [DataMember]
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        private int groupID;
        [DataMember]
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        private GroupRoles groupRole;
        [DataMember]
        public GroupRoles GroupRole
        {
            get { return groupRole; }
            set { groupRole = value; }
        }

        public GroupUserDTO()
            : base()
        {
 
        }

    }
}
