using EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace StudyingController.ViewModels.Models
{
    public class VisitingsModel : BaseModel, IDTOable<VisitingsDTO>
    {
        #region Fields & Properties

        private StudentRef student;
        public StudentRef Student
        {
            get { return student; }
            set { student = value; }
        }

        private LectureRef lecture;
        public LectureRef Lecture
        {
            get { return lecture; }
            set { lecture = value; }
        }

        private PracticeRef practice;
        public PracticeRef Practice
        {
            get { return practice; }
            set { practice = value; }
        }

        private ObservableCollection<VisitingModel> visitings;
        public ObservableCollection<VisitingModel> Visitings
        {
            get { return visitings; }
            set 
            { 
                visitings = value;
            }
        }
        
        #endregion

        #region Constructors

        public VisitingsModel(VisitingsDTO visitings)
            : base(visitings)
        {
            Visitings = new ObservableCollection<VisitingModel>();
            this.Assign(visitings);   
        }

        public VisitingsModel()
            : base()
        {
            Visitings = new ObservableCollection<VisitingModel>();
        }

        #endregion

        #region Methods

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            VisitingsDTO visitings = (entity as VisitingsDTO);

            Student = visitings.Student;

            Lecture = visitings.Lecture;

            Practice = visitings.Practice;

            if (this.visitings != null)
            {
                this.visitings.CollectionChanged -= visitings_CollectionChanged;
                foreach (var visiting in this.visitings)
                {
                    visiting.PropertyChanged -= visiting_PropertyChanged;
                }
            }

            this.visitings = new ObservableCollection<VisitingModel>(visitings.Visitings.OrderBy(v=>v.Date).Select(v=>new VisitingModel(v)).ToList());

            this.visitings.CollectionChanged += visitings_CollectionChanged;

            foreach (var visiting in this.visitings)
            {
                visiting.PropertyChanged += visiting_PropertyChanged;
            }
        }

        private void visiting_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnModelChanged();
        }

        private void visitings_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Visitings");
        }

        private void OnModelChanged()
        {
            if (ModelChanged != null)
                ModelChanged(this, null);
        }
        #endregion

        #region Event

        public event EventHandler ModelChanged;

        #endregion

        public VisitingsDTO ToDTO()
        {
            return new VisitingsDTO
            {
                ID = this.ID,
                Student = this.Student,
                Lecture = this.Lecture,
                Practice = this.Practice,
                Visitings = this.Visitings.Select(v=>v.ToDTO()).ToList()
            };
        }
    }
}
