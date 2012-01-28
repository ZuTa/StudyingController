using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels
{
    public interface IManipulateable
    {

        ReadOnlyObservableCollection<NamedCommandData> AddCommands { get; }
        
        RelayCommand RemoveCommand { get; }
        RelayCommand UpdateCommand { get; }
        
    }
}
