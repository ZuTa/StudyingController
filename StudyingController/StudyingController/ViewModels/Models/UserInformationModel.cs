using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.Common;
using System.Text.RegularExpressions;
using System.Reflection;

namespace StudyingController.ViewModels.Models
{
    public class UserInformationModel : BaseModel, IDTOable<UserInformationDTO>
    {
        private string firstName;
        [Validateable]
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
        [Validateable]
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
        [Validateable]
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

        private bool IsFirstNameValid(out string error)
        {
            error = null;
            if (firstName == null || firstName.Length == 0)
            {
                error = Properties.Resources.ErrorFieldEmpty;
                return false;
            }
            if (firstName.Length > 250)
            {
                error = Properties.Resources.ErrorFieldGreater;
                return false;
            }
            if (!Regex.IsMatch(firstName, "^([а-яА-ЯіІїЇa-zA-Z]\\-?)*[а-яА-ЯіІїЇa-zA-Z]$"))
            {
                error = Properties.Resources.ErrorBadCharsUsed;
                return false;
            }
            return true;
        }

        private bool IsLastNameValid(out string error)
        {
            error = null;
            if (lastName == null || lastName.Length == 0)
            {
                error = Properties.Resources.ErrorFieldEmpty;
                return false;
            }
            if (lastName.Length > 250)
            {
                error = Properties.Resources.ErrorFieldGreater;
                return false;
            }
            if (!Regex.IsMatch(lastName, "^([а-яА-ЯіІїЇa-zA-Z]\\-?)*[а-яА-ЯіІїЇa-zA-Z]$"))
            {
                error = Properties.Resources.ErrorBadCharsUsed;
                return false;
            }
            return true;
        }

        private bool IsEmailValid(out string error)
        {
            error = null;
            if (email == null || email.Length == 0)
            {
                error = Properties.Resources.ErrorFieldEmpty;
                return false;
            }
            if (!Regex.IsMatch(email, @"^[-a-z0-9!#$%&'*+/=?^_`{|}~]+(\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*@([a-z0-9]([-a-z0-9]{0,61}[a-z0-9])?\.)*(aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|[a-z][a-z])$"))
            {
                error = Properties.Resources.ErrorBadCharsUsed;
                return false;
            }
            return true;
        }

        protected override string Validate(string property)
        {
            string error = base.Validate(property);
            if (error == null)
            {
                switch (property)
                {
                    case "FirstName":
                        IsFirstNameValid(out error);
                        break;
                    case "LastName":
                        IsLastNameValid(out error);
                        break;
                    case "Email":
                        IsEmailValid(out error);
                        break;
                    default:
                        break;
                }
            }
            return error;
        }

    }
}
