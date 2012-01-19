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
    public abstract class SaveableViewModel : BaseApplicationViewModel, IEditable
    {
        #region Fields & Properties

        protected bool isModified;
        public bool IsModified
        {
            get { return isModified; }
        }

        protected BaseEntityDTO originalEntity;

        protected BaseModel model;
        
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

        protected virtual void OnViewModified()
        {
            if (ViewModified != null)
                ViewModified(this, EventArgs.Empty);
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
