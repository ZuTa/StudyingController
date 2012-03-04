using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using System.Collections.ObjectModel;
using StudyingController.ViewModels.Models;
using System.IO;

namespace StudyingController.ViewModels
{
    public class AttachmentsViewModel : LoadableViewModel, IProviderable
    {
        #region Fields & Properties

        private ObservableCollection<AttachmentDTO> attachments;
        public ObservableCollection<AttachmentDTO> Attachments
        {
            get { return attachments; }
            set 
            {
                if (attachments != value)
                {
                    attachments = value;
                    OnPropertyChanged("Attachments");
                }
            }
        }

        private BaseEntityDTO currentEntity;
        public BaseEntityDTO CurrentEntity
        {
            get { return currentEntity; }
            set
            {
                if (currentEntity != value)
                {
                    currentEntity = value;
                    OnPropertyChanged("CurrentEntity");
                    OnSelectedEntityChanged(currentEntity);
                }
            }
        }


        protected ObservableCollection<AttachmentDTO> previousAttachments;
        protected BaseEntityDTO previousSelectedEntity;

        #endregion

        #region Constructors

        public AttachmentsViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            //Attachment = new AttachmentViewModel(UserInterop, ControllerInterop, Dispatcher);
            //InitializeAttachments(null);
            LoadData();
        }

        public AttachmentsViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, TeacherDTO teacher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            //Attachment = new AttachmentViewModel(UserInterop, ControllerInterop, Dispatcher);
            //InitializeAttachments(teacher.ID);
            LoadData();
        }

        #endregion

        #region Commands
        #endregion

        #region Methods

        private void InitializeAttachments(int? teacherID)
        {
            if (teacherID == null)
            {
                Attachments = new ObservableCollection<AttachmentDTO>(ControllerInterop.Service.GetAttachments(ControllerInterop.Session, ControllerInterop.User.ID));
            }
            else
            {
                Attachments = new ObservableCollection<AttachmentDTO>(ControllerInterop.Service.GetAttachments(ControllerInterop.Session, (int)teacherID));
            }
        }

        private void OnSelectedEntityChanged(BaseEntityDTO entity)
        {
            if (SelectedEntityChangedEvent != null)
                SelectedEntityChangedEvent(this, new SelectedEntityChangedArgs(entity));
        }

        protected override void LoadData()
        {
            InitializeAttachments(null);
        }

        protected override void ClearData()
        {
            previousSelectedEntity = CurrentEntity;
            previousAttachments = Attachments;
            Attachments = new ObservableCollection<AttachmentDTO>();
        }

        public void Refresh()
        {
            ClearData();
            LoadData();
            CurrentEntity = Attachments.Where(a => previousAttachments.ToList().Find(p => a.ID == p.ID) == null).FirstOrDefault();
            if(CurrentEntity==null)
                if (previousSelectedEntity != null)
                    CurrentEntity = (from a in Attachments
                                     where a.ID == previousSelectedEntity.ID
                                     select a).FirstOrDefault();
                else
                    CurrentEntity = null;
        }

        #endregion

        #region Callbacks
        #endregion

        #region Events

        public event SelectedEntityChangedHandler SelectedEntityChangedEvent;

        #endregion
    }
}
