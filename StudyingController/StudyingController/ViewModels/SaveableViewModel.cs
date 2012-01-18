using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using StudyingController.ViewModels.Models;

namespace StudyingController.ViewModels
{
    public abstract class SaveableViewModel : BaseApplicationViewModel
    {
        #region Fields & Properties

        private bool isModified;

        protected BaseEntityDTO originalEntity;

        protected BaseModel model;
        
        #endregion

        #region Constructor

        public SaveableViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
        }

        #endregion
    }
}
