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

            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            
            mainWindow = new MainWindow();

            loginViewModel = new LoginViewModel(this, this, mainWindow.Dispatcher);

            mainViewModel = new MainViewModel(this, this, mainWindow.Dispatcher);

            mainWindow.DataContext = loginViewModel;
            
            mainWindow.Show();
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            while (ex.InnerException != null)
                ex = ex.InnerException;

            if (ex is FaultException<SCS.ControllerServiceException>)
                ShowError((ex as FaultException<SCS.ControllerServiceException>).Detail.Reason);
            else
                ShowError(ex.Message);

            e.Handled = true;
        }

        #region Methods


        #endregion

        #region Callbacks


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

        private void OnSessionChanged()
        {
            if (SessionChanged != null)
                SessionChanged(this, EventArgs.Empty);

            mainWindow.DataContext = mainViewModel;
        }

        public event EventHandler SessionChanged;

        #endregion

        #region IUserInterop

        public void ShowMessage(string text)
        {
            MessageBox.Show(text);
        }

        public void ShowError(string text)
        {
            MessageBox.Show(text, StudyingController.Properties.Resources.ErrorTxt, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

    }
}
