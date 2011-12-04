using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    public class CathedraDTO : NamedEntityDTO
    {

        private int facultyID;
        [DataMember]
        public int FacultyID
        {
            get { return facultyID; }
            set { facultyID = value; }
        }

    }
}
