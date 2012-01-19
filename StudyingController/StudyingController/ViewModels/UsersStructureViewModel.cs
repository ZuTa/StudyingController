using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;

namespace StudyingController.ViewModels
{
    public class UsersStructureViewModel : EditableViewModel
    { 
        #region Fields & Properties

        private IProviderable entitiesProvider;
        public override IProviderable EntitiesProvider
        {
            get { return entitiesProvider; }
            set
            {
                if (entitiesProvider != value)
                {
                    entitiesProvider = value;
                    OnPropertyChanged("EntitiesProvider");
                }
            }
        }

        #endregion

        #region Constructors

        public UsersStructureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            entitiesProvider = new UniversityTreeViewModel(userInterop, controllerInterop, dispatcher);
            entitiesProvider.SelectedEntityChangedEvent += new SelectedEntityChangedHandler(EntitesProvider_SelectedEntityChangedEvent);
        }

        #endregion

        #region Methods

        protected override SaveableViewModel GetViewModel(EntitiesDTO.BaseEntityDTO entity)
        {
            if (entity is SystemUserDTO)
                return null;

            return null;
            //throw new NotImplementedException("Unknown entity");
        }

        #endregion

        #region Callbacks

        #endregion

    }
}
