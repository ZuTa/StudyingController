using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    [DataContract]
    public class TeacherDTO : SystemUserDTO, ICathedrable
    {
        private CathedraRef cathedra;
        [DataMember]
        public CathedraRef Cathedra
        {
            get { return cathedra; }
            set { cathedra = value; }
        }

        //private int cathedraID;
        //[DataMember]
        //public int CathedraID
        //{
        //    get { return cathedraID; }
        //    set { cathedraID = value; }
        //}

        private List<LectureRef> lectures;
        [DataMember]
        public List<LectureRef> Lectures
        {
            get { return lectures; }
            set { lectures = value; }
        }
    }
}
