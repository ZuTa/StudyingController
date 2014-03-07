 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EntitiesDTO
{
    [DataContract]
    [Flags]
    public enum VisitingValue
    {
        [EnumMember]
        Present = 0,
       
        [EnumMember]
        Absent = 1,
       
        [EnumMember]
        Sick = 2,
       
        [EnumMember]
        Individual = 3,
    }

    [DataContract]
    public class VisitingDTO : BaseEntityDTO
    {
        private DateTime date;
        [DataMember]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private VisitingValue value;
        [DataMember]
        public VisitingValue Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private string description;
        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private StudentRef student;
        [DataMember]
        public StudentRef Student
        {
            get { return student; }
            set { student = value; }
        }

        private LectureRef lecture;
        [DataMember]
        public LectureRef Lecture
        {
            get { return lecture; }
            set { lecture = value; }
        }

        private PracticeRef practice;
        [DataMember]
        public PracticeRef Practice
        {
            get { return practice; }
            set { practice = value; }
        }

        public VisitingDTO()
        {
        }
    }
}
