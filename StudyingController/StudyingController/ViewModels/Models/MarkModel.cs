using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.Common;
using System.Text.RegularExpressions;

namespace StudyingController.ViewModels.Models
{
    public abstract class MarkModel : BaseModel
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

        private decimal markValue;
        public decimal MarkValue
        {
            get { return markValue; }
            set 
            {
                if (markValue != value)
                {
                    markValue = value;
                    OnPropertyChanged("MarkValue");
                    
                }
            }
        }

        private string mark;
        [Validateable]
        public string Mark
        {
            get { return mark != null ? mark : mark = MarkValue.ToString(); }
            set
            {
                
                if (mark==null || !mark.Equals(value))
                {
                    string error;
                    mark = value;
                    if(IsMarkValid(out error))
                    {
                        MarkValue = decimal.Parse(mark);
                    }
                    OnModelChanged();
                }
            }
        }

        private string description;
        [Validateable]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        #endregion

        #region Constructors

        public MarkModel(MarkDTO mark)
            : base(mark)
        {
            this.Assign(mark);   
        }

        public MarkModel()
            : base()
        {

        }

        #endregion

        #region Methods

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            MarkDTO mark = (entity as MarkDTO);

            studentID = mark.StudentID;
            student = mark.Student;
            markValue = mark.MarkValue;
        }

        protected virtual bool IsMarkValid(out string error)
        {
            error = null;
            decimal dec;
            if (!decimal.TryParse(mark, out dec))
            {
                error = Properties.Resources.ErrorLoginBadChars;
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
                    case "Mark":
                        IsMarkValid(out error);
                        break;
                    default:
                        break;
                }
            }
            return error;
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
