using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ModelDTO
{
    [DataContract]
    public class SystemUserDTO : BaseDTO
    {
        private string login;
        [DataMember]
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        private string password;
        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public SystemUserDTO()
            : base()
        {
        
        }

        public override string ToString()
        {
            return Login;
        }
    }
}
