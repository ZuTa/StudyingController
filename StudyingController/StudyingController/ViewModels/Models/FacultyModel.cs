using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class FacultyModel : NamedModel
    {
        #region Fields & Properties

        private InstituteDTO institute;
        [Validateable]
        public InstituteDTO Institute
        {
            get { return institute; }
            set 
            { 
                institute = value;
                OnPropertyChanged("Institute");
            }
        }

        #endregion

        public FacultyModel(FacultyDTO faculty)
            : base(faculty)
        {
            institute = faculty.Institute;
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            FacultyDTO faculty = entity as FacultyDTO;
            this.Institute = faculty.Institute;
        }

        public FacultyDTO ToDTO()
        {
            return new FacultyDTO
            {
                ID = this.ID,
                Name = this.Name,
                InstituteID = Institute == null ? (int?)null : Institute.ID
            };
        }

        protected override string Validate(string property)
        {
            return base.Validate(property);
        }
    }
}
