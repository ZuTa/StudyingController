using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.Common;

namespace StudyingController.ViewModels.Models
{
    public abstract class ControlModel : NamedModel
    {
        #region Fields & Properties

        private DateTime date;
        [Validateable]
        public DateTime Date
        {
            get { return date; }
            set 
            { 
                date = value;
                OnPropertyChanged("Date");
            }
        }

        private string description;
        [Validateable]
        public string Description
        {
            get { return description; }
            set 
            { 
                description = value;
                OnPropertyChanged("Description");
            }
        }

        private Decimal maxMark;
        [Validateable]
        public Decimal MaxMark
        {
            get { return maxMark; }
            set 
            { 
                maxMark = value;
                OnPropertyChanged("MaxMark");
            }
        }
        #endregion

        #region Constructors

        public ControlModel(ControlDTO control)
            : base(control)
        {
            this.Assign(control);   
        }

        public ControlModel()
            : base()
        {

        }
        #endregion

        #region Methods

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            ControlDTO control = entity as ControlDTO;
            Date = control.Date;
            Description = control.Description;
            MaxMark = control.MaxMark;
        }

        private bool IsDescriptionValid(out string error)
        {
            error = null;
            if (Description == null ||  Description.Length == 0)
            {
                error = Properties.Resources.ErrorFieldEmpty;
                return false;
            }
            else if (Description.Length > 300)
            {
                error = Properties.Resources.ErrorFieldGreater;
                return false;
            }

            return true;
        }

        private bool IsMarkValid(out string error)
        {
            error = null;
            if (!(MaxMark <= 100 && MaxMark > 0))
            {
                error = Properties.Resources.ErrorMark;
                return false;
            }
            return true;
        }

        private bool IsDateValid(out string error)
        {
            error = null;
            if (!(Date.Year >= 2000))
            {
                error = Properties.Resources.ErrorDate;
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
                    case "Date":
                        IsDateValid(out error);
                        break;
                    case "Description":
                        IsDescriptionValid(out error);
                        break;
                    case "MaxMark":
                        IsMarkValid(out error);
                        break;
                    default:
                        break;
                }
            }
            return error;
        }
        #endregion
    }
}
