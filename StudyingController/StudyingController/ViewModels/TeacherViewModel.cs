using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.ViewModels.Models;
using StudyingController.Common;
using System.Windows.Threading;

namespace StudyingController.ViewModels
{
    public class TeacherViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public TeacherDTO OriginalTeacher
        {
            get { return originalEntity as TeacherDTO; }
        }

        public TeacherModel Teacher
        {
            get { return Model as TeacherModel; }
        }

        private List<CathedraDTO> cathedras;
        public List<CathedraDTO> Cathedras
        {
            get { return cathedras; }
            set
            {
                cathedras = value;
                OnPropertyChanged("Cathedras");
            }
        }

        #endregion

        #region Constructors

        public TeacherViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            cathedras = new List<CathedraDTO>();

            originalEntity = new TeacherDTO(){ Role = UserRoles.Teacher };
        }

        public TeacherViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, TeacherDTO teacher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            cathedras = new List<CathedraDTO>();

            originalEntity = teacher;
        }

        #endregion

        #region Methods

        protected override void DoRefresh()
        {
            throw new NotImplementedException();
        }

        public override void Remove()
        {
            ControllerInterop.Service.DeleteUser(ControllerInterop.Session, Teacher.ID);
        }

        public override void Rollback()
        {
            Teacher.Assign(OriginalTeacher);
            SetUnModified();
        }

        public override void Save()
        {
            TeacherDTO teacherDTO = Teacher.ToDTO();
            if(teacherDTO.Password == null)
                teacherDTO.Password = HashHelper.ComputeHash((Model as SystemUserModel).Login);
            else
                teacherDTO.Password = HashHelper.ComputeHash((Model as SystemUserModel).Password);
            ControllerInterop.Service.SaveUser(ControllerInterop.Session, teacherDTO);
            SetUnModified();
        }

        protected override void LoadData()
        {
            Cathedras = ControllerInterop.Service.GetAllCathedras(ControllerInterop.Session);

            TeacherDTO teacher = originalEntity as TeacherDTO;
            if (teacher.Exists())
            {
                teacher.Cathedra = (from cathedra in cathedras
                                    where cathedra.ID == OriginalTeacher.CathedraID
                                    select cathedra).FirstOrDefault();
            }

            Model = new TeacherModel(teacher);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);

        }

        protected override void ClearData()
        {
            if (Cathedras != null)
                Cathedras.Clear();
        }

        #endregion
    }
}
