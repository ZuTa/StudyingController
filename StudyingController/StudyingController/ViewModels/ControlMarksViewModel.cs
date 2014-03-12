using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using System.Collections.ObjectModel;
using StudyingController.ViewModels.Models;

namespace StudyingController.ViewModels
{
    public class ControlMarksViewModel: BaseSaveableViewModel
    {
        #region Fields & Properties

        public override bool CanSave
        {
            get
            {
                return IsModelsValid();//base.CanSave && IsModelsValid();
            }
        }

        private ControlDTO control;
        public ControlDTO Control
        {
            get { return control; }
            protected set 
            {
                if (control == null || !control.IsSameDatabaseObject(value))
                {
                    control = value;
                    OnControlChanged();
                }
            }
        }

        private ObservableCollection<MarkModel> marks;
        public ObservableCollection<MarkModel> Marks
        {
            get { return marks; }
            set
            {
                if (marks != value)
                {
                    marks = value;
                    OnPropertyChanged("Marks");
                }
            }
        }

        #endregion

        #region Constructor

        public ControlMarksViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, ControlDTO control)
            : base(userInterop, controllerInterop, dispatcher) 
        {
            this.Control = control;
        }

        #endregion

        protected bool isModified;
        public override bool IsModified
        {
            get 
            {
                return isModified;
            }
        }

        private bool IsModelsValid()
        {
            foreach (var model in Marks)
                if (!model.IsValid)
                    return false;

            return true;
        }


        public override void Save()
        {
            if (isModified)
            {
                if (Control is PracticeControlDTO)
                {
                    List<MarkDTO> practiceMarks = new List<MarkDTO>();
                    foreach (PracticeControlMarkModel pc in Marks)
                        practiceMarks.Add(pc.ToDTO());
                    ControllerInterop.Service.SaveMarks(ControllerInterop.Session, Control as PracticeControlDTO, practiceMarks);
                }
                else
                {
                    List<MarkDTO> lectureMarks = new List<MarkDTO>();
                    foreach (LectureControlMarkModel lc in Marks)
                        lectureMarks.Add(lc.ToDTO());
                    ControllerInterop.Service.SaveMarks(ControllerInterop.Session, Control as LectureControlDTO, lectureMarks);
                }
                ClearData();
                Load();
                SetUnModified();
            }
        }

        public override void Rollback()
        {
            OnControlChanged();
            SetUnModified();
        }

        protected virtual void OnControlChanged()
        {
            ClearData();
            Load();
            SetUnModified();
        }

        protected override object LoadDataFromServer()
        {
            return ControllerInterop.Service.GetMarks(ControllerInterop.Session, control);
        }

        protected override void AfterDataLoaded()
        {
            base.AfterDataLoaded();

            if(Marks!=null)
                Marks.CollectionChanged -= new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Marks_CollectionChanged);
            Marks = new ObservableCollection<MarkModel>();
            foreach (MarkDTO m in (DataSource as List<MarkDTO>))
            {
                if (control is LectureControlDTO)
                {
                    LectureControlMarkModel lcm = new LectureControlMarkModel(m as LectureControlMarkDTO);
                    lcm.ModelChanged += new EventHandler(MarkModelChanged);
                    Marks.Add(lcm);
                }
                else
                {
                    PracticeControlMarkModel pcm = new PracticeControlMarkModel(m as PracticeControlMarkDTO);
                    pcm.ModelChanged += new EventHandler(MarkModelChanged);
                    Marks.Add(pcm);
                }
            }
            Marks.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Marks_CollectionChanged);
        }

        private void Marks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SetModified();
        }

        private void MarkModelChanged(object sender, EventArgs e)
        {
            SetModified();
        }

        protected override void ClearData()
        {
            if (Marks != null)
                Marks.Clear();
        }

        private void UpdateProperties()
        {
            OnPropertyChanged("IsModified");
            OnPropertyChanged("CanSave");
        }

        protected virtual void SetModified()
        {
            isModified = true;

            OnViewModified();

            UpdateProperties();

            OnViewModelChanged();
        }

        protected virtual void SetUnModified()
        {
            isModified = false;

            OnViewUnModified();

            UpdateProperties();

            OnViewModelChanged();
        }

        private void OnViewModelChanged()
        {
            if (ViewModelChanged != null)
                ViewModelChanged(this, null);
        }

        public event EventHandler ViewModelChanged;
    }
}
