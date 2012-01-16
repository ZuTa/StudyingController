using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    public class GroupDTO : NamedEntityDTO
    {
        private int cathedraID;
        [DataMember]
        public int CathedraID
        {
            get { return cathedraID; }
            set { cathedraID = value; }
        }

    }
}
