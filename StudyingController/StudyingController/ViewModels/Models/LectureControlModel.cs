using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.Common;

namespace StudyingController.ViewModels.Models
{
    public class LectureControlModel : ControlModel, IDTOable<LectureControlDTO>
    {
        #region Fields & Properties

        private int lectureID;
        [Validateable]
        public int LectureID
        {
            get { return lectureID; }
            set { lectureID = value; }
        }

        #endregion

        #region Constructors

        public LectureControlModel(LectureControlDTO control)
            : base(control)
        {
            this.Assign(control);
        }

        public LectureControlModel()
            : base()
        {

        }

        #endregion

        #region Methods
        
        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);
            LectureID = (entity as LectureControlDTO).LectureID;
        }

        public LectureControlDTO ToDTO()
        {
            return new LectureControlDTO()
            {
                ID = this.ID,
                Name = this.Name,
                Date = this.Date,
                Description = this.Description,
                MaxMark = this.MaxMark,
                LectureID = this.LectureID
            };
        }

        #endregion
    }
}
