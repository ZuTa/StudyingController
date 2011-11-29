using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.SCS;

namespace StudyingController.ViewModels
{
    public class NamedCommandData 
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private RelayCommand command;
        public RelayCommand Command
        {
            get { return command; }
            set { command = value; }
        }
    }
}
