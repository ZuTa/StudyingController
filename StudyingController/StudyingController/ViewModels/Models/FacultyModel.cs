using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.Common;
using System.Collections.ObjectModel;

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
                if (institute != value)
                {
                    institute = value;
                    OnPropertyChanged("Institute");
                }
            }
        }

        private ObservableCollection<SpecializationModel> specializations;
        public ObservableCollection<SpecializationModel> Specializations
        {
            get { return specializations; }
            set 
            {
                if (specializations != value)
                {
                    specializations = value;
                    OnPropertyChanged("Specializations");
                }
            }
        }

        #endregion

        public FacultyModel(FacultyDTO faculty)
            : base(faculty)
        {
            institute = faculty.Institute;
            specializations = faculty.Specializations.ToModelList<SpecializationModel, SpecializationDTO>();
            specializations.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(specializations_CollectionChanged);
            foreach(SpecializationModel model in specializations)
                model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(model_PropertyChanged);
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            FacultyDTO faculty = entity as FacultyDTO;
            this.Institute = faculty.Institute;
            this.specializations = faculty.Specializations.ToModelList<SpecializationModel, SpecializationDTO>();
        }

        public FacultyDTO ToDTO()
        {
            return new FacultyDTO
            {
                ID = this.ID,
                Name = this.Name,
                InstituteID = Institute == null ? (int?)null : Institute.ID,
                Specializations = this.specializations.ToDTOList<SpecializationDTO, SpecializationModel>()
            };
        }

        protected override string Validate(string property)
        {
            return base.Validate(property);
        }

        #region Callbacks

        private void model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Specializations");
        }

        private void specializations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Specializations");
        }


        #endregion
    }
}
