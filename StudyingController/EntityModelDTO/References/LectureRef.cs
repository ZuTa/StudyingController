﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace EntitiesDTO
{
    [DataContract]
    public class LectureRef : BaseRef
    {
        [DataMember]
        public SubjectRef Subject { get; set; }
    }
}
