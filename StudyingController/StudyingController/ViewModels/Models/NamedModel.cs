using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using System.Text.RegularExpressions;
using StudyingController.Common;

namespace StudyingController.ViewModels.Models
{
    public abstract class NamedModel : BaseModel
    {
        private string name;
        [Validateable]
        public string Name
        {
            get { return name; }
            set 
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public NamedModel()
        {
        }

        public NamedModel(NamedEntityDTO entity)
            : base(entity)
        {            
            this.name = entity.Name;
        }

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            NamedEntityDTO namedEntity = entity as NamedEntityDTO;
            this.Name = namedEntity.Name;
        }

        public override string ToString()
        {
            return Name;
        }

        private bool IsNameValid(out string error)
        {
            error = null;
            if (name == null || name.Length == 0)
            {
                error = Properties.Resources.ErrorFieldEmpty;
                return false;
            }
            if (name.Length > 250)
            {
                error = Properties.Resources.ErrorFieldGreater;
                return false;
            }
            if (!Regex.IsMatch(name, "^([а-яА-ЯіІїЇa-zA-Z0-9][\\- ]?)*[а-яА-ЯіІїЇa-zA-Z0-9]$"))
            {
                error = Properties.Resources.ErrorBadCharsUsed;
                return false;
            }
            return true;
        }

        protected override string Validate(string property)
        {
            if (property.Equals("Name"))
            {
                string error;
                IsNameValid(out error);
                return error;
            }
            return null;
        }
    }
}
