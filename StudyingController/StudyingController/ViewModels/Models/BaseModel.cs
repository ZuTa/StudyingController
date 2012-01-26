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

        public virtual bool IsValid
        {
            get
            {
                return true;
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
            Validation(propertyName);
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
