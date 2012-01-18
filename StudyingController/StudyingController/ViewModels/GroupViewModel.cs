using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;

namespace StudyingController.ViewModels
{
    public class GroupViewModel : BaseApplicationViewModel
    {
        #region Fields & Properties

        public bool IsModified
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool CanModify
        {
            get { throw new NotImplementedException(); }
        }

        public bool CanSave
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Constructors

        public GroupViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
        }

        public GroupViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, GroupDTO group)
            : base(userInterop, controllerInterop, dispatcher)
        {
        }

        #endregion

        #region Methods

        public void Save()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Callbacks

        #endregion

    }
}
