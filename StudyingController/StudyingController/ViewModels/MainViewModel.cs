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
    public class MainViewModel : LoadableViewModel
    {
        #region Fields & Properties

        public bool IsUserNotSecretary
        {
            get
            {
                return !(IsUserFacultySecretary || IsUserInstituteSecretary || IsUserMainSecretary);
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
                return (CurrentWorkspace is IAdditionalCommands) && (CurrentWorkspace as IAdditionalCommands).IsVisible;
            }
        }

        public bool IsSaveable
        {
            get
            {
                return CurrentWorkspace is BaseSaveableViewModel && (CurrentWorkspace as BaseSaveableViewModel).EditMode == EditModes.Editable;
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

        private bool isNotBusy = true;
        public override bool IsNotBusy
        {
            get
            {
                return base.IsNotBusy && isNotBusy;
            }
        }

        public bool AllowExport
        {
            get { return CurrentWorkspace is IExportable; }
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
        private UserRateStructureViewModel userRateStructureViewModel;
        private UsersStructureViewModel usersStructureViewModel;
        private AttachmentsViewModel attachmentsViewModel;
        private ControlStructureViewModel controlStructureViewModel;

        private UserProfileViewModel userProfileViewModel;

        #endregion

        #region Constructors

        public MainViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            workspaces = new Stack<BaseApplicationViewModel>();

            userProfileViewModel = new UserProfileViewModel(userInterop, controllerInterop, dispatcher, controllerInterop.User);
            userProfileViewModel.Load();
        }

        #endregion

        #region Commands

        private RelayCommand rateCommand;
        public RelayCommand RateCommand
        {
            get
            {
                if (rateCommand == null)
                    rateCommand = new RelayCommand((param) => OpenUserRates());
                return rateCommand;
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

        private RelayCommand homeCommand;
        public RelayCommand HomeCommand
        {
            get
            {
                if (homeCommand == null)
                    homeCommand = new RelayCommand(param => ChangeCurrentWorkspace(userProfileViewModel));
                return homeCommand;
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

        private RelayCommand exportToExcelCommand;
        public RelayCommand ExportToExcelCommand
        {
            get
            {
                if (exportToExcelCommand == null)
                    exportToExcelCommand = new RelayCommand(param => ExportToExcel());
                return exportToExcelCommand;
            }
        }

        private RelayCommand settingsCommand;
        public RelayCommand SettingsCommand
        {
            get
            {
                if (settingsCommand == null)
                    settingsCommand = new RelayCommand(param => OpenSettings());
                return settingsCommand;
            }
        }

        #endregion

        #region Methods

        private void VerifyActions()
        {
        }

        protected override object LoadDataFromServer()
        {
            return null;
        }

        protected override void AfterDataLoaded()
        {
            base.AfterDataLoaded();

            if (workspaces.Count == 0)
                PushWorkspace(userProfileViewModel);
            else
            {
                //foreach (BaseApplicationViewModel workspace in workspaces)
                //    if (workspace is LoadableViewModel)
                //        (workspace as LoadableViewModel).Load();
               //if( CurrentWorkspace is LoadableViewModel)
               //    (CurrentWorkspace as LoadableViewModel).Load();
            }
        }

        protected override void ClearData()
        {
            ClearWorkspaces();
        }

        private void OpenAttachments()
        {
            if (attachmentsViewModel == null)
                attachmentsViewModel = new AttachmentsViewModel(UserInterop, ControllerInterop, Dispatcher, ControllerInterop.Session.User);

            ChangeCurrentWorkspace(attachmentsViewModel);
        }

        private void OpenControlsStructure()        {
            if (controlStructureViewModel == null)
                controlStructureViewModel = new ControlStructureViewModel(UserInterop, ControllerInterop, Dispatcher)
                    {
                        EditMode = ControllerInterop.User.Role == UserRoles.Student ? EditModes.ReadOnly : EditModes.Editable
                    };
            
            ChangeCurrentWorkspace(controlStructureViewModel);
        }

        private void OpenLessonsStructure()
        {
            if (lectureStructureViewModel == null)
            {
                lectureStructureViewModel = new LessonStuctureViewModel(UserInterop, ControllerInterop, Dispatcher);
                lectureStructureViewModel.IsVisibleChanged += new EventHandler(lectureStructureViewModel_IsVisibleChanged);
            }

            ChangeCurrentWorkspace(lectureStructureViewModel);
        }

        private void OpenUserRates()
        {
            if (userRateStructureViewModel == null)
                userRateStructureViewModel = new UserRateStructureViewModel(UserInterop, ControllerInterop, Dispatcher);

            ChangeCurrentWorkspace(userRateStructureViewModel);
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

        private void OpenSettings()
        {
            //(CurrentWorkspace as IExportable).ExportToExcel();
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
                (workspace as ControlStructureViewModel).WorkspaceChanged += ControlStructureViewModel_WorkspaceChanged;
        }

        private void UnsubscribeFromEvents(BaseApplicationViewModel workspace)
        {
            if (workspace is UsersStructureViewModel)
                (workspace as UsersStructureViewModel).IsSendingMessageChanged -= UsersStructureViewModel_IsSendingMessageChanged;
            else if (workspace is ControlStructureViewModel)
                (workspace as ControlStructureViewModel).WorkspaceChanged -= ControlStructureViewModel_WorkspaceChanged;
        }

        protected virtual void OnLogout()
        {
            if (Logout != null)
                Logout(this, EventArgs.Empty);
        }

        private void PushWorkspace(BaseApplicationViewModel viewModel)
        {
            if (viewModel is LoadableViewModel)
                (viewModel as LoadableViewModel).Load();

            workspaces.Push(viewModel);
            SubscribeToEvents(viewModel);

            OnCurrentWorkspaceChanged();

            Load();
        }

        private void PopWorkspace()
        {
            UnsubscribeFromEvents(workspaces.Pop());

            OnCurrentWorkspaceChanged();
        }

        private void ClearWorkspaces()
        {
            //foreach (BaseApplicationViewModel workspace in workspaces)
            //    if (workspace is SaveableViewModel) (workspace as SaveableViewModel).Load();
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
            OnPropertyChanged("AllowExport");
        }

        private void SaveCurrentWorkspace()
        {
            (CurrentWorkspace as BaseSaveableViewModel).Save();
            if (workspaces.Peek() is PracticeControlViewModel && !(workspaces.Peek() as PracticeControlViewModel).IsExisted || workspaces.Peek() is LectureControlViewModel && !(workspaces.Peek() as LectureControlViewModel).IsExisted) PopWorkspace();
            (CurrentWorkspace as BaseSaveableViewModel).Load();
        }

        private void RollbackCurrentWorkspace()
        {
            (CurrentWorkspace as BaseSaveableViewModel).Rollback();
        }

        private void ExportToExcel()
        {
            (CurrentWorkspace as IExportable).ExportToExcel();
        }

        #endregion

        #region Callbacks

        private void UsersStructureViewModel_IsSendingMessageChanged(object sender, EventArgs e)
        {
            isNotBusy = !(sender as UsersStructureViewModel).IsSendingMessage;

            OnPropertyChanged("IsNotBusy");
        }

        private void ControlStructureViewModel_WorkspaceChanged(BaseApplicationViewModel viewModel)
        {
            PushWorkspace(viewModel);
        }

        private void lectureStructureViewModel_IsVisibleChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("HasAdditionalCommands");
        }

        #endregion

        #region Events

        public event EventHandler Logout;

        #endregion
    }
}
