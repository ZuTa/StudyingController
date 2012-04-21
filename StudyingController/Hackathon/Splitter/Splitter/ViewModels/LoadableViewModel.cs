using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using System.Threading;

namespace Splitter.ViewModels
{
    public abstract class LoadableViewModel : BaseApplicationViewModel
    {
        #region Fields & Properties

        private object lockObject = new object();

        private int loadCount;
        protected int LoadCount
        {
            get { return loadCount; }
            set 
            {
                if (loadCount != value)
                {
                    loadCount = value;
                    OnPropertyChanged("IsLoading");
                    OnPropertyChanged("IsNotBusy");
                }
            }
        }

        public bool IsLoading
        {
            get
            {
                return loadCount != 0;
            }
        }

        public virtual bool IsNotBusy
        {
            get
            {
                return !IsLoading;
            }
        }

        #endregion

        #region Constructors

        public LoadableViewModel(IUserInterop userInterop, ISplitterInterop splitterInterop, Dispatcher dispatcher)
            : base(userInterop, splitterInterop, dispatcher)
        {
        }

        #endregion

        #region Methods

        protected abstract void LoadData();
        protected abstract void ClearData();

        public void Load()
        {
            StartLoading();

            ClearData();
            LoadData();

            StopLoading();
        }

        protected virtual void StartLoading(int count = 1)
        {
            lock (lockObject)
                LoadCount += count;
        }

        protected virtual void StopLoading(int count  = 1)
        {
            lock (lockObject)
                LoadCount -= count;
        }

        #endregion
    }
}
