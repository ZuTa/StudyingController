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
    public class LectureDataViewModel : SaveableViewModel, IExportable
    {
        #region Fields & Properties

        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        public bool IsExisted
        {
            get
            {
                return OriginalControl.ID != 0;
            }
        }

        public override bool CanSave
        {
            get
            {
                return base.CanSave
                    && LectureVisitings != null 
                    && LectureVisitings.CanSave 
                    || base.CanSave && LectureVisitings == null;
            }
        }

        public override bool IsModified
        {
            get
            {
                return base.IsModified || (LectureVisitings != null && LectureVisitings.IsModified);
            }
        }

        public bool IsUserStudent
        {
            get
            {
                return ControllerInterop.User.Role == UserRoles.Student;
            }
        }

        private LectureControlsViewModel lectureControls;
        public LectureControlsViewModel LectureControls
        {
            get { return lectureControls; }
            set 
            { 
                lectureControls = value;
                OnPropertyChanged("LectureControls");
            }
        }

        private LectureVisitingsViewModel lectureVisitings;
        public LectureVisitingsViewModel LectureVisitings
        {
            get { return lectureVisitings; }
            set 
            {
                lectureVisitings = value;
                OnPropertyChanged("LectureVisitings");
            }
        }

        public BaseEntityDTO OriginalControl
        {
            get
            {
                return originalEntity;
            }
        }

        public LectureModel Control
        {
            get { return Model as LectureModel; }
        }

        #endregion

        #region Constructors

        public LectureDataViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, LectureDTO lesson)
            : base(userInterop, controllerInterop, dispatcher)
        {
            InitControl(lesson);
        }

        public void InitControl(LectureDTO lesson)
        {
            originalEntity = lesson;

            Model = new LectureModel(lesson);

            LectureControls = new LectureControlsViewModel(UserInterop, ControllerInterop, Dispatcher, lesson) { EditMode = ControllerInterop.User.Role == UserRoles.Student ? EditModes.ReadOnly : EditModes.Editable };
            LectureControls.ControlOpened += new LectureControlsViewModel.ControlOpenedHandler(lessonControlsViewModel_WorkspaceChanged);

            LectureVisitings = new LectureVisitingsViewModel(UserInterop, ControllerInterop, Dispatcher, lesson);

            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);

            if (IsExisted)
            {
                LectureVisitings.ViewModified += LectureVisitings_ViewChanged;
                LectureVisitings.ViewUnModified += LectureVisitings_ViewChanged;
            }
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
            LectureVisitings.Save();
            SetUnModified();
        }

        public override void Rollback()
        {
            //Control.Assign(OriginalControl);
            LectureVisitings.Rollback();
            SetUnModified();
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        protected override void LoadData()
        {
            LectureControls.Load();
            LectureVisitings.Load();
        }

        protected override void ClearData()
        {
        }

        #endregion

        void LectureVisitings_ViewChanged(object sender, EventArgs e)
        {
            if (LectureVisitings.IsModified)
                SetModified();
            else
                SetUnModified();
        }

        public bool AllowExportToExcel
        {
            get { return SelectedIndex == 1 && LectureVisitings.AllowExportToExcel; }
        }

        public void ExportToExcel()
        {
            LectureVisitings.ExportToExcel();
        }
    }
}
