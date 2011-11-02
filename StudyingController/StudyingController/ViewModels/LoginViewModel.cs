using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using System.Windows.Input;
using StudyingController.UserData;
using System.Windows.Controls;

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

        private string loginDataError;
        public string LoginDataError
        {
            get { return loginDataError; }
            set
            {
                loginDataError = value;
                OnPropertyChanged("LoginDataError");
            }
        }

        #endregion
        
        #region Constructors
        
        public LoginViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            loginConfig = new UserData.LoginConfig();
            //TODO: this is connetion to a service. replace it
            //controllerInterop.Service = new SCS.ControllerServiceClient("BasicHttpBinding_IControllerService");
            //controllerInterop.Service = new SCS.ControllerServiceClient("BasicHttpBinding_IControllerService", new System.ServiceModel.EndpointAddress(uri));
        }

        #endregion

        #region Commands

        private RelayCommand loginCommand;
        public ICommand LoginCommand
        {
            get 
            {
                if (loginCommand == null)
                    loginCommand = new RelayCommand((param) => UserLogin(param), (param) => CanUserLogin()); 

                return loginCommand;
            }
        }

        #endregion

        #region Methods

        private void UserLogin(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;
            LoginConfig.Password = password;
        }

        private bool CanUserLogin()
        {
            return true;
        }
        #endregion

        #region Events

        public event EventHandler SuccessfulLoginEvent;

        #endregion
    }
}
