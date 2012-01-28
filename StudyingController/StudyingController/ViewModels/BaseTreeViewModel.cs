using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.Common;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public abstract class BaseTreeViewModel : LoadableViewModel, IProviderable
    {
        #region Fields & Properties

        private Tree tree;
        public Tree Tree
        {
            get { return tree; }
            protected set 
            {
                if (tree != value)
                {
                    tree = value;
                    OnPropertyChanged("Tree");
                }
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

                    OnPropertyChanged("CurrentEntity");
                    OnSelectedEntityChanged(currentEntity);
                }
            }
        }

        private TreeNode selectedNode;
        public TreeNode SelectedNode
        {
            get { return selectedNode; }
            set { selectedNode = value; }
        }

        protected BaseEntityDTO previousSelectedEntity;

        #endregion

        #region Constructors

        public BaseTreeViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            Tree = new Tree();
            Tree.Changed += new EventHandler(tree_TreeChanged);

            Load();
        }

        #endregion

        #region Commands

        private RelayCommand selectedEntityChangedCommand;
        public RelayCommand SelectedEntityChangedCommand
        {
            get
            {
                if (selectedEntityChangedCommand == null)
                    selectedEntityChangedCommand = new RelayCommand(param => CurrentEntity = param as BaseEntityDTO);
                return selectedEntityChangedCommand;
            }
        }

        #endregion

        #region Methods

        protected IEnumerable<BaseEntityDTO> EnumerateEntities()
        {
            foreach (TreeNode node in tree.Enumerate())
            {
                yield return node.Tag as BaseEntityDTO;
            }
        }

        protected BaseEntityDTO GetActualEntity(BaseEntityDTO entity)
        {
            return EnumerateEntities().FirstOrDefault(e => e.IsSameDatabaseObject(entity));
        }

        public void Refresh()
        {
            Load();
        }

        protected virtual void OnSelectedEntityChanged(BaseEntityDTO entity)
        {
            if (SelectedEntityChangedEvent != null)
                SelectedEntityChangedEvent(this, new SelectedEntityChangedArgs(entity));
        }

        protected override void ClearData()
        {
            previousSelectedEntity = CurrentEntity;

            lock (Tree)
                Tree.Clear();
        }

        protected override void LoadData()
        {
        }

        #endregion

        #region Callbacks

        private void tree_TreeChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("Tree");
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
