using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels
{
    public class UsersStructureViewModel : EditableViewModel
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
        
        #endregion

        #region Constructors

        public UsersStructureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            entitiesProvider = new UsersTreeViewModel(userInterop, controllerInterop, dispatcher);
            entitiesProvider.SelectedEntityChangedEvent += new SelectedEntityChangedHandler(EntitesProvider_SelectedEntityChangedEvent);
        }

        #endregion

        #region Commands

        private RelayCommand addInstituteAdminCommand;
        public RelayCommand AddInstituteAdminCommand
        {
            get
            {
                if (addInstituteAdminCommand == null)
                    addInstituteAdminCommand = new RelayCommand(param => ChangeCurrentWorkspace(new InstituteViewModel(UserInterop, ControllerInterop, Dispatcher)));

                return addInstituteAdminCommand; 
            }
        }

        private RelayCommand addInstituteSecretaryCommand;
        public RelayCommand AddInstituteSecretaryCommand
        {
            get
            {
                if (addInstituteSecretaryCommand == null)
                    addInstituteSecretaryCommand = new RelayCommand(param => ChangeCurrentWorkspace(new InstituteViewModel(UserInterop, ControllerInterop, Dispatcher)));

                return addInstituteSecretaryCommand;
            }
        }

        private RelayCommand addFacultyAdminCommand;
        public RelayCommand AddFacultyAdminCommand
        {
            get
            {
                if (addFacultyAdminCommand == null)
                    addFacultyAdminCommand = new RelayCommand(param => ChangeCurrentWorkspace(new InstituteViewModel(UserInterop, ControllerInterop, Dispatcher)));

                return addFacultyAdminCommand;
            }
        }

        private RelayCommand addFacultySecretaryCommand;
        public RelayCommand AddFacultySecretaryCommand
        {
            get
            {
                if (addFacultySecretaryCommand == null)
                    addFacultySecretaryCommand = new RelayCommand(param => ChangeCurrentWorkspace(new InstituteViewModel(UserInterop, ControllerInterop, Dispatcher)));

                return addFacultySecretaryCommand;
            }
        }

        private RelayCommand addTeacherCommand;
        public RelayCommand AddTeacherCommand
        {
            get
            {
                if (addTeacherCommand == null)
                    addTeacherCommand = new RelayCommand(param => ChangeCurrentWorkspace(new InstituteViewModel(UserInterop, ControllerInterop, Dispatcher)));

                return addTeacherCommand;
            }
        }

        private RelayCommand addStudentCommand;
        public RelayCommand AddStudentCommand
        {
            get
            {
                if (addStudentCommand == null)
                    addStudentCommand = new RelayCommand(param => ChangeCurrentWorkspace(new InstituteViewModel(UserInterop, ControllerInterop, Dispatcher)));

                return addStudentCommand;
            }
        }

        private RelayCommand addMainAdminCommand;
        public RelayCommand AddMainAdminCommand
        {
            get
            {
                if (addMainAdminCommand == null)
                    addMainAdminCommand = new RelayCommand(param => ChangeCurrentWorkspace(new InstituteViewModel(UserInterop, ControllerInterop, Dispatcher)));

                return addMainAdminCommand;
            }
        }

        private RelayCommand addMainSecretaryCommand;
        public RelayCommand AddMainSecretaryCommand
        {
            get
            {
                if (addMainSecretaryCommand == null)
                    addMainSecretaryCommand = new RelayCommand(param => ChangeCurrentWorkspace(new InstituteViewModel(UserInterop, ControllerInterop, Dispatcher)));

                return addMainSecretaryCommand;
            }
        }

        #endregion

        #region Methods

        protected override void DefineAddCommands()
        {
            switch (ControllerInterop.User.Role)
            {
                case UserRoles.MainAdmin:
                    addCommands.Add(new NamedCommandData() { Name = "Головного адміністратора", Command = AddMainAdminCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Головного секретаря", Command = AddMainSecretaryCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Адміністратора інституту", Command = AddInstituteAdminCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Секретаря інституту", Command = AddInstituteSecretaryCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Адміністратора факультету", Command = AddFacultyAdminCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Секретаря факультету", Command = AddFacultySecretaryCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Викладача", Command = AddTeacherCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Студента", Command = AddStudentCommand });
                    break;
                case UserRoles.MainSecretary:
                    addCommands.Add(new NamedCommandData() { Name = "Головного секретаря", Command = AddMainSecretaryCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Адміністратора інституту", Command = AddInstituteAdminCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Секретаря інституту", Command = AddInstituteSecretaryCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Адміністратора факультету", Command = AddFacultyAdminCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Секретаря факультету", Command = AddFacultySecretaryCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Викладача", Command = AddTeacherCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Студента", Command = AddStudentCommand });
                    break;
                case UserRoles.InstituteAdmin:
                    addCommands.Add(new NamedCommandData() { Name = "Адміністратора інституту", Command = AddInstituteAdminCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Секретаря інституту", Command = AddInstituteSecretaryCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Адміністратора факультету", Command = AddFacultyAdminCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Секретаря факультету", Command = AddFacultySecretaryCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Викладача", Command = AddTeacherCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Студента", Command = AddStudentCommand });
                    break;
                case UserRoles.InstituteSecretary:
                    addCommands.Add(new NamedCommandData() { Name = "Секретаря інституту", Command = AddInstituteSecretaryCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Адміністратора факультету", Command = AddFacultyAdminCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Секретаря факультету", Command = AddFacultySecretaryCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Викладача", Command = AddTeacherCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Студента", Command = AddStudentCommand });
                    break;
                case UserRoles.FacultyAdmin:
                    addCommands.Add(new NamedCommandData() { Name = "Адміністратора факультету", Command = AddFacultyAdminCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Секретаря факультету", Command = AddFacultySecretaryCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Викладача", Command = AddTeacherCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Студента", Command = AddStudentCommand });
                    break;
                case UserRoles.FacultySecretary:                    
                    addCommands.Add(new NamedCommandData() { Name = "Секретаря факультету", Command = AddFacultySecretaryCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Викладача", Command = AddTeacherCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Студента", Command = AddStudentCommand });
                    break;
                default:
                    throw new NotImplementedException("Unknown user's role");
            }
        }

        protected override SaveableViewModel GetViewModel(EntitiesDTO.BaseEntityDTO entity)
        {
            if (entity is SystemUserDTO)
                return null;

            return null;
        }

        #endregion

        #region Callbacks

        #endregion

    }
}
