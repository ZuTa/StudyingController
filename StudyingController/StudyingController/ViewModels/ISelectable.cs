using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels
{
    public interface IProviderable
    {
        BaseEntityDTO CurrentEntity { get; set; }

        void Refresh();

        event SelectedEntityChangedHandler SelectedEntityChangedEvent;
    }
}
