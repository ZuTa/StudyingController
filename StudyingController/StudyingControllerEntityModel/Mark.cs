using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public abstract partial class Mark : IDataBase
    {
        #region Constructors

        public Mark()
        {
        }

        public Mark(MarkDTO mark)
        {
            Assign(mark);
        }

        #endregion

        public void Assign(MarkDTO mark)
        {
            ID = mark.ID;
            MarkValue = mark.MarkValue;
            StudentID = mark.StudentID;
        }
    }
}
