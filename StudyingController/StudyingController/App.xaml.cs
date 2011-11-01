using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using StudyingController.Common;
using StudyingController.ViewModels;

namespace StudyingController
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IUserInterop, IControllerInterop
    {
        #region Properties

        private LoginViewModel loginViewModel;
        private MainWindow mainWindow;
        
        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            
            mainWindow = new MainWindow();
            loginViewModel = new LoginViewModel(this, this, mainWindow.Dispatcher);

            mainWindow.DataContext = loginViewModel;
            
            mainWindow.Show();
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        #region Methods


        #endregion

        #region Callbacks


        #endregion

        #region IControllerInterop

        #endregion

        #region IUserInterop

        public void ShowMessage(string text)
        {
            throw new NotImplementedException();
        }

        public void ShowError(string text)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
