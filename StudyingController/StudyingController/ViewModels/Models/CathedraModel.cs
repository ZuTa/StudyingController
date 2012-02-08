using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.Common;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels.Models
{
    public class CathedraModel : NamedModel, IDTOable<CathedraDTO>
    {
        #region Fields & Properties

        private FacultyDTO faculty;
        [Validateable]
        public FacultyDTO Faculty
        {
            get { return faculty; }
            set 
            { 
                faculty = value;
                OnPropertyChanged("Faculty");
            }
        }

        private ObservableCollection<SubjectModel> subjects;
        public ObservableCollection<SubjectModel> Subjects
        {
            get { return subjects; }
            set
            {
                if (subjects != value)
                {
                    subjects = value;
                    OnPropertyChanged("Subjects");
                }
            }
        }
        #endregion



        #region Constructors
        
        public CathedraModel(CathedraDTO cathedra)
            : base(cathedra)
        {
            this.faculty = cathedra.Faculty;
            subjects = cathedra.Subjects.ToModelList<SubjectModel, SubjectDTO>();
            subjects.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(subjects_CollectionChanged);
            foreach(SubjectModel model in subjects)
                model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(model_PropertyChanged);
        }

        #endregion

        #region Methods

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            CathedraDTO cathedra = entity as CathedraDTO;
            this.Faculty = cathedra.Faculty;
        }

        public CathedraDTO ToDTO()
        {
            if (faculty == null)
                throw new Exception("Need UI validation!");

            return new CathedraDTO
            {
                ID = this.ID,
                Name = this.Name,
                FacultyID = Faculty.ID,
                Subjects = this.Subjects.ToDTOList<SubjectDTO, SubjectModel>()
            };
        }

        private bool IsFacultyValid(out string error)
        {
            error = null;
            if (faculty == null)
            {
                error = Properties.Resources.ErrorStructureNotFound;
                return false;
            }
            return true;
        }

        protected override string Validate(string property)
        {
            string error = base.Validate(property);
            if (error == null)
            {
                switch (property)
                {
                    case "Faculty":
                        IsFacultyValid(out error);
                        break;
                    default:
                        break;
                }
            }
            return error;
        }

        #endregion

        #region Callbacks

        void model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Subjects");
        }

        void subjects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Subjects");
        }

        #endregion
    }
}
