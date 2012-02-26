using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    public class CathedraDTO : NamedEntityDTO
    {

        private FacultyDTO faculty;
        [DataMember]
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

        private List<SubjectDTO> subjects;
        [DataMember]
        public List<SubjectDTO> Subjects
        {
            get { return subjects; }
            set { subjects = value; }
        }

        public CathedraDTO()
            : base()
        {
            subjects = new List<SubjectDTO>();
        }
    }
}
