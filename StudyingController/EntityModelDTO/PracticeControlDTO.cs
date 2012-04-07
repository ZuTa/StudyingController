using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    [DataContract]
    public class PracticeControlDTO : ControlDTO
    {
        private int practiceID;
        [DataMember]
        public int PracticeID
        {
            get { return practiceID; }
            set { practiceID = value; }
        }
    }
}
