using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;

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

        protected override void LoadData()
        {
            BuildUsersTree();
        }

        private void BuildUsersTree()
        {
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

            StartLoading();
            ControllerInterop.Service.BeginGetUsers(ControllerInterop.Session, roles, OnGetUsersCompleted, null);
        }

        #endregion

        #region Callbacks

        private void OnGetUsersCompleted(IAsyncResult ar)
        {
            Dispatcher.Invoke(
               new Action<IAsyncResult>(iar =>
               {
                   List<SystemUserDTO> users = ControllerInterop.Service.EndGetUsers(iar);

                   Dictionary<UserRoles, List<SystemUserDTO>> groups = new Dictionary<UserRoles, List<SystemUserDTO>>();
                   foreach (SystemUserDTO user in users)
                   {
                       if (!groups.ContainsKey(user.Role))
                           groups.Add(user.Role, new List<SystemUserDTO>());
                       groups[user.Role].Add(user);
                   }

                   foreach (UserRoles role in Enum.GetValues(typeof(UserRoles)))
                   {
                       if (role != UserRoles.None)
                       {
                           TreeNode parentNode = Tree.AppendNode(new TreeNode { Name = EnumLocalizeHelper.GetText<PluralizeNameAttribute>(role), Tag = role });

                           if (groups.ContainsKey(role))
                           {
                               foreach (SystemUserDTO user in groups[role])
                                   Tree.AppendNode(new TreeNode { Name = user.UserInformation.ToString(), Tag = user }, parentNode);
                           }
                       }
                   }

                   StopLoading();
               }), ar);
        }

        #endregion
    }
}
