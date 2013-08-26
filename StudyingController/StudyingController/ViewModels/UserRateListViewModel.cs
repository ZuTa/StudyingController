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
            rates.Add(new UserRateItemDTO { Rate = 100, User = new SystemUserDTO { UserInformation = new UserInformationDTO { FirstName = "Максим", LastName = "Пижов" } } });
            rates.Add(new UserRateItemDTO { Rate = 100, User = new SystemUserDTO { UserInformation = new UserInformationDTO { FirstName = "Артем", LastName = "Єрмак" } } });
            rates.Add(new UserRateItemDTO { Rate = 100, User = new SystemUserDTO { UserInformation = new UserInformationDTO { FirstName = "Тарас", LastName = "Зубик" } } });
            rates.Add(new UserRateItemDTO { Rate = 100, User = new SystemUserDTO { UserInformation = new UserInformationDTO { FirstName = "Юрій", LastName = "Здебський" } } });
            rates.Add(new UserRateItemDTO { Rate = 100, User = new SystemUserDTO { UserInformation = new UserInformationDTO { FirstName = "Ярослав", LastName = "Страх" } } });
            rates.Add(new UserRateItemDTO { Rate = 100, User = new SystemUserDTO { UserInformation = new UserInformationDTO { FirstName = "Дмитро", LastName = "Редько" } } });
            rates.Add(new UserRateItemDTO { Rate = 100, User = new SystemUserDTO { UserInformation = new UserInformationDTO { FirstName = "Дмитро", LastName = "Гиць" } } });
            rates.Add(new UserRateItemDTO { Rate = 100, User = new SystemUserDTO { UserInformation = new UserInformationDTO { FirstName = "Вікторія", LastName = "Яременко" } } });
            rates.Add(new UserRateItemDTO { Rate = 100, User = new SystemUserDTO { UserInformation = new UserInformationDTO { FirstName = "Олена", LastName = "Сем\'янків" } } });
            rates.Add(new UserRateItemDTO { Rate = 100, User = new SystemUserDTO { UserInformation = new UserInformationDTO { FirstName = "Юлія", LastName = "Юр\'єва" } } });
            rates.Add(new UserRateItemDTO { Rate = 100, User = new SystemUserDTO { UserInformation = new UserInformationDTO { FirstName = "Ілля", LastName = "Шумілов" } } });
            rates.Add(new UserRateItemDTO { Rate = 100, User = new SystemUserDTO { UserInformation = new UserInformationDTO { FirstName = "Олександр", LastName = "Кочубеєнко" } } });
            Random rand = new Random();

            rates.ToList().ForEach(item => item.Rate = rand.Next(100));
            rates = new ObservableCollection<UserRateItemDTO>(rates.OrderBy(item => -item.Rate).ToList());
            Rates = new ReadOnlyObservableCollection<UserRateItemDTO>(rates);
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
