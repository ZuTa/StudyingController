using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Institute : IDTOable<InstituteDTO>
    {
        #region Constructors

        public Institute()
        {
        }

        public Institute(InstituteDTO institute)
        {
            this.ID = institute.ID;
            UpdateData(institute);
        }

        #endregion

        public InstituteDTO ToDTO()
        {
            InstituteDTO institute = new InstituteDTO
            {
                ID = this.ID,
                Name = this.Name
            };

            return institute;
        }

        public void UpdateData(InstituteDTO institute)
        {
            this.Name = institute.Name;
        }
    }
}
