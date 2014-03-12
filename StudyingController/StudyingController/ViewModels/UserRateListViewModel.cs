using EntitiesDTO;
using StudyingController.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public class UserRateListViewModel : SaveableViewModel
    {
        private BaseEntityDTO entity;
        private ObservableCollection<UserRateItemDTO> rates;
        public ReadOnlyObservableCollection<UserRateItemDTO> Rates { get; private set; }

        public UserRateListViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, BaseEntityDTO entity)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.entity = entity;
            rates = new ObservableCollection<UserRateItemDTO>();
            Rates = new ReadOnlyObservableCollection<UserRateItemDTO>(rates);
        }

        protected override object LoadDataFromServer()
        {
            return ControllerInterop.Service.GetStudentRateList(this.ControllerInterop.Session, entity);
        }

        protected override void AfterDataLoaded()
        {
            foreach (var r in DataSource as List<UserRateItemDTO>)
            {
                r.Rate *= 100;
                rates.Add(r);
            }
        }

        protected override void ClearData()
        {
            rates.Clear();
        }

        public override bool IsModified
        {
            get
            {
                return false;
            }
        }

        public override EditModes EditMode
        {
            get
            {
                return EditModes.ReadOnly;
            }
        }

        public override void Remove()
        {
            throw new NotImplementedException("You can't edit rating yet!");
        }

        protected override void DoRefresh()
        {
            throw new NotImplementedException("You can't remove rating yet!");
        }

        public override void Save()
        {
            throw new NotImplementedException("You can't remove rating yet!");
        }

        public override void Rollback()
        {
            throw new NotImplementedException("You can't remove rating yet!");
        }
    }
}
