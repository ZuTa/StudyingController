using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    public class StudentDTO : SystemUserDTO, IGroupable
    {
        private int groupID;
        [DataMember]
        public int GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }
    }
}
