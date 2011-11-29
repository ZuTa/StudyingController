using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    public enum UserRoles
    {
        /// <summary>
        /// Головний адміністратор
        /// </summary>
        MainAdmin = 0,
        /// <summary>
        /// Адміністратор інституту
        /// </summary>
        InstituteAdmin = 1,
        /// <summary>
        /// Адміністратор факультет
        /// </summary>
        FacultyAdmin = 2,
        /// <summary>
        /// Головний секретар
        /// </summary>
        MainSecretary = 3,
        /// <summary>
        /// Секретар інституту
        /// </summary>
        InstituteSecretary = 4,
        /// <summary>
        /// Секретар факультету
        /// </summary>
        FacultySecretary = 5,
        /// <summary>
        /// Викладач
        /// </summary>
        Teacher = 6,
        /// <summary>
        /// Студент
        /// </summary>
        Student = 7
    }

    [DataContract]
    public class SystemUserDTO : BaseEntityDTO
    {
        private string login;
        [DataMember]
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        private UserRoles userRole;
        [DataMember]
        public UserRoles UserRole
        {
            get { return userRole; }
            set { userRole = value; }
        }

        private UserInformationDTO userInformation;
        [DataMember]
        public UserInformationDTO UserInformation
        {
            get { return userInformation; }
            set { userInformation = value; }
        }

    }
}
