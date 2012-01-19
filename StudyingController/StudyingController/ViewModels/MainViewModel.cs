using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using System.Windows.Input;
using System.Collections.ObjectModel;
using EntitiesDTO;

namespace StudyingController.ViewModels
{   
    public class MainViewModel : BaseApplicationViewModel
    {
        #region Fields & Properties

        #region Named commands

        #region Main commands

        private ObservableCollection<NamedCommandData> adminMainCommands;
        public ObservableCollection<NamedCommandData> AdminMainCommands
        {
            get
            {
                if (adminMainCommands == null)
                    adminMainCommands = new ObservableCollection<NamedCommandData>
                    { 
                        new NamedCommandData(){Name = "Структура університету", Command = UniversityStructureCommand},
                        new NamedCommandData(){Name = "Користувачі", Command = UsersStructureCommand}
                    };
                return adminMainCommands;
            }
        }

        #endregion

        #region Add commands

        private ObservableCollection<NamedCommandData> allAddingCommands;
        public ObservableCollection<NamedCommandData> AllAddingCommands
        {
            get
            {
                if (allAddingCommands == null)
                    allAddingCommands = new ObservableCollection<NamedCommandData>
                    { 
                        new NamedCommandData(){Name = "Інститут", Command = AddInstituteCommand},
                        new NamedCommandData(){Name = "Факультет", Command = AddFacultyCommand},
                        new NamedCommandData(){Name = "Кафедру", Command = AddCathedraCommand},
                        new NamedCommandData(){Name = "Групу", Command = AddGroupCommand},
                    };
                return allAddingCommands;
            }
        }



        #endregion

        #endregion

        public bool IsSaveable
        {
            get
            {
                return CurrentWorkspace is EditableViewModel;
            }
        }

        private Stack<BaseApplicationViewModel> workspaces;

        public BaseApplicationViewModel CurrentWorkspace
        {
            get 
            {
                if (workspaces.Count == 0)
                    return null;

                return workspaces.Peek();
            }
        }

        public bool HasWorkspaces
        {
            get
            {
                return workspaces.Count > 0;
            }
        }

        private ObservableCollection<NamedCommandData> mainCommands;
        private ReadOnlyObservableCollection<NamedCommandData> mainCommandsRO;
        public ReadOnlyObservableCollection<NamedCommandData> MainCommands
        {
            get { return mainCommandsRO; }
        }//Collection for left buttont

        private ObservableCollection<NamedCommandData> pathCommands;
        private ReadOnlyObservableCollection<NamedCommandData> pathCommandsRO;
        public ReadOnlyObservableCollection<NamedCommandData> PathCommands
        {
            get { return pathCommandsRO; }
        }//Collection for path

        private ObservableCollection<NamedCommandData> currentCommands;
        private ReadOnlyObservableCollection<NamedCommandData> currentCommandsRO;
        public ReadOnlyObservableCollection<NamedCommandData> CurrentCommands
        {
            get { return currentCommandsRO; }
        }//Collection for toolbar
        

        #endregion

        #region Constructors
        
        public MainViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            workspaces = new Stack<BaseApplicationViewModel>();

            mainCommands = GetMainCommands();
            mainCommandsRO = new ReadOnlyObservableCollection<NamedCommandData>(mainCommands);

            currentCommands = GetModificationCommands();
            currentCommandsRO = new ReadOnlyObservableCollection<NamedCommandData>(currentCommands);
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

