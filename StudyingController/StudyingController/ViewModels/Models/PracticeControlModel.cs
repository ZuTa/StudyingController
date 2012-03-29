using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class PracticeControlModel : ControlModel, IDTOable<PracticeControlDTO>
    {
        #region Fields & Properties

        private int practiceID;
        [Validateable]
        public int PracticeID
        {
            get { return practiceID; }
            set { practiceID = value; }
        }

        #endregion

        #region Constructors

        public PracticeControlModel(PracticeControlDTO control)
            : base(control)
        {
            this.Assign(control);
        }

        public PracticeControlModel()
            : base()
        {

        }

        #endregion

        #region Methods

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);
            PracticeID = (entity as PracticeControlDTO).PracticeID;
        }

        public PracticeControlDTO ToDTO()
        {
            return new PracticeControlDTO()
            {
                ID = this.ID,
                Name = this.Name,
                Date = this.Date,
                Description = this.Description,
                MaxMark = this.MaxMark,
                PracticeID = this.PracticeID
            };
        }

        #endregion
    }
}
