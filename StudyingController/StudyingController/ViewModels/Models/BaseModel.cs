using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using EntitiesDTO;
using System.Text.RegularExpressions;

namespace StudyingController.ViewModels.Models
{
    public abstract class BaseModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private int id;
        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("ID");
            }
        }

        public BaseModel(BaseEntityDTO entity)
        {
            this.id = entity.ID;
        }

        public virtual void Assign(BaseEntityDTO entity)
        {
            this.id = entity.ID;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            Common.Checks.CheckPropertyExists(this, propertyName);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #region Methods for Validation 

        protected string IsTextValid(string value)
        {
            if (value==null || value.Length == 0)
                return "Поле не може бути порожнім";
            if (!Regex.IsMatch(value, "^[а-яА-ЯіІїЇa-zA-Z\\- ]+$"))
                return "Використовуються недопустимі символи";
            return null;
        }

        protected string IsTextNumberValid(string value)
        {
            if (value==null || value.Length == 0)
                return "Поле не може бути порожнім";
            if (!Regex.IsMatch(value, "^[а-яА-ЯіІїЇa-zA-Z0-9\\- ]+$"))
                return "Використовуються недопустимі символи";
            return null;
        }

        protected string IsSelectedItem(object value)
        {
            if (value == null)
                return "Потрібно вибрати структуру";
            return null;
        }

        #endregion

        protected virtual string Validation(string property)
        {
            return null;
        }

        #region IDataErrorInfo

        public string Error
        {
            get { return null; }
        }

        public string this[string property]
        {
            get { return Validation(property); }
        }

        #endregion
    }
}
