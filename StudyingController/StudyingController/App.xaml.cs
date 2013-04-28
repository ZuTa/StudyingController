using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using StudyingController.Common;
using StudyingController.ViewModels;
using System.ServiceModel;
using StudyingController.ClientData;
using System.Threading;
using System.Globalization;

namespace StudyingController
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IUserInterop, IControllerInterop
    {
        #region Properties

        private LoginViewModel loginViewModel;
        private MainViewModel mainViewModel;
        private MainWindow mainWindow;
        
        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US"); 
            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            
            mainWindow = new MainWindow();

            loginViewModel = new LoginViewModel(this, this, mainWindow.Dispatcher);
            loginViewModel.Load();

            mainWindow.DataContext = loginViewModel;
            
            mainWindow.Show();
        }

        #region Methods

        private void ShowMainView()
        {
            if (mainViewModel != null)
                mainViewModel.Logout -= mainViewModel_Logout;

            mainViewModel = new MainViewModel(this, this, mainWindow.Dispatcher);
            mainViewModel.Logout += new EventHandler(mainViewModel_Logout);

            mainViewModel.Load();

            mainWindow.DataContext = mainViewModel;
        }

        private void Logout()
        {
            Session = null;
            mainWindow.DataContext = loginViewModel;
        }

        #endregion

        #region Callbacks

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            while (ex.InnerException != null)
                ex = ex.InnerException;

            if (ex is FaultException<SCS.ControllerServiceException>)
                ShowMessage((ex as FaultException<SCS.ControllerServiceException>).Detail.Reason);
            else
                ShowMessage(ex.Message);

            e.Handled = true;

            Logout();
        }

        private void mainViewModel_Logout(object sender, EventArgs e)
        {
            Logout();
        }

        #endregion

        #region IControllerInterop

        private SCS.ControllerServiceClient service = null;
        public SCS.ControllerServiceClient Service
        {
            get
            {
                return service;
            }
            set
            {
                service = value;
            }
        }

        private SCS.Session session;
        public SCS.Session Session
        {
            get
            {
                return session;
            }
            set
            {
                if (session != value)
                {
                    session = value;
                    OnSessionChanged();
                }
            }
        }

        public EntitiesDTO.SystemUserDTO User
        {
            get 
            {
                return (Session == null) ? null : Session.User;
            }
        }

        public EntitiesDTO.StudyRangeDTO StudyRange
        {
            get
            {
                return (Session == null) ? null : Session.StudyRange;
            }
        }

        private void OnSessionChanged()
        {
            if (SessionChanged != null)
                SessionChanged(this, EventArgs.Empty);

            if (Session != null)
                ShowMainView();
        }

        public event EventHandler SessionChanged;

        #endregion

        #region IUserInterop

        public void ShowMessage(string text)
        {
            MessageBox.Show(text);
        }

        public MessageResults ShowMessage(string message, string caption, MessageButtons buttons, MessageTypes type)
        {
            MessageBoxImage image = MessageBoxImage.None;

            switch (type)
            {
                case MessageTypes.Error:
                    image = MessageBoxImage.Error;
                    break;
                case MessageTypes.Information:
                    image = MessageBoxImage.Information;
                    break;
                case MessageTypes.Question:
                    image = MessageBoxImage.Question;
                    break;
                default:
                    throw new NotImplementedException();
            }

            MessageBoxButton button = MessageBoxButton.OK;

            switch (buttons)
            {
                case MessageButtons.OK:
                    button = MessageBoxButton.OK;
                    break;
                case MessageButtons.YesNo:
                    button = MessageBoxButton.YesNo;
                    break;
                case MessageButtons.OKCancel:
                    button = MessageBoxButton.OKCancel;
                    break;
                case MessageButtons.YesNoCancel:
                    button = MessageBoxButton.YesNoCancel;
                    break;
                default:
                    throw new NotImplementedException();
            }

            switch (MessageBox.Show(message, caption, button, image))
            {
                case MessageBoxResult.OK:
                    return MessageResults.OK;
                case MessageBoxResult.Cancel:
                    return MessageResults.Cancel;
                case MessageBoxResult.Yes:
                    return MessageResults.Yes;
                case MessageBoxResult.No:
                    return MessageResults.No;
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion
    }
}
