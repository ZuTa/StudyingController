using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Cathedra : IDTOable<CathedraDTO>, IDataBase
    {
        #region Constructors

        public Cathedra()
        {
        }

        public Cathedra(CathedraDTO cathedra)
        {
            Assign(cathedra);
        }

        #endregion

        #region Methods

        public CathedraDTO ToDTO()
        {
            CathedraDTO cathedra = new CathedraDTO
            {
                ID = this.ID,
                Name = this.Name,
                FacultyID = this.FacultyID,
                Subjects = this.Subjects.ToDTOList<SubjectDTO, Subject>()
            };

            if (this.Faculty != null)
                cathedra.Faculty = this.Faculty.ToDTO();

            return cathedra;
        }


        public void Assign(CathedraDTO entity)
        {
            this.ID = entity.ID;
            this.Name = entity.Name;
            this.FacultyID = entity.FacultyID;
            this.Subjects.Update<Subject, SubjectDTO>(entity.Subjects);
        }

        #endregion
    }
}
