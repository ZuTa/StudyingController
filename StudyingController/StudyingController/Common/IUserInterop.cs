using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyingController.Common
{
    public interface IUserInterop
    {
        void ShowMessage(string text);

        void ShowError(string text);
    }
}
