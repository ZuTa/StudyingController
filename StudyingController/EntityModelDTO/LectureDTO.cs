using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    public class LectureDTO : BaseEntityDTO
    {
        private TeacherDTO teacher;
        public TeacherDTO Teacher
        {
            get { return teacher; }
            set { teacher = value; }
        }

        private SubjectDTO subject;
        [DataMember]
        public SubjectDTO Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        private int teacherID;
        [DataMember]
        public int TeacherID
        {
            get { return teacherID; }
            set { teacherID = value; }
        }

        private List<GroupDTO> groups;
        [DataMember]
        public List<GroupDTO> Groups
        {
            get { return groups; }
            set { groups = value; }
        }
    }
}
