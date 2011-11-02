﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public class LoginViewModel : BaseApplicationViewModel
    {

        public LoginViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            //TODO: this is connetion to a service. replace it
            //controllerInterop.Service = new SCS.ControllerServiceClient("BasicHttpBinding_IControllerService");
        }
    }
}
