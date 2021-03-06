﻿using System;
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
using System.Threading.Tasks;

namespace StudyingController.ViewModels
{
    public class LoginViewModel : LoadableViewModel, IPasswordConsumer
    {
        #region Fields & Properties

        private object lockObject = new object();

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
                    if (passwordSource != null && LoginConfig!=null)
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
        }

        #endregion

        #region Commands

        private RelayCommand loginCommand;
        public ICommand LoginCommand
        {
            get 
            {
                if (loginCommand == null)
                    loginCommand = new RelayCommand(
                        (param) =>
                        {
                            UserLogin();
                        },
                        (param) => CanUserLogin());

                return loginCommand;
            }
        }

        #endregion

        #region Methods

        protected override object LoadDataFromServer()
        {
            return null;
        }

        protected override void AfterDataLoaded()
        {
            base.AfterDataLoaded();

            LoginConfig = LoginConfig.Load();

            if (loginConfig.IsAutologin)
                UserLogin();
        }

        protected override void ClearData()
        {
            LoginConfig = null;
        }

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
            lock (lockObject)
            {
                if (isLoggingIn)
                    return;

                try
                {
                    Task.Factory.StartNew(() => StartLogging());

                    if (passwordSource != null)
                        LoginConfig.Password = passwordSource.GetPassword();

                    Task.Factory.StartNew(() =>
                        {
                            ControllerInterop.Service = new SCS.ControllerServiceClient("BasicHttpBinding_IControllerService", GetServiceEndPoint());

                            this.ControllerInterop.Service.BeginLogin(LoginConfig.Login, HashHelper.ComputeHash(LoginConfig.Password), OnLoginCompleted, null);
                        });
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
            return LoginConfig == null ? false : LoginConfig.IsValid;
        }

        private EndpointAddress GetServiceEndPoint()
        {
            StringBuilder uri = new StringBuilder();
            uri.Append(LoginConfig.Server.IndexOf("http://") == -1 ? "http://" + LoginConfig.Server : LoginConfig.Server);
            
            if (uri.ToString().IndexOf('/', 7) == -1) uri.Append("/");
            
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
