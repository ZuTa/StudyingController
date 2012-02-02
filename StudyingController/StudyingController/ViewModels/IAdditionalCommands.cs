using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels
{
    public interface IAdditionalCommands
    {
        ObservableCollection<NamedCommandData> AdditionalCommands { get; }

        void UpdateCommandsEnabledState();
    }
}
