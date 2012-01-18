using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public interface IDTOable<T> where T : BaseEntityDTO
    {

        T ToDTO();

    }
}
