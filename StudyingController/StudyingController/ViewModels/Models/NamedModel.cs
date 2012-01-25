using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public abstract class NamedModel : BaseModel
    {
        private string name;
        public string Name
        {
            get { return name; }
            set 
            {
                name = value;
                OnPropertyChanged("Name");
            }
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
            this.name = namedEntity.Name;
        }

        public override string ToString()
        {
            return Name;
        }

        protected override string Validation(string property)
        {
            //if (property.Equals("Name"))
                return base.IsTextValid(name);
           // return null;
        }
    }
}
