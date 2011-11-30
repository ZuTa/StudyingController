using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyingController.Common
{
    public class TreeNode
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private List<TreeNode> childs;
        public List<TreeNode> Childs
        {
            get { return childs; }
            set { childs = value; }
        }

        public TreeNode()
        {
            childs = new List<TreeNode>();
        }
    }
}
