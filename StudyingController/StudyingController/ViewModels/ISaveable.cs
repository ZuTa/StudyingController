using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyingController.ViewModels
{
    public interface ISaveable 
    {
        void Save();

        bool IsModified { get; set; }

        bool IsReadOnly { get; set; }

        bool CanModify { get; }

        bool CanSave { get; }



    }
}
