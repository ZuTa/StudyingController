using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;

namespace StudyingController.ViewModels
{
    public class UniversityStructureViewModel : EditableViewModel
    {
        #region Fields & Properties

        public override bool IsModified
        {
            get { throw new NotImplementedException(); }
        }

        private ISelectable entitiesProvider;
        public override ISelectable EntitiesProvider
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

        public UniversityStructureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            entitiesProvider = new UniversityTreeViewModel(userInterop, controllerInterop, dispatcher);
            entitiesProvider.SelectedEntityChangedEvent += new SelectedEntityChangedHandler(EntitesProvider_SelectedEntityChangedEvent);
        }

        #endregion

        #region Methods

        protected override BaseApplicationViewModel GetViewModel(EntitiesDTO.BaseEntityDTO entity)
        {
            if (entity is InstituteDTO)
                return new InstituteViewModel(UserInterop, ControllerInterop, Dispatcher, entity as InstituteDTO);
            if (entity is FacultyDTO)
                return new FacultyViewModel(UserInterop, ControllerInterop, Dispatcher, entity as FacultyDTO);
            if (entity is CathedraDTO)
                return new CathedraViewModel(UserInterop, ControllerInterop, Dispatcher, entity as CathedraDTO);

            return null;
            //throw new NotImplementedException("Unknown entity");
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Callbacks

        #endregion

       
    }
}
