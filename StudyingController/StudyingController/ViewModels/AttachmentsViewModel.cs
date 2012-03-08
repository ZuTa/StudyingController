using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudyingController.Common;
using System.Windows.Threading;
using EntitiesDTO;
using System.Collections.ObjectModel;
using StudyingController.ViewModels.Models;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;

namespace StudyingController.ViewModels
{
    public class AttachmentsViewModel : LoadableViewModel, IEditable,IAdditionalCommands, IRefreshable
    {
        #region Fields & Properties

        private ObservableCollection<AttachmentModel> attachments;
        public ObservableCollection<AttachmentModel> Attachments
        {
            get { return attachments; }
            set 
            {
                if (attachments != value)
                {
                    attachments = value;
                    OnPropertyChanged("Attachments");
                }
            }
        }

        private BaseModel currentEntity;
        public BaseModel CurrentEntity
        {
            get { return currentEntity; }
            set
            {
                if (currentEntity != value)
                {
                    currentEntity = value;
                    OnPropertyChanged("CurrentEntity");
                    UpdateCommandsEnabledState();
                }
            }
        }

        protected ObservableCollection<AttachmentModel> previousAttachments;
        protected BaseModel previousSelectedEntity;

        private SystemUserDTO user;

        private bool CanManipulate
        {
            get { return CurrentEntity != null; }
        }

        private EditModes editMode;
        public EditModes EditMode
        {
            get { return editMode; }
            set { editMode = value; }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsEditingAllowed
        {
            get { throw new NotImplementedException(); }
        }

        private bool isModified;
        public bool IsModified
        {
            get { return isModified; }
        }

        public bool CanSave
        {
            get { return IsModified && IsModelsValid(); }
        }

        private ObservableCollection<NamedCommandData> additionalCommands;
        public ObservableCollection<NamedCommandData> AdditionalCommands
        {
            get
            {
                if (additionalCommands == null)
                {
                    additionalCommands = new ObservableCollection<NamedCommandData>();
                    NamedCommandData addAttachmentNamedCommand = new NamedCommandData
                    {
                        Command = AddAttachmentCommand,
                        Name = "Додати",
                        Type = CommandTypes.Add
                    };
                    addAttachmentNamedCommand.UpdateEnabledState = () => addAttachmentNamedCommand.IsEnabled = true;
                    NamedCommandData removeAttachmentNamedCommand = new NamedCommandData
                    {
                        Command = RemoveAttachmentCommand,
                        Name = "Видалити",
                        Type = CommandTypes.Remove
                    };
                    removeAttachmentNamedCommand.UpdateEnabledState = () => removeAttachmentNamedCommand.IsEnabled = CanManipulate;
                    NamedCommandData downloadAttachmentNamedCommand = new NamedCommandData
                    {
                        Command = DownloadAttachmentCommand,
                        Name = "Завантажити",
                        Type = CommandTypes.Download
                    };
                    downloadAttachmentNamedCommand.UpdateEnabledState = () => downloadAttachmentNamedCommand.IsEnabled = CanManipulate;
                    NamedCommandData openAttachmentNamedCommand = new NamedCommandData
                    {
                        Command = OpenAttachmentCommand,
                        Name = "Відкрити",
                        Type = CommandTypes.Open
                    };
                    openAttachmentNamedCommand.UpdateEnabledState = () => openAttachmentNamedCommand.IsEnabled = CanManipulate;

                    additionalCommands.Add(addAttachmentNamedCommand);
                    additionalCommands.Add(removeAttachmentNamedCommand);
                    additionalCommands.Add(downloadAttachmentNamedCommand);
                    additionalCommands.Add(openAttachmentNamedCommand);
                    UpdateCommandsEnabledState();
                }
                return additionalCommands;
            }
        }

        #endregion

        #region Constructors

        public AttachmentsViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, SystemUserDTO user)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.user = user;
            if (user.Role == UserRoles.Teacher)
                EditMode = EditModes.Editable;
            else
                EditMode = EditModes.ReadOnly;
            LoadData();
        }

        #endregion

        #region Commands

        private RelayCommand addAttachmentCommand;
        public RelayCommand AddAttachmentCommand
        {
            get
            {
                if (addAttachmentCommand == null)
                {
                    addAttachmentCommand = new RelayCommand(param =>
                    {
                        LoadOpenDialog();
                    });
                }
                return addAttachmentCommand;
            }
        }

        private RelayCommand removeAttachmentCommand;
        public RelayCommand RemoveAttachmentCommand
        {
            get
            {
                if (removeAttachmentCommand == null)
                {
                    removeAttachmentCommand = new RelayCommand(param =>
                    {
                        Attachments.Remove((CurrentEntity as AttachmentModel));
                    });
                }
                return removeAttachmentCommand;
            }
        }

        private RelayCommand downloadAttachmentCommand;
        public RelayCommand DownloadAttachmentCommand
        {
            get
            {
                if (downloadAttachmentCommand == null)
                {
                    downloadAttachmentCommand = new RelayCommand(param =>
                    {
                        LoadSaveDialog();
                    });
                }
                return downloadAttachmentCommand;
            }
        }

        private RelayCommand openAttachmentCommand;
        public RelayCommand OpenAttachmentCommand
        {
            get
            {
                if (openAttachmentCommand == null)
                {
                    openAttachmentCommand = new RelayCommand(param =>
                    {
                        OpenAttachment();
                    });
                }
                return openAttachmentCommand;
            }
        }

