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
    public class ControlStructureViewModel : EditableViewModel, IRefreshable
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
            if (entity is LectureDTO || entity is PracticeTeacherDTO)
            {
                LessonControlsViewModel lessonControlsViewModel = new LessonControlsViewModel(UserInterop, ControllerInterop, Dispatcher, entity);
                lessonControlsViewModel.WorkspaceChanged += new LessonControlsViewModel.ChangeWorkspaceHandler(lessonControlsViewModel_WorkspaceChanged);
                return lessonControlsViewModel;

            }
            
            return null;
        }

        void lessonControlsViewModel_WorkspaceChanged(BaseModel model)
        {
            if (WorkspaceChanged != null)
            {
                if (entitiesProvider.CurrentEntity is LectureDTO && model is ControlModel) WorkspaceChanged(new LectureControlViewModel(UserInterop, ControllerInterop, Dispatcher, (model as ControlModel).ToDTO()));
            }
        }

        #endregion

        #region Events

        public delegate void ChangeWorkspaceHandler(BaseApplicationViewModel viewModel);
        public event ChangeWorkspaceHandler WorkspaceChanged;
        
        #endregion

    }
}
