using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using ModelDTO;

namespace Splitter.ViewModels
{
    public abstract class BaseApplicationViewModel : BaseViewModel
    {
        #region Field & Properties

        private IUserInterop userInterop;
        protected IUserInterop UserInterop
        {
            get { return userInterop; }
        }

        private ISplitterInterop splitterInterop;
        protected ISplitterInterop SplitterInterop
        {
            get { return splitterInterop; }
        }

        private Dispatcher dispatcher;
        protected Dispatcher Dispatcher
        {
            get { return dispatcher; }
        }

        #endregion

        #region Constructors

        protected BaseApplicationViewModel(IUserInterop userInterop, ISplitterInterop controllerInterop, Dispatcher dispatcher)
            : base()
        {
            Common.Checks.AssertNotNull(dispatcher, "dispatcher");
            Common.Checks.AssertNotNull(userInterop, "userInterop");

            this.userInterop = userInterop;
            this.splitterInterop = controllerInterop;
            this.dispatcher = dispatcher;
        }

        #endregion

    }
}
