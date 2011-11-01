using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public abstract class BaseApplicationViewModel : BaseViewModel
    {
        #region Field & Properties

        private IUserInterop userInterop;
        protected IUserInterop UserInterop
        {
            get { return userInterop; }
        }

        private IControllerInterop controllerInterop;
        protected IControllerInterop ControllerInterop
        {
            get { return controllerInterop; }
        }

        private Dispatcher dispatcher;
        protected Dispatcher Dispatcher
        {
            get { return dispatcher; }
        }

        #endregion

        #region Constructors

        protected BaseApplicationViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base()
        {
            Common.Checks.AssertNotNull(dispatcher, "dispatcher");
            Common.Checks.AssertNotNull(userInterop, "userInterop");

            this.userInterop = userInterop;
            this.controllerInterop = controllerInterop;
            this.dispatcher = dispatcher;
        }

        #endregion

    }
}
