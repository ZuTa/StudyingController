﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using System.Collections.ObjectModel;
using StudyingController.Common;

namespace StudyingController.ViewModels.Models
{
    class LectureModel : BaseModel, IDTOable<LectureDTO>
    {
        #region Fields & Properties

        private ObservableCollection<GroupDTO> groups;
        public ObservableCollection<GroupDTO> Groups
        {
            get
            {
                return groups;
            }

            set
            {
                groups = value;
            }
        }

        private TeacherDTO teacher;
        public TeacherDTO Teacher
        {
            get { return teacher; }
            set { teacher = value; }
        }

        private int teacherID;
        public int TeacherID
        {
            get { return teacherID; }
            set { teacherID = value; }
        }

        private SubjectDTO subject;
        public SubjectDTO Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        #endregion

        #region Constructors

        public LectureModel(LectureDTO lecture)
            :base(lecture)
        {
            Assign(lecture);
            this.groups.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(groups_CollectionChanged);
        }

        #endregion

        #region Methods

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            LectureDTO lecture = entity as LectureDTO;
            this.groups = new ObservableCollection<GroupDTO>(lecture.Groups);
            this.teacher = lecture.Teacher;
            this.teacherID = lecture.TeacherID;
            this.subject = lecture.Subject;
        }

        public LectureDTO ToDTO()
        {
            return new LectureDTO() 
            { 
                ID = this.ID ,
                Teacher = this.Teacher,
                TeacherID = this.TeacherID,
                Subject = this.Subject,
                Groups = this.Groups.ToList()
            };
        }

        protected override string Validate(string property)
        {
            return base.Validate(property);
        }

        #endregion

        #region Callbacks

        private void groups_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("Groups");
        }   

        #endregion
    }
}