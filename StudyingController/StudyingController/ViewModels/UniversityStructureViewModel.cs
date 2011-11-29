using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    class UniversityStructureViewModel : BaseApplicationViewModel
    {

        #region Fields & Properties

        private Tree structureTree;
        public Tree StructureTree
        {
            get { return structureTree; }
            set
            {
                structureTree = value;
                OnPropertyChanged("StructureTree");
            }
        }

        #endregion

        #region Constructors

        public UniversityStructureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            structureTree = new Tree();
            TreeNode tn1 = new TreeNode { Name = "TreeNode1" };
            TreeNode tn2 = new TreeNode { Name = "SubTreeNode1" };
            tn1.Childs.Add(tn2);
            tn2.Childs.Add(new TreeNode { Name = "SubSubTreeNode1" });
            tn1.Childs.Add(new TreeNode { Name = "SubTreeNode2" });
            structureTree.Nodes.Add(tn1);
            TreeNode tn3 = new TreeNode { Name = "TreeNode3" };
            structureTree.Nodes.Add(tn3);
        }

        #endregion


    }
}
