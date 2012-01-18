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
                        new NamedCommandData(){Name = "Факультет", Command = null},
                        new NamedCommandData(){Name = "Кафедру", Command = null},
                        new NamedCommandData(){Name = "Групу", Command = null},
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

        private RelayCommand addEntityCommand;
        public RelayCommand AddEntityCommand
        {
            get 
            {
                if (addEntityCommand == null)
                    addEntityCommand = new RelayCommand(param => AddEntity());
                return addEntityCommand; 
            }
        }

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

        #region University structure

        private RelayCommand addInstituteCommand;
        public RelayCommand AddInstituteCommand
        {
            get
            {
                if (addInstituteCommand == null)
                    addInstituteCommand = new RelayCommand(param => AddInstitute());
                return addInstituteCommand; 
            }
        }

        #endregion

        #endregion

        #region Methods

        private void AddInstitute()
        {
        }

        private void AddEntity()
        {
            PushWorkspace(new InstituteViewModel(UserInterop, ControllerInterop, Dispatcher));   
        }

        private void ModifyEntity()
        {
            throw new NotImplementedException();
        }

        private void RemoveEntity()
        {
            throw new NotImplementedException();
        }

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
            if (workspace is BaseUniversityTreeViewModel)
                (workspace as BaseUniversityTreeViewModel).SelectedEntityChangedEvent += SelectedEntityChanged;
        }

        private void UnsubscribeFromEvents(BaseApplicationViewModel workspace)
        {
            if (workspace is BaseUniversityTreeViewModel)
                (workspace as BaseUniversityTreeViewModel).SelectedEntityChangedEvent -= SelectedEntityChanged;
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

        #endregion

        #region Callbacks

        private void SelectedEntityChanged(object sender, SelectedEntityChangedArgs e)
        {
            //ChangeCurrentCommands(e.Value);
        }

        #endregion

        #region Events

        public event EventHandler Logout;

        #endregion
    }
}
