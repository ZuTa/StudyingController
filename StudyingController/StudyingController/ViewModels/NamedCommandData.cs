using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.SCS;
using StudyingController.ViewModels;

namespace StudyingController.ViewModels
{
    public class NamedCommandData : BaseViewModel
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

        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set 
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    OnPropertyChanged("IsEnabled");
                }
            }
        }

        private Action updateEnabledState;
        public Action UpdateEnabledState
        {
            get { return updateEnabledState; }
            set { updateEnabledState = value; }
        }
    }
}
