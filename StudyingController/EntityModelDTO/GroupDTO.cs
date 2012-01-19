using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    public class GroupDTO : NamedEntityDTO
    {
        private CathedraDTO cathedra;
        public CathedraDTO Cathedra
        {
            get { return cathedra; }
            set { cathedra = value; }
        }

        private int cathedraID;
        [DataMember]
        public int CathedraID
        {
            get { return cathedraID; }
            set { cathedraID = value; }
        }

    }
}
