using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    public class PracticeDTO:BaseEntityDTO
    {
        private SubjectDTO subject;
        public SubjectDTO Subject
        {
            get { return subject; }
            set { subject = value; }
        }


        private int subjectID;
        [DataMember]
        public int SubjectID
        {
            get { return subjectID; }
            set { subjectID = value; }
        }
    }
}
