using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ModelDTO
{
    [DataContract]
    public class GroupMessageDTO : BaseDTO
    {
        private string text;
        [DataMember]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private DateTime date;
        [DataMember]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private int groupUserID;
        [DataMember]
        public int GroupUserID
        {
            get { return groupUserID; }
            set { groupUserID = value; }
        }

        public GroupMessageDTO()
            : base()
        {

        }
    }
}
