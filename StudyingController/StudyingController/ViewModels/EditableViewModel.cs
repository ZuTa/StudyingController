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

        public string HeaderText
        {
            get { return Properties.Resources.UniversityStructureHeaderText; }
        }

        public abstract bool IsModified
        {
            get;
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

        protected abstract BaseApplicationViewModel GetViewModel(BaseEntityDTO entity);

        #endregion

        #region Callbacks

        protected virtual void EntitesProvider_SelectedEntityChangedEvent(object sender, SelectedEntityChangedArgs e)
        {
            OnPropertyChanged("CurrentWorkspace");
        }

        protected virtual void ModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
         
        }

        #endregion

  
    }
}
