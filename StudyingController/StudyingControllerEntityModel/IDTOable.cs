using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public interface IDTOable<T> where T : BaseEntityDTO
    {
        T ToDTO();

        void UpdateData(T entity);
    }
}
