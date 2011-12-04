using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Cathedra : IDTOable<CathedraDTO>
    {
        public CathedraDTO ToDTO()
        {
            CathedraDTO cathedra = new CathedraDTO
            {
                ID = this.ID,
                Name = this.Name,
                FacultyID = this.FacultyID
            };

            return cathedra;
        }
    }
}
