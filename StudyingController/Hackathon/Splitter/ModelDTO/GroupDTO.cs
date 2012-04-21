using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ModelDTO
{
    [DataContract]
    public class GroupDTO : BaseDTO
    {
        private string name;
        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string description;
        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int creatorID;
        [DataMember]
        public int CreatorID
        {
            get { return creatorID; }
            set { creatorID = value; }
        }

        public GroupDTO()
            :base()
        {
            
        }
    }
}
