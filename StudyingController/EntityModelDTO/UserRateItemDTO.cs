using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EntitiesDTO
{
    [DataContract]
    public class UserRateItemDTO
    {
        [DataMember]
        public StudentDTO Student { get; set; }

        [DataMember]
        public double Rate { get; set; }

        public decimal RateDecimal 
        {
            get { return Convert.ToDecimal(Rate); }
            set { Rate = Decimal.ToDouble(value); }
        }
    }
}
