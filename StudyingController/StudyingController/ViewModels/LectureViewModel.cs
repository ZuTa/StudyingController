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

        #endregion

        #region Constructors

         public LectureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            originalEntity = new LectureDTO();
            Model = new LectureModel(originalEntity as LectureDTO);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public LectureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, LectureDTO lecture)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalEntity = lecture;
            this.Model = new LectureModel(lecture);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods

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
