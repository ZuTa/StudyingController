using EntitiesDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace StudyingController.ViewModels.Models
{
    public class VisitingModel : BaseModel, IDTOable<VisitingDTO>
    {
        #region Fields & Properties

        private StudentRef student;
        [Browsable(false)]
        public StudentRef Student
        {
            get { return student; }
            set { student = value; }
        }

        private LectureRef lecture;
        [Browsable(false)]
        public LectureRef Lecture
        {
            get { return lecture; }
            set { lecture = value; }
        }

        private PracticeRef practice;
        [Browsable(false)]
        public PracticeRef Practice
        {
            get { return practice; }
            set { practice = value; }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        private VisitingValue value;
        public VisitingValue Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        #endregion

        #region Constructors

        public VisitingModel(VisitingDTO visiting)
            : base(visiting)
        {
            this.Assign(visiting);
        }

        public VisitingModel()
            : base()
        {
        }

        #endregion

        #region Methods

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            VisitingDTO visiting = (entity as VisitingDTO);
            this.Date = visiting.Date;
            this.Description = visiting.Description;
            this.Lecture = visiting.Lecture;
            this.Practice = visiting.Practice;
            this.Student = visiting.Student;
            this.Value = visiting.Value;
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

        public VisitingDTO ToDTO()
        {
            return new VisitingDTO
            {
                ID = this.ID,
                Date = this.Date,
                Description = this.Description,
                Lecture = this.Lecture,
                Practice = this.Practice,
                Student = this.Student,
                Value = this.Value
            };
        }
    }
}
