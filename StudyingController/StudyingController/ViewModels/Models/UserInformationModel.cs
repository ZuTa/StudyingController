using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class UserInformationModel : BaseModel, IDTOable<UserInformationDTO>
    {
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set 
            { 
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set 
            { 
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set 
            { 
                email = value;
                OnPropertyChanged("Email");
            }
        }

        public UserInformationModel(UserInformationDTO entity)
            : base(entity)
        {
            this.firstName = entity.FirstName;
            this.lastName = entity.LastName;
            this.email = entity.Email;
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            UserInformationDTO userInformation = entity as UserInformationDTO;
            this.FirstName = userInformation.FirstName;
            this.LastName = userInformation.LastName;
            this.Email = userInformation.Email;
        }

        public UserInformationDTO ToDTO()
        {
            return new UserInformationDTO
            {
                ID = this.ID,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email
            };
        }
    }
}
