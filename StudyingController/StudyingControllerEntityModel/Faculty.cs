using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Faculty : IDTOable<FacultyDTO>, IDataBase
    {
        #region Constructors

        public Faculty()
        {
        }

        public Faculty(FacultyDTO faculty)
        {
            Assign(faculty);
        }

        #endregion

        public FacultyDTO ToDTO()
        {
            FacultyDTO faculty = new FacultyDTO
            {
                ID = this.ID,
                Name = this.Name,
                InstituteID = this.InstituteID,
                Specializations = this.Specializations.ToDTOList<SpecializationDTO, Specialization>()
            };
            if (this.Institute != null)
            {
                faculty.Institute = this.Institute.ToDTO();
            }
            return faculty;            
        }

        public void Assign(FacultyDTO entity)
        {
            this.ID = entity.ID;
            this.Name = entity.Name;
            this.InstituteID = entity.InstituteID;
            this.Specializations.Update<Specialization, SpecializationDTO>(entity.Specializations);
        }
    }
}
