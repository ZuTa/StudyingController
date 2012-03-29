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
                LectureControlsViewModel lectureControlsViewModel = new LectureControlsViewModel(UserInterop, ControllerInterop, Dispatcher, entity) { EditMode = ControllerInterop.User.Role == UserRoles.Student ? EditModes.ReadOnly : EditModes.Editable };
                lectureControlsViewModel.ControlOpened += new LectureControlsViewModel.ControlOpenedHandler(lessonControlsViewModel_WorkspaceChanged);
                return lectureControlsViewModel;

            }

            if (entity is PracticeTeacherDTO)
            {
                PracticeControlsViewModel practiceControlsViewModel = new PracticeControlsViewModel(UserInterop, ControllerInterop, Dispatcher, entity) { EditMode = ControllerInterop.User.Role == UserRoles.Student ? EditModes.ReadOnly : EditModes.Editable };
                practiceControlsViewModel.ControlOpened += new PracticeControlsViewModel.ControlOpenedHandler(lessonControlsViewModel_WorkspaceChanged);
                return practiceControlsViewModel;
            }
            
            return null;
        }

        void lessonControlsViewModel_WorkspaceChanged(BaseModel model)
        {
            if (WorkspaceChanged != null)
            {
                if (model is LectureControlModel) WorkspaceChanged(new LectureControlViewModel(UserInterop, ControllerInterop, Dispatcher, (model as LectureControlModel).ToDTO()) { EditMode = ControllerInterop.User.Role == UserRoles.Student ? EditModes.ReadOnly : EditModes.Editable });
                else if (model is PracticeControlModel) WorkspaceChanged(new PracticeControlViewModel(UserInterop, ControllerInterop, Dispatcher, (model as PracticeControlModel).ToDTO()) { EditMode = ControllerInterop.User.Role == UserRoles.Student ? EditModes.ReadOnly : EditModes.Editable });
            }
        }

        #endregion

        #region Events

        public delegate void ChangeWorkspaceHandler(BaseApplicationViewModel viewModel);
        public event ChangeWorkspaceHandler WorkspaceChanged;
        
        #endregion

    }
}
