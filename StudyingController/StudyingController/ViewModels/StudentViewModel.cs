﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.ViewModels.Models;
using StudyingController.Common;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public class StudentViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public StudentDTO OriginalStudent
        {
            get { return originalEntity as StudentDTO; }
        }

        public StudentModel Student
        {
            get { return Model as StudentModel; }
        }

        private List<GroupDTO> groups;
        public List<GroupDTO> Groups
        {
            get { return groups; }
            set
            {
                groups = value;
                OnPropertyChanged("Groups");
            }
        }

        #endregion

        #region Constructors

        public StudentViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            groups = new List<GroupDTO>();
            Load();

            originalEntity = new StudentDTO();
            Model = new StudentModel(originalEntity as StudentDTO) { Role = UserRoles.Student };
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public StudentViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, StudentDTO student)
            : base(userInterop, controllerInterop, dispatcher)
        {
            groups = new List<GroupDTO>();
            Load();

            originalEntity = student;
            student.Group = (from g in groups
                                where g.ID == OriginalStudent.GroupID
                                select g).FirstOrDefault();

            Model = new StudentModel(student);
            Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        public override void Rollback()
        {
            Student.Assign(OriginalStudent);
            SetUnModified();
        }

        public override void Save()
        {
            StudentDTO groupAdminDTO = Student.ToDTO();
            if(groupAdminDTO.Password == null)
                groupAdminDTO.Password = HashHelper.ComputeHash((Model as SystemUserModel).Login);
            else
                groupAdminDTO.Password = HashHelper.ComputeHash((Model as SystemUserModel).Password);
            ControllerInterop.Service.SaveUser(ControllerInterop.Session, groupAdminDTO);
            SetUnModified();
        }

        public void Load()
        {
            //Groups = ControllerInterop.Service.get(ControllerInterop.Session);
        }

        #endregion
    }
}