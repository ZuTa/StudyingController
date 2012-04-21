using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelDTO;

namespace EntityModel
{
    public interface IDTOable<T> where T : BaseDTO
    {
        T ToDTO();

        void Assign(T entity);
    }
}
