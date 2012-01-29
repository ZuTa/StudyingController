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
            get { return model as TeacherModel; }
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
            Load();

            originalEntity = new TeacherDTO();
            model = new TeacherModel(originalEntity as TeacherDTO) { Role = UserRoles.Teacher };
            this.model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public TeacherViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, TeacherDTO teacher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            cathedras = new List<CathedraDTO>();
            Load();

            originalEntity = teacher;
            teacher.Cathedra = (from cathedra in cathedras
                                        where cathedra.ID == OriginalTeacher.CathedraID
                                        select cathedra).FirstOrDefault();

            model = new TeacherModel(teacher);
            model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods

        public override void Remove()
        {
            throw new NotImplementedException();
        }

        public override void Rollback()
        {
            Teacher.Assign(OriginalTeacher);
            SetUnModified();
        }

        public override void Save()
        {
            TeacherDTO cathedraAdminDTO = Teacher.ToDTO();
            cathedraAdminDTO.Password = HashHelper.ComputeHash((model as SystemUserModel).Login);
            ControllerInterop.Service.SaveUser(ControllerInterop.Session, cathedraAdminDTO);
            SetUnModified();
        }

        public void Load()
        {
            Cathedras = ControllerInterop.Service.GetAllCathedras(ControllerInterop.Session);
        }

        #endregion
    }
}
