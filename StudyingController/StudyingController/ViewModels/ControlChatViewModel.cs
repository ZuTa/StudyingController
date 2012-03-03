using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.Common;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels
{
    public class ControlChatViewModel : BaseApplicationViewModel
    {
        #region Field & Properties

        private ControlDTO control;
        public ControlDTO Control
        {
            get { return control; }
            protected set 
            {
                if (control == null || !control.IsSameDatabaseObject(value))
                {
                    control = value;
                    OnControlChanged();
                }
            }
        }

        private string currentMessage = string.Empty;
        public string CurrentMessage
        {
            get { return currentMessage; }
            set 
            {
                currentMessage = value;
                OnPropertyChanged("CurrentMessage");
                OnPropertyChanged("CanSendMessage");
            }
        }

        public bool CanSendMessage
        {
            get
            {
                return CurrentMessage.Length > 0;
            }
        }

        private ObservableCollection<ControlMessageDTO> messages;
        private ReadOnlyObservableCollection<ControlMessageDTO> messagesRO;
        public ReadOnlyObservableCollection<ControlMessageDTO> Messages
        {
            get { return messagesRO; }
            set { messagesRO = value; }
        }

        #endregion

        #region Contructors

        public ControlChatViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, ControlDTO control)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.Control = control;

            this.messages = new ObservableCollection<ControlMessageDTO>();
            this.messagesRO = new ReadOnlyObservableCollection<ControlMessageDTO>(this.messages);
        }

        #endregion

        #region Methods

        protected virtual void OnControlChanged()
        {
            ClearData();
            LoadMessages();
        }

        private void LoadMessages()
        {
            var list = ControllerInterop.Service.GetControlMessages(ControllerInterop.Session, control.ID);

            foreach (var message in list)
                messages.Add(message);
        }

        public void Refresh()
        {
            ClearData();
            LoadMessages();
        }

        private void ClearData()
        {
            messages.Clear();
        }

        private void AddMessage(string text)
        {
            ControlMessageDTO message = new ControlMessageDTO { Text = text.Trim(), Date = DateTime.UtcNow, Owner = ControllerInterop.User, ControlID = control.ID };

            ControllerInterop.Service.SaveControlMessage(ControllerInterop.Session, message);

            Refresh();
        }

        #endregion

        #region Commands

        private RelayCommand sendMessageCommand;
        public RelayCommand SendMessageCommand
        {
            get
            {
                if (sendMessageCommand == null)
                    sendMessageCommand = new RelayCommand(param =>
                    {
                        AddMessage(CurrentMessage);

                        CurrentMessage = string.Empty;
                    });
                return sendMessageCommand; 
            }
        }

        #endregion
    }
}
