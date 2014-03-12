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
    public class LectureControlsViewModel : SaveableViewModel
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

        private LectureControlModel currentControl;
        public LectureControlModel CurrentControl
        {
            get { return currentControl; }
            set 
            { 
                currentControl = value;
                OnPropertyChanged("CurrentControl");
            }
        }

        private ObservableCollection<LectureControlModel> controls;
        public ObservableCollection<LectureControlModel> Controls
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

        public LectureControlsViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, BaseEntityDTO lesson)
            : base(userInterop, controllerInterop, dispatcher)
        {
            controls = new ObservableCollection<LectureControlModel>();

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

        protected override object LoadDataFromServer()
        {
            return ControllerInterop.Service.GetLectureControls(ControllerInterop.Session, OriginalLesson.ID);
        }

        protected override void AfterDataLoaded()
        {
            BaseEntityDTO lesson = originalEntity as BaseEntityDTO;

            if (Controls != null)
            {
                foreach (ControlModel controlModel in Controls)
                    controlModel.PropertyChanged -= ModelPropertyChanged;
            }

            Controls = (DataSource as List<LectureControlDTO>).ToModelList<LectureControlModel, LectureControlDTO>();

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
            OnControlOpened(new LectureControlModel(){LectureID = OriginalLesson.ID, Date = DateTime.Now});
        }

        public override void Save()
        {
            ControllerInterop.Service.SaveLectureControls(ControllerInterop.Session, OriginalLesson.ID, Controls.ToDTOList<LectureControlDTO, LectureControlModel>());
            SetUnModified();
        }

        public override void Rollback()
        {
            Load();
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
