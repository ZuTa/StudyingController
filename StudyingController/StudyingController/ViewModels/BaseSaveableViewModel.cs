using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public enum EditModes
    {
        Editable,
        ReadOnly
    }

    public abstract class BaseSaveableViewModel : LoadableViewModel
    {
        #region Fields & Properties

        public abstract bool IsModified { get;}

        private EditModes editMode;
        public virtual EditModes EditMode
        {
            get { return editMode; }
            set { editMode = value; }
        }

        public virtual bool CanSave
        {
            get
            {
                return IsModified;
            }
        }

        #endregion

        #region Constructors

        public BaseSaveableViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
        }

        #endregion

        #region Methods

        public abstract void Save();

        public abstract void Rollback();

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

        #region Events

        public event EventHandler ViewModified;
        public event EventHandler ViewUnModified;

        #endregion

    }
}
