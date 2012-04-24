using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EntitiesDTO
{
    [DataContract]
    [Flags]
    public enum UserRoles
    {
        /// <summary>
        /// Без ролі
        /// </summary>
        [EnumMember]
        None = 0,
        /// <summary>
        /// Головний адміністратор
        /// </summary>
        [ResourceName("MainAdminTxt")]
        [PluralizeName("MainAdminsTxt")]
        [EnumMember]
        MainAdmin = 1,
        /// <summary>
        /// Адміністратор інституту
        /// </summary>
        [ResourceName("InstituteAdminTxt")]
        [PluralizeName("InstituteAdminsTxt")]
        [EnumMember]
        InstituteAdmin = 2,
        /// <summary>
        /// Адміністратор факультет
        /// </summary>
        [ResourceName("FacultyAdminTxt")]
        [PluralizeName("FacultyAdminsTxt")]
        [EnumMember]
        FacultyAdmin = 4,
        /// <summary>
        /// Головний секретар
        /// </summary>
        [ResourceName("MainSecretaryTxt")]
        [PluralizeName("MainSecretariesTxt")]
        [EnumMember]
        MainSecretary = 8,
        /// <summary>
        /// Секретар інституту
        /// </summary>
        [ResourceName("InstituteSecretaryTxt")]
        [PluralizeName("InstituteSecretariesTxt")]
        [EnumMember]
        InstituteSecretary = 16,
        /// <summary>
        /// Секретар факультету
        /// </summary>
        [ResourceName("FacultySecretaryTxt")]
        [PluralizeName("FacultySecretariesTxt")]
        [EnumMember]
        FacultySecretary = 32,
        /// <summary>
        /// Викладач
        /// </summary>
        [ResourceName("TeacherTxt")]
        [PluralizeName("TeachersTxt")]
        [EnumMember]
        Teacher = 64,
        /// <summary>
        /// Студент
        /// </summary>
        [ResourceName("StudentTxt")]
        [PluralizeName("StudentsTxt")]
        [EnumMember]
        Student = 128
    }

    [DataContract]
    [KnownType(typeof(UserInformationDTO))]
    [KnownType(typeof(FacultyAdminDTO))]
    [KnownType(typeof(InstituteAdminDTO))]
    [KnownType(typeof(InstituteSecretaryDTO))]
    [KnownType(typeof(FacultySecretaryDTO))]
    [KnownType(typeof(TeacherDTO))]
    [KnownType(typeof(StudentDTO))]

    public class SystemUserDTO : BaseEntityDTO
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

        private UserRoles role;
        [DataMember]
        public UserRoles Role
        {
            get { return role; }
            set { role = value; }
        }

        private UserInformationDTO userInformation;
        [DataMember]
        public UserInformationDTO UserInformation
        {
            get { return userInformation; }
            set { userInformation = value; }
        }

        public SystemUserDTO()
        {
            userInformation = new UserInformationDTO();
        }

        public override string ToString()
        {
            return UserInformation.ToString();
        }
    }
}
