using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Specialization : IDTOable<SpecializationDTO>, IDataBase
    {

        #region Constructors

        public Specialization()
        {
        }

        public Specialization(SpecializationDTO entity)
        {
            Assign(entity);
        }

        #endregion

        public SpecializationDTO ToDTO()
        {
            return new SpecializationDTO
            {
                ID = this.ID,
                Name = this.Name,
                FacultyID = this.FacultyID
            };
        }

        public void Assign(SpecializationDTO entity)
        {
            this.ID = entity.ID;
            this.Name = entity.Name;
            this.FacultyID = entity.FacultyID;
        }
    }
}
