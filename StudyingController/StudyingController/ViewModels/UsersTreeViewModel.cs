using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using System.Diagnostics;

namespace StudyingController.ViewModels
{
    public class UsersTreeViewModel : BaseTreeViewModel
    {
        #region Fields & Properties

        #endregion

        #region Constructors

        public UsersTreeViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
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
            BuildUsersTree();
        }

        private void BuildUsersTree()
        {
            ClearData();
            string data = "";
            Stopwatch sw = new Stopwatch();
            sw.Start();
            UserRoles roles = UserRoles.None;
         
            switch (ControllerInterop.Session.User.Role)
            {
                case UserRoles.MainAdmin:
                    roles = UserRoles.MainAdmin | UserRoles.MainSecretary | UserRoles.InstituteAdmin | UserRoles.InstituteSecretary | UserRoles.FacultyAdmin | UserRoles.FacultySecretary | UserRoles.Student | UserRoles.Teacher;
                    break;
                case UserRoles.MainSecretary:
                    roles = UserRoles.MainSecretary | UserRoles.InstituteAdmin | UserRoles.InstituteSecretary | UserRoles.FacultyAdmin | UserRoles.FacultySecretary | UserRoles.Student | UserRoles.Teacher;
                    break;
                case UserRoles.InstituteAdmin:
                    roles = UserRoles.InstituteAdmin | UserRoles.InstituteSecretary | UserRoles.FacultyAdmin | UserRoles.FacultySecretary | UserRoles.Student | UserRoles.Teacher;
                    break;
                case UserRoles.InstituteSecretary:
                    roles = UserRoles.InstituteSecretary | UserRoles.FacultyAdmin | UserRoles.FacultySecretary | UserRoles.Student | UserRoles.Teacher;
                    break;
                case UserRoles.FacultyAdmin:
                    roles = UserRoles.FacultyAdmin | UserRoles.FacultySecretary | UserRoles.Student | UserRoles.Teacher;
                    break;
                case UserRoles.FacultySecretary:
                    roles = UserRoles.FacultySecretary | UserRoles.Student | UserRoles.Teacher;
                    break;
                default:
                    throw new NotImplementedException("Unknown user's role!");
            }

            data += sw.ElapsedMilliseconds + " - ";
            StartLoading();
            data += sw.ElapsedMilliseconds + " - ";
            List<SystemUserDTO> users = ControllerInterop.Service.GetUsers(ControllerInterop.Session, roles);
            data += sw.ElapsedMilliseconds + " - ";
            Dictionary<UserRoles, List<SystemUserDTO>> groups = new Dictionary<UserRoles, List<SystemUserDTO>>();
            foreach (SystemUserDTO user in users)
            {
                if (!groups.ContainsKey(user.Role))
                    groups.Add(user.Role, new List<SystemUserDTO>());
                groups[user.Role].Add(user);
            }
            data += sw.ElapsedMilliseconds + " - ";
            foreach (UserRoles role in Enum.GetValues(typeof(UserRoles)))
            {
                if (role != UserRoles.None)
                {
                    TreeNode parentNode = Tree.AppendNode(new TreeNode(EnumLocalizeHelper.GetText<PluralizeNameAttribute>(role), role, (int)role, 0));

                    if (groups.ContainsKey(role))
                    {
                        foreach (SystemUserDTO user in groups[role])
                            Tree.AppendNode(new TreeNode(user.Name, user, user.ID, 1), parentNode);
                    }
                }
            }
            data += sw.ElapsedMilliseconds + " - ";
            StopLoading();
            data += sw.ElapsedMilliseconds + " - ";

            //System.Windows.Forms.MessageBox.Show(data);
        }

        #endregion

        #region Callbacks
        #endregion
    }
}
