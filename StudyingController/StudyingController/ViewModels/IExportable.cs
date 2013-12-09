using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyingController.ViewModels
{
    public interface IExportable
    {
        bool AllowExportToExcel { get; }

        void ExportToExcel();
    }
}
