using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using System.Windows.Input;
using StudyingController.UserData;

namespace StudyingController.ViewModels
{
    public class LoginViewModel : BaseApplicationViewModel
    {
        #region Fields & Properties

        private LoginConfig loginConfig;
        public LoginConfig LoginConfig
        {
            get { return loginConfig; }
            set 
            {
                if (loginConfig != value)
                {
                    loginConfig = value;
                    OnPropertyChanged("LoginConfig");
                }
            }
        }

        #endregion
        
        #region Constructors
        
        public LoginViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            loginConfig = new UserData.LoginConfig();
        }

        #endregion

        #region Commands

        private RelayCommand loginCommand;
        public ICommand LoginCommand
        {
            get 
            {
                if (loginCommand == null)
                    loginCommand = new RelayCommand((param) => UserLogin(), (param) => CanUserLogin()); 

                return loginCommand;
            }
        }

        #endregion

        #region Methods

        private void UserLogin()
        {
 
        }

        private bool CanUserLogin()
        {
            return true;
        }
        #endregion

        #region Events

        public event EventHandler LoginEvent;

        #endregion
    }
}
