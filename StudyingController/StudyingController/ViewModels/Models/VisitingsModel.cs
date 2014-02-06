using EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyingController.ViewModels.Models
{
    public abstract class VisitingsModel : BaseModel
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

        private List<VisitingDTO> visitings;

        public List<VisitingDTO> Visitings
        {
            get { return visitings; }
            set { visitings = value; }
        }
        
        #endregion

        #region Constructors

        public VisitingsModel(VisitingsDTO visitings)
            : base(visitings)
        {
            this.Assign(visitings);   
        }

        public VisitingsModel()
            : base()
        {

        }

        #endregion

        #region Methods

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            VisitingsDTO visitings = (entity as VisitingsDTO);

            StudentID = visitings.StudentID;
            this.visitings = visitings.Visitings;
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
    }
}
