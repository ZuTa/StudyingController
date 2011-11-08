using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using System.Windows.Input;
using StudyingController.ClientData;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.ServiceModel;

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
            loginConfig = LoginConfig.Load();
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

            ControllerInterop.Service = new SCS.ControllerServiceClient("BasicHttpBinding_IControllerService");

            if (ControllerInterop.Service.IsValidLogin(LoginConfig.Login, HashHelper.ComputeHash(LoginConfig.Password)) && SuccessfulLoginEvent != null)
            {
                LoginConfig.Save();
                SuccessfulLoginEvent(this, EventArgs.Empty);
            }
        }

        private bool CanUserLogin()
        {
            if ((LoginConfig.Login != null && new Regex("^[a-z0-9]+$").IsMatch(LoginConfig.Login))
                && (LoginConfig.Port != null && new Regex("^[0-9]+$").IsMatch(LoginConfig.Port))
                && (LoginConfig.Server != null && new Regex("^http://[a-z0-9/.]+$").IsMatch(LoginConfig.Server)))
            {
                LoginDataError = string.Empty;
                return true;
            }
            LoginDataError = StudyingController.Properties.Resources.LoginDataError; 
            return false;
        }
        #endregion

        #region Events

        public event EventHandler SuccessfulLoginEvent;

        #endregion
    }
}
