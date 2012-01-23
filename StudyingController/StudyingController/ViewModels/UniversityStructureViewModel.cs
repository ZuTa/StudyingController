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

        public UniversityStructureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            entitiesProvider = new UniversityTreeViewModel(userInterop, controllerInterop, dispatcher);
            entitiesProvider.SelectedEntityChangedEvent += new SelectedEntityChangedHandler(EntitesProvider_SelectedEntityChangedEvent);
        }

        #endregion

        #region Commands

        private RelayCommand addInstituteCommand;
        public RelayCommand AddInstituteCommand
        {
            get
            {
                if (addInstituteCommand == null)
                    addInstituteCommand = new RelayCommand(param =>
                    {
                        ChangeCurrentWorkspace(new InstituteViewModel(UserInterop, ControllerInterop, Dispatcher));
                    });
                return addInstituteCommand;
            }
        }

        private RelayCommand addFacultyCommand;
        public RelayCommand AddFacultyCommand
        {
            get
            {
                if (addFacultyCommand == null)
                    addFacultyCommand = new RelayCommand(param =>
                    {
                        ChangeCurrentWorkspace(new FacultyViewModel(UserInterop, ControllerInterop, Dispatcher));
                    });
                return addFacultyCommand;
            }
        }

        private RelayCommand addCathedraCommand;
        public RelayCommand AddCathedraCommand
        {
            get
            {
                if (addCathedraCommand == null)
                    addCathedraCommand = new RelayCommand(param =>
                    {
                        ChangeCurrentWorkspace(new CathedraViewModel(UserInterop, ControllerInterop, Dispatcher));
                    });
                return addCathedraCommand;
            }
        }

        private RelayCommand addGroupCommand;
        public RelayCommand AddGroupCommand
        {
            get
            {
                if (addGroupCommand == null)
                    addGroupCommand = new RelayCommand(param =>
                    {
                        ChangeCurrentWorkspace(new GroupViewModel(UserInterop, ControllerInterop, Dispatcher));
                    });
                return addGroupCommand;
            }
        }

        #endregion

        #region Methods

        protected override void DefineAddCommands()
        {
            addCommands.Clear();

            switch (ControllerInterop.User.Role)
            {
                case UserRoles.MainAdmin:
                case UserRoles.MainSecretary:
                    addCommands.Add(new NamedCommandData { Name = "Інститут", Command = AddInstituteCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Факультет", Command = AddFacultyCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Кафедру", Command = AddCathedraCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Групу", Command = AddGroupCommand });
                    break;
                case UserRoles.InstituteAdmin:
                case UserRoles.InstituteSecretary:
                    addCommands.Add(new NamedCommandData() { Name = "Факультет", Command = AddFacultyCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Кафедру", Command = AddCathedraCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Групу", Command = AddGroupCommand });
                    break;
                case UserRoles.FacultyAdmin:
                case UserRoles.FacultySecretary:
                    addCommands.Add(new NamedCommandData() { Name = "Кафедру", Command = AddCathedraCommand });
                    addCommands.Add(new NamedCommandData() { Name = "Групу", Command = AddGroupCommand });
                    break;
                default:
                    throw new NotImplementedException("Unknown user's role");
            }

        }

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

            return viewModel;
        }

        #endregion

        #region Callbacks

        #endregion

       
    }
}
