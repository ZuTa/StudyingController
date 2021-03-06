﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using System.Collections.ObjectModel;
using StudyingController.ViewModels.Models;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StudyingController.ViewModels
{
    public class UsersStructureViewModel : EditableViewModel, IAdditionalCommands, IManipulateable, IRefreshable
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

        protected ObservableCollection<NamedCommandData> addCommands;
        private ReadOnlyObservableCollection<NamedCommandData> addCommandsRO;
        public ReadOnlyObservableCollection<NamedCommandData> AddCommands
        {
            get { return addCommandsRO; }
        }

        public bool CanGeneratePassword
        {
            get
            {
                return CurrentWorkspace == null ? false : CurrentWorkspace.Model is SystemUserModel && CurrentWorkspace.Model.Exists();
            }
        }

        private bool isSendingMessage;
        public bool IsSendingMessage
        {
            get { return isSendingMessage; }

            set
            {
                if (isSendingMessage != value)
                {
                    isSendingMessage = value;
                    OnIsSendingMessageChanged();
                }
            }
        }
       
        #endregion

        #region Constructors

        public UsersStructureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            addCommands = new ObservableCollection<NamedCommandData>();
            addCommandsRO = new ReadOnlyObservableCollection<NamedCommandData>(addCommands);

            entitiesProvider = new UsersTreeViewModel(userInterop, controllerInterop, dispatcher);
            entitiesProvider.SelectedEntityChangedEvent += new SelectedEntityChangedHandler(EntitesProvider_SelectedEntityChangedEvent);

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
                        if (UserInterop.ShowMessage(Properties.Resources.RemoveEntityTxt, Properties.Resources.DefaultMessageText, MessageButtons.YesNo, MessageTypes.Question) == MessageResults.Yes)
                        {
                            CurrentWorkspace.Remove();
                            EntitiesProvider.Refresh();
                        }
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

        private RelayCommand addInstituteAdminCommand;
        public RelayCommand AddInstituteAdminCommand
        {
            get
            {
                if (addInstituteAdminCommand == null)
                    addInstituteAdminCommand = new RelayCommand(param => ChangeCurrentWorkspace(new InstituteAdminViewModel(UserInterop, ControllerInterop, Dispatcher)));

                return addInstituteAdminCommand; 
            }
        }

        private RelayCommand addInstituteSecretaryCommand;
        public RelayCommand AddInstituteSecretaryCommand
        {
            get
            {
                if (addInstituteSecretaryCommand == null)
                    addInstituteSecretaryCommand = new RelayCommand(param => ChangeCurrentWorkspace(new InstituteSecretaryViewModel(UserInterop, ControllerInterop, Dispatcher)));

                return addInstituteSecretaryCommand;
            }
        }

        private RelayCommand addFacultyAdminCommand;
        public RelayCommand AddFacultyAdminCommand
        {
            get
            {
                if (addFacultyAdminCommand == null)
                    addFacultyAdminCommand = new RelayCommand(param => ChangeCurrentWorkspace(new FacultyAdminViewModel(UserInterop, ControllerInterop, Dispatcher)));

                return addFacultyAdminCommand;
            }
        }

        private RelayCommand addFacultySecretaryCommand;
        public RelayCommand AddFacultySecretaryCommand
        {
            get
            {
                if (addFacultySecretaryCommand == null)
                    addFacultySecretaryCommand = new RelayCommand(param => ChangeCurrentWorkspace(new FacultySecretaryViewModel(UserInterop, ControllerInterop, Dispatcher)));

                return addFacultySecretaryCommand;
            }
        }

        private RelayCommand addTeacherCommand;
        public RelayCommand AddTeacherCommand
        {
            get
            {
                if (addTeacherCommand == null)
                    addTeacherCommand = new RelayCommand(param => ChangeCurrentWorkspace(new TeacherViewModel(UserInterop, ControllerInterop, Dispatcher)));

                return addTeacherCommand;
            }
        }

        private RelayCommand addStudentCommand;
        public RelayCommand AddStudentCommand
        {
            get
            {
                if (addStudentCommand == null)
                    addStudentCommand = new RelayCommand(param => ChangeCurrentWorkspace(new StudentViewModel(UserInterop, ControllerInterop, Dispatcher)));

                return addStudentCommand;
            }
        }

        private RelayCommand addMainAdminCommand;
        public RelayCommand AddMainAdminCommand
        {
            get
            {
                if (addMainAdminCommand == null)
                    addMainAdminCommand = new RelayCommand(param => ChangeCurrentWorkspace(new MainAdminViewModel(UserInterop, ControllerInterop, Dispatcher)));

                return addMainAdminCommand;
            }
        }

        private RelayCommand addMainSecretaryCommand;
        public RelayCommand AddMainSecretaryCommand
        {
            get
            {
                if (addMainSecretaryCommand == null)
                    addMainSecretaryCommand = new RelayCommand(param => ChangeCurrentWorkspace(new MainSecretaryViewModel(UserInterop, ControllerInterop, Dispatcher)));

                return addMainSecretaryCommand;
            }
        }

        private RelayCommand generatePasswordCommand;
        public RelayCommand GeneratePasswordCommand
        {
            get
            {
                if (generatePasswordCommand == null)
                    generatePasswordCommand = new RelayCommand(param =>
                    {
                        lock (locker)
                            IsSendingMessage = true;

                        Task.Factory.StartNew(()=>
                            {
                                (CurrentWorkspace.Model as SystemUserModel).Password = PasswordGenerator.Generate();
                                MailMessage message = new MailMessage(Properties.Resources.EmailOwner,(CurrentWorkspace.Model as SystemUserModel).Email, Properties.Resources.EmailSubject, Properties.Resources.EmailText + (CurrentWorkspace.Model as SystemUserModel).Password);
                                MailSender mail = MailSender.GetInstance();
                                if(mail.SendMessage(message))
                                {
                                    UserInterop.ShowMessage(String.Format(Properties.Resources.SuccessSendEmail, (CurrentWorkspace.Model as SystemUserModel).Login, (CurrentWorkspace.Model as SystemUserModel).Email));
                                    Dispatcher.Invoke(new Action(()=>
                                        (CurrentWorkspace as SaveableViewModel).Save()));
                                }
                                else
                                {
                                    UserInterop.ShowMessage(Properties.Resources.ErrorSendEmail);
                                    Dispatcher.Invoke(new Action(()=>
                                        (CurrentWorkspace as SaveableViewModel).Rollback()));
                                }
                                lock(locker)
                                    IsSendingMessage = false;
                            });
                    });

                return generatePasswordCommand;
            }
        }
        
        #endregion

        #region Methods

        private void OnIsSendingMessageChanged()
        {
            if (IsSendingMessageChanged != null)
                IsSendingMessageChanged(this, null);
        }

        protected void DefineAddCommands()
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
            SystemUserDTO user = entity as SystemUserDTO;
            if (user != null)
            {
                switch (user.Role)
                {
                    case UserRoles.MainAdmin:
                        return new MainAdminViewModel(UserInterop, ControllerInterop, Dispatcher, user);
                    case UserRoles.MainSecretary:
                        return new MainSecretaryViewModel(UserInterop, ControllerInterop, Dispatcher, user);
                    case UserRoles.InstituteAdmin:
                        return new InstituteAdminViewModel(UserInterop, ControllerInterop, Dispatcher, user as InstituteAdminDTO);
                    case UserRoles.InstituteSecretary:
                        return new InstituteSecretaryViewModel(UserInterop, ControllerInterop, Dispatcher, user as InstituteSecretaryDTO);
                    case UserRoles.FacultyAdmin:
                        return new FacultyAdminViewModel(UserInterop, ControllerInterop, Dispatcher, user as FacultyAdminDTO);
                    case UserRoles.FacultySecretary:
                        return new FacultySecretaryViewModel(UserInterop, ControllerInterop, Dispatcher, user as FacultySecretaryDTO);
                    case UserRoles.Teacher:
                        return new TeacherViewModel(UserInterop, ControllerInterop, Dispatcher, user as TeacherDTO);
                    case UserRoles.Student:
                        return new StudentViewModel(UserInterop, ControllerInterop, Dispatcher, user as StudentDTO);
                    default:
                        throw new NotImplementedException();
                }
            }
            return null;
        }

        public void UpdateCommandsEnabledState()
        {
            foreach (NamedCommandData ncd in additionalCommands)
                if (ncd.UpdateEnabledState != null)
                    ncd.UpdateEnabledState();
        }

        #endregion

        #region Event

        public EventHandler IsSendingMessageChanged;

        #endregion

        #region Callbacks

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
                    NamedCommandData generatePassword = new NamedCommandData { Command = GeneratePasswordCommand, Name = "Генерувати пароль", IsEnabled = CanGeneratePassword,Type = CommandTypes.GeneratePassword};
                    generatePassword.UpdateEnabledState = () => generatePassword.IsEnabled = CanGeneratePassword;
                    additionalCommands.Add(generatePassword);
                }
                return additionalCommands;
            }
        }

        #endregion


        public bool IsVisible
        {
            get { return true; }
        }
    }
}
