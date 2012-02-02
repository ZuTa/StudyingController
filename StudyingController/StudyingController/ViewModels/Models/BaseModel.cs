using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using EntitiesDTO;
using System.Text.RegularExpressions;
using System.Reflection;
using StudyingController.Common;

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

        public bool IsValid
        {
            get
            {
                return ValidateProperties();
            }
        }

        public BaseModel()
        { }

        public BaseModel(BaseEntityDTO entity)
        {
            this.id = entity.ID;
        }

        public virtual void Assign(BaseEntityDTO entity)
        {
            this.id = entity.ID;
        }

        protected bool ValidateProperties()
        {
            bool result = true;

            Type type = this.GetType();
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                if (propertyInfo.GetCustomAttributes(typeof(ValidateableAttribute), false).Length > 0)
                {
                    BaseModel model = propertyInfo.GetValue(this, null) as BaseModel;
                    if (model == null)
                        result = Validate(propertyInfo.Name) == null;
                    else
                        result = model.ValidateProperties();
                    if (!result)
                        break;
                }
            }

            return result;
        }

        public bool Exists()
        {
            return this.ID > 0;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            Common.Checks.CheckPropertyExists(this, propertyName);
            Validate(propertyName);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual string Validate(string property)
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
            get { return Validate(property); }
        }

        #endregion
    }
}
