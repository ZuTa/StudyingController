﻿using System;
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

        private UserInformationViewModel userInformationViewModel;
        public UserInformationViewModel UserInformationViewModel
        {
            get { return userInformationViewModel; }
            set
            {
                if (userInformationViewModel != value)
                {
                    userInformationViewModel = value;
                    OnPropertyChanged("UserInformationViewModel");
                }
            }
        }

        public bool IsUserMainAdmin
        {
            get
            {
                return ControllerInterop.User == null ? false : ControllerInterop.User.Role == UserRoles.MainAdmin;
            }
        }

        public bool IsUserInstituteAdmin
        {
            get
            {
                return ControllerInterop.User == null ? false : ControllerInterop.User.Role == UserRoles.InstituteAdmin;
            }
        }

        public bool IsUserFacultyAdmin
        {
            get
            {
                return ControllerInterop.User == null ? false : ControllerInterop.User.Role == UserRoles.FacultyAdmin;
            }
        }

        public bool IsUserMainSecretary
        {
            get
            {
                return ControllerInterop.User == null ? false : ControllerInterop.User.Role == UserRoles.MainSecretary;
            }
        }

        public bool IsUserInstituteSecretary
        {
            get
            {
                return ControllerInterop.User == null ? false : ControllerInterop.User.Role == UserRoles.InstituteSecretary;
            }
        }

        public bool IsUserFacultySecretary
        {
            get
            {
                return ControllerInterop.User == null ? false : ControllerInterop.User.Role == UserRoles.FacultySecretary;
            }
        }

        public bool IsUserTeacher
        {
            get
            {
                return ControllerInterop.User == null ? false : ControllerInterop.User.Role == UserRoles.Teacher;
            }
        }

        public bool IsUserStudent
        {
            get
            {
                return ControllerInterop.User == null ? false : ControllerInterop.User.Role == UserRoles.Student;
            }
        }

        public bool IsUserAdminOrSecretary
        {
            get
            {
                return IsUserMainAdmin || IsUserMainSecretary || IsUserInstituteAdmin || IsUserInstituteSecretary || IsUserFacultyAdmin || IsUserFacultySecretary;
            }
        }

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
            UserInformationViewModel = new UserInformationViewModel(userInterop, controllerInterop, dispatcher, controllerInterop.User.UserInformation);
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

        #endregion

        #region Methods

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
