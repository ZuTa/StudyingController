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
            set { isEnabled = value; }
        } 

        internal void UpdateActivity()
        {
            //isEnabled = 
            OnIsEnabledChanged(null);
            OnPropertyChanged("IsEnabled");
        }

        public delegate void IsEnabledChangedEventHandler(object sender, EventArgs e);
        public event IsEnabledChangedEventHandler IsEnabledChanged;

        private void OnIsEnabledChanged(EventArgs e)
        {
            //IsEnableChangedEventHandler handler = IsEnableChanged;
            if (IsEnabledChanged != null)
                IsEnabledChanged(this, e);
        }
    }
}
