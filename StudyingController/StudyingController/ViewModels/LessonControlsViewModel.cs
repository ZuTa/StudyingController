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
    public class LessonControlsViewModel : SaveableViewModel
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

        #region Constructors

        public LessonControlsViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, BaseEntityDTO lesson)
            : base(userInterop, controllerInterop, dispatcher)
        {
            controls = new ObservableCollection<ControlModel>();

            originalEntity = lesson;
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

        #region Methods

        protected override void DoRefresh()
        {
            throw new NotImplementedException();
        }

        protected override void LoadData()
        {
            BaseEntityDTO lesson = originalEntity as BaseEntityDTO;

            if (Controls != null)
            {
                foreach (ControlModel controlModel in Controls)
                    controlModel.PropertyChanged -= ModelPropertyChanged;
            }

            if (OriginalLesson is LectureDTO)
                Controls = ControllerInterop.Service.GetLectureControls(ControllerInterop.Session, OriginalLesson.ID).ToModelList<ControlModel, ControlDTO>();
            else if (OriginalLesson is PracticeTeacherDTO)
                Controls = ControllerInterop.Service.GetPracticeControls(ControllerInterop.Session, (OriginalLesson as PracticeTeacherDTO).PracticeID).ToModelList<ControlModel, ControlDTO>();

            foreach (ControlModel controlModel in Controls)
                controlModel.PropertyChanged += ModelPropertyChanged;

            if (lesson is LectureDTO)
                Model = new LectureModel(lesson as LectureDTO);
            else if (lesson is PracticeTeacherDTO)
                Model = new PracticeTeacherModel(lesson as PracticeTeacherDTO);

        }

        protected override void ClearData()
        {
            if (Controls != null)
                Controls.Clear();
        }

        private bool IsControlSelected()
        {
            return !(currentControl == null);
        }

        private void EditControl()
        {
            OnControlOpened(currentControl);
        }

        private void AddControl()
        {
            OnControlOpened(new ControlModel());
        }

        public override void Save()
        {
            if (OriginalLesson is LectureDTO)
                ControllerInterop.Service.SaveLectureControls(ControllerInterop.Session, OriginalLesson.ID, Controls.ToDTOList<ControlDTO, ControlModel>());
            else if (OriginalLesson is PracticeTeacherDTO)
                ControllerInterop.Service.SavePracticeControls(ControllerInterop.Session, (OriginalLesson as PracticeTeacherDTO).PracticeID, Controls.ToDTOList<ControlDTO, ControlModel>());

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

        protected virtual void OnControlOpened(ControlModel model)
        {
            if (ControlOpened != null)
                ControlOpened(model);
        }

        #endregion

        #region Events

        public delegate void ControlOpenedHandler(BaseModel model); 

        public event ControlOpenedHandler ControlOpened;

        #endregion
    }
}
