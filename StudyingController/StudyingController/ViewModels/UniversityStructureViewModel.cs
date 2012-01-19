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

        protected override SaveableViewModel GetViewModel(EntitiesDTO.BaseEntityDTO entity)
        {
            if (entity == null)
                return null;

            SaveableViewModel viewModel;
            if (entity is InstituteDTO)
                viewModel = new InstituteViewModel(UserInterop, ControllerInterop, Dispatcher, entity as InstituteDTO);
            else if (entity is FacultyDTO)
                viewModel = new FacultyViewModel(UserInterop, ControllerInterop, Dispatcher, entity as FacultyDTO);
            else if (entity is CathedraDTO)
                viewModel = new CathedraViewModel(UserInterop, ControllerInterop, Dispatcher, entity as CathedraDTO);
            else
                throw new NotImplementedException("Unknown entity");

            viewModel.ViewModified += new EventHandler(ViewModelModified);

            return viewModel;
        }

        public override void Save()
        {
            base.Save();

        }

        #endregion

        #region Callbacks

        #endregion

       
    }
}
