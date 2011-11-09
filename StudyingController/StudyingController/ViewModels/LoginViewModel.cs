using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using System.Windows.Input;
using StudyingController.UserData;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.ServiceModel;

namespace StudyingController.ViewModels
{
    public class LoginViewModel : BaseApplicationViewModel, IPasswordConsumer
    {
        #region Fields & Properties

        private object loggingInLock = new object();

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
                    OnPropertyChanged("PasswordSource");
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
                    loginCommand = new RelayCommand((param) => UserLogin(param), (param) => CanUserLogin()); 

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

        private void UserLogin(object parameter)
        {
            lock (loggingInLock)
            {
                if (IsLoggingIn)
                    return;

                try
                {
                    StartLogging();

                    LoginConfig.Password = HashHelper.ComputeHash(passwordSource.GetPassword());

                    ControllerInterop.Service = new SCS.ControllerServiceClient("BasicHttpBinding_IControllerService");
                    this.ControllerInterop.Service.BeginLogin(LoginConfig.Login, LoginConfig.Password, OnLoginCompleted, null);
                    //if (this.ControllerInterop.Service.IsValidLogin(LoginConfig.Login, LoginConfig.Password) && SuccessfulLoginEvent != null)
                    //    SuccessfulLoginEvent(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    LoginDataError = ex.Message;
                }
                finally
                {
                    StopLogging();
                }
            }
        }

        private bool CanUserLogin()
        {
            if (!IsLoggingIn &&
                (LoginConfig.Login != null && new Regex("^[a-z0-9]+$").IsMatch(LoginConfig.Login))
                && (LoginConfig.Port != null && new Regex("^[0-9]+$").IsMatch(LoginConfig.Port))
                && (LoginConfig.Server != null && new Regex("^http://[a-z0-9/]+$").IsMatch(LoginConfig.Server)))
                return true;
            
            return false;
        }
        #endregion

        #region Callbacks

        private void OnLoginCompleted(IAsyncResult iar)
        {
            this.Dispatcher.Invoke(new Action<IAsyncResult>((ar) =>
                {
                    try
                    {
                        if (this.ControllerInterop.Service.EndLogin(ar) && SuccessfulLoginEvent != null)
                            SuccessfulLoginEvent(this, EventArgs.Empty);
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

        #region Events

        public event EventHandler SuccessfulLoginEvent;

        #endregion

    }
}
