using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using StudyingController.SCS;

namespace StudyingController.ViewModels
{
    public class BaseUniversityStructureViewModel : LoadableViewModel
    {
        #region Fields & Properties

        private Tree tree;
        public Tree Tree
        {
            get { return tree; }
            set
            {
                tree = value;
                OnPropertyChanged("Tree");
            }
        }

        #endregion

        #region Constructors

        public BaseUniversityStructureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            tree = new Common.Tree();
            tree.Changed += new EventHandler(tree_TreeChanged);

            ReBuildUniversityTree();
        }

        #endregion

        #region Methods

        private void ReBuildUniversityTree()
        {
            lock (tree)
                tree.Clear();

            switch (ControllerInterop.Session.User.UserRole)
            {
                case UserRoles.MainSecretary:
                case UserRoles.MainAdmin:
                    StartLoading(2);

                    ControllerInterop.Service.BeginGetInstitutes(ControllerInterop.Session, OnGetInstisutesCompleted, null);
                    ControllerInterop.Service.BeginGetFaculties(ControllerInterop.Session, null, OnGetFacultiesCompleted, null);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region Callbacks

        private void tree_TreeChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("Tree");
        }

        private void OnGetInstisutesCompleted(IAsyncResult ar)
        {
            Dispatcher.Invoke(
                new Action<IAsyncResult>(iar =>
                {
                    try
                    {
                        List<InstituteDTO> institutes = ControllerInterop.Service.EndGetInstitutes(iar);
                        foreach (var institute in institutes)
                        {
                            lock (tree)
                            {
                                TreeNode node = tree.AppendNode(new TreeNode { Name = institute.Name, Tag = institute }, null);

                                StartLoading();
                                ControllerInterop.Service.BeginGetFaculties(ControllerInterop.Session, institute.ID, OnGetFacultiesCompleted, node);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        StopLoading();
                    }
                }), ar);
        }

        private void OnGetFacultiesCompleted(IAsyncResult ar)
        {
            Dispatcher.Invoke(
               new Action<IAsyncResult>(iar =>
               {
                   try
                   {
                       TreeNode parentNode = ar.AsyncState as TreeNode;

                       var faculties = ControllerInterop.Service.EndGetFaculties(iar);
                       foreach (var faculty in faculties)
                       {
                           lock (tree)
                           {
                               TreeNode node = tree.AppendNode(new TreeNode { Name = faculty.Name, Tag = faculty }, parentNode);

                               StartLoading();
                               ControllerInterop.Service.BeginGetCathedras(ControllerInterop.Session, faculty.ID, OnGetCathedrasCompleted, node);
                           }
                       }
                   }
                   catch (Exception ex)
                   {
                       throw ex;
                   }
                   finally
                   {
                       StopLoading();
                   }
               }), ar);
        }

        private void OnGetCathedrasCompleted(IAsyncResult ar)
        {
            Dispatcher.Invoke(
               new Action<IAsyncResult>(iar =>
               {
                   try
                   {
                       TreeNode parentNode = ar.AsyncState as TreeNode;

                       var cathedras = ControllerInterop.Service.EndGetCathedras(iar);
                       foreach (var cathedra in cathedras)
                       {
                           TreeNode node = tree.AppendNode(new TreeNode { Name = cathedra.Name, Tag = cathedra }, parentNode);
                       }
                   }
                   catch (Exception ex)
                   {
                       throw ex;
                   }
                   finally
                   {
                       StopLoading();
                   }
               }), ar);
        }

        #endregion


    }
}
