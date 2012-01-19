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
    public abstract class BaseUniversityTreeViewModel : LoadableViewModel, ISelectable, IRefreshable
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

        private BaseEntityDTO currentEntity;
        public BaseEntityDTO CurrentEntity
        {
            get { return currentEntity; }
            set 
            {
                if (currentEntity != value)
                {
                    currentEntity = value;

                    SelectedEntityChanged(currentEntity);
                    OnPropertyChanged("CurrentEntity");
                }
            }
        }

        #endregion

        #region Constructors

        public BaseUniversityTreeViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            tree = new Tree();
            tree.Changed += new EventHandler(tree_TreeChanged);

            ReBuildUniversityTree();
        }

        #endregion

        #region Commands

        private RelayCommand selectedEntityChangedCommand;
        public RelayCommand SelectedEntityChangedCommand
        {
            get 
            {
                if (selectedEntityChangedCommand == null)
                    selectedEntityChangedCommand = new RelayCommand(param => SelectedEntityChanged(param as BaseEntityDTO));
                return selectedEntityChangedCommand; 
            }
        }

        #endregion

        #region Methods

        private void SelectedEntityChanged(BaseEntityDTO entity)
        {
            currentEntity = entity;
            OnSelectedEntityChanged(entity as BaseEntityDTO);
        }

        protected virtual void OnSelectedEntityChanged(BaseEntityDTO entity)
        {
            if (SelectedEntityChangedEvent != null)
                SelectedEntityChangedEvent(this, new SelectedEntityChangedArgs(entity));
        }

        private void ReBuildUniversityTree()
        {
            lock (tree)
                tree.Clear();

            switch (ControllerInterop.Session.User.UserRole)
            {
                case UserRoles.MainSecretary:
                case UserRoles.MainAdmin:
                    StartLoading();

                    ControllerInterop.Service.BeginGetInstitutes(ControllerInterop.Session, OnGetInstisutesCompleted, null);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void Refresh()
        {
            ReBuildUniversityTree();
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
                        //Get faculties without institute
                        StartLoading();
                        ControllerInterop.Service.BeginGetFaculties(ControllerInterop.Session, null, OnGetFacultiesCompleted, null);

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

        #region Events

        public event SelectedEntityChangedHandler SelectedEntityChangedEvent;

        #endregion

    }

    public delegate void SelectedEntityChangedHandler(object sender, SelectedEntityChangedArgs e);
    public class SelectedEntityChangedArgs
    {
        private BaseEntityDTO _value;
        public BaseEntityDTO Value
        {
            get { return _value; }
        }

        public SelectedEntityChangedArgs(BaseEntityDTO entity)
        {
            this._value = entity;
        }
    }
}
