using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    [DataContract]
    public class PracticeTeacherDTO:BaseEntityDTO
    {
        private List<StudentDTO> students;
        public List<StudentDTO> Students
        {
            get { return students; }
            set { students = value; }
        }
        

        private PracticeDTO practice;
        public PracticeDTO Practice
        {
            get { return practice; }
            set { practice = value; }
        }

        private int practiceID;
        [DataMember]
        public int PracticeID
        {
            get { return practiceID; }
            set { practiceID = value; }
        }

        private int teacherID;
        [DataMember]
        public int TeacherID
        {
            get { return teacherID; }
            set { teacherID = value; }
        }
    }
}
