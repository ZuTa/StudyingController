using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using StudyingController.ViewModels.Models;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels
{
    class LectureStuctureViewModel:EditableViewModel,IAdditionalCommands,IRefreshable
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

        public bool IsLecturesViewed
        {
            get 
            {
                if (lastViewModel != CurrentWorkspace && CurrentWorkspace != null)
                    lastViewModel = CurrentWorkspace;
                return CurrentWorkspace is TeacherLecturesViewModel; 
            }
        }

        public SaveableViewModel lastViewModel;

        private bool isLessonsSelect
        {
            get { return (CurrentWorkspace is TeacherLecturesViewModel) || (CurrentWorkspace is TeacherPracticesViewModel); }
        }

        #endregion

        #region Constuctors

        public LectureStuctureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
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

        private RelayCommand loadLecturesViewModel;
        public RelayCommand LoadLecturesViewModel
        {
            get
            {
                if (loadLecturesViewModel == null)
                {
                    loadLecturesViewModel = new RelayCommand(param =>
                        ChangeCurrentWorkspace(new TeacherLecturesViewModel(UserInterop, ControllerInterop, Dispatcher,(CurrentWorkspace as TeacherPracticesViewModel).OriginalTeacher)));
                }
                return loadLecturesViewModel;
            }
        }

        private RelayCommand loadPracticesViewModel;
        public RelayCommand LoadPracticesViewModel
        {
            get
            {
                if (loadPracticesViewModel == null)
                {
                    loadPracticesViewModel = new RelayCommand(param =>
                        ChangeCurrentWorkspace(new TeacherPracticesViewModel(UserInterop, ControllerInterop, Dispatcher, (CurrentWorkspace as TeacherLecturesViewModel).OriginalTeacher)));
                }
                return loadPracticesViewModel;
            }
        }

        #endregion

        #region Methods

        protected override SaveableViewModel GetViewModel(EntitiesDTO.BaseEntityDTO entity)
        {
            if (entity == null)
                return null;
            SaveableViewModel viewModel;
            if (entity is TeacherDTO && !(lastViewModel is TeacherPracticesViewModel))
                viewModel = new TeacherLecturesViewModel(UserInterop, ControllerInterop, Dispatcher, entity as TeacherDTO);
            else if (entity is TeacherDTO)
                viewModel = new TeacherPracticesViewModel(UserInterop, ControllerInterop, Dispatcher, entity as TeacherDTO);
            else if (entity is LectureDTO)
                viewModel = new LectureViewModel(UserInterop, ControllerInterop, Dispatcher, entity as LectureDTO);
            else if (entity is PracticeTeacherDTO)
                viewModel = new PracticeTeacherViewModel(UserInterop, ControllerInterop, Dispatcher, entity as PracticeTeacherDTO);
            else 
                viewModel = null;
            return viewModel;
        }


        #endregion

        private ObservableCollection<NamedCommandData> additionalCommands;
        public ObservableCollection<NamedCommandData> AdditionalCommands
        {
            get
            {
                if (additionalCommands == null)
                {
                    additionalCommands = new ObservableCollection<NamedCommandData>();
                    NamedCommandData lectureNamedCommand = new NamedCommandData 
                    {
                        Command =  LoadLecturesViewModel,
                        IsEnabled = !IsLecturesViewed && isLessonsSelect,
                        Name="Лекції",
                        Image = PictureEnum.Lecture
                    };
                    lectureNamedCommand.UpdateEnabledState = () => lectureNamedCommand.IsEnabled = !IsLecturesViewed && isLessonsSelect;
                    NamedCommandData practiceNamedCommand = new NamedCommandData 
                    {
                        Command = LoadPracticesViewModel,
                        IsEnabled = IsLecturesViewed && isLessonsSelect,
                        Name = "Практика",
                        Image = PictureEnum.Practice
                    };
                    practiceNamedCommand.UpdateEnabledState = () => practiceNamedCommand.IsEnabled = IsLecturesViewed && isLessonsSelect;
                    additionalCommands.Add(lectureNamedCommand);
                    additionalCommands.Add(practiceNamedCommand);
                }
                return additionalCommands;
            }
        }

        public void UpdateCommandsEnabledState()
        {
            foreach (NamedCommandData ncd in additionalCommands)
                if (ncd.UpdateEnabledState != null)
                    ncd.UpdateEnabledState();
        }

        public ReadOnlyObservableCollection<NamedCommandData> AddCommands
        {
            get { throw new NotImplementedException(); }
        }

    }
}
