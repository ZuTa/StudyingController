using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.ViewModels.Models;
using StudyingController.Common;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public class LectureControlViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public BaseEntityDTO OriginalControl
        {
            get
            {
                return originalEntity;
            }
        }

        public ControlModel Control
        {
            get { return Model as ControlModel; }
        }

        #endregion

        #region Constructors

        public LectureControlViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, ControlDTO control)
            : base(userInterop, controllerInterop, dispatcher)
        {
            originalEntity = control;

            Model = new ControlModel(control);

            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods

        public override void Save()
        {
            throw new NotImplementedException();
        }

        public override void Rollback()
        {
            throw new NotImplementedException();
        }

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
