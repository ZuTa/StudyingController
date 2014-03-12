using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using System.Collections.ObjectModel;
using StudyingController.ViewModels.Models;

namespace StudyingController.ViewModels
{
    public class UserProfileViewModel : LoadableViewModel
    {
        #region Fields & Properties

        private SystemUserDTO user;
        public SystemUserDTO User
        {
            get { return user; }
            set 
            {
                user = value;

                OnPropertyChanged("RoleText");
            }
        }

        public DateTime CurrentDate
        {
            get
            {
                return DateTime.Now;
            }
        }

        public DateTime NewYearDate
        {
            get
            {
                return new DateTime(CurrentDate.Year, 1, 1, 0, 0, 1);
            }
        }

        private InstituteDTO institute;
        public InstituteDTO Institute
        {
            get { return institute; }
            set { institute = value; }
        }

        public bool IsInstituteNotNull
        {
            get
            {
                return Institute != null;
            }
        }
        public bool IsFacultyNotNull
        {
            get
            {
                return Faculty != null;
            }
        }
        public bool IsCathedraNotNull
        {
            get
            {
                return Cathedra != null;
            }
        }
        public bool IsGroupNotNull
        {
            get
            {
                return Group != null;
            }
        }
        private FacultyDTO faculty;
        public FacultyDTO Faculty
        {
            get { return faculty; }
            set { faculty = value; }
        }

        private CathedraDTO cathedra;
        public CathedraDTO Cathedra
        {
            get { return cathedra; }
            set { cathedra = value; }
        }

        private GroupDTO group;
        public GroupDTO Group
        {
            get { return group; }
            set { group = value; }
        }

        public string RoleText
        {
            get
            {
                return User.Role.GetText<ResourceNameAttribute>();
            }
        }

        private ObservableCollection<NotificationModel> notifications;
        public ObservableCollection<NotificationModel> Notifications
        {
            get { return notifications; }
            set { notifications = value; }
        }

        #endregion

        #region Constructors

        public UserProfileViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, SystemUserDTO user)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.user = user;
            this.notifications = new ObservableCollection<NotificationModel>();
        }

        #endregion

        #region Methods

        protected override object LoadDataFromServer()
        {
            return null;
        }

        protected override void AfterDataLoaded()
        {
            base.AfterDataLoaded();

            switch (user.Role)
            {
                case UserRoles.MainAdmin:
                case UserRoles.MainSecretary:
                    break;
                case UserRoles.InstituteAdmin:
                    institute = ControllerInterop.Service.GetInstituteByID(ControllerInterop.Session, (user as InstituteAdminDTO).InstituteID);
                    break;
                case UserRoles.InstituteSecretary:
                    institute = ControllerInterop.Service.GetInstituteByID(ControllerInterop.Session, (user as InstituteSecretaryDTO).InstituteID);
                    break;
                case UserRoles.FacultyAdmin:
                    faculty = ControllerInterop.Service.GetFacultyByID(ControllerInterop.Session, (user as FacultyAdminDTO).FacultyID);
                    institute = ControllerInterop.Service.GetInstituteByID(ControllerInterop.Session, faculty.InstituteID);
                    break;
                case UserRoles.FacultySecretary:
                    faculty = ControllerInterop.Service.GetFacultyByID(ControllerInterop.Session, (user as FacultySecretaryDTO).FacultyID);
                    institute = ControllerInterop.Service.GetInstituteByID(ControllerInterop.Session, faculty.InstituteID);
                    break;
                case UserRoles.Teacher:
                    cathedra = ControllerInterop.Service.GetCathedraByID(ControllerInterop.Session, (user as TeacherDTO).Cathedra.ID);
                    faculty = ControllerInterop.Service.GetFacultyByID(ControllerInterop.Session, cathedra.FacultyID);
                    institute = ControllerInterop.Service.GetInstituteByID(ControllerInterop.Session, faculty.InstituteID);
                    break;
                case UserRoles.Student:
                    group = ControllerInterop.Service.GetGroupByID(ControllerInterop.Session, (user as StudentDTO).GroupID);
                    cathedra = ControllerInterop.Service.GetCathedraByID(ControllerInterop.Session, group.CathedraID);
                    faculty = ControllerInterop.Service.GetFacultyByID(ControllerInterop.Session, cathedra.FacultyID);
                    institute = ControllerInterop.Service.GetInstituteByID(ControllerInterop.Session, faculty.InstituteID);
                    break;
            }

            this.LoadNotifications();

            OnPropertyChanged("RoleText");

            OnPropertiesChanged();
        }

        protected override void ClearData()
        {
            institute = null;
            faculty = null;
            group = null;
            cathedra = null;
        }

        private void OnPropertiesChanged()
        {
            OnPropertyChanged("Institute");
            OnPropertyChanged("IsInstituteNotNull");
            OnPropertyChanged("Faculty");
            OnPropertyChanged("IsFacultyNotNull");
            OnPropertyChanged("Cathedra");
            OnPropertyChanged("IsCathedraNotNull");
            OnPropertyChanged("Group");
            OnPropertyChanged("IsGroupNotNull");
        }

        private void LoadNotifications()
        {
            this.notifications.Clear();

            foreach (var notification in ControllerInterop.Service.GetNotifications(ControllerInterop.Session, user.ID))
            {
                this.notifications.Add(new NotificationModel(notification));
            }
        }

        #endregion
    }
}
