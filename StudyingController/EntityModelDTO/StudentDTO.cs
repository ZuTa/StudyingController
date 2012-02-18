using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    public class StudentDTO : SystemUserDTO, IGroupable
    {
        public string Name
        {
            get { return string.Format("{0} {1}", UserInformation.LastName, UserInformation.FirstName); }
        }

        private GroupDTO group;
        public GroupDTO Group
        {
            get { return group; }
            set { group = value; }
        }

        private int groupID;
        [DataMember]
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
    }
}
