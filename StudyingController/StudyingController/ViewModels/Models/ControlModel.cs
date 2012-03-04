using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.Common;

namespace StudyingController.ViewModels.Models
{
    public class ControlModel : NamedModel, IDTOable<ControlDTO>
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

        public ControlDTO ToDTO()
        {
            return new ControlDTO() 
            {
                ID = this.ID,
                Name = this.Name,
                Date = this.Date,
                Description = this.Description,
                MaxMark = this.MaxMark
            };
        }

        protected override string Validate(string property)
        {
            string error = base.Validate(property);
            if (error == null)
            {
                switch (property)
                {
                    case "Date":
                        break;
                    case "Description":
                        break;
                    case "MaxMark":
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
