using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EntitiesDTO
{
    [DataContract]
    public class BaseRef
    {
        [DataMember]
        public int ID { get; set; }
    }
}
