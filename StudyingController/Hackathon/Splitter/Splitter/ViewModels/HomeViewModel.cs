using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;

namespace Splitter.ViewModels
{
    public class HomeViewModel: LoadableViewModel
    {
        #region Fields & Properties

        private bool isUserRegistered;
        public bool IsUserRegistered
        {
            get { return isUserRegistered; }
            set { isUserRegistered = value; }
        }

        #endregion

        #region Constructors

        public HomeViewModel(IUserInterop userInterop, ISplitterInterop controllerInterop, Dispatcher dispatcher)
            :base(userInterop, controllerInterop, dispatcher)
        {
        }

        #endregion 

        #region Methods
        #endregion

        protected override void LoadData()
        {
            //throw new NotImplementedException();
        }

        protected override void ClearData()
        {
            //throw new NotImplementedException();
        }
    }
}
