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
        public SystemUserDTO User { get; set; }

        [DataMember]
        public double Rate { get; set; }
    }
}
