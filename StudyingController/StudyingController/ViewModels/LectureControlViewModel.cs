using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.ViewModels.Models;
using StudyingController.Common;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public class LectureControlViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public bool IsExisted
        {
            get 
            {
                return OriginalControl.ID != 0;
            }
        }

        public bool IsNotStudent
        {
            get
            {
                return ControllerInterop.User.Role != UserRoles.Student;
            }
        }

        public override bool CanSave
        {
            get
            {
                if (!IsExisted) return base.CanSave;
                return base.CanSave && MarksViewModel != null && MarksViewModel.CanSave || base.CanSave && MarksViewModel == null;
            }
        }

        public override bool IsModified
        {
            get
            {
                return base.IsModified || (MarksViewModel != null && MarksViewModel.IsModified);
            }
        }

        public bool IsUserStudent
        {
            get
            {
                return ControllerInterop.User.Role == UserRoles.Student;
            }
        }

        private decimal mark;
        public decimal Mark
        {
            get { return mark; }
            set { mark = value; }
        }

        private ControlChatViewModel chatViewModel;
        public ControlChatViewModel ChatViewModel
        {
            get { return chatViewModel; }
            set 
            { 
                chatViewModel = value;
                OnPropertyChanged("ChatViewModel");
            }
        }

        private ControlMarksViewModel marksViewModel;
        public ControlMarksViewModel MarksViewModel
        {
            get { return marksViewModel; }
            set 
            { 
                marksViewModel = value;
                OnPropertyChanged("MarksViewModel");
            }
        }

        public BaseEntityDTO OriginalControl
        {
            get
            {
                return originalEntity;
            }
        }

        public ControlModel Control
        {
            get { return Model as LectureControlModel; }
        }

        #endregion

        #region Constructors

        public LectureControlViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, LectureControlDTO control)
            : base(userInterop, controllerInterop, dispatcher)
        {
            originalEntity = control;

            Model = new LectureControlModel(control);


            ChatViewModel = new ControlChatViewModel(UserInterop, ControllerInterop, Dispatcher, control);
            MarksViewModel = new ControlMarksViewModel(UserInterop, ControllerInterop, Dispatcher, control);

            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);

            if (IsExisted) MarksViewModel.ViewModelChanged += new EventHandler(MarksViewModel_ViewModelChanged);
        }

        #endregion

        #region Methods

        protected override void DoRefresh()
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            ControllerInterop.Service.SaveLectureControl(ControllerInterop.Session, (Control as LectureControlModel).ToDTO());
            MarksViewModel.Save();
            SetUnModified();
        }

        public override void Rollback()
        {
            Control.Assign(OriginalControl);
            MarksViewModel.Rollback();
            SetUnModified();
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        protected override void LoadData()
        {
            if (IsUserStudent) mark = ControllerInterop.Service.GetLectureMark(ControllerInterop.Session, ControllerInterop.User.ID, Model.ID);
        }

        protected override void ClearData()
        {
        }

        #endregion
        private void MarksViewModel_ViewModelChanged(object sender, EventArgs e)
        {
            if (MarksViewModel.IsModified)
                SetModified();
            else
                SetUnModified();
        }
    }
}
