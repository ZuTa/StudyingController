using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ModelDTO
{
    [DataContract]
    public class SkillDTO : BaseDTO
    {
        private string name;
        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int groupID;
        [DataMember]
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        public SkillDTO()
            :base()
        { 

        }
    }
}
