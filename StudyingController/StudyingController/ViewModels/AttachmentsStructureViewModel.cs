using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using EntitiesDTO;

namespace StudyingController.ViewModels
{
    public class AttachmentsStructureViewModel : EditableViewModel, IAdditionalCommands, IRefreshable
    {
        #region Fields & Properties

        private IProviderable entitiesProvider;
        public override IProviderable EntitiesProvider
        {
            get { return entitiesProvider; }
            set
            {
                if (entitiesProvider != value)
                {
                    entitiesProvider = value;
                    OnPropertyChanged("EntitiesProvider");
                }
            }
        }

        public bool IsNotNewAttachment
        {
            get { return CurrentWorkspace == null || !(CurrentWorkspace is AttachmentViewModel) ? false : (CurrentWorkspace as AttachmentViewModel).IsNotNewAttachment; }
        }

        public bool CanAddNewAttachment
        {
            get { return CurrentWorkspace == null || !(CurrentWorkspace is AttachmentViewModel) ? true : (CurrentWorkspace as AttachmentViewModel).IsNotNewAttachment; }
        }

        #endregion

        #region Constructors

        public AttachmentsStructureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            entitiesProvider = new AttachmentsViewModel(userInterop, controllerInterop, dispatcher);
            entitiesProvider.SelectedEntityChangedEvent += new SelectedEntityChangedHandler(entitiesProvider_SelectedEntityChangedEvent);
        }

        #endregion

        #region Commands 
        
        private RelayCommand addAttachmentCommand;
        public RelayCommand AddAttachmentCommand
        {
            get
            {
                if (addAttachmentCommand == null)
                {
                    addAttachmentCommand = new RelayCommand(param =>
                    {
                        ChangeCurrentWorkspace(new AttachmentViewModel(UserInterop, ControllerInterop, Dispatcher));
                        SetIsNewAttachment();
                    });
                }
                return addAttachmentCommand;
            }
        }

        private RelayCommand removeAttachmentCommand;
        public RelayCommand RemoveAttachmentCommand
        {
            get
            {
                if (removeAttachmentCommand == null)
                {
                    removeAttachmentCommand = new RelayCommand(param => 
                    {
                        ControllerInterop.Service.DeleteAttachment(ControllerInterop.Session, (entitiesProvider as AttachmentsViewModel).CurrentEntity.ID);
                        entitiesProvider.Refresh();
                    });
                }
                return removeAttachmentCommand;
            }
        }

        private RelayCommand downloadAttachmentCommand;
        public RelayCommand DownloadAttachmentCommand
        {
            get
            {
                if (downloadAttachmentCommand == null)
                {
                    downloadAttachmentCommand = new RelayCommand(param =>
                    {
                        (CurrentWorkspace as AttachmentViewModel).LoadSaveDialog();
                    });
                }
                return downloadAttachmentCommand;
            }
        }

        private RelayCommand openAttachmentCommand;
        public RelayCommand OpenAttachmentCommand
        {
            get
            {
                if (openAttachmentCommand == null)
                {
                    openAttachmentCommand = new RelayCommand(param =>
                    {
                        (CurrentWorkspace as AttachmentViewModel).OpenAttachment();
                    });
                }
                return openAttachmentCommand;
            }
        }

        #endregion

        #region Methods

        private void SetIsNewAttachment()
        {
            (CurrentWorkspace as AttachmentViewModel).IsNewAttachment = true;
            UpdateCommandsEnabledState();
        }

        protected override SaveableViewModel GetViewModel(BaseEntityDTO entity)
        {
            if(entity!=null)
                return new AttachmentViewModel(UserInterop, ControllerInterop, Dispatcher,entity as AttachmentDTO);
            return null;
        }

        public void UpdateCommandsEnabledState()
        {
            foreach (var ac in AdditionalCommands)
                if (ac.UpdateEnabledState != null)
                    ac.UpdateEnabledState();
        }

        #endregion

        #region Event
        #endregion

        #region Callbacks

        private void entitiesProvider_SelectedEntityChangedEvent(object sender, SelectedEntityChangedArgs e)
        {
            ChangeCurrentWorkspace(GetViewModel(entitiesProvider.CurrentEntity));
        }

        #endregion

        #region IAdditionalCommands

        private ObservableCollection<NamedCommandData> additionalCommands;
        public ObservableCollection<NamedCommandData> AdditionalCommands
        {
            get
            {
                if (additionalCommands == null)
                {
                    additionalCommands = new ObservableCollection<NamedCommandData>();
                    NamedCommandData addAttachmentNamedCommand = new NamedCommandData
                    {
                        Command = AddAttachmentCommand,
                        Name = "Додати",
                        Type = CommandTypes.Add
                    };
                    addAttachmentNamedCommand.UpdateEnabledState = () => addAttachmentNamedCommand.IsEnabled = CanAddNewAttachment;
                    NamedCommandData removeAttachmentNamedCommand = new NamedCommandData
                    {
                        Command = RemoveAttachmentCommand,
                        Name = "Видалити",
                        Type = CommandTypes.Remove
                    };
                    removeAttachmentNamedCommand.UpdateEnabledState = () => removeAttachmentNamedCommand.IsEnabled = IsNotNewAttachment;
                    NamedCommandData downloadAttachmentNamedCommand = new NamedCommandData
                    {
                        Command = DownloadAttachmentCommand,
                        Name = "Завантажити",
                        Type = CommandTypes.Download
                    };
                    downloadAttachmentNamedCommand.UpdateEnabledState = () => downloadAttachmentNamedCommand.IsEnabled = IsNotNewAttachment;
                    NamedCommandData openAttachmentNamedCommand = new NamedCommandData
                    {
                        Command = OpenAttachmentCommand,
                        Name = "Відкрити",
                        Type = CommandTypes.Open
                    };
                    openAttachmentNamedCommand.UpdateEnabledState = () => openAttachmentNamedCommand.IsEnabled = IsNotNewAttachment;

                    additionalCommands.Add(addAttachmentNamedCommand);
                    additionalCommands.Add(removeAttachmentNamedCommand);
                    additionalCommands.Add(downloadAttachmentNamedCommand);
                    additionalCommands.Add(openAttachmentNamedCommand);
                    UpdateCommandsEnabledState();
                }
                return additionalCommands;
            }
        }

        #endregion
    }
}
