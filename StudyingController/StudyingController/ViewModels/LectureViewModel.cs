using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.ViewModels.Models;
using StudyingController.Common;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels
{
    class LectureViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public LectureDTO OriginalLecture
        {
            get { return originalEntity as LectureDTO; }
        }

        public LectureModel Lecture
        {
            get { return Model as LectureModel; }
        }

        private ObservableCollection<GroupDTO> unusedGroups;
        public ObservableCollection<GroupDTO> UnusedGroups
        {
            get { return unusedGroups; }
            set { unusedGroups = value; }
        }

        #endregion

        #region Constructors

         public LectureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalEntity = new LectureDTO();
            this.Model = new LectureModel(originalEntity as LectureDTO);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);

             InitializeGroups();
        }

        public LectureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, LectureDTO lecture)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalEntity = lecture;
            this.Model = new LectureModel(lecture);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);

            InitializeGroups();
        }

        #endregion

        #region Methods

        private void InitializeGroups()
        {
            unusedGroups = new ObservableCollection<GroupDTO>();

            List<GroupDTO> groups = ControllerInterop.Service.GetAllGroups(ControllerInterop.Session);

            foreach (GroupDTO group in groups)
            {
                if (OriginalLecture.Groups.Find(g => g.IsSameDatabaseObject(group)) == null)
                    unusedGroups.Add(group);
            }
        }

        public override void Remove()
        {
            
        }

        public override void Rollback()
        {
            Lecture.Assign(OriginalLecture);
            SetUnModified();
        }

        public override void Save()
        {
            ControllerInterop.Service.SaveLecture(ControllerInterop.Session, Lecture.ToDTO());
            SetUnModified();
        }
        #endregion

        #region Callbacks

        #endregion
    }
}
