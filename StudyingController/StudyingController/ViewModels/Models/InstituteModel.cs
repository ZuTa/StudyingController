using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class InstituteModel : NamedModel
    {
        public InstituteModel(InstituteDTO institute)
            : base(institute)
        {
        }

    }
}
