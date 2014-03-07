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

        [DataMember]
        public StudentRef Student { get; set; }

        [DataMember]
        public LectureRef Lecture { get; set; }

        [DataMember]
        public PracticeRef Practice { get; set; }

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
