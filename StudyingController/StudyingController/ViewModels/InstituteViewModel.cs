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
    public class InstituteViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public InstituteDTO OriginalInstitute
        {
            get { return originalEntity as InstituteDTO; }
        }

        public InstituteModel Institute
        {
            get { return model as InstituteModel; }
        }

        #endregion

        #region Constructors

        public InstituteViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            originalEntity = new InstituteDTO();
            model = new InstituteModel(originalEntity as InstituteDTO);
            //this.institute.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);

        }

        public InstituteViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, InstituteDTO institute)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalEntity = institute;
            this.model = new InstituteModel(institute);
            //this.institute.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods

        public void Save()
        {
            InstituteDTO instituteDTO = Institute.ToDTO();
        }

        #endregion

        #region Callbacks


        #endregion
    }
}
