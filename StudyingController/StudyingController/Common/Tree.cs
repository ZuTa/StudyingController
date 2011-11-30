using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.ViewModels;

namespace StudyingController.Common
{
    public class Tree
    {
        private List<TreeNode> nodes;
        public List<TreeNode> Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }

        public Tree()
        {
            nodes = new List<TreeNode>();
        }
    }

}
