﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyingController.Common
{
    public interface IControllerInterop
    {
        SCS.ControllerServiceClient Service { get; set; }
    }
}