        #endregion

        #region Methods

        private bool IsModelsValid()
        {
            foreach (var model in Attachments)
                if (!model.IsValid)
                    return false;
            return true;
        }

        protected virtual void OnViewModified()
        {
            if (ViewModified != null)
                ViewModified(this, EventArgs.Empty);
        }

        protected virtual void OnViewUnModified()
        {
            if (ViewUnModified != null)
                ViewUnModified(this, EventArgs.Empty);
        }

        protected virtual void SetModified()
        {
            isModified = true;
            OnViewModified();
            OnPropertyChanged("IsModified");
            OnPropertyChanged("CanSave");
        }

        protected virtual void SetUnModified()
        {
            isModified = false;

            OnViewUnModified();
            OnPropertyChanged("IsModified");
            OnPropertyChanged("CanSave");
        }

        private void InitializeAttachments(int userID)
        {
            if (Attachments != null)
            Attachments.CollectionChanged -= new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Attachments_CollectionChanged);
            Attachments = new ObservableCollection<AttachmentModel>();
            foreach (var a in ControllerInterop.Service.GetAttachments(ControllerInterop.Session, userID))
            {
                AttachmentModel attModel = new AttachmentModel(a);
                attModel.ModelChanged += new EventHandler(attachmentModel_ModelChanged);
                Attachments.Add(attModel);
            }
            Attachments.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Attachments_CollectionChanged);
        }

        protected override void LoadData()
        {
            InitializeAttachments(user.ID);
        }

        protected override void ClearData()
        {
            previousSelectedEntity = CurrentEntity;
            previousAttachments = Attachments;
            Attachments = new ObservableCollection<AttachmentModel>();
        }

        public void Refresh()
        {
            ClearData();
            LoadData();
            CurrentEntity = Attachments.Where(a => previousAttachments.ToList().Find(p => a.ID == p.ID) == null).FirstOrDefault();
            if(CurrentEntity==null)
                if (previousSelectedEntity != null)
                    CurrentEntity = (from a in Attachments
                                     where a.ID == previousSelectedEntity.ID
                                     select a).FirstOrDefault();
                else
                    CurrentEntity = null;
        }

        private void LoadOpenDialog()
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "All Files|*.*";
            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == true)
            {
                if (fileDialog.FileName == null)
                    fileDialog.FileName = "";
                Match match = Regex.Match(fileDialog.FileName, @"(?<=\\)[\w _\-=]*(\.[\w _\-=]+)?$");
                AttachmentDTO attachment = new AttachmentDTO
                {
                    Name = match.Value,
                    TeacherID = user.ID,
                    Data = ReadFile(fileDialog.FileName),
                    DateAdded = DateTime.Now,
                    Description = ""
                };
                AttachmentModel attachmentModel = new AttachmentModel(attachment);
                attachmentModel.ModelChanged += new EventHandler(attachmentModel_ModelChanged);
                Attachments.Add(attachmentModel);
                CurrentEntity = attachmentModel;
            }
        }

        public void OpenAttachment()
        {
            string tempPath = Path.GetTempPath();
            string tempFile = tempPath + "\\" + Path.GetRandomFileName() + (CurrentEntity as AttachmentModel).Name;
            FileStream fStream = new FileStream(tempFile, FileMode.CreateNew, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fStream);
            foreach (byte b in (CurrentEntity as AttachmentModel).Data)
            {
                bw.Write(b);
                bw.Flush();
            }
            bw.Close();
            fStream.Close();
            Process process = new Process();
            process.StartInfo.FileName = tempFile;
            process.StartInfo.Arguments = "";
            process.Start();
        }

        public void LoadSaveDialog()
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                FileStream fStream = new FileStream(folderBrowser.SelectedPath + "\\" + (CurrentEntity as AttachmentModel).Name, FileMode.CreateNew, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fStream);
                foreach (byte b in (CurrentEntity as AttachmentModel).Data)
                {
                    bw.Write(b);
                    bw.Flush();
                }
                bw.Close();
                fStream.Close();
            }
        }

        private byte[] ReadFile(string sPath)
        {
            byte[] data = null;
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fStream);

            data = br.ReadBytes((int)numBytes);
            br.Close();
            fStream.Close();
            return data;
        }

        public void Save()
        {
            List<AttachmentDTO> atts = new List<AttachmentDTO>();
            foreach(var a in Attachments)
                atts.Add(a.ToDTO());
            ControllerInterop.Service.SaveAttachments(ControllerInterop.Session, user.ID, atts);
            SetUnModified();
            Refresh();
        }

        public void Rollback()
        {
            Refresh();
            SetUnModified();
        }

        public void UpdateCommandsEnabledState()
        {
            foreach (var ac in AdditionalCommands)
                if (ac.UpdateEnabledState != null)
                    ac.UpdateEnabledState();
        }

        #endregion

        #region Callbacks

        private void attachmentModel_ModelChanged(object sender, EventArgs e)
        {
            SetModified();
        }

        private void Attachments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SetModified();
        }

        #endregion

        #region Events

        public event EventHandler ViewModified;

        public event EventHandler ViewUnModified;

        #endregion

        
        
    }
}
