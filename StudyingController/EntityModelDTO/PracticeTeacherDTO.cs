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
        [DataMember]
        public List<StudentDTO> Students
        {
            get { return students; }
            set { students = value; }
        }
        
        private PracticeRef practice;
        [DataMember]
        public PracticeRef Practice
        {
            get { return practice; }
            set { practice = value; }
        }

        private TeacherRef teacher;
        [DataMember]
        public TeacherRef Teacher
        {
            get { return teacher; }
            set { teacher = value; }
        }
    }
}
