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
    public abstract class EditableViewModel : BaseApplicationViewModel, IManipulateable
    {
        #region Fields & Properties

        private ObservableCollection<NamedCommandData> commands;
        private ReadOnlyObservableCollection<NamedCommandData> commandsRO;
        public ReadOnlyObservableCollection<NamedCommandData> Commands
        {
            get { return commandsRO; }
        }

        public bool IsModified
        {
            get
            {
                return (CurrentWorkspace == null) ? false : CurrentWorkspace.IsModified;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return !IsModified;
            }
        }

        public bool CanAdd
        {
            get 
            {
                return !IsModified;
            }
        }

        public bool CanRemove
        {
            get
            {
                return !IsModified;
            }
        }

        public bool CanSave
        {
            get 
            {
                return IsModified && (CurrentWorkspace == null ? false : CurrentWorkspace.IsValid); 
            }
        }

        public bool CanRefresh
        {
            get
            {
                return !IsModified;
            }
        }

        public abstract IProviderable EntitiesProvider { get; set; }

        private SaveableViewModel currentWorkspace;
        public SaveableViewModel CurrentWorkspace
        {
            get
            {
                return currentWorkspace;
            }
            protected set
            {
                if (currentWorkspace != value)
                {
                    currentWorkspace = value;
                    OnPropertyChanged("CurrentWorkspace");
                }
            }
        }

        protected ObservableCollection<NamedCommandData> addCommands;
        private ReadOnlyObservableCollection<NamedCommandData> addCommandsRO;
        public ReadOnlyObservableCollection<NamedCommandData> AddCommands
        {
            get { return addCommandsRO; }
        }

        #endregion

        #region Constructors

        public EditableViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            addCommands = new ObservableCollection<NamedCommandData>();
            addCommandsRO = new ReadOnlyObservableCollection<NamedCommandData>(addCommands);

            DefineAddCommands();
        }

        #endregion

        #region Commands

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                if (removeCommand == null)
                    removeCommand = new RelayCommand(param =>
                        {
                            throw new NotImplementedException();
                        });
                return removeCommand; 
            }
        }

        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get 
            {
                if (updateCommand == null)
                    updateCommand = new RelayCommand(param =>
                        {
                            EntitiesProvider.Refresh();
                        });
                return updateCommand; 
            }
        }

        #endregion

        #region Methods

        public void Save()
        {
            CurrentWorkspace.Save();
        }

        public void Rollback()
        {
            CurrentWorkspace.Rollback();
        }

        protected abstract SaveableViewModel GetViewModel(BaseEntityDTO entity);

        protected abstract void DefineAddCommands();

        protected virtual void UpdateProperties()
        {
            OnPropertyChanged("IsModified");
            OnPropertyChanged("CanSave");
            OnPropertyChanged("IsEnabled");
            OnPropertyChanged("CanAdd");
            OnPropertyChanged("CanRemove");
            OnPropertyChanged("CanRefresh");
        }

        private void ViewModelUnModified(object sender, EventArgs e)
        {
            UpdateProperties();

            EntitiesProvider.Refresh();
        }

        private void ViewModelModified(object sender, EventArgs e)
        {
            UpdateProperties();
        }

        public void ChangeCurrentWorkspace(SaveableViewModel viewModel)
        {
            UnsubscribeFromEvrents();
            CurrentWorkspace = viewModel;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            if (CurrentWorkspace != null)
            {
                CurrentWorkspace.ViewModified += new EventHandler(ViewModelModified);
                CurrentWorkspace.ViewUnModified += new EventHandler(ViewModelUnModified);
            }
        }

        private void UnsubscribeFromEvrents()
        {
            if (CurrentWorkspace != null)
            {
                CurrentWorkspace.ViewModified -= ViewModelModified;
                CurrentWorkspace.ViewUnModified -= ViewModelUnModified;
            }
        }

        #endregion

        #region Callbacks

        protected virtual void EntitesProvider_SelectedEntityChangedEvent(object sender, SelectedEntityChangedArgs e)
        {
            ChangeCurrentWorkspace(GetViewModel(EntitiesProvider.CurrentEntity));

            UpdateProperties();
        }


        #endregion

        #region Events
        #endregion

    }

}
