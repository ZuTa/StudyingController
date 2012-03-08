using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    public class AttachmentDTO : NamedEntityDTO
    {
        public string Size
        {
            get
            {
                if (Data != null)
                    return Math.Round(Data.Length / 1024.0, 3).ToString() + " kb";
                return "-";
            }
        }

        private int teacherID;
        [DataMember]
        public int TeacherID
        {
            get { return teacherID; }
            set { teacherID = value; }
        }

        private string description;
        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private DateTime dateAdded;
        [DataMember]
        public DateTime DateAdded
        {
            get { return dateAdded; }
            set { dateAdded = value; }
        }

        private byte[] data;
        [DataMember]
        public byte[] Data
        {
            get { return data; }
            set { data = value; }
        }

    }
}
