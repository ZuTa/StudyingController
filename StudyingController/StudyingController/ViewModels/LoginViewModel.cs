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
using System.Data.SqlClient;
using System.Data.EntityClient;

namespace StudyingController.ViewModels
{
    public class LoginViewModel : BaseApplicationViewModel, IPasswordConsumer
    {
        #region Fields & Properties

        private object loggingInLock = new object();

        public bool CanChange
        {
            get { return !IsLoggingIn; }
        }

        private bool isLoggingIn;
        public bool IsLoggingIn
        {
            get
            {
                return isLoggingIn;
            }
            private set
            {
                if (value != isLoggingIn)
                {
                    isLoggingIn = value;
                    OnPropertyChanged("IsLoggingIn");
                    OnPropertyChanged("CanChange");
                }
            }
        }

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

        private IPasswordSource passwordSource;
        public IPasswordSource PasswordSource
        {
            get
            {
                return passwordSource;
            }
            set
            {
                if (passwordSource != value)
                {
                    passwordSource = value;
                    if (passwordSource != null)
                        PasswordSource.SetPassword(LoginConfig.Password);
                    OnPropertyChanged("PasswordSource");
                }
            }
        }

        #endregion
        
        #region Constructors
        
        public LoginViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            LoginConfig = LoginConfig.Load();
            if (loginConfig.IsAutologin) UserLogin(); 
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

        private void StartLogging()
        {
            IsLoggingIn = true;
        }

        private void StopLogging()
        {
            IsLoggingIn = false;
        }

        private void UserLogin()
        {
            lock (loggingInLock)
            {
                if (IsLoggingIn)
                    return;

                try
                {
                    StartLogging();

                    if(!LoginConfig.IsAutologin) LoginConfig.Password = passwordSource.GetPassword();

                    ControllerInterop.Service = new SCS.ControllerServiceClient("BasicHttpBinding_IControllerService", GetServiceEndPoint());
                    this.ControllerInterop.Service.BeginLogin(LoginConfig.Login, HashHelper.ComputeHash(LoginConfig.Password), OnLoginCompleted, null);
                }
                catch (Exception ex)
                {
                    LoginDataError = ex.Message;
                    StopLogging();
                  }             
            }
        }

        private bool CanUserLogin()
        {
            if ((LoginConfig.Login != null && new Regex("^[a-z0-9]+$").IsMatch(LoginConfig.Login))
                && (LoginConfig.Port != null && new Regex("^[0-9]+$").IsMatch(LoginConfig.Port))
                && (LoginConfig.Server != null && new Regex("^http://[a-zA-Z0-9/.-]+/$").IsMatch(LoginConfig.Server)))
                return true;
            
            return false;
        }

        private EndpointAddress GetServiceEndPoint()
        {
            StringBuilder uri = new StringBuilder();
            uri.Append(LoginConfig.Server);
            uri.Insert(uri.ToString().IndexOf('/', 7), ":" + LoginConfig.Port);
            uri.Append(StudyingController.Properties.Resources.Service);
            return new EndpointAddress(uri.ToString());
        }

        #endregion

        #region Callbacks

        private void OnLoginCompleted(IAsyncResult iar)
        {
            this.Dispatcher.Invoke(new Action<IAsyncResult>((ar) =>
                {
                    try
                    {
                        ControllerInterop.Session = this.ControllerInterop.Service.EndLogin(ar);
                        if (!LoginConfig.IsMemorizeLogin) 
                        { 
                            LoginConfig.Login = string.Empty; 
                            LoginConfig.Password = string.Empty;
                            LoginConfig.IsAutologin = false;
                        }
                        LoginConfig.Save();   
                    }
                    catch (FaultException<SCS.ControllerServiceException> exc)
                    {
                        LoginDataError = exc.Detail.Reason;
                    }
                    finally
                    {
                        StopLogging();
                    }

                }), new object[] { iar });
        }

        #endregion

    }
}
