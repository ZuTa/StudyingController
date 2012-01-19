using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using EntitiesDTO;

namespace StudyingController.ViewModels
{
    public abstract class EditableViewModel : BaseApplicationViewModel
    {
        #region Fields & Properties

        public bool IsModified
        {
            get
            {
                return (CurrentWorkspace == null) ? false : CurrentWorkspace.IsModified;
            }
        }

        public bool IsEnabled
        {
            get
            {
                return !IsModified;
            }
        }

        public bool CanSave
        {
            get 
            {
                return IsModified; 
            }
        }

        public abstract ISelectable EntitiesProvider { get; set; }

        private SaveableViewModel currentWorkspace;
        public SaveableViewModel CurrentWorkspace
        {
            get
            {
                return currentWorkspace;
            }
            set
            {
                if (currentWorkspace != value)
                {
                    currentWorkspace = value;
                    OnPropertyChanged("CurrentWorkspace");
                }
            }
        }

        #endregion

        #region Constructors

        public EditableViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
        }

        #endregion

        #region Methods

        public virtual void Save()
        {
            CurrentWorkspace.Save();
        }

        protected abstract SaveableViewModel GetViewModel(BaseEntityDTO entity);

        protected virtual void ViewModified()
        {
            OnPropertyChanged("CanSave");
            OnPropertyChanged("IsEnabled");
        }

        #endregion

        #region Callbacks

        protected virtual void EntitesProvider_SelectedEntityChangedEvent(object sender, SelectedEntityChangedArgs e)
        {
            if (CurrentWorkspace != null)
                CurrentWorkspace.ViewModified -= ViewModelModified;

            CurrentWorkspace = GetViewModel(EntitiesProvider.CurrentEntity);
            ViewModified();
        }

        protected void ViewModelModified(object sender, EventArgs e)
        {
            ViewModified();
        }

        #endregion

        #region Events
        #endregion
    }

}
