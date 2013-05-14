using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class SubjectModel : NamedModel, IDTOable<SubjectDTO>
    {
        #region Fields & Properties

        private int cathedraID;
        public int CathedraID
        {
            get { return cathedraID; }
            set
            {
                if (cathedraID != value)
                {
                    cathedraID = value;
                    OnPropertyChanged("CathedraID");
                }
            }
        }

        private int rate;
        public int Rate
        {
            get { return rate; }
            set
            {
                if (rate != value)
                {
                    rate = value;
                    OnPropertyChanged("Rate");
                }
            }
        }

        #endregion

        #region Constructors

        public SubjectModel()
        {

        }

        public SubjectModel(SubjectDTO subject)
            : base(subject)
        {
            this.cathedraID = subject.Cathedra.ID;
        }

        #endregion

        #region Methods

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            SubjectDTO subject = entity as SubjectDTO;
            this.CathedraID = subject.CathedraID;
            this.Rate = subject.Rate;
        }

        public SubjectDTO ToDTO()
        {
            return new SubjectDTO
            {
                ID = this.ID,
                Name = this.Name,
                CathedraID = this.CathedraID,
                Rate = this.Rate,
            };
        }

        protected override string Validate(string property)
        {
            //TODO: Add validation
            return base.Validate(property);
        }
        #endregion
    }
}
