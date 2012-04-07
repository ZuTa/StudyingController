using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class LectureControl : IDTOable<LectureControlDTO>, IDataBase
    {
        #region Constructors
        
        public LectureControl(LectureControlDTO control)
            :base(control)
        {
            Assign(control);
        }

        public LectureControl()
            : base()
        {

        }

        #endregion

        public LectureControlDTO ToDTO()
        {
            LectureControlDTO control = new LectureControlDTO()
            {
                ID = this.ID,
                Name = this.Name,
                Description = this.Description,
                Date = this.Date,
                MaxMark = this.MaxMark,
                LectureID = this.LectureID
            };

            return control;
        }

        public void Assign(LectureControlDTO entity)
        {
            base.Assign(entity);
            LectureID = entity.LectureID;
        }

    }
}
