using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    public class InstituteAdminDTO : SystemUserDTO, IInstituteable
    {
        private int instituteID;
        [DataMember]
        public int InstituteID
        {
            get { return instituteID; }
            set { instituteID = value; }
        }
    }
}
