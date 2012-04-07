using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class PracticeControl : IDTOable<PracticeControlDTO>, IDataBase
    {
        #region Constructors

        public PracticeControl(PracticeControlDTO control)
            : base(control)
        {
            Assign(control);
        }

        public PracticeControl()
            : base()
        {
        }

        #endregion

        public PracticeControlDTO ToDTO()
        {
            PracticeControlDTO control = new PracticeControlDTO()
            {
                ID = this.ID,
                Name = this.Name,
                Description = this.Description,
                Date = this.Date,
                MaxMark = this.MaxMark,
                PracticeID = this.PracticeID
            };

            return control;
        }

        public void Assign(PracticeControlDTO entity)
        {
            base.Assign(entity);
            PracticeID = entity.PracticeID;
        }

    }
}
