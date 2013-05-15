using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels
{
    public class UniversityStructureViewModel : EditableViewModel, IManipulateable,IRefreshable
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

        protected ObservableCollection<NamedCommandData> addCommands;
        private ReadOnlyObservableCollection<NamedCommandData> addCommandsRO;
        public ReadOnlyObservableCollection<NamedCommandData> AddCommands
        {
            get { return addCommandsRO; }
        }

        #endregion

        #region Constructors

        public UniversityStructureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            addCommands = new ObservableCollection<NamedCommandData>();
            addCommandsRO = new ReadOnlyObservableCollection<NamedCommandData>(addCommands);

            entitiesProvider = new UniversityTreeViewModel(userInterop, controllerInterop, dispatcher);
            entitiesProvider.SelectedEntityChangedEvent += new SelectedEntityChangedHandler(EntitesProvider_SelectedEntityChangedEvent);

            DefineAddCommands();
        }

        #endregion

        #region Commands

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                if (removeCommand == null)
                    removeCommand = new RelayCommand(param =>
                    {
                        if (UserInterop.ShowMessage(Properties.Resources.RemoveEntityTxt, Properties.Resources.DefaultMessageText, MessageButtons.YesNo, MessageTypes.Question) == MessageResults.Yes)
                        {
                            CurrentWorkspace.Remove();
                            EntitiesProvider.Refresh();
                        }
                    });
                return removeCommand;
            }
        }

        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                    updateCommand = new RelayCommand(param =>
                    {
                        EntitiesProvider.Refresh();
                    });
                return updateCommand;
            }
        }

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

        protected void DefineAddCommands()
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
            else if (entity is GroupDTO)
                viewModel = new GroupViewModel(UserInterop, ControllerInterop, Dispatcher, entity as GroupDTO);
            else
                throw new NotImplementedException("Unknown entity");

            return viewModel;
        }

        #endregion

        #region Callbacks

        #endregion

    }
}
