using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    public class StudentDTO : SystemUserDTO, IGroupable
    {
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

        private string gradebook;
        [DataMember]
        public string Gradebook
        {
            get { return gradebook; }
            set { gradebook = value; }
        }

        private string studentCard;
        [DataMember]
        public string StudentCard
        {
            get { return studentCard; }
            set { studentCard = value; }
        }
    }
}
