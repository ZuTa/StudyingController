using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    [DataContract]
    public class PracticeControlMarkDTO : MarkDTO
    {
        private int practiceControlID;
        [DataMember]
        public int PracticeControlID
        {
          get { return practiceControlID; }
          set { practiceControlID = value; }
        }
        
    }
}
