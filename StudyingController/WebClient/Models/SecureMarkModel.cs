using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThinClient.SCS;

namespace ThinClient.Models
{
    public class SecureMarkModel
    {
        public string StudentName
        {
            get 
            {
                var nameAndSurname = string.Empty;
                if (this.Mark != null && this.Mark.Student != null && this.Mark.Student.UserInformation != null)
                {
                    nameAndSurname = this.Mark.Student.UserInformation.LastName + this.Mark.Student.UserInformation.FirstName;
                }

                return nameAndSurname;
            }
        }

        public MarkDTO Mark
        {
            get;
            private set;
        }

        public Guid Guid
        {
            get;
            private set;
        }

        public SecureMarkModel(MarkDTO mark) 
        {
            this.Mark = mark;
            this.Guid = Guid.NewGuid();
        }
    }
}