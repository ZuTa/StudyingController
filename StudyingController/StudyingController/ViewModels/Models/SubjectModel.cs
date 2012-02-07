using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class SubjectModel : NamedModel
    {
        #region Fields & Properties

        private CathedraDTO cathedra;
        public CathedraDTO Cathedra
        {
            get { return cathedra; }
            set 
            { 
                cathedra = value;
                OnPropertyChanged("Cathedra");
            }
        }
        #endregion

        #region Constructors

        public SubjectModel(SubjectDTO subject)
            : base(subject)
        {
            this.cathedra = subject.Cathedra;
        }

        #endregion

        #region Methods

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            SubjectDTO subject = entity as SubjectDTO;
            this.Cathedra = subject.Cathedra;
        }

        public SubjectDTO ToDTO()
        {
            return new SubjectDTO
            {
                ID = this.ID,
                Name = this.Name,
                CathedraID = this.cathedra.ID,
            };
        }
        #endregion
    }
}
