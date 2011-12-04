using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.ViewModels;
using System.Collections.ObjectModel;

namespace StudyingController.Common
{
    public class Tree
    {
        #region Fields & Properties

        private ObservableCollection<TreeNode> nodes;
        private ReadOnlyObservableCollection<TreeNode> nodesRO;
        public ReadOnlyObservableCollection<TreeNode> Nodes
        {
            get { return nodesRO; }
        }

        #endregion

        #region Constructors

        public Tree()
        {
            nodes = new ObservableCollection<TreeNode>();
            nodesRO = new ReadOnlyObservableCollection<TreeNode>(nodes);

            nodes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(nodes_CollectionChanged);
        }

        #endregion

        #region Callbacks

        private void nodes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnChanged();
        }

        void node_Changed(object sender, EventArgs e)
        {
            OnChanged();
        }


        #endregion

        #region Methods

        public TreeNode AppendNode(TreeNode node, TreeNode parentNode)
        {
            node.Changed += new EventHandler(node_Changed);
            if (parentNode != null)
            {
                parentNode.AddChild(node);
                node.ParentNode = parentNode;
            }
            else
                nodes.Add(node);

            return node;
        }

        public void Clear()
        {
            this.nodes.Clear();
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
