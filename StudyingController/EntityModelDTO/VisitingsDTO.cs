using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EntitiesDTO
{
    [DataContract]
    public class VisitingsDTO : BaseEntityDTO
    {
        public override int ID
        {
            get{ return -1;}
            set{ }
        }

        private int studentID;
        [DataMember]
        public int StudentID
        {
            get { return studentID; }
            set { studentID = value; }
        }

        private string studentName;
        [DataMember]
        public string StudentName
        {
            get { return studentName; }
            set { studentName = value; }
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

        private List<VisitingDTO> visitings;
        [DataMember]
        public List<VisitingDTO> Visitings
        {
            get { return visitings; }
            set { visitings = value; }
        }

        public VisitingsDTO()
        {
            visitings = new List<VisitingDTO>();
        }
    }
}
