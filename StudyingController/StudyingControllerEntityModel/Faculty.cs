using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Faculty : IDTOable<FacultyDTO>
    {
        #region Constructors

        public Faculty()
        {
        }

        public Faculty(FacultyDTO faculty)
        {
            this.ID = faculty.ID;
            UpdateData(faculty);
        }

        #endregion

        public FacultyDTO ToDTO()
        {
            FacultyDTO faculty = new FacultyDTO
            {
                ID = this.ID,
                Name = this.Name,
                InstituteID = this.InstituteID
            };

            return faculty;
        }

        public void UpdateData(FacultyDTO entity)
        {
            this.Name = entity.Name;
            this.InstituteID = entity.InstituteID;
        }
    }
}
