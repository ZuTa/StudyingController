using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class LectureControl
    {
        #region Constructors
        
        public LectureControl(int lectureID, ControlDTO control)
            :base(control)
        {
            LectureID = lectureID;
        }

        public LectureControl()
            : base()
        {
        }

        #endregion
    }
}
