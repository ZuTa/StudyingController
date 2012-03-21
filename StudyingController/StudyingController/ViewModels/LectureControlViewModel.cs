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

        private int lectureID;
        public int LectureID
        {
            get { return lectureID; }
            set { lectureID = value; }
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

        public BaseEntityDTO OriginalControl
        {
            get
            {
                return originalEntity;
            }
        }

        public ControlModel Control
        {
            get { return Model as ControlModel; }
        }

        #endregion

        #region Constructors

        public LectureControlViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, ControlDTO control, int lectureID)
            : base(userInterop, controllerInterop, dispatcher)
        {
            originalEntity = control;
            this.lectureID = lectureID;
            
            Model = new ControlModel(control);

            ChatViewModel = new ControlChatViewModel(UserInterop, ControllerInterop, Dispatcher, control);

            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods

        protected override void DoRefresh()
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            ControllerInterop.Service.SaveLectureControl(ControllerInterop.Session, Control.ToDTO(), LectureID);
            SetUnModified();
        }

        public override void Rollback()
        {
            Control.Assign(OriginalControl);
            SetUnModified();
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        protected override void LoadData()
        {
        }

        protected override void ClearData()
        {
        }

        #endregion
    }
}
