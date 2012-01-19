using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class InstituteModel : NamedModel, IDTOable<InstituteDTO>
    {
        public InstituteModel(InstituteDTO institute)
            : base(institute)
        {
        }

        public virtual void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);
        }

        public InstituteDTO ToDTO()
        {
            throw new NotImplementedException();
        }
    }
}
