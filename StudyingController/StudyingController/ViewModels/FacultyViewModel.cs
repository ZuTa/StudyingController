﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using StudyingController.ViewModels.Models;

namespace StudyingController.ViewModels
{
    public class FacultyViewModel : SaveableViewModel
    {

        #region Fields & Properties

        public FacultyDTO OriginalFaculty
        {
            get { return originalEntity as FacultyDTO; }
        }
      
        public FacultyModel Faculty
        {
            get { return Model as FacultyModel; }
        }

        private List<InstituteDTO> institutes;
        public List<InstituteDTO> Institutes
        {
            get { return institutes; }
            set 
            { 
                institutes = value;
                OnPropertyChanged("Institutes");
            }
        }  

        #endregion

        #region Constructors

        public FacultyViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            institutes = new List<InstituteDTO>();

            originalEntity = new FacultyDTO();
        }

        public FacultyViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, FacultyDTO faculty)
            : base(userInterop, controllerInterop, dispatcher)
        {
            institutes = new List<InstituteDTO>();

            originalEntity = faculty;
        }

        #endregion

        #region Commands

        private RelayCommand addSpecializationCommand;
        public RelayCommand AddSpecializationCommand
        {
            get
            {
                if (addSpecializationCommand == null)
                    addSpecializationCommand = new RelayCommand(param => Faculty.Specializations.Add(new SpecializationModel { Name = string.Empty, FacultyID = Faculty.ID }));
                return addSpecializationCommand; 
            }
        }

        private RelayCommand removeSpecializationCommand;
        public RelayCommand RemoveSpecializationCommand
        {
            get
            {
                if (removeSpecializationCommand == null)
                    removeSpecializationCommand = new RelayCommand(param => Faculty.Specializations.Remove(param as SpecializationModel));
                return removeSpecializationCommand; 
            }
        }

        #endregion

        #region Methods
        
        protected override void DoRefresh()
        {
            throw new NotImplementedException();
        }

        public override void Remove()
        {
            ControllerInterop.Service.DeleteFaculty(ControllerInterop.Session, Faculty.ID); 
        }

        public override void Rollback()
        {
            Faculty.Assign(OriginalFaculty);
            SetUnModified();
        }

        protected override object LoadDataFromServer()
        {
            return ControllerInterop.Service.GetInstitutes(ControllerInterop.Session);
        }

        protected override void AfterDataLoaded()
        {
            base.AfterDataLoaded();

            Institutes = DataSource as List<InstituteDTO>; 

            FacultyDTO faculty = originalEntity as FacultyDTO;

            if (faculty.Exists())
            {
                faculty.Institute = (from institute in institutes
                                     where institute.ID == OriginalFaculty.InstituteID
                                     select institute).FirstOrDefault();
            }

            Model = new FacultyModel(faculty);
            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
            UpdateProperties();
        }


        protected override void ClearData()
        {
            if (institutes != null)
                institutes.Clear();
        }

        public override void Save()
        {
            FacultyDTO facultyDTO = Faculty.ToDTO();
            ControllerInterop.Service.SaveFaculty(ControllerInterop.Session, facultyDTO);
            SetUnModified();
        }

        #endregion

        #region Callbacks



        #endregion
    }
}
