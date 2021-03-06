﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.ViewModels.Models;
using StudyingController.Common;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels
{
    public class LectureViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public LectureDTO OriginalLecture
        {
            get { return originalEntity as LectureDTO; }
        }

        public LectureModel Lecture
        {
            get { return Model as LectureModel; }
        }

        private ObservableCollection<GroupDTO> unusedGroups;
        public ObservableCollection<GroupDTO> UnusedGroups
        {
            get { return unusedGroups; }
            set
            {
                if (unusedGroups != value)
                {
                    unusedGroups = value;
                    OnPropertyChanged("UnusedGroups");
                }
            }
        }

        private ObservableCollection<GroupDTO> usedGroups;
        public ObservableCollection<GroupDTO> UsedGroups
        {
            get { return usedGroups; }
            set
            {
                if (usedGroups != value)
                {
                    usedGroups = value;
                    OnPropertyChanged("UsedGroups");
                }
            }
        }

        private SelectorViewModel selector;
        public SelectorViewModel Selector
        {
            get { return selector; }
            set { selector = value; }
        }

        #endregion

        #region Constructors

         public LectureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalEntity = new LectureDTO();
            
            this.selector = new SelectorViewModel(userInterop, controllerInterop, dispatcher, false);
        }

         public LectureViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, LectureDTO lecture)
             : base(userInterop, controllerInterop, dispatcher)
         {
             this.originalEntity = lecture;

             var teacher = ControllerInterop.Service.GetTeacher(ControllerInterop.Session, lecture.TeacherID);
             var cathedra = ControllerInterop.Service.GetCathedra(ControllerInterop.Session, teacher.Cathedra.ID);
             var faculty = ControllerInterop.Service.GetFaculty(ControllerInterop.Session, cathedra.FacultyID);

             this.selector = new SelectorViewModel(userInterop, controllerInterop, dispatcher, faculty, selector_SelectorItemChanged, false);
         }

        #endregion

        #region Methods

        protected override void DoRefresh()
        {
            throw new NotImplementedException();
        }

        private void InitializeGroups(BaseEntityDTO entity)
        {
            unusedGroups = new ObservableCollection<GroupDTO>();

            if (Lecture != null)
            {
                List<GroupDTO> groups = ControllerInterop.Service.GetAllGroups(ControllerInterop.Session);

                UsedGroups = new ObservableCollection<GroupDTO>(Lecture.Groups);

                foreach (GroupDTO group in groups)
                    if (Lecture.Groups.Find(g => g.ID == group.ID) == null)
                        UnusedGroups.Add(group);

                if (entity is InstituteDTO)
                {
                    List<GroupDTO> instituteGroups;
                    if (entity.ID > 0)
                        instituteGroups = ControllerInterop.Service.GetInstituteGroups(ControllerInterop.Session, entity.ID);
                    else
                        instituteGroups = ControllerInterop.Service.GetAllGroups(ControllerInterop.Session);
                    UsedGroups = new ObservableCollection<GroupDTO>((from g in usedGroups
                                                                     where instituteGroups.Find(st => st.ID == g.ID) != null
                                                                     select g).ToList());
                    UnusedGroups = new ObservableCollection<GroupDTO>((from g in unusedGroups
                                                                       where instituteGroups.Find(st => st.ID == g.ID) != null
                                                                       select g).ToList());
                }
                else if (entity is FacultyDTO)
                {
                    List<GroupDTO> facultyGroups = ControllerInterop.Service.GetFacultyGroups(ControllerInterop.Session, entity.ID);
                    UsedGroups = new ObservableCollection<GroupDTO>((from g in usedGroups
                                                                     where facultyGroups.Find(st => st.ID == g.ID) != null
                                                                     select g).ToList());
                    UnusedGroups = new ObservableCollection<GroupDTO>((from g in unusedGroups
                                                                       where facultyGroups.Find(st => st.ID == g.ID) != null
                                                                       select g).ToList());
                }
                else if (entity is CathedraDTO)
                {
                    List<GroupDTO> cathedraGroups = ControllerInterop.Service.GetGroups(ControllerInterop.Session, new CathedraRef { ID = entity.ID });
                    UsedGroups = new ObservableCollection<GroupDTO>((from g in usedGroups
                                                                     where cathedraGroups.Find(st => st.ID == g.ID) != null
                                                                     select g).ToList());
                    UnusedGroups = new ObservableCollection<GroupDTO>((from g in unusedGroups
                                                                       where cathedraGroups.Find(st => st.ID == g.ID) != null
                                                                       select g).ToList());
                }
                UsedGroups.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(UsedGroups_CollectionChanged);
                UnusedGroups.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(UsedGroups_CollectionChanged);
            }
        }
       
        public override void Remove()
        {
            
        }

        public override void Rollback()
        {
            Lecture.Assign(OriginalLecture);
            SetUnModified();
            Selector.Helper.Entity = Selector.CurrentEntity;
        }

        public override void Save()
        {
            foreach (var group in UsedGroups)
            {
                if (Lecture.Groups.Find(g => g.ID == group.ID) == null)
                    Lecture.Groups.Add(group);
            }
            foreach (var group in UnusedGroups)
            {
                if (Lecture.Groups.Find(g => g.ID == group.ID) != null)
                    Lecture.Groups.Remove(group);
            }

            ControllerInterop.Service.SaveLecture(ControllerInterop.Session, Lecture.ToDTO());
            SetUnModified();

            Selector.Helper.Entity = Selector.CurrentEntity;
        }

        protected override object LoadDataFromServer()
        {
            return null;
        }

        protected override void AfterDataLoaded()
        {
            Model = new LectureModel(originalEntity as LectureDTO);
            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);

            selector.SelectorItemChanged += new EventHandler(selector_SelectorItemChanged);
            selector.UpdateProperties(selector.Helper.Entity, false);
        }

        protected override void ClearData()
        {
            if (selector != null)
                selector.SelectorItemChanged -= new EventHandler(selector_SelectorItemChanged);

            if (UsedGroups != null)
                UsedGroups = null;
            if (UnusedGroups != null)
                UnusedGroups = null;
        }

        #endregion

        #region Callbacks

        private void UsedGroups_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SetModified();

            Selector.IsEnabled = false;

            OnPropertyChanged("UsedGroups");
            OnPropertyChanged("UnusedGroups");
        }

        private void selector_SelectorItemChanged(object sender, EventArgs e)
        {
            if (e is StudyingController.ViewModels.SelectorViewModel.SelectorItemChangedEventArgs)
                InitializeGroups((e as StudyingController.ViewModels.SelectorViewModel.SelectorItemChangedEventArgs).Entity);
            else
                throw new Exception("Not StudyingController.ViewModels.SelectorViewModel.SelectorItemChangedEventArgs");
        }
        #endregion
    }
}
