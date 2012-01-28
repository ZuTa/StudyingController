using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.ViewModels;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels
{
    public class Tree : IList<TreeNode>
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

        public TreeNode AppendNode(TreeNode node, TreeNode parentNode = null)
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

        public IEnumerable<TreeNode> Enumerate()
        {
            foreach (TreeNode node in nodes)
            {
                List<TreeNode> result = new List<TreeNode>();
                EnumerateNode(node, result);
                foreach (TreeNode n in result)
                    yield return n;
            }
        }

        private  void EnumerateNode(TreeNode node, List<TreeNode> result)
        {
            result.Add(node);
            foreach (TreeNode n in node.Childs)
            {                
                EnumerateNode(n, result);
            }
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

        #region IList

        public int IndexOf(TreeNode item)
        {
            return nodes.IndexOf(item);
        }

        public void Insert(int index, TreeNode item)
        {
            nodes.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            nodes.RemoveAt(index);
        }

        public TreeNode this[int index]
        {
            get
            {
                return nodes[index];
            }
            set
            {
                nodes[index] = value;
            }
        }

        public void Add(TreeNode item)
        {
            nodes.Add(item);
        }

        public bool Contains(TreeNode item)
        {
            return nodes.Contains(item);
        }

        public void CopyTo(TreeNode[] array, int arrayIndex)
        {
            nodes.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return nodes.Count; }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(TreeNode item)
        {
            return nodes.Remove(item);
        }

        public IEnumerator<TreeNode> GetEnumerator()
        {
            return nodes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return nodes.GetEnumerator();
        }

        #endregion
    }

}
