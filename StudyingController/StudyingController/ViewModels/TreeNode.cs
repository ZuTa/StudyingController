using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels
{
    public class TreeNode : BaseViewModel
    {
        #region Fields & Properties

        private int imageIndex;
        public int ImageIndex
        {
            get { return imageIndex; }
            set 
            {
                if (imageIndex != value)
                {
                    imageIndex = value;
                    OnPropertyChanged("ImageIndex");
                }
            }
        }

        private int index;
        public int Index
        {
            get { return index; }
            set
            {
                if (index != value)
                {
                    index = value;
                    OnPropertyChanged("Index");
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        private ObservableCollection<TreeNode> childs;
        private ReadOnlyObservableCollection<TreeNode> childsRO;
        public ReadOnlyObservableCollection<TreeNode> Children
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
            set
            {
                if (parentNode != value)
                {
                    parentNode = value;
                    OnPropertyChanged("ParentNode");
                }
            }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        #endregion

        #region Constructors

        public TreeNode(string name, object tag, int index, int imageIndex)
        {
            this.name = name;
            this.tag = tag;
            this.index = index;
            this.imageIndex = imageIndex;

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
