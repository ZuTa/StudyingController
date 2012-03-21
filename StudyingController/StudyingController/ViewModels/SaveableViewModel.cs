using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using StudyingController.ViewModels.Models;
using System.ComponentModel;

namespace StudyingController.ViewModels
{
    public abstract class SaveableViewModel : BaseSaveableViewModel, IRefreshable
    {
        #region Fields & Properties

        protected bool isModified;
        public override bool IsModified
        {
            get { return isModified; }
        }

        public virtual bool IsValid
        {
            get { return model.IsValid; }
        }

        public override bool CanSave
        {
            get
            {
                return base.CanSave && IsValid;
            }
        }

        protected BaseEntityDTO originalEntity;

        protected BaseModel model;
        public BaseModel Model
        {
            get { return model; }
            set { model = value; }
        }
        
        #endregion

        #region Constructor

        public SaveableViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
        }

        #endregion

        #region Methods

        public abstract void Remove();

        protected abstract void DoRefresh();

        public void Refresh()
        {
            DoRefresh();
        }

        protected virtual void SetModified()
        {
            isModified = true;

            OnViewModified(); 
        }

        protected virtual void SetUnModified()
        {
            isModified = false;

            OnViewUnModified();            
        }

        protected virtual void UpdateProperties()
        {
            OnPropertyChanged("IsModified");
            OnPropertyChanged("CanSave");

            if (this is IAdditionalCommands)
                (this as IAdditionalCommands).UpdateCommandsEnabledState();
        }

        protected override void OnViewModified()
        {
            base.OnViewModified();

            UpdateProperties();
        }

        protected override void OnViewUnModified()
        {
            base.OnViewUnModified();

            UpdateProperties();
        }

        #endregion

        #region Callbacks

        protected virtual void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetModified();
        }

        #endregion

        #region Events

        #endregion

        
    }
}
