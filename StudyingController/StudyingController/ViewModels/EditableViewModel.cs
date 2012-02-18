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
    public abstract class EditableViewModel : BaseApplicationViewModel
    {
        #region Fields & Properties

        protected object locker = new object();

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
                return !IsModified && CurrentWorkspace != null;
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

        #endregion

        #region Constructors

        public EditableViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
        }

        #endregion

        #region Commands



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

        protected virtual void UpdateProperties()
        {
            OnPropertyChanged("IsModified");
            OnPropertyChanged("CanSave");
            OnPropertyChanged("IsEnabled");
            OnPropertyChanged("CanAdd");
            OnPropertyChanged("CanRemove");
            OnPropertyChanged("CanRefresh");

            if (this is IAdditionalCommands)
                (this as IAdditionalCommands).UpdateCommandsEnabledState();
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
            UpdateProperties();
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

            //UpdateProperties();
        }


        #endregion

        #region Events
        #endregion

    }

}
