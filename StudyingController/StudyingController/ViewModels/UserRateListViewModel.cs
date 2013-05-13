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
    public class UserRateListViewModel : LoadableViewModel
    {
        private ObservableCollection<UserRateItemDTO> rates;
        public ReadOnlyObservableCollection<UserRateItemDTO> Rates { get; private set; }

        public UserRateListViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            rates = new ObservableCollection<UserRateItemDTO>();

            Rates = new ReadOnlyObservableCollection<UserRateItemDTO>(rates);
        }

        protected override void LoadData()
        {
            rates.Add( new UserRateItemDTO { Rate = 100, User = new SystemUserDTO { UserInformation = new UserInformationDTO { FirstName = "Max", LastName ="Pyzhov"}}});
            rates.Add( new UserRateItemDTO { Rate = 50, User = new SystemUserDTO { UserInformation = new UserInformationDTO { FirstName = "Artemko", LastName ="Jermak"}}});
        }

        protected override void ClearData()
        {
            rates.Clear();
        }
    }
}
