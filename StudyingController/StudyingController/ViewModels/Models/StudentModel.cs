﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingController.ViewModels.Models
{
    public class StudentModel : SystemUserModel, IDTOable<StudentDTO>
    {
        #region Fields & Properties

        private GroupDTO group;
        public GroupDTO Group
        {
            get { return group; }
            set 
            { 
                group = value;
                OnPropertyChanged("Group");
            }
        }

        #endregion

        #region Constructors

        public StudentModel(StudentDTO student)
            : base(student)
        {
            this.group = student.Group;
        }

        #endregion

        #region Methods

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            StudentDTO student = entity as StudentDTO;
            this.Group = student.Group;
        }

        public StudentDTO ToDTO()
        {
            return new StudentDTO
            {
                ID = this.ID,
                Login = this.Login,
                Password = this.Password,
                Role = this.Role,
                UserInformation = this.UserInformation.ToDTO(),
                GroupID = this.Group.ID
            };
        }

        private bool IsGroupValid(out string error)
        {
            error = null;
            if (group == null)
            {
                error = Properties.Resources.ErrorStructureNotFound;
                return false;
            }
            return true;
        }

        protected override string Validate(string property)
        {
            string error = base.Validate(property);
            if (error == null)
            {
                switch (property)
                {
                    case "Group":
                        IsGroupValid(out error);
                        break;
                    default:
                        break;
                }
            }
            return error;
        }

        #endregion
    }
}