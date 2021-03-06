﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using StudyingController.SCS;
using EntitiesDTO;
using Common;

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

        protected override object LoadDataFromServer()
        {
            return ControllerInterop.Service.GetUniversityTree(ControllerInterop.Session, ControllerInterop.Session.User.CopyTo(() => new SystemUserRef()));
        }

        protected override void AfterDataLoaded()
        {
            base.AfterDataLoaded();
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
            List<InstituteDTO> institutes = (DataSource as List<BaseEntityDTO>)
                .Where(ds => ds is InstituteDTO)
                .Select(ds => ds as InstituteDTO)
                .ToList();

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
                faculties = (DataSource as List<BaseEntityDTO>)
                    .Where(ds => ds is FacultyDTO && (ds as FacultyDTO).InstituteID != null && (ds as FacultyDTO).InstituteID == instituteID.Value)
                    .Select(ds => ds as FacultyDTO)
                    .ToList();
            else
                faculties = (DataSource as List<BaseEntityDTO>)
                     .Where(ds => ds is FacultyDTO && (ds as FacultyDTO).InstituteID == null)
                     .Select(ds => ds as FacultyDTO)
                     .ToList();

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
            List<CathedraDTO> cathedras = (DataSource as List<BaseEntityDTO>)
                .Where(ds => ds is CathedraDTO && (ds as CathedraDTO).FacultyID == facultyID)
                .Select(ds => ds as CathedraDTO)
                .ToList();
            foreach (var cathedra in cathedras)
            {
                lock (Tree)
                {
                    TreeNode node = Tree.AppendNode(new TreeNode(cathedra.Name, cathedra, cathedra.ID, 2), parentNode);
                    LoadGroups(cathedra.ID, node);
                }
            }
        }

        private void LoadGroups(int cathedraID, TreeNode parentNode)
        {
            List<GroupDTO> groups = ControllerInterop.Service.GetGroups(ControllerInterop.Session, new CathedraRef { ID = cathedraID });
            foreach (var group in groups)
            {
                TreeNode node = Tree.AppendNode(new TreeNode(group.Name, group, group.ID, 3), parentNode);
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
