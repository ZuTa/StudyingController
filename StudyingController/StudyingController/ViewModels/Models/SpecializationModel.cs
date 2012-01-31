using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class SpecializationModel : NamedModel, IDTOable<SpecializationDTO>
    {
        #region Fields & Properties

        private int facultyID;
        public int FacultyID
        {
            get { return facultyID; }
            set
            {
                if (facultyID != value)
                {
                    facultyID = value;
                    OnPropertyChanged("FacultyID");
                }
            }
        }

        #endregion

        #region Constructors

        public SpecializationModel()
        {
        }

        public SpecializationModel(SpecializationDTO entity)
            :base(entity)
        {
        }

        #endregion

        #region Methods

        public SpecializationDTO ToDTO()
        {
            return new SpecializationDTO
            {
                ID = this.ID,
                Name = this.Name,
                FacultyID = this.FacultyID
            };
        }

        public override void Assign(EntitiesDTO.BaseEntityDTO entity)
        {
            base.Assign(entity);

            SpecializationDTO specialization = entity as SpecializationDTO;
            this.FacultyID = specialization.FacultyID;
        }

        #endregion
    }
}
