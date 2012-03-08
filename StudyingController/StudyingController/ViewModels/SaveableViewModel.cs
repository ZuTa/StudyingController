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
    public abstract class SaveableViewModel : BaseApplicationViewModel, IEditable, IRefreshable
    {
        #region Fields & Properties

        protected bool isModified;
        public bool IsModified
        {
            get { return isModified; }
        }

        public virtual bool IsValid
        {
            get { return model.IsValid; }
        }

        public bool CanSave
        {
            get
            {
                return IsModified && IsValid;
            }
        }

        private EditModes editMode;
        public EditModes EditMode
        {
            get { return editMode; }
            set { editMode = value; }
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

        public abstract void Save();

        public abstract void Rollback();

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
            if (ViewUnModified != null) OnViewUnModified();
            else UpdateProperties();
        }

        protected virtual void UpdateProperties()
        {
            OnPropertyChanged("IsModified");
            OnPropertyChanged("CanSave");

            if (this is IAdditionalCommands)
                (this as IAdditionalCommands).UpdateCommandsEnabledState();
        }

        protected virtual void OnViewModified()
        {
            if (ViewModified != null)
                ViewModified(this, EventArgs.Empty);
            else
                UpdateProperties();
        }

        protected virtual void OnViewUnModified()
        {
            if (ViewUnModified != null)
                ViewUnModified(this, EventArgs.Empty);
        }

        #endregion

        #region Callbacks

        protected virtual void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetModified();
        }

        #endregion

        #region Events

        public event EventHandler ViewModified;
        public event EventHandler ViewUnModified;

        #endregion

        
    }
}
