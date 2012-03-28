using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class PracticeControl
    {
        #region Constructors

        public PracticeControl(int practiceID, ControlDTO control)
            : base(control)
        {
            PracticeID = practiceID;
        }


        public PracticeControl()
            : base()
        {
        }

        #endregion

    }
}
