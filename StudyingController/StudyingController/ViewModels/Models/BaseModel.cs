using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public abstract class BaseModel : INotifyPropertyChanged
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
    }
}
