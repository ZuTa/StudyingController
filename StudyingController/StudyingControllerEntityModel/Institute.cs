using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Institute : IDTOable<InstituteDTO>, IDataBase
    {
        #region Constructors

        public Institute()
        {
        }

        public Institute(InstituteDTO institute)
        {
            Assign(institute);
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

        public void Assign(InstituteDTO institute)
        {
            this.ID = institute.ID;
            this.Name = institute.Name;
        }
    }
}
