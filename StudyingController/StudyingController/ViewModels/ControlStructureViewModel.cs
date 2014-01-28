using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using EntitiesDTO;
using StudyingController.ViewModels.Models;

namespace StudyingController.ViewModels
{
    public class ControlStructureViewModel : EditableViewModel
    {
        #region Fields & Properties

        private IProviderable entitiesProvider;
        public override IProviderable EntitiesProvider
        {
            get { return entitiesProvider; }
            set
            {
                if (entitiesProvider != value)
                {
                    entitiesProvider = value;
                    OnPropertyChanged("EntitiesProvider");
                }
            }
        }

        #endregion

        #region Constructors

        public ControlStructureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            entitiesProvider = new LessonTreeViewModel(userInterop, controllerInterop, dispatcher);
            entitiesProvider.SelectedEntityChangedEvent += new SelectedEntityChangedHandler(EntitesProvider_SelectedEntityChangedEvent);
        }

        #endregion


        #region Commands

        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                    updateCommand = new RelayCommand(param =>
                    {
                        EntitiesProvider.Refresh();
                    });
                return updateCommand;
            }
        }

        #endregion

        #region Methods

        protected override SaveableViewModel GetViewModel(EntitiesDTO.BaseEntityDTO entity)
        {
            if (entity is LectureDTO)
            {
                LectureDataViewModel lectureDataViewModel = new LectureDataViewModel(UserInterop, ControllerInterop, Dispatcher, entity as LectureDTO);
                lectureDataViewModel.WorkspaceChanged += (vm) =>
                    {
                        if (WorkspaceChanged != null)
                            WorkspaceChanged(vm);
                    };
                return lectureDataViewModel;
            }

            if (entity is PracticeTeacherDTO)
            {
                PracticeDataViewModel practiceDataViewModel = new PracticeDataViewModel(UserInterop, ControllerInterop, Dispatcher, entity as PracticeTeacherDTO);
                practiceDataViewModel.WorkspaceChanged += (vm) =>
                {
                    if (WorkspaceChanged != null)
                        WorkspaceChanged(vm);
                };
                return practiceDataViewModel;
            }
            
            return null;
        }

        #endregion

        #region Events

        public event ChangeWorkspaceHandler WorkspaceChanged;
        
        #endregion
    }

    public delegate void ChangeWorkspaceHandler(BaseApplicationViewModel viewModel);
}
