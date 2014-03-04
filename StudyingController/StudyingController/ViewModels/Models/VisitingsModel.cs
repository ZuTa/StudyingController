using EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace StudyingController.ViewModels.Models
{
    public class VisitingsModel : BaseModel, IDTOable<VisitingsDTO>
    {
        #region Fields & Properties

        private StudentDTO student;

        public string Student
        {
            get { return string.Format("{0} {1}", student.UserInformation.LastName, student.UserInformation.FirstName); }
        }

        private int studentID;
        public int StudentID
        {
            get { return studentID; }
            set { studentID = value; }
        }

        private string studentName;
        public string StudentName
        {
            get { return studentName; }
            set { studentName = value; }
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

            StudentID = visitings.StudentID;
            StudentName = visitings.StudentName;
            this.visitings = new ObservableCollection<VisitingModel>(visitings.Visitings.Select(v=>new VisitingModel(v)).ToList());
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
                StudentID = this.StudentID,
                StudentName = this.StudentName,
                Visitings = this.Visitings.Select(v=>v.ToDTO()).ToList()
            };
        }
    }
}
