using EntitiesDTO;
using StudyingController.Common;
using StudyingController.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public class PracticeDataViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public override bool CanSave
        {
            get
            {
                return base.CanSave;
                    //&& MarksViewModel != null 
                    //&& MarksViewModel.CanSave 
                    //|| base.CanSave && MarksViewModel == null;
            }
        }

        public override bool IsModified
        {
            get
            {
                return base.IsModified ;//|| (MarksViewModel != null && MarksViewModel.IsModified);
            }
        }

        public bool IsUserStudent
        {
            get
            {
                return ControllerInterop.User.Role == UserRoles.Student;
            }
        }

        private PracticeControlsViewModel practiceControls;
        public PracticeControlsViewModel PracticeControls
        {
            get { return practiceControls; }
            set 
            {
                practiceControls = value;
                OnPropertyChanged("PracticeControls");
            }
        }

        // Here must be visitings
        //private ControlMarksViewModel marksViewModel;
        //public ControlMarksViewModel MarksViewModel
        //{
        //    get { return marksViewModel; }
        //    set 
        //    { 
        //        marksViewModel = value;
        //        OnPropertyChanged("MarksViewModel");
        //    }
        //}

        public BaseEntityDTO OriginalControl
        {
            get
            {
                return originalEntity;
            }
        }

        public PracticeTeacherModel Control
        {
            get { return Model as PracticeTeacherModel; }
        }

        #endregion

        #region Constructors

        public PracticeDataViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, PracticeTeacherDTO lesson)
            : base(userInterop, controllerInterop, dispatcher)
        {
            InitControl(lesson);
        }

        public void InitControl(PracticeTeacherDTO lesson)
        {
            originalEntity = lesson;

            Model = new PracticeTeacherModel(lesson);

            PracticeControls = new PracticeControlsViewModel(UserInterop, ControllerInterop, Dispatcher, lesson) { EditMode = ControllerInterop.User.Role == UserRoles.Student ? EditModes.ReadOnly : EditModes.Editable };
            PracticeControls.ControlOpened += new PracticeControlsViewModel.ControlOpenedHandler(lessonControlsViewModel_WorkspaceChanged);

            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        void lessonControlsViewModel_WorkspaceChanged(BaseModel model)
        {
            if (WorkspaceChanged != null)
            {
                if (model is LectureControlModel) WorkspaceChanged(new LectureControlViewModel(UserInterop, ControllerInterop, Dispatcher, (model as LectureControlModel).ToDTO()) { EditMode = ControllerInterop.User.Role == UserRoles.Student ? EditModes.ReadOnly : EditModes.Editable });
                else if (model is PracticeControlModel) WorkspaceChanged(new PracticeControlViewModel(UserInterop, ControllerInterop, Dispatcher, (model as PracticeControlModel).ToDTO()) { EditMode = ControllerInterop.User.Role == UserRoles.Student ? EditModes.ReadOnly : EditModes.Editable });
            }
        }

        public event ChangeWorkspaceHandler WorkspaceChanged;

        #endregion

        #region Methods

        protected override void DoRefresh()
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            //MarksViewModel.Save();
            SetUnModified();
        }

        public override void Rollback()
        {
            //Control.Assign(OriginalControl);
            //MarksViewModel.Rollback();
            SetUnModified();
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        protected override object LoadDataFromServer()
        {
            return null;
        }

        protected override void AfterDataLoaded()
        {
            base.AfterDataLoaded();
            //if (IsUserStudent) mark = ControllerInterop.Service.GetLectureMark(ControllerInterop.Session, ControllerInterop.User.ID, Model.ID);
            PracticeControls.Load();
        }

        protected override void ClearData()
        {
        }

        #endregion

        //private void MarksViewModel_ViewModelChanged(object sender, EventArgs e)
        //{
        //    if (MarksViewModel.IsModified)
        //        SetModified();
        //    else
        //        SetUnModified();
        //}
    }
}
