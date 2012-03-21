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
    public class TeacherLecturesViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public TeacherDTO OriginalTeacher
        {
            get { return originalEntity as TeacherDTO; }
        }

        public TeacherModel Teacher
        {
            get { return Model as TeacherModel; }
        }

        private ObservableCollection<SubjectDTO> usedSubjects;
        public ObservableCollection<SubjectDTO> UsedSubjects
        {
            get { return usedSubjects; }
            set { usedSubjects = value; }
        }

        private ObservableCollection<SubjectDTO> unusedSubjects;
        public ObservableCollection<SubjectDTO> UnusedSubjects
        {
            get { return unusedSubjects; }
            set { unusedSubjects = value; }
        }

        #endregion

        #region Constructors

         public TeacherLecturesViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalEntity = new TeacherDTO();

            this.Model = new TeacherModel(originalEntity as TeacherDTO);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public TeacherLecturesViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, TeacherDTO teacher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalEntity = teacher;

            this.Model = new TeacherModel(teacher);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods

        protected override void DoRefresh()
        {
            throw new NotImplementedException();
        }

        private void InitializeSubjects()
        {
            if (UsedSubjects != null)
                UsedSubjects.CollectionChanged -= usedSubjects_CollectionChanged;


            unusedSubjects = new ObservableCollection<SubjectDTO>();
            usedSubjects = new ObservableCollection<SubjectDTO>();

            List<SubjectDTO> subjects = ControllerInterop.Service.GetSubjects(ControllerInterop.Session, OriginalTeacher.CathedraID);

            foreach (SubjectDTO subject in subjects)
            {
                if (OriginalTeacher.Lectures.Find(g => g.Subject.ID == subject.ID) == null)
                    unusedSubjects.Add(subject);
                else
                    usedSubjects.Add(subject);
            }

            UsedSubjects.CollectionChanged += usedSubjects_CollectionChanged;
        }

        protected override void LoadData()
        {
            InitializeSubjects();
        }

        protected override void ClearData()
        {
            if (UsedSubjects != null)
                UsedSubjects.Clear();

            if (UnusedSubjects != null)
                UnusedSubjects.Clear();
        }

        public override void Remove()
        {
            
        }

        public override void Rollback()
        {
            Teacher.Assign(OriginalTeacher);
            SetUnModified();
        }

        public override void Save()
        {
            ControllerInterop.Service.SaveTeacherSubjects(ControllerInterop.Session, OriginalTeacher.ID, usedSubjects.ToList());
            SetUnModified();
        }


        void usedSubjects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SetModified();
            
            OnPropertyChanged("UsedSubjects");
            OnPropertyChanged("UnusedSubjects");
        }
        #endregion
    }
}
