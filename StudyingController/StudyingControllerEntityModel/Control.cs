using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public abstract partial class Control : IDataBase
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
