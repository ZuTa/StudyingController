using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace StudyingController.Common
{
    public class TreeNode
    {
        #region Fields & Properties

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private ObservableCollection<TreeNode> childs;
        private ReadOnlyObservableCollection<TreeNode> childsRO;
        public ReadOnlyObservableCollection<TreeNode> Childs
        {
            get { return childsRO; }
            set { childsRO = value; }
        }

        private object tag;
        public object Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        private TreeNode parentNode;
        public TreeNode ParentNode
        {
            get { return parentNode; }
            set { parentNode = value; }
        }

        #endregion

        #region Constructors

        public TreeNode()
        {
            childs = new ObservableCollection<TreeNode>();
            childsRO = new ReadOnlyObservableCollection<TreeNode>(childs);

            childs.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(childs_CollectionChanged);
        }

        #endregion

        #region Methods

        public void AddChild(TreeNode node)
        {
            this.childs.Add(node);
        }

        #endregion

        #region Callbacks

        private void childs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnChanged();
        }

        #endregion

        #region Events

        protected virtual void OnChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }
        public event EventHandler Changed;

        #endregion
    }
}
