using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.Common;
using System.Windows.Threading;
using StudyingController.ViewModels.Models;

namespace StudyingController.ViewModels
{
    class TeacherLecturesViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public TeacherDTO OriginalTeacherLecture
        {
            get { return originalEntity as TeacherDTO; }
        }

        public TeacherLecturesModel TeacherLecture
        {
            get { return Model as TeacherLecturesModel; }
        }

        private List<LectureDTO> allLectures;

        public List<LectureDTO> FreeLectures
        {
            get { return GetFreeLectures(UsedLectures); }
        }

        private List<LectureDTO> usedLecture;
        public List<LectureDTO> UsedLectures
        {
            get
            {
                return usedLecture;
            }

            set
            {
                if (!usedLecture.Equals(value))
                {
                    usedLecture = value;
                    OnPropertyChanged("UsedLectures");
                    OnPropertyChanged("FreeLectures");
                }
            }
        }

        #endregion

        #region Constructors

         public TeacherLecturesViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            originalEntity = new TeacherDTO();
            Model = new TeacherLecturesModel(originalEntity as TeacherDTO);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
            usedLecture = ControllerInterop.Service.GetLectures(ControllerInterop.Session, OriginalTeacherLecture.ID);
            //allLectures = 
        }

        public TeacherLecturesViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, TeacherDTO teacher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalEntity = teacher;
            this.Model = new TeacherLecturesModel(teacher);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
            usedLecture = ControllerInterop.Service.GetLectures(ControllerInterop.Session, OriginalTeacherLecture.ID);
        }

        #endregion

        #region Methods

        private List<LectureDTO> GetFreeLectures(List<LectureDTO> used)
        {
            List<LectureDTO> free = new List<LectureDTO>();
            foreach (LectureDTO lecture in allLectures)
                if (!used.Contains(lecture))
                    free.Add(lecture);
            return free;
        }

        public override void Remove()
        {
            
        }

        public override void Rollback()
        {
            
        }

        public override void Save()
        {
            
        }

        #endregion
    }
}
