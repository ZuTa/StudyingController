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

        public bool IsUserRegistered
        {
            get 
            {
                return RegistrationViewModel.IsLoggedIn; 
            }
        }

        public bool IsUserNotRegistered
        {
            get
            {
                return !IsUserRegistered;
            }
        }

        private RegistrationViewModel registrationViewModel;
        public RegistrationViewModel RegistrationViewModel
        {
            get { return registrationViewModel; }
            set { registrationViewModel = value; }
        }

        #endregion

        #region Constructors

        public HomeViewModel(IUserInterop userInterop, ISplitterInterop splitterInterop, Dispatcher dispatcher)
            :base(userInterop, splitterInterop, dispatcher)
        {
            registrationViewModel = new RegistrationViewModel(userInterop, splitterInterop, dispatcher);
        }

        #endregion 

        #region Methods
        #endregion

        protected override void LoadData()
        {
            
        }

        protected override void ClearData()
        {
            //throw new NotImplementedException();
        }
    }
}
