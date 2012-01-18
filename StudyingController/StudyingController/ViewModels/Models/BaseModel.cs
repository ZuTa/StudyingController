﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    internal class BaseModel : INotifyPropertyChanged
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

        internal BaseModel(BaseEntityDTO entity)
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