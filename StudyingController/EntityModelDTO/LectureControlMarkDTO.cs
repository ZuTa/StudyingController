using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    [DataContract]
    public class LectureControlMarkDTO : MarkDTO
    {
        private int lectureControlID;
        [DataMember]
        public int LectureControlID
        {
            get { return lectureControlID; }
            set { lectureControlID = value; }
        }
    }
}
