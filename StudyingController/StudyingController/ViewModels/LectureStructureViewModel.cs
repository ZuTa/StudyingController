using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using StudyingController.ViewModels.Models;

namespace StudyingController.ViewModels
{
    class LectureStuctureViewModel:EditableViewModel
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

        #region Constuctors

        public LectureStuctureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            entitiesProvider = new LectureTreeViewModel(userInterop, controllerInterop, dispatcher);
            entitiesProvider.SelectedEntityChangedEvent += new SelectedEntityChangedHandler(EntitesProvider_SelectedEntityChangedEvent);
        }

        #endregion

        #region Commands
        #endregion

        #region Methods

        protected override SaveableViewModel GetViewModel(EntitiesDTO.BaseEntityDTO entity)
        {
            if (entity == null)
                return null;
            SaveableViewModel viewModel;
            if (entity is TeacherDTO)
                viewModel = new TeacherLecturesViewModel(UserInterop, ControllerInterop, Dispatcher, entity as TeacherDTO);
            else if (entity is LectureDTO)
                viewModel = new LectureViewModel(UserInterop, ControllerInterop, Dispatcher, entity as LectureDTO);
            else
                viewModel = null;
            return viewModel;
        }

        #endregion
    }
}
