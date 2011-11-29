using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels
{   
    public class MainViewModel : BaseApplicationViewModel
    {
        #region Fields & Properties

        private BaseApplicationViewModel currentWorkspace = null; 

        public BaseApplicationViewModel CurrentWorkspace
        {
            get 
            { 
                return currentWorkspace; 
            }
            set 
            {
                if (currentWorkspace != value)
                    currentWorkspace = value;
                OnPropertyChanged("CurrentWorkspace");
            }
        }

        private ObservableCollection<NamedCommandData> visibleCommands;
        private ReadOnlyObservableCollection<NamedCommandData> visibleCommandsRO;
        public ReadOnlyObservableCollection<NamedCommandData> VisibleCommands
        {
            get { return visibleCommandsRO; }
        }//Collection for left buttont

        private ObservableCollection<NamedCommandData> pathCommands;
        private ReadOnlyObservableCollection<NamedCommandData> pathCommandsRO;
        public ReadOnlyObservableCollection<NamedCommandData> PathCommands
        {
            get { return pathCommandsRO; }
        }//Collection for path

        private ObservableCollection<NamedCommandData> toolbarCommands;
        private ReadOnlyObservableCollection<NamedCommandData> toolbarCommandsRO;
        public ReadOnlyObservableCollection<NamedCommandData> ToolbarCommands
        {
            get { return toolbarCommandsRO; }
        }//Collection for toolbar
        

        #endregion

        #region Constructors
        
        public MainViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            visibleCommands = GetVisibleCommands(controllerInterop);
            visibleCommandsRO = new ReadOnlyObservableCollection<NamedCommandData>(visibleCommands);
        }

        #endregion

        #region Commands

        private RelayCommand universityStructureCommand;
        public RelayCommand UniversityStructureCommand
        {
            get 
            {
                if (universityStructureCommand == null) 
                    universityStructureCommand = new RelayCommand((param) => OpenUniversityStructure());
                return universityStructureCommand;
            }
        }

        private RelayCommand logoutCommand;
        public RelayCommand LogoutCommand
        {
            get 
            {
                if (logoutCommand == null)
                    logoutCommand = new RelayCommand(param => OnLogout());
                return logoutCommand; 
            }
        }

        #endregion

        #region Methods

        private ObservableCollection<NamedCommandData> GetVisibleCommands(IControllerInterop controllerInterop)
        {
            switch (controllerInterop.Session.User.UserRole)
            {
                case SCS.UserRoles.MainAdmin:
                    return new ObservableCollection<NamedCommandData>{ new NamedCommandData(){Name = "Структура університету", Command = UniversityStructureCommand},
                                                                       new NamedCommandData(){Name = "Користувачі"}
                                                                     };
            }
            return new ObservableCollection<NamedCommandData>();
        }

        private void OpenUniversityStructure()
        {
            CurrentWorkspace = new UniversityStructureViewModel(UserInterop, ControllerInterop, Dispatcher);
        }

        protected virtual void OnLogout()
        {
            if (Logout != null)
                Logout(this, EventArgs.Empty);
        }

        #endregion

        #region Events

        public event EventHandler Logout;

        #endregion
    }
}
