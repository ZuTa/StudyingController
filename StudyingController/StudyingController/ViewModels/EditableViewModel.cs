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
    public abstract class EditableViewModel : BaseApplicationViewModel, ISaveable
    {
        #region Fields & Properties

        public string HeaderText
        {
            get { return Properties.Resources.UniversityStructureHeaderText; }
        }

        private bool isModified;
        public bool IsModified
        {
            get { return isModified; }
            set
            {
                isModified = value;
                OnPropertyChanged("IsModified");
                OnPropertyChanged("CanSave");
            }
        }

        private bool isReadOnly;
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                isReadOnly = value;
                OnPropertyChanged("IsReadOnly");
                OnPropertyChanged("CanModify");
            }
        }

        public bool CanModify
        {
            get
            {
                return !isReadOnly;
            }
        }

        public bool CanSave
        {
            get
            {
                return !isModified;
            }
        }

        public abstract ISelectable EntitiesProvider { get; set; }

        public BaseApplicationViewModel CurrentWorkspace
        {
            get
            {
                return GetViewModel(EntitiesProvider.CurrentEntity);
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

        public abstract void Save();

        protected virtual void SetModified()
        {
            IsModified = true;
        }

        protected abstract BaseApplicationViewModel GetViewModel(BaseEntityDTO entity);

        #endregion

        #region Callbacks

        protected virtual void EntitesProvider_SelectedEntityChangedEvent(object sender, SelectedEntityChangedArgs e)
        {
            OnPropertyChanged("CurrentWorkspace");
        }

        protected virtual void ModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SetModified();
        }

        #endregion

  
    }
}
