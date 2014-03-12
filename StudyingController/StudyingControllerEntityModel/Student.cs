using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;

namespace StudyingControllerEntityModel
{
    public partial class Student : IDTOable<StudentDTO>
    {
        private int currentGroupID;
        public int CurrentGroupID
        {
            get { return currentGroupID; }
            set { currentGroupID = value; }
        }

        #region Constructors

        public Student()
        {
 
        }

        public Student(SystemUserDTO user)
            :base(user)
        {
            Assign(user as StudentDTO);
        }

        #endregion
        
        public new StudentDTO ToDTO()
        {
            return new StudentDTO
            {
                ID = this.ID,
                Login = this.Login,
                Role = this.Role,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                LastName = this.LastName,
                Picture = this.Picture,
                Birth = this.Birth.HasValue ? this.Birth.Value: DateTime.MinValue,
                Email = this.Email,
                GroupID = this.Groups.First().ID,
                Gradebook = this.Gradebook,
                StudentCard = this.StudentCard,
            };
        }

        public void Assign(StudentDTO entity)
        {
            base.Assign(entity);
            this.CurrentGroupID = entity.GroupID;
            this.Gradebook = entity.Gradebook;
            this.StudentCard = entity.StudentCard;
        }

    }
}
