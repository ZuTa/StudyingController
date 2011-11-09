using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyingController.Common
{
    public interface IControllerInterop
    {
        SCS.ControllerServiceClient Service { get; set; }

        SCS.Session Session { get; set; }

        event EventHandler SessionChanged;
    }
}
