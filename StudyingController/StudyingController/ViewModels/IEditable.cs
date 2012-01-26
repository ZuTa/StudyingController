using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyingController.ViewModels
{
    public interface IEditable
    {

        bool IsModified { get; }

        bool IsValid { get; }

        event EventHandler ViewModified;

        event EventHandler ViewUnModified;

    }
}
