using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyingController.ViewModels
{
    public enum EditModes
    {
        ReadOnly,
        Editable
    }

    public interface IEditable
    {
        EditModes EditMode { get; set; }

        bool IsModified { get; }

        bool CanSave { get; }

        void Save();

        void Rollback();

        event EventHandler ViewModified;

        event EventHandler ViewUnModified;
    }
}
