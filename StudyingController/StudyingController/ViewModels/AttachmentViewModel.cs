using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntitiesDTO;
using StudyingController.ViewModels.Models;
using StudyingController.Common;
using System.Windows.Threading;
using System.IO;
using System.ComponentModel;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;

namespace StudyingController.ViewModels
{
    public class AttachmentViewModel : SaveableViewModel, IDataErrorInfo
    {
        #region Fields & Properties

        public AttachmentDTO OriginalAttachment
        {
            get { return originalEntity as AttachmentDTO; }
        }

        public AttachmentModel Attachment
        {
            get { return Model as AttachmentModel; }
        }

        private string attachmentPath;
        [Validateable]
        public string AttachmentPath
        {
            get { return attachmentPath; }
            set
            {
                if (attachmentPath != value)
                {
                    attachmentPath = value;
                    if(attachmentPath==null)
                        attachmentPath = "";
                    Match match = Regex.Match(attachmentPath, @"(?<=\\)[\w _\-=]*(\.[\w _\-=]+)?$");
                    Attachment.Name = match.Value;
                    OnPropertyChanged("AttachmentPath");
                    SetModified();
                }
            }
        }

        private bool isValid;
        public override bool IsValid
        {
            get
            {
                return base.IsValid && isValid;
            }
        }

        private bool isNewAttachment;
        public bool IsNewAttachment
        {
            get { return isNewAttachment; }
            set 
            {
                if (isNewAttachment != value)
                {
                    isNewAttachment = value;
                    OnPropertyChanged("IsNewAttachment");
                    OnPropertyChanged("AttachmentPath");
                    OnPropertyChanged("IsNotNewAttachment");
                }
            }
        }

        public bool IsNotNewAttachment
        {
            get { return !IsNewAttachment; }
        }

        #endregion

        #region Constructors

        public AttachmentViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalEntity = new AttachmentDTO();
            this.Model = new AttachmentModel(originalEntity as AttachmentDTO);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Model_PropertyChanged);
            IsNewAttachment = false;
        }

        public AttachmentViewModel(IUserInterop userInterop, IControllerInterop controllerInterop, Dispatcher dispatcher, AttachmentDTO attachment)
            : base(userInterop, controllerInterop, dispatcher)
        {
            this.originalEntity = attachment;
            this.Model = new AttachmentModel(attachment);
            this.Model.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ModelPropertyChanged);
            IsNewAttachment = false;
        }

        #endregion

        #region Commands

        private RelayCommand browseCommand;
        public RelayCommand BrowseCommand
        {
            get
            {
                if (browseCommand == null)
                {
                    browseCommand = new RelayCommand(param =>
                    {
                        LoadOpenDialog();
                    });
                }
                return browseCommand;
            }
        }

        
        #endregion

        #region Methods

        public void OpenAttachment()
        {
            string tempPath = Path.GetTempPath();
            string tempFile = tempPath+"\\"+Path.GetRandomFileName()+Attachment.Name;
            FileStream fStream = new FileStream(tempFile, FileMode.CreateNew, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fStream);
            foreach (byte b in Attachment.Data)
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
                FileStream fStream = new FileStream(folderBrowser.SelectedPath + "\\" + Attachment.Name, FileMode.CreateNew, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fStream);
                foreach (byte b in Attachment.Data)
                {
                    bw.Write(b);
                    bw.Flush();
                }
                bw.Close();
                fStream.Close();
            }
        }

        private void LoadOpenDialog()
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "All Files|*.*";
            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == true)
            {
                AttachmentPath = fileDialog.FileName;
            }
        }

        public override void Remove()
        {
        }

        public override void Rollback()
        {
            Attachment.Assign(OriginalAttachment);
            SetUnModified();
            IsNewAttachment = false;
        }

        public override void Save()
        {
            if (isNewAttachment)
                AddNewAttachment();
            else
                ControllerInterop.Service.EditAttachment(ControllerInterop.Session, Attachment.ToDTO());
            SetUnModified();
            IsNewAttachment = false;
        }

        private void AddNewAttachment()
        {
            if (AttachmentPath != null)
            {
                byte[] data = ReadFile(AttachmentPath);
                AttachmentDTO attachment = new AttachmentDTO
                {
                    Data = data,
                    DateAdded = DateTime.Now,
                    Description = Attachment.Description,
                    Name = Attachment.Name,
                    TeacherID = ControllerInterop.Session.User.ID
                };

                ControllerInterop.Service.SaveAttachment(ControllerInterop.Session, attachment);

                AttachmentPath = null;
            }
            else
                throw new NullReferenceException();
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

        private bool IsPathValid(out string error)
        {
            error = null;
            if (isNewAttachment == true && (AttachmentPath == null || AttachmentPath.Length == 0))
            {
                error = Properties.Resources.ErrorFieldEmpty;
                return false;
            }
            try
            {
                FileInfo fileInfo = new FileInfo(AttachmentPath);
                Attachment.Size = Math.Round(fileInfo.Length / 1024.0, 3).ToString() + " kb";
                if (fileInfo.Length > 1024 * 1024)
                {
                    error = Properties.Resources.ErrorAttachmentIsTooLarge;
                    return false;
                }
            }
            catch { }
            return true;
        }

        protected string Validate(string property)
        {
            string error = string.Empty;
                switch (property)
                {
                    case "AttachmentPath":
                        isValid = IsPathValid(out error);
                        break;
                    default:
                        break;
                }
            return error;
        }

        #endregion

        #region Callbacks

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SetModified();
        }

        #endregion

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string property]
        {
            get { return Validate(property); }
        }
    }
}
