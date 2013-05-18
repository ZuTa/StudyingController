using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Objects.DataClasses;

namespace EntitiesDTO
{
    [DataContract]
    [KnownType(typeof(InstituteDTO))]
    [KnownType(typeof(FacultyDTO))]
    [KnownType(typeof(CathedraDTO))]
    [KnownType(typeof(GroupDTO))]
    public abstract class BaseEntityDTO 
    {
        private int id;
        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public bool IsSameDatabaseObject(BaseEntityDTO obj)
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
