using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    public class ControlMessageDTO : BaseEntityDTO
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

        private ControlDTO control;
        public ControlDTO Control
        {
            get { return control; }
            set { control = value; }
        }

        private int controlID;
        [DataMember]
        public int ControlID
        {
            get { return controlID; }
            set { controlID = value; }
        }

        private SystemUserDTO owner;
        [DataMember]
        public SystemUserDTO Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public override string ToString()
        {
            return text;
        }
    }
}
