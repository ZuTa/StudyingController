using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.Common
{
    public interface IControllerInterop
    {
        SCS.ControllerServiceClient Service { get; set; }

        SCS.Session Session { get; set; }

        SystemUserDTO User { get; }

        event EventHandler SessionChanged;
    }
}
