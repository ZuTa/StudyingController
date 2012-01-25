using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.ViewModels.Models;
using StudyingController.Common;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public class InstituteAdminViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public InstituteAdminDTO OriginalInstituteAdmin
        {
            get { return originalEntity as InstituteAdminDTO; }
        }

        public InstituteAdminModel InstituteAdmin
        {
            get { return model as InstituteAdminModel; }
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

        public InstituteAdminViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            institutes = new List<InstituteDTO>();
            Load();

            originalEntity = new InstituteAdminDTO();
            model = new InstituteAdminModel(originalEntity as InstituteAdminDTO);
            this.model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public InstituteAdminViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, InstituteAdminDTO instituteAdmin)
            : base(userInterop, controllerInterop, dispatcher)
        {
            institutes = new List<InstituteDTO>();
            Load();

            originalEntity = instituteAdmin;
            instituteAdmin.Institute = (from institute in institutes
                                        where institute.ID == OriginalInstituteAdmin.InstituteID
                                        select institute).FirstOrDefault();

            model = new InstituteAdminModel(instituteAdmin);
            model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods

        public override void Rollback()
        {
            InstituteAdmin.Assign(OriginalInstituteAdmin);
            SetUnModified();
        }

        public override void Save()
        {
            InstituteAdminDTO instituteAdminDTO = InstituteAdmin.ToDTO();
            //ControllerInterop.Service.(ControllerInterop.Session, instituteDTO);
            SetUnModified();
        }

        public void Load()
        {
            Institutes = ControllerInterop.Service.GetInstitutes(ControllerInterop.Session);
        }

        #endregion
    }
}
