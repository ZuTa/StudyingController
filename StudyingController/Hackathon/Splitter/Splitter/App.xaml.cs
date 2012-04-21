using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using StudyingController.Common;

namespace Splitter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, ISplitterInterop, IUserInterop
    {
        #region Fields & Properties

        private MainWindow mainWindow;

        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            mainWindow = new MainWindow();

            mainWindow.Show();
        }

        #region Methods

        #endregion

        #region Callbacks


        #endregion

        #region ISplitterInterop

        private SS.SplitterServiceClient service = null;
        public SS.SplitterServiceClient Service
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

        private SS.Session session;
        public SS.Session Session
        {
            get
            {
                return session;
            }
            set
            {
                session = value;
            }
        }

        public ModelDTO.SystemUserDTO User
        {
            get
            {
                return session == null ? null : session.User;
            }
        }

        public event EventHandler SessionChanged;
        protected virtual void OnSessionChanged()
        {
            if (SessionChanged != null)
                SessionChanged(this, EventArgs.Empty);
        }

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
