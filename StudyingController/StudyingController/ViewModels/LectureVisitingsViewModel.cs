using EntitiesDTO;
using StudyingController.Common;
using StudyingController.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public class LectureVisitingsViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public BaseEntityDTO OriginalLesson
        {
            get
            {
                return originalEntity;
            }
        }

        public BaseModel Lesson
        {
            get { return Model; }
        }

        private ObservableCollection<VisitingsModel> visitings;
        public ObservableCollection<VisitingsModel> Visitings
        {
            get { return visitings; }
            set 
            { 
                visitings = value;
                OnPropertyChanged("Visitings");
                OnPropertyChanged("Dates");
            }
        }

        public ObservableCollection<DateTime> Dates
        {
            get
            {
                return new ObservableCollection<DateTime>(visitings.SelectMany(v => v.Visitings)
                              .Select(v => v.Date)
                              .OrderBy(d => d)
                              .Distinct()
                              .ToList());
            }
        }

        #endregion

        #region Constructors

        public LectureVisitingsViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, BaseEntityDTO lesson)
            : base(userInterop, controllerInterop, dispatcher)
        {
            visitings = new ObservableCollection<VisitingsModel>();

            originalEntity = lesson;
        }

        #endregion

        #region Commands

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                if (addCommand == null)
                    addCommand = new RelayCommand((param) => AddVisiting());
                return addCommand;
            }
        }

        #endregion

        #region Methods

        protected override void DoRefresh()
        {
            throw new NotImplementedException();
        }

        protected override void LoadData()
        {
            BaseEntityDTO lesson = originalEntity as BaseEntityDTO;

            if (Visitings != null)
            {
                foreach (VisitingsModel visitingsModel in Visitings)
                    visitingsModel.PropertyChanged -= ModelPropertyChanged;
            }

            Visitings = ControllerInterop.Service.GetVisitingsForLecture(ControllerInterop.Session, OriginalLesson.ID).ToModelList<VisitingsModel, VisitingsDTO>();

            //List<VisitingsModel> visits = new List<VisitingsModel>();
            ////visits.Add(new VisitingsModel
            ////{
            ////    ID = 1,
            ////    StudentID = 1,
            ////});
          
            //for (int i = 1; i < 10; i++)
            //{
            //    var v = new VisitingsModel
            //    {
            //        ID = i,
            //        StudentID = i,
            //        StudentName = "Студент " + i.ToString()
            //    };
            //    for (int j = 1; j < 20; j++)
            //    {
            //        v.Visitings.Add(new VisitingModel
            //        {
            //            Date = DateTime.Today.AddDays(j),
            //           // Description = "desc" + j.ToString(),
            //            ID = j,
            //            StudentID = i,
            //            Value = VisitingValue.Present
            //        });
            //    }
            //    visits.Add(v);
            //}

            //Visitings = new ObservableCollection<VisitingsModel>(visits);

            foreach (VisitingsModel visitingsModel in Visitings)
                visitingsModel.PropertyChanged += ModelPropertyChanged;

            if (lesson is LectureDTO)
                Model = new LectureModel(lesson as LectureDTO);
            else if (lesson is PracticeTeacherDTO)
                Model = new PracticeTeacherModel(lesson as PracticeTeacherDTO);

        }

        protected override void ClearData()
        {
            if (Visitings != null)
                Visitings.Clear();
        }

        public override void Save()
        {
            //ControllerInterop.Service.SaveLectureControls(ControllerInterop.Session, OriginalLesson.ID, Controls.ToDTOList<LectureControlDTO, LectureControlModel>());
            SetUnModified();
        }

        public override void Rollback()
        {
            LoadData();
            SetUnModified();
        }

        public override void Remove()
        {
            //Controls.Remove(CurrentControl);
            SetModified();
        }

        private void AddVisiting()
        {
            if (Dates.Contains(DateTime.Today))
            {
                var visitings = Visitings.ToList();
                foreach (var vis in visitings)
                    vis.Visitings.Add(new VisitingModel { Date = DateTime.Today,/* Lecture = vis.*/StudentID = vis.StudentID });

                Visitings = new ObservableCollection<VisitingsModel>(visitings);
            }
        }
        #endregion

        #region Events

        #endregion
    }
}
