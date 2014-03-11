using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EntitiesDTO
{
    [DataContract]
    public class MarkRef : BaseRef
    {
        public decimal Value { get; set; }
    }
}
