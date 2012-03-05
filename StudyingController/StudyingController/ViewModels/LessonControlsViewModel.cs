using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.Common;
using System.Windows.Threading;
using StudyingController.ViewModels.Models;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels
{
    class LessonControlsViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public BaseEntityDTO OriginalLesson
        {
            get
            {
                return originalEntity;
            }
        }

        public BaseModel Lesson
        {
            get { return Model; }
        }

        private ControlModel currentControl;
        public ControlModel CurrentControl
        {
            get { return currentControl; }
            set 
            { 
                currentControl = value;
                OnPropertyChanged("CurrentControl");
            }
        }

        private ObservableCollection<ControlModel> controls;
        public ObservableCollection<ControlModel> Controls
        {
            get { return controls; }
            set 
            { 
                controls = value;
                OnPropertyChanged("Controls");
            }
        }

        #endregion

        #region Commands

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get 
            {
                if (addCommand == null)
                    addCommand = new RelayCommand((param) => AddControl());
                return addCommand;
            }
        }

        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                if (editCommand == null)
                    editCommand = new RelayCommand((param) => EditControl(), (param) => IsControlSelected());
                return editCommand;

            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                if (removeCommand == null)
                    removeCommand = new RelayCommand((param) => Remove(), (param) => IsControlSelected());
                return removeCommand;

            }
        }


        #endregion

        #region Constructors

        public LessonControlsViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, BaseEntityDTO lesson)
            : base(userInterop, controllerInterop, dispatcher)
        {
            controls = new ObservableCollection<ControlModel>();

            originalEntity = lesson;

            if (lesson is LectureDTO)
            {
                LoadData();
                Model = new LectureModel(lesson as LectureDTO);
            }
            else if (lesson is PracticeTeacherDTO)
            {
                LoadData();
                Model = new PracticeTeacherModel(lesson as PracticeTeacherDTO);
            }

            foreach(ControlModel controlModel in Controls)
                controlModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            if (OriginalLesson is LectureDTO) Controls = ControllerInterop.Service.GetLectureControls(ControllerInterop.Session, OriginalLesson.ID).ToModelList<ControlModel, ControlDTO>();
            if (OriginalLesson is PracticeTeacherDTO) Controls = ControllerInterop.Service.GetPracticeControls(ControllerInterop.Session, (OriginalLesson as PracticeTeacherDTO).PracticeID).ToModelList<ControlModel, ControlDTO>();
        }

        private void ApplyChanges()
        {
            if (OriginalLesson is LectureDTO) ControllerInterop.Service.SaveLectureControls(ControllerInterop.Session, OriginalLesson.ID, Controls.ToDTOList<ControlDTO, ControlModel>());
            if (OriginalLesson is PracticeTeacherDTO) ControllerInterop.Service.SavePracticeControls(ControllerInterop.Session, (OriginalLesson as PracticeTeacherDTO).PracticeID, Controls.ToDTOList<ControlDTO, ControlModel>());
        }

        private bool IsControlSelected()
        {
            return !(currentControl == null);
        }

        private void EditControl()
        {
            if (WorkspaceChanged != null)
                WorkspaceChanged(currentControl);
        }

        private void AddControl()
        {
            Controls.Add(new ControlModel() { Name = "Новий контроль", Date = DateTime.Today });
            CurrentControl = Controls.Last();
            SetModified();
        }

        public override void Save()
        {
            ApplyChanges();
            SetUnModified();
        }

        public override void Rollback()
        {
            LoadData();
            SetUnModified();
        }

        public override void Remove()
        {
            Controls.Remove(CurrentControl);
            SetModified();
        }

        #endregion

        #region Events

        public delegate void ChangeWorkspaceHandler(BaseModel model); 
        public event ChangeWorkspaceHandler WorkspaceChanged;

        #endregion

    }
}
