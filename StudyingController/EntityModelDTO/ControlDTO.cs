using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    [DataContract]
    [KnownType(typeof(LectureControlDTO))]
    [KnownType(typeof(PracticeControlDTO))]
    public abstract class ControlDTO : NamedEntityDTO
    {
        private DateTime date;
        [DataMember]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private string description;
        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private Decimal maxMark;
        [DataMember]
        public Decimal MaxMark
        {
            get { return maxMark; }
            set { maxMark = value; }
        }
    }
}
