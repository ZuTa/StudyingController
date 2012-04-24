using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    [DataContract]
    [KnownType(typeof(LectureControlMarkDTO))]
    [KnownType(typeof(PracticeControlMarkDTO))]
    public abstract class MarkDTO : BaseEntityDTO 
    {
        protected StudentDTO student;
        [DataMember]
        public StudentDTO Student
        {
            get { return student; }
            set { student = value; }
        }

        protected int studentID;
        [DataMember]
        public int StudentID
        {
            get { return studentID; }
            set { studentID = value; }
        }

        protected decimal markValue;
        [DataMember]
        public decimal MarkValue
        {
            get { return markValue; }
            set { markValue = value; }
        }

        protected string description;
        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
