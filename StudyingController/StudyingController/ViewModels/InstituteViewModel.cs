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
    public class InstituteViewModel : BaseApplicationViewModel, ISaveable
    {
        #region Fields & Properties

        private InstituteDTO originalInstitute;

        private InstituteModel institute;
        internal InstituteModel Institute
        {
            get { return institute; }
            set { institute = value; }
        }

        #endregion

        #region Constructors

        public InstituteViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            originalInstitute = new InstituteDTO();
            institute = new InstituteModel(originalInstitute);
            //this.institute.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);

        }

        public InstituteViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, InstituteDTO institute)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalInstitute = institute;
            this.institute = new InstituteModel(institute);
            //this.institute.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods

        public void Save()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Callbacks


        #endregion




        public bool IsModified
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool CanModify
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanSave
        {
            get { throw new NotImplementedException(); }
        }
    }
}
