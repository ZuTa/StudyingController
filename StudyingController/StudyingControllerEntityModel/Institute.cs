using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Institute : IDTOable<InstituteDTO>
    {

        public InstituteDTO ToDTO()
        {
            InstituteDTO institute = new InstituteDTO
            {
                ID = this.ID,
                Name = this.Name
            };

            return institute;
        }
    }
}
