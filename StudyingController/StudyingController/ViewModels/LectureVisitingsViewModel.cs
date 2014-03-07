using EntitiesDTO;
using StudyingController.Common;
using StudyingController.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public class LectureVisitingsViewModel : SaveableViewModel, IExportable
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
                {
                    visitingsModel.PropertyChanged -= ModelPropertyChanged;
                    visitingsModel.ModelChanged -= visitingsModel_ModelChanged;
                }
            }

            Visitings = ControllerInterop.Service.GetVisitingsForLecture(ControllerInterop.Session, new LectureRef { ID = OriginalLesson.ID }).ToModelList<VisitingsModel, VisitingsDTO>();

            foreach (VisitingsModel visitingsModel in Visitings)
            {
                visitingsModel.PropertyChanged += ModelPropertyChanged;
                visitingsModel.ModelChanged += visitingsModel_ModelChanged;
            }

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
            ControllerInterop.Service.SaveVisitingsForLecture(ControllerInterop.Session,
                new LectureRef { ID = OriginalLesson.ID },
                Visitings.SelectMany(vis=>vis.Visitings).Select(vis=>vis.ToDTO()).ToList());
            SetUnModified();
        }

        public override void Rollback()
        {
            LoadData();
            SetUnModified();
        }

        public override void Remove()
        {
            SetModified();
        }

        private void AddVisiting()
        {
            if (!Dates.Contains(DateTime.Today))
            {
                foreach (var vis in Visitings)
                    vis.Visitings.Add(new VisitingModel { Date = DateTime.Today, Lecture = vis.Lecture ,Student = vis.Student });
            }
        }

        protected override void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.ModelPropertyChanged(sender, e);
            if (e.PropertyName == "Visitings")
                OnPropertyChanged("Dates");
        }

        private void visitingsModel_ModelChanged(object sender, EventArgs e)
        {
            SetModified();
        }

        #endregion

        #region Events

        #endregion

        public bool AllowExportToExcel
        {
            get { return OriginalLesson != null && OriginalLesson.ID != 0; }
        }

        public void ExportToExcel()
        {
            string title = string.Format("Відвідуваність студентів.");

            List<List<string>> data = new List<List<string>>();

            List<string> header = new List<string>();

            int counter = 1;

            header.Add("№");
            header.Add("Прізвище");
            header.Add("Ім'я");
            
            foreach (var dat in Dates)
                header.Add(dat.ToString("dd.MM.yyyy"));

            foreach (var vis in Visitings)
            {
                data.Add(new List<string>());
                data.Last().Add(counter++.ToString());

                StudentModel user = new StudentModel(ControllerInterop.Service.GetStudent(ControllerInterop.Session, vis.Student.ID));
                data.Last().Add(user.UserInformation.FirstName);
                data.Last().Add(user.UserInformation.LastName);

                //GroupModel group = new GroupModel(ControllerInterop.Service.GetGroup(ControllerInterop.Session, user.Group.ID));
                //data.Last().Add(group.Name);

                foreach (var v in vis.Visitings)
                {
                    data.Last().Add(string.Format("{0} {1}", Localize(v.Value), v.Description));
                }
            }
            ExportHelper.ExportToExcelWithHeader(title, data, header);
        }

        private string Localize(VisitingValue value)
        {
            switch (value)
            {
                case VisitingValue.Absent:
                    return "Від.";
                case VisitingValue.Individual:
                    return "Інд.";
                case VisitingValue.Present:
                    return "Пр.";
                case VisitingValue.Sick:
                    return "Хв.";
            }
            return "";
        }
    }
}
