using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    public class FacultySecretaryDTO : SystemUserDTO, IFacultyable
    {
        private FacultyDTO faculty;
        public FacultyDTO Faculty
        {
            get { return faculty; }
            set { faculty = value; }
        }

        private int facultyID;
        [DataMember]
        public int FacultyID
        {
            get { return facultyID; }
            set { facultyID = value; }
        }
    }
}
