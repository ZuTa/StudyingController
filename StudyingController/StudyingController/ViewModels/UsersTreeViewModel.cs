using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public class UsersTreeViewModel : BaseUniversityTreeViewModel
    {
        #region Fields & Properties
        
        public string HeaderText
        {
            get { return Properties.Resources.UserStructureHeaderText; }
        }

        #endregion

        #region Constructors

        public UsersTreeViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
        }

        #endregion

        #region Methods


        #endregion

        #region Callbacks


        #endregion

    }
}
