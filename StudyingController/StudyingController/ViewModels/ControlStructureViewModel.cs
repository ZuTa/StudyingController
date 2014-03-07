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
    public class ControlStructureViewModel : EditableViewModel, IExportable
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

        private LectureDataViewModel lectureDataViewModel;
        private PracticeDataViewModel practiceDataViewModel;

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
                if (lectureDataViewModel == null)
                {
                    lectureDataViewModel = new LectureDataViewModel(UserInterop, ControllerInterop, Dispatcher, entity as LectureDTO);
                    lectureDataViewModel.PropertyChanged += lectureDataViewModel_PropertyChanged;
                    lectureDataViewModel.WorkspaceChanged += (vm) =>
                        {
                            if (WorkspaceChanged != null)
                                WorkspaceChanged(vm);
                        };
                }
                else
                    lectureDataViewModel.InitControl(entity as LectureDTO);
                return lectureDataViewModel;
            }

            if (entity is PracticeTeacherDTO)
            {
                if (practiceDataViewModel == null)
                {
                    practiceDataViewModel = new PracticeDataViewModel(UserInterop, ControllerInterop, Dispatcher, entity as PracticeTeacherDTO);
                    practiceDataViewModel.WorkspaceChanged += (vm) =>
                    {
                        if (WorkspaceChanged != null)
                            WorkspaceChanged(vm);
                    };
                }
                else
                    practiceDataViewModel.InitControl(entity as PracticeTeacherDTO);
                return practiceDataViewModel;
            }

            return null;
        }

        private void lectureDataViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedIndex")
                UpdateProperties();
        }

        #endregion

        #region Events

        public event ChangeWorkspaceHandler WorkspaceChanged;
        
        #endregion

        protected override void UpdateProperties()
        {
            base.UpdateProperties();
            OnPropertyChanged("AllowExportToExcel");
        }

        public bool AllowExportToExcel
        {
            get
            {
                return EntitiesProvider.CurrentEntity as LectureDTO != null
                    && lectureDataViewModel != null
                    && lectureDataViewModel.AllowExportToExcel;
            }
        }

        public void ExportToExcel()
        {
            lectureDataViewModel.ExportToExcel();
        }

    }

    public delegate void ChangeWorkspaceHandler(BaseApplicationViewModel viewModel);
}
