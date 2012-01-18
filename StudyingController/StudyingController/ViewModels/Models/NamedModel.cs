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

        public virtual void Assign(BaseEntityDTO entity)
        {
            NamedEntityDTO model = entity as NamedEntityDTO;

            this.name = model.name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
