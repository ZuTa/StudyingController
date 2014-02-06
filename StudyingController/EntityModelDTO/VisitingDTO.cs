 
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
    [KnownType(typeof(UserInformationDTO))]
    [KnownType(typeof(FacultyAdminDTO))]
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

        private int studentID;
        [DataMember]
        public int StudentID
        {
            get { return studentID; }
            set { studentID = value; }
        }

        private LectureDTO lecture;
        [DataMember]
        public LectureDTO Lecture
        {
            get { return lecture; }
            set { lecture = value; }
        }

        private PracticeDTO practice;
        [DataMember]
        public PracticeDTO Practice
        {
            get { return practice; }
            set { practice = value; }
        }

        public VisitingDTO()
        {
        }
    }
}
