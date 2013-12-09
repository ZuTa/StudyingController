using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using System.Collections.ObjectModel;
using StudyingController.ViewModels.Models;

namespace StudyingController.ViewModels
{
    public class UserRateStructureViewModel : EditableViewModel, IRefreshable, IExportable
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

        public UserRateStructureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
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

            if (entity is InstituteDTO || entity is FacultyDTO || entity is CathedraDTO || entity is GroupDTO)
                return new UserRateListViewModel(UserInterop, ControllerInterop, Dispatcher, entity);
            else
                throw new NotImplementedException("Unknown entity");
        }

        protected override void UpdateProperties()
        {
            OnPropertyChanged("AllowExportToExcel");
        }

        #endregion

        #region Callbacks

        #endregion

        public bool AllowExportToExcel
        {
            get
            {
                return (EntitiesProvider.CurrentEntity as InstituteDTO != null
                        || EntitiesProvider.CurrentEntity as FacultyDTO != null
                        || EntitiesProvider.CurrentEntity as CathedraDTO != null
                        || EntitiesProvider.CurrentEntity as GroupDTO != null)
                    && (CurrentWorkspace as UserRateListViewModel).Rates.Count > 0;
            }
        }

        public void ExportToExcel()
        {
            string title = string.Format("{0}. Рейтинг студентів.", (EntitiesProvider.CurrentEntity as NamedEntityDTO).Name);

            List<List<string>> data = new List<List<string>>();

            List<string> header = new List<string>();

            int counter = 1;
            
            header.Add("№");
            header.Add("Прізвище");
            header.Add("Ім'я");
            header.Add("Група");
            header.Add("Спеціалізація");
            header.Add("Кафедра");
            header.Add("Факультет");
            header.Add("Рейтинг");

            foreach (var rate in (CurrentWorkspace as UserRateListViewModel).Rates)
            {
                StudentModel user = new StudentModel(rate.Student);

                data.Add(new List<string>());
                data.Last().Add(counter++.ToString());
                data.Last().Add(user.UserInformation.FirstName);
                data.Last().Add(user.UserInformation.LastName);

                GroupModel group = new GroupModel(ControllerInterop.Service.GetGroup(ControllerInterop.Session, rate.Student.GroupID));
                data.Last().Add(group.Name);

                SpecializationModel specialization = new SpecializationModel(ControllerInterop.Service.GetSpecialization(ControllerInterop.Session, group.SpecializationId));
                data.Last().Add(specialization.Name);

                CathedraModel cathedra = new CathedraModel(ControllerInterop.Service.GetCathedra(ControllerInterop.Session, group.CathedraId));
                data.Last().Add(cathedra.Name);

                FacultyModel faculty = new FacultyModel(ControllerInterop.Service.GetFaculty(ControllerInterop.Session, cathedra.FacultyId));
                data.Last().Add(faculty.Name);

                data.Last().Add(rate.Rate.ToString());
            }

            ExportHelper.ExportToExcelWithHeader(title, data, header);
        }
    }
}
