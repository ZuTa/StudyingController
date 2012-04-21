using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ModelDTO
{
    [DataContract]
    public abstract class BaseDTO
    {
        private int id;
        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public bool IsSameDatabaseObject(BaseDTO obj)
        {
            if (obj == null)
                return false;
            if (obj.ID < 0)
                return false;

            return this.GetType() == obj.GetType() && obj.ID == this.ID;
        }

        public bool Exists()
        {
            return this.id > 0;
        }
    }
}
