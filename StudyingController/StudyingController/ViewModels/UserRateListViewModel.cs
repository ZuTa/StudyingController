﻿using EntitiesDTO;
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
        private ObservableCollection<UserRateItemDTO> rates;
        public ReadOnlyObservableCollection<UserRateItemDTO> Rates { get; private set; }

        public UserRateListViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, BaseEntityDTO entity)
            : base(userInterop, controllerInterop, dispatcher)
        {
            rates = new ObservableCollection<UserRateItemDTO>();

            Rates = new ReadOnlyObservableCollection<UserRateItemDTO>(rates);
            
        }

        protected override void LoadData()
        {
            rates.Add( new UserRateItemDTO { Rate = 50, User = new SystemUserDTO { UserInformation = new UserInformationDTO { FirstName = "Max", LastName ="Pyzhov"}}});
            rates.Add(new UserRateItemDTO { Rate = 27, User = new SystemUserDTO { UserInformation = new UserInformationDTO { FirstName = "Artemko", LastName = "Jermak" } } });
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
