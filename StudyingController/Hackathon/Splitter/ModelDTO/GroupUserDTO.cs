using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ModelDTO
{
    [DataContract]
    [Flags]
    public enum UserRoles
    {
        /// <summary>
        /// Учасник
        /// </summary>
        [EnumMember]
        Participant = 0,
        /// <summary>
        /// Головний адміністратор
        /// </summary>
        [EnumMember]
        None = 0,

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

        public GroupUserDTO()
            : base()
        {
 
        }

    }
}
