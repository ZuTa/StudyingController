using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using System.Collections.ObjectModel;

namespace StudyingController.ViewModels.Models
{
    public class PracticeTeacherModel : BaseModel, IDTOable<PracticeTeacherDTO>
    {
        #region Fields & Properties

        private List<StudentDTO> students;
        public List<StudentDTO> Students
        {
            get
            {
                return students;
            }

            set
            {
                students = value;
            }
        }

        private TeacherRef teacher;
        public TeacherRef Teacher
        {
            get { return teacher; }
            set { teacher = value; }
        }

        private PracticeRef practice;
        public PracticeRef Practice
        {
            get { return practice; }
            set { practice = value; }
        }

        #endregion

        #region Constructors

        public PracticeTeacherModel(PracticeTeacherDTO pract)
            : base(pract)
        {
            Assign(pract);
        }

        #endregion

        #region Methods

        public override void Assign(BaseEntityDTO entity)
        {
            base.Assign(entity);

            PracticeTeacherDTO pract = entity as PracticeTeacherDTO;
            this.students = pract.Students;
            this.teacher = pract.Teacher;
            this.practice = pract.Practice;
        }

        public PracticeTeacherDTO ToDTO()
        {
            return new PracticeTeacherDTO()
            {
                ID = this.ID,
                Practice = this.practice,
                Teacher = this.Teacher,
                Students = this.students.ToList()
            };
        }

        protected override string Validate(string property)
        {
            return base.Validate(property);
        }

        #endregion

        #region Callbacks
        #endregion
    }
}
