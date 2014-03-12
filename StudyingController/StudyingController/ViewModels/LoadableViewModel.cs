using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace StudyingController.ViewModels
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

        private object dataSource;

        protected object DataSource
        {
            get { return dataSource; }
        }


        #endregion

        #region Constructors

        public LoadableViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
        }

        #endregion

        #region Methods


        protected virtual void BeforeDataLoading()
        {
        }

        /// <summary>
        /// Only loading from server
        /// </summary>
        /// <returns></returns>
        protected abstract object LoadDataFromServer();

        protected virtual void AfterDataLoaded()
        {
        }

        protected abstract void ClearData();

        public Task Load()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StartLoading();
            Console.WriteLine("{1}. Start loading {0} ms.", sw.ElapsedMilliseconds, this.GetType().Name);
            sw.Restart();
            BeforeDataLoading();
            Console.WriteLine("{1}. Before data loading {0} ms.", sw.ElapsedMilliseconds, this.GetType().Name);
            sw.Restart();
            ClearData();
            Console.WriteLine("{1}. Clear data {0} ms.", sw.ElapsedMilliseconds, this.GetType().Name);
            sw.Restart();
            return Task.Factory.StartNew(() =>
                {
                    dataSource = LoadDataFromServer();
                    //Thread.Sleep(5000);
                    Console.WriteLine("{1}. Load from server {0} ms.", sw.ElapsedMilliseconds, this.GetType().Name);
                    sw.Restart();
                }).ContinueWith((task) =>
                    {
                        Dispatcher.Invoke(new Action(() =>
                            {
                                AfterDataLoaded();
                                Console.WriteLine("{1}. After loading {0} ms.", sw.ElapsedMilliseconds, this.GetType().Name);
                                sw.Restart();
                                StopLoading();
                            }));
                    }, TaskContinuationOptions.ExecuteSynchronously); 
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
