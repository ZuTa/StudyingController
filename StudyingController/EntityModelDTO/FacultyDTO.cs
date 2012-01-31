using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    public class FacultyDTO : NamedEntityDTO
    {
        private InstituteDTO institute;
        public InstituteDTO Institute
        {
            get { return institute; }
            set { institute = value; }
        }

        private int? instituteID;
        [DataMember]
        public int? InstituteID
        {
            get { return instituteID; }
            set { instituteID = value; }
        }

        private List<SpecializationDTO> specializations;
        [DataMember]
        public List<SpecializationDTO> Specializations
        {
            get { return specializations; }
            set { specializations = value; }
        }

        public FacultyDTO()
            : base()
        {
            specializations = new List<SpecializationDTO>();
        }
    }
}
