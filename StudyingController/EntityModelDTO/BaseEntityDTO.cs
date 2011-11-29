using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Objects.DataClasses;

namespace EntitiesDTO
{
    [DataContract]
    public class BaseEntityDTO 
    {
        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
    }
}
