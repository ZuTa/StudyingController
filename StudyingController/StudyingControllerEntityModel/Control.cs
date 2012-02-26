using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Control : IDTOable<ControlDTO>, IDataBase
    {
        #region Constructors

        public Control()
        {
        }

        public Control(ControlDTO control)
        {
            Assign(control);
        }

        #endregion

        public ControlDTO ToDTO()
        {
            ControlDTO control = new ControlDTO()
            {
                ID = this.ID,
                Name = this.Name,
                Description = this.Description,
                Date = this.Date,
                MaxMark = this.MaxMark
            };

            return control;
        }

        public void Assign(ControlDTO entity)
        {
            ID = entity.ID;
            Name = entity.Name;
            Description = entity.Description;
            Date = entity.Date;
            MaxMark = entity.MaxMark;
        }
    }
}
