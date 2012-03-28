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
    public class PracticeControlViewModel : SaveableViewModel
    {
        #region Fields & Properties

        private int practiceID;
        public int PracticeID
        {
            get { return practiceID; }
            set { practiceID = value; }
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

        public PracticeControlViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, ControlDTO control, int practiceID)
            : base(userInterop, controllerInterop, dispatcher)
        {
            originalEntity = control;
            this.practiceID = practiceID;

            if (IsUserStudent) mark = ControllerInterop.Service.GetPracticeMark(ControllerInterop.Session, ControllerInterop.User.ID, control.ID);

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
            ControllerInterop.Service.SavePracticeControl(ControllerInterop.Session, Control.ToDTO(), PracticeID);
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
