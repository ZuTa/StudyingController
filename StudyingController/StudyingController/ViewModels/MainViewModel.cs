using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using System.Windows.Input;

namespace StudyingController.ViewModels
{   
    public class MainViewModel : BaseApplicationViewModel
    {
        #region Fields & Properties

        private BaseApplicationViewModel currentWorkspace = null; 

        public BaseApplicationViewModel CurrentWorkspace
        {
            get 
            { 
                return currentWorkspace; 
            }
            set 
            {
                if (currentWorkspace != value)
                    currentWorkspace = value;
                OnPropertyChanged("CurrentWorkspace");
            }
        }

        #endregion

        #region Constructors
        
        public MainViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
        }

        #endregion

        #region Commands

        private RelayCommand universityStructureCommand;
        public ICommand UniversityStructureCommand
        {
            get 
            {
                if (universityStructureCommand == null) universityStructureCommand = new RelayCommand((param) => OpenUniversityStructure());
                return universityStructureCommand;
            }
        }

        #endregion

        #region Methods

        private void OpenUniversityStructure()
        {
            CurrentWorkspace = new UniversityStructureViewModel(UserInterop, ControllerInterop, Dispatcher);
        }

        #endregion
    }
}
