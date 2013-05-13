using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Subject : IDTOable<SubjectDTO>, IDataBase
    {
        #region Constructors

        public Subject()
        {

        }

        public Subject(SubjectDTO subject)
        {
            Assign(subject);
        }

        #endregion

        #region Methods

        public SubjectDTO ToDTO()
        {
            return new SubjectDTO
            {
                ID = this.ID,
                Name = this.Name,
                CathedraID = this.CathedraID,
                Rate = this.Rate
            };
        }
        public void Assign(SubjectDTO entity)
        {
            this.ID = entity.ID;
            this.Name = entity.Name;
            this.CathedraID = entity.CathedraID;
            this.Rate = entity.Rate;
        }


        #endregion

    }
}
