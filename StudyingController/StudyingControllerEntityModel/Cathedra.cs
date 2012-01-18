using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Cathedra : IDTOable<CathedraDTO>
    {
        #region Constructors

        public Cathedra()
        {
        }

        public Cathedra(CathedraDTO cathedra)
        {
            this.ID = cathedra.ID;
            UpdateData(cathedra);
        }

        #endregion

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


        public void UpdateData(CathedraDTO entity)
        {
            this.Name = entity.Name;
            this.FacultyID = entity.FacultyID;
        }
    }
}
