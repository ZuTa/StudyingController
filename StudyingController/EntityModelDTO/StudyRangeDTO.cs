using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    public class StudyRangeDTO : BaseEntityDTO
    {
        private int year;
        [DataMember]
        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        private int yearPart;
        [DataMember]
        public int YearPart
        {
            get { return yearPart; }
            set { yearPart = value; }
        }

        public override string ToString()
        {
            return string.Format("{0} рік, {1} семестр", year, YearPart);
        }
    }
}
