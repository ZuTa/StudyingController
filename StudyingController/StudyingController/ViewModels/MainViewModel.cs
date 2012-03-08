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

        public bool HasAdditionalCommands
        {
            get
            {
                return CurrentWorkspace is IAdditionalCommands;
            }
        }

        public bool IsSaveable
        {
            get
            {
                return CurrentWorkspace is IEditable && (CurrentWorkspace as IEditable).EditMode == EditModes.Editable;
            }
        }

        public bool IsManipulateable
        {
            get
            {
                return CurrentWorkspace is IManipulateable;
            }
        }

        public bool IsRefreshable
        {
            get
            {
                return CurrentWorkspace is IRefreshable;
            }
        }

        public bool IsManipulateableOrRefreshable
        {
            get
            {
                return IsRefreshable || IsManipulateable;
            }
        }

        private bool isNotBusy;
        public bool IsNotBusy
        {
            get { return isNotBusy; }

            set
            {
                if (isNotBusy != value)
                {
                    isNotBusy = value;
                    OnPropertyChanged("IsNotBusy");
                }
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

        private LessonStuctureViewModel lectureStructureViewModel;
        private UniversityStructureViewModel universityStructureViewModel;
        private UsersStructureViewModel usersStructureViewModel;
        private AttachmentsStructureViewModel attachmentsStructureViewModel;
        private ControlStructureViewModel controlStructureViewModel;
        #endregion

        #region Constructors

        public MainViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            workspaces = new Stack<BaseApplicationViewModel>();
            UserInformationViewModel = new UserInformationViewModel(userInterop, controllerInterop, dispatcher, controllerInterop.User.UserInformation);
            isNotBusy = true;
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

        private RelayCommand lessonCommand;
        public RelayCommand LessonCommand
        {
            get
            {
                if (lessonCommand == null)
                    lessonCommand = new RelayCommand(param => OpenLessonsStructure());
                return lessonCommand;
            }
        }

        private RelayCommand attachmentCommand;
        public RelayCommand AttachmentCommand
        {
            get
            {
                if (attachmentCommand == null)
                    attachmentCommand = new RelayCommand(param => OpenAttachments());
                return attachmentCommand;
            }
        }

        private RelayCommand controlStructureCommand;
        public RelayCommand ControlStructureCommand
        {
            get
            {
                if (controlStructureCommand == null)
                    controlStructureCommand = new RelayCommand(param => OpenControlsStructure());

                return controlStructureCommand;
            }
        }

        #endregion

        #region Methods

        private void OpenAttachments()
        {
            if (attachmentsStructureViewModel == null)
                attachmentsStructureViewModel = new AttachmentsStructureViewModel(UserInterop, ControllerInterop, Dispatcher);
                        ChangeCurrentWorkspace(attachmentsStructureViewModel);
        }

        private void OpenControlsStructure()        {
            if (controlStructureViewModel == null)
                controlStructureViewModel = new ControlStructureViewModel(UserInterop, ControllerInterop, Dispatcher) { EditMode = ControllerInterop.User.Role == UserRoles.Student ? EditModes.ReadOnly : EditModes.Editable };
            
            ChangeCurrentWorkspace(controlStructureViewModel);
        }

        void controlStructureViewModel_WorkspaceChanged(BaseApplicationViewModel viewModel)
        {
            PushWorkspace(viewModel);
        }

        private void OpenLessonsStructure()
        {
            if (lectureStructureViewModel == null)
                lectureStructureViewModel = new LessonStuctureViewModel(UserInterop, ControllerInterop, Dispatcher);

            ChangeCurrentWorkspace(lectureStructureViewModel);
        }

        private void OpenUniversityStructure()
        {
            if (universityStructureViewModel == null)
                universityStructureViewModel = new UniversityStructureViewModel(UserInterop, ControllerInterop, Dispatcher);
            ChangeCurrentWorkspace(universityStructureViewModel);
        }

        private void OpenUsersStructure()
        {
            if (usersStructureViewModel == null)
                usersStructureViewModel = new UsersStructureViewModel(UserInterop, ControllerInterop, Dispatcher);
            ChangeCurrentWorkspace(usersStructureViewModel);
        }

        private void ChangeCurrentWorkspace(BaseApplicationViewModel viewModel)
        {
            ClearWorkspaces();

            PushWorkspace(viewModel);
        }

        private void SubscribeToEvents(BaseApplicationViewModel workspace)
        {
            if (workspace is UsersStructureViewModel)
                (workspace as UsersStructureViewModel).IsSendingMessageChanged += UsersStructureViewModel_IsSendingMessageChanged;
            else if (workspace is ControlStructureViewModel)
                controlStructureViewModel.WorkspaceChanged += new ControlStructureViewModel.ChangeWorkspaceHandler(controlStructureViewModel_WorkspaceChanged);
        }

        private void UnsubscribeFromEvents(BaseApplicationViewModel workspace)
        {
            if (workspace is UsersStructureViewModel)
                (workspace as UsersStructureViewModel).IsSendingMessageChanged -= UsersStructureViewModel_IsSendingMessageChanged;
            else if (workspace is ControlStructureViewModel)
                controlStructureViewModel.WorkspaceChanged -= controlStructureViewModel_WorkspaceChanged;
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
            OnPropertyChanged("IsManipulateable");
            OnPropertyChanged("HasAdditionalCommands");
            OnPropertyChanged("IsManipulateableOrRefreshable");
            OnPropertyChanged("IsRefreshable");
        }

        private void SaveCurrentWorkspace()
        {
            (CurrentWorkspace as IEditable).Save();
        }

        private void RollbackCurrentWorkspace()
        {
            (CurrentWorkspace as IEditable).Rollback();
        }
        #endregion

        #region Callbacks

        private void UsersStructureViewModel_IsSendingMessageChanged(object sender, EventArgs e)
        {
            IsNotBusy = !(sender as UsersStructureViewModel).IsSendingMessage;
        }

        #endregion

        #region Events

        public event EventHandler Logout;

        #endregion
    }
}
