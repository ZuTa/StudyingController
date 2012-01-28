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
        }

        private void BuildUniversityTree()
        {
            StartLoading();

            switch (ControllerInterop.Session.User.Role)
            {
                case UserRoles.MainSecretary:
                case UserRoles.MainAdmin:
                    LoadInstitutes();
                    LoadFaculties(null, null);
                    break;
                case UserRoles.InstituteAdmin:
                case UserRoles.InstituteSecretary:
                    LoadFaculties(null, null);
                    LoadFaculties((ControllerInterop.Session.User as IInstituteable).InstituteID, null);
                    break;
                case UserRoles.FacultyAdmin:
                case UserRoles.FacultySecretary:
                    LoadCathedras((ControllerInterop.Session.User as IFacultyable).FacultyID, null);
                    break;
                default:
                    throw new NotImplementedException("Unknown user's role!");
            }

            StopLoading();
        }

        private void LoadInstitutes()
        {
            List<InstituteDTO> institutes = ControllerInterop.Service.GetInstitutes(ControllerInterop.Session);
            foreach (var institute in institutes)
            {
                lock (Tree)
                {
                    TreeNode node = Tree.AppendNode(new TreeNode(institute.Name, institute, institute.ID, 0));

                    LoadFaculties(institute.ID, node);
                    
                }
            }
        }

        private void LoadFaculties(int? instituteID, TreeNode parentNode)
        {
            List<FacultyDTO> faculties;
            if (instituteID.HasValue)
                faculties = ControllerInterop.Service.GetFaculties(ControllerInterop.Session, instituteID.Value);
            else
                faculties = ControllerInterop.Service.GetFaculties(ControllerInterop.Session, null);

            foreach (var faculty in faculties)
            {
                lock (Tree)
                {
                    TreeNode node = Tree.AppendNode(new TreeNode(faculty.Name, faculty, faculty.ID, 1), parentNode);

                    LoadCathedras(faculty.ID, node);
                  
                }
            }
        }

        private void LoadCathedras(int facultyID, TreeNode parentNode)
        {
            List<CathedraDTO> cathedras = ControllerInterop.Service.GetCathedras(ControllerInterop.Session, facultyID);
            foreach (var cathedra in cathedras)
            {
                TreeNode node = Tree.AppendNode(new TreeNode(cathedra.Name, cathedra, cathedra.ID, 2), parentNode);
            }
        }


        #endregion

        #region Callbacks
        /*
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

        */

        #endregion

        #region Events

        #endregion

    }

}
