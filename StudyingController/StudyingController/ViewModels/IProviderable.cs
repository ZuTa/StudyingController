using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels
{
    public interface IProviderable : IRefreshable
    {
        BaseEntityDTO CurrentEntity { get; set; }

        event SelectedEntityChangedHandler SelectedEntityChangedEvent;
    }
}
