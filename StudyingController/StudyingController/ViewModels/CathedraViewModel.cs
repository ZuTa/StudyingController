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
    public class CathedraViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public CathedraDTO OriginalCathedra
        {
            get
            {
                return originalEntity as CathedraDTO;
            }
        }

        public CathedraModel Cathedra
        {
            get { return Model as CathedraModel; }         
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

        #endregion

        #region Constructors

        public CathedraViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            faculties = new List<FacultyDTO>();

            originalEntity = new CathedraDTO();
        }

        public CathedraViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, CathedraDTO cathedra)
            : base(userInterop, controllerInterop, dispatcher)
        {
            faculties = new List<FacultyDTO>();
            
            originalEntity = cathedra;
        }

        #endregion

        #region Commands

        private RelayCommand addSubjectCommand;
        public RelayCommand AddSubjectCommand
        {
            get
            {
                if (addSubjectCommand == null)
                    addSubjectCommand = new RelayCommand(param => Cathedra.Subjects.Add(new SubjectModel { Name = string.Empty, CathedraID = Cathedra.ID }));
                return addSubjectCommand;
            }
        }

        private RelayCommand removeSubjectCommand;
        public RelayCommand RemoveSubjectCommand
        {
            get
            {
                if (removeSubjectCommand == null)
                    removeSubjectCommand = new RelayCommand(param => Cathedra.Subjects.Remove(param as SubjectModel));
                return removeSubjectCommand;
            }
        }

        #endregion

        #region Methods

        protected override void DoRefresh()
        {
            throw new NotImplementedException();
        }

        public override void Rollback()
        {
            Cathedra.Assign(OriginalCathedra);
            SetUnModified();
        }

        public override void Save()
        {
            CathedraDTO cathedraDTO = Cathedra.ToDTO();
            ControllerInterop.Service.SaveCathedra(ControllerInterop.Session, cathedraDTO);
            SetUnModified();
        }

        public override void Remove()
        {
            ControllerInterop.Service.DeleteCathedra(ControllerInterop.Session, Cathedra.ID);
        }

        protected override object LoadDataFromServer()
        {
            return ControllerInterop.Service.GetAllFaculties(ControllerInterop.Session);
        }

        protected override void AfterDataLoaded()
        {
            base.AfterDataLoaded();

            Faculties = DataSource as List<FacultyDTO>;

            CathedraDTO cathedra = originalEntity as CathedraDTO;

            if (cathedra.Exists())
            {
                cathedra.Faculty = (from faculty in faculties
                                    where faculty.ID == OriginalCathedra.FacultyID
                                    select faculty).FirstOrDefault();
            }

            Model = new CathedraModel(cathedra);
            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        protected override void ClearData()
        {
            if (faculties != null)
                faculties.Clear();
        }
        #endregion

        #region Callbacks

        #endregion
    }
}
