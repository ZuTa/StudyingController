using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using StudyingController.ViewModels.Models;

namespace StudyingController.ViewModels
{
    public class GroupViewModel : SaveableViewModel
    {
        #region Fields & Properties

        public GroupDTO OriginalGroup
        {
            get
            {
                return originalEntity as GroupDTO;
            }
        }

        public GroupModel Group
        {
            get { return model as GroupModel; }
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

        public GroupViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            cathedras = new List<CathedraDTO>();
            Load();

            originalEntity = new GroupDTO();
            model = new GroupModel(originalEntity as GroupDTO);
            model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        public GroupViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, GroupDTO group)
            : base(userInterop, controllerInterop, dispatcher)
        {
            cathedras = new List<CathedraDTO>();
            Load();

            originalEntity = group;
            group.Cathedra = (from cathedra in cathedras
                                where cathedra.ID == OriginalGroup.CathedraID
                                select cathedra).FirstOrDefault();

            model = new GroupModel(group);
            model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
        }

        #endregion

        #region Methods

        private void Load()
        {
            //Cathedras.AddRange(ControllerInterop.Service.get);
        }

        public void Save()
        {
            GroupDTO groupDTO = Group.ToDTO();
            ControllerInterop.Service.SaveGroup(ControllerInterop.Session, groupDTO);
            SetUnModified();
        }

        public override void Rollback()
        {
            Group.Assign(OriginalGroup);
            SetUnModified();
        }

        #endregion

        #region Callbacks

        #endregion
    }
}
