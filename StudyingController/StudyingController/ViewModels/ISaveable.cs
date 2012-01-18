using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.ViewModels.Models;

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
