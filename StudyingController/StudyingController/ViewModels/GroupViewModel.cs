using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using StudyingController.ViewModels.Models;

namespace StudyingController.ViewModels
{
    public class GroupViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public GroupDTO OriginalGroup
        {
            get
            {
                return originalEntity as GroupDTO;
            }
        }

        public GroupModel Group
        {
            get { return Model as GroupModel; }
        }

        private FacultyDTO faculty;
        public FacultyDTO Faculty
        {
            get { return faculty; }
            set
            {
                faculty = value;
                RefreshProperties();
                OnPropertyChanged("Faculty");
            }
        }

        private List<FacultyDTO> faculties;
        public List<FacultyDTO> Faculties
        {
            get { return faculties; }
            set 
            { 
                faculties = value;
                OnPropertyChanged("Faculties");
            }
        }

        private List<SpecializationDTO> specializations;
        public List<SpecializationDTO> Specializations
        {
            get { return specializations; }
            set 
            { 
                specializations = value;
                OnPropertyChanged("Specializations");
            }
        }

        private List<CathedraDTO> cathedras;
        public List<CathedraDTO> Cathedras
        {
            get { return cathedras; }
            set
            {
                cathedras = value;
                OnPropertyChanged("Cathedras");
            }
        }

        #endregion

        #region Constructors

        public GroupViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            faculties = new List<FacultyDTO>();
            cathedras = new List<CathedraDTO>();

            originalEntity = new GroupDTO();
        }

        public GroupViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, GroupDTO group)
            : base(userInterop, controllerInterop, dispatcher)
        {
            faculties = new List<FacultyDTO>();
            cathedras = new List<CathedraDTO>();

            originalEntity = group;
        }

        #endregion

        #region Methods

        protected override void DoRefresh()
        {
            throw new NotImplementedException();
        }

        public override void Remove()
        {
            ControllerInterop.Service.DeleteGroup(ControllerInterop.Session, Group.ID);
        }

        private void RefreshProperties()
        {
            if (Faculty != null && Group != null)
            {
                Cathedras = ControllerInterop.Service.GetCathedras(ControllerInterop.Session, Faculty.ID);
                if (Group.Cathedra != null) Group.Cathedra = (from cathedra in Cathedras
                                                              where cathedra.ID == Group.Cathedra.ID
                                                              select cathedra).FirstOrDefault();

                Specializations = ControllerInterop.Service.GetSpecializations(ControllerInterop.Session, Faculty.ID);
                if (Group.Specialization != null) Group.Specialization = (from specialization in Specializations
                                                                          where specialization.ID == Group.Specialization.ID
                                                                          select specialization).FirstOrDefault();
            }
        }

        protected override void ClearData()
        {
            if (faculties != null)
                faculties.Clear();

            if (cathedras != null)
                cathedras.Clear();
        }

        protected override void LoadData()
        {
            Faculties = ControllerInterop.Service.GetAllFaculties(ControllerInterop.Session);

            GroupDTO group = originalEntity as GroupDTO;

            if (originalEntity.Exists())
            {
                group.Cathedra = (from cathedra in ControllerInterop.Service.GetAllCathedras(ControllerInterop.Session)
                                  where cathedra.ID == OriginalGroup.CathedraID
                                  select cathedra).FirstOrDefault();

                group.Specialization = ControllerInterop.Service.GetSpecializationByID(ControllerInterop.Session, group.SpecializationID);

            }

            Model = new GroupModel(group);
//TODO:CHANGE THIS FUCKING CODE
            if (originalEntity.Exists())
            {
                Faculty = (from faculty in faculties
                           where faculty.ID == OriginalGroup.Cathedra.FacultyID
                           select faculty).FirstOrDefault();
            }

            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public override void Save()
        {
            GroupDTO groupDTO = Group.ToDTO();
            groupDTO.StudyRangeID = ControllerInterop.StudyRange.ID;
            ControllerInterop.Service.SaveGroup(ControllerInterop.Session, groupDTO);
            SetUnModified();
        }

        public override void Rollback()
        {
            Group.Assign(OriginalGroup);
            SetUnModified();
        }

        #endregion

        #region Callbacks

        #endregion
    }
}
