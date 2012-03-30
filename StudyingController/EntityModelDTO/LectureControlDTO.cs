using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    [DataContract]
    public class LectureControlDTO : ControlDTO
    {
        private int lectureID;
        [DataMember]
        public int LectureID
        {
            get { return lectureID; }
            set { lectureID = value; }
        }
    }
}
