using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using StudyingController.SCS;
using EntitiesDTO;

namespace StudyingController.ViewModels
{
    public class UniversityTreeViewModel : BaseTreeViewModel
    {
        #region Fields & Properties

        #endregion

        #region Constructors

        public UniversityTreeViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {

        }

        #endregion

        #region Commands

        #endregion

        #region Methods

        protected override void LoadData()
        {
            base.LoadData();

            BuildUniversityTree();

            CurrentEntity = previousSelectedEntity;
        }

        private void BuildUniversityTree()
        {
            switch (ControllerInterop.Session.User.Role)
            {
                case UserRoles.MainSecretary:
                case UserRoles.MainAdmin:
                    StartLoading();

                    ControllerInterop.Service.BeginGetInstitutes(ControllerInterop.Session, OnGetInstisutesCompleted, null);
                    break;
                case UserRoles.InstituteAdmin:
                case UserRoles.InstituteSecretary:
                    StartLoading();

                    ControllerInterop.Service.BeginGetFaculties(ControllerInterop.Session, (ControllerInterop.Session.User as IInstituteable).InstituteID, OnGetFacultiesCompleted, null);
                    break;
                case UserRoles.FacultyAdmin:
                case UserRoles.FacultySecretary:
                    StartLoading();

                    ControllerInterop.Service.BeginGetCathedras(ControllerInterop.Session, (ControllerInterop.Session.User as IFacultyable).FacultyID, OnGetCathedrasCompleted, null);
                    break;
                default:
                    throw new NotImplementedException("Unknown user's role!");
            }
        }

        #endregion

        #region Callbacks

        private void OnGetInstisutesCompleted(IAsyncResult ar)
        {
            Dispatcher.Invoke(
                new Action<IAsyncResult>(iar =>
                {
                    try
                    {
                        //Get faculties without institute
                        StartLoading();
                        ControllerInterop.Service.BeginGetFaculties(ControllerInterop.Session, null, OnGetFacultiesCompleted, null);

                        List<InstituteDTO> institutes = ControllerInterop.Service.EndGetInstitutes(iar);
                        foreach (var institute in institutes)
                        {
                            lock (Tree)
                            {
                                TreeNode node = Tree.AppendNode(new TreeNode { Name = institute.Name, Tag = institute });

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
                           lock (Tree)
                           {
                               TreeNode node = Tree.AppendNode(new TreeNode { Name = faculty.Name, Tag = faculty }, parentNode);

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
                           TreeNode node = Tree.AppendNode(new TreeNode { Name = cathedra.Name, Tag = cathedra }, parentNode);
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

        #region Events

        #endregion

    }

}
