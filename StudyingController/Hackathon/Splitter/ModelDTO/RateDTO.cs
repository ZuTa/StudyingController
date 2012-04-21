using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ModelDTO
{
    [DataContract]
    public class RateDTO : BaseDTO
    {
        private int value;
        [DataMember]
        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private int skillID;
        [DataMember]
        public int SkillID
        {
            get { return skillID; }
            set { skillID = value; }
        }


        private int groupUserID;
        [DataMember]
        public int GroupUserID
        {
            get { return groupUserID; }
            set { groupUserID = value; }
        }

        public RateDTO()
            :base()
        {

        }
    }
}