        private RelayCommand usersStructureCommand;
        public RelayCommand UsersStructureCommand
        {
            get 
            {
                if (usersStructureCommand == null)
                    usersStructureCommand = new RelayCommand(param => OpenUsersStructure());
                return usersStructureCommand; 
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

        private RelayCommand saveCurrentWorkspaceCommand;
        public RelayCommand SaveCurrentWorkspaceCommand
        {
            get
            {
                if (saveCurrentWorkspaceCommand == null)
                    saveCurrentWorkspaceCommand = new RelayCommand(param => SaveCurrentWorkspace());
                return saveCurrentWorkspaceCommand;
            }
        }

        private RelayCommand rollbackCurrentWorkspaceCommand;
        public RelayCommand RollbackCurrentWorkspaceCommand
        {
            get
            {
                if (rollbackCurrentWorkspaceCommand == null)
                    rollbackCurrentWorkspaceCommand = new RelayCommand(param => RollbackCurrentWorkspace());
                return rollbackCurrentWorkspaceCommand;
            }
        }

        #region University structure

        private RelayCommand addInstituteCommand;
        public RelayCommand AddInstituteCommand
        {
            get
            {
                if (addInstituteCommand == null)
                    addInstituteCommand = new RelayCommand(param => 
                    {
                        EditableViewModel viewModel = CurrentWorkspace as EditableViewModel;
                        viewModel.ChangeCurrentWorkspace(new InstituteViewModel(UserInterop, ControllerInterop, Dispatcher));
                    });
                return addInstituteCommand; 
            }
        }

        private RelayCommand addFacultyCommand;
        public RelayCommand AddFacultyCommand
        {
            get
            {
                if (addFacultyCommand == null)
                    addFacultyCommand = new RelayCommand(param =>
                    {
                        EditableViewModel viewModel = CurrentWorkspace as EditableViewModel;
                        viewModel.ChangeCurrentWorkspace(new FacultyViewModel(UserInterop, ControllerInterop, Dispatcher));
                    });
                return addFacultyCommand;
            }
        }

        private RelayCommand addCathedraCommand;
        public RelayCommand AddCathedraCommand
        {
            get
            {
                if (addCathedraCommand == null)
                    addCathedraCommand = new RelayCommand(param =>
                    {
                        EditableViewModel viewModel = CurrentWorkspace as EditableViewModel;
                        viewModel.ChangeCurrentWorkspace(new CathedraViewModel(UserInterop, ControllerInterop, Dispatcher));
                    });
                return addCathedraCommand;
            }
        }

        private RelayCommand addGroupCommand;
        public RelayCommand AddGroupCommand
        {
            get
            {
                if (addGroupCommand == null)
                    addGroupCommand = new RelayCommand(param =>
                    {
                        EditableViewModel viewModel = CurrentWorkspace as EditableViewModel;
                        viewModel.ChangeCurrentWorkspace(new GroupViewModel(UserInterop, ControllerInterop, Dispatcher));
                    });
                return addGroupCommand;
            }
        }
        #endregion

        #endregion

        #region Methods

        private ObservableCollection<NamedCommandData> GetMainCommands()
        {
            switch (ControllerInterop.Session.User.UserRole)
            {
                case UserRoles.MainAdmin:
                    return AdminMainCommands;
                default:
                    return new ObservableCollection<NamedCommandData>();
            }
        }

        private ObservableCollection<NamedCommandData> GetModificationCommands()
        {
            switch (ControllerInterop.Session.User.UserRole)
            {
                case UserRoles.MainAdmin:
                    return AllAddingCommands;
                default:
                    return new ObservableCollection<NamedCommandData>();
            }
        }

        private void OpenUniversityStructure()
        {
            ChangeCurrentWorkspace(new UniversityStructureViewModel(UserInterop, ControllerInterop, Dispatcher));
        }

        private void OpenUsersStructure()
        {
            ChangeCurrentWorkspace(new UsersStructureViewModel(UserInterop, ControllerInterop, Dispatcher));
        }

        private void ChangeCurrentWorkspace(BaseApplicationViewModel viewModel)
        {
            ClearWorkspaces();

            PushWorkspace(viewModel);
        }

        private void SubscribeToEvents(BaseApplicationViewModel workspace)
        {
        }

        private void UnsubscribeFromEvents(BaseApplicationViewModel workspace)
        {
        }

        protected virtual void OnLogout()
        {
            if (Logout != null)
                Logout(this, EventArgs.Empty);
        }

        private void AddCurrentCommands(ObservableCollection<NamedCommandData> collection)
        {
            foreach (NamedCommandData command in collection)
                currentCommands.Add(command);
        }

        private void PushWorkspace(BaseApplicationViewModel viewModel)
        {
            workspaces.Push(viewModel);
            SubscribeToEvents(viewModel);

            OnCurrentWorkspaceChanged();
        }

        private void PopWorkspace()
        {
            UnsubscribeFromEvents(workspaces.Pop());

            OnCurrentWorkspaceChanged();
        }

        private void ClearWorkspaces()
        {
            while (workspaces.Count > 0)
            {
                PopWorkspace();
            }
        }

        private void OnCurrentWorkspaceChanged()
        {
            OnPropertyChanged("CurrentWorkspace");
            OnPropertyChanged("HasWorkspaces");
            OnPropertyChanged("IsSaveable");
        }

        private void SaveCurrentWorkspace()
        {
            (CurrentWorkspace as EditableViewModel).Save();
        }

        private void RollbackCurrentWorkspace()
        {
            (CurrentWorkspace as EditableViewModel).Rollback();
        }
        #endregion

        #region Callbacks

        #endregion

        #region Events

        public event EventHandler Logout;

        #endregion
    }
}
