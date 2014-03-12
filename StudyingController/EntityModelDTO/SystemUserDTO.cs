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

        public string Name
        {
            get { return string.Format("{0} {1} {2}", LastName, FirstName, MiddleName); }
        }

        public string ShortName
        {
            get
            {
                return LastName
                + (!string.IsNullOrEmpty(FirstName) ? FirstName[0] + "." : "")
                + (!string.IsNullOrEmpty(MiddleName) ? MiddleName[0] + "." : "");
            }
        }

        private string firstName;
        [DataMember]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string middleName;
        [DataMember]
        public string MiddleName
        {
            get { return middleName; }
            set { middleName = value; }
        }

        private string lastName;
        [DataMember]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private byte[] picture;
        [DataMember]
        public byte[] Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        private DateTime birth;
        [DataMember]
        public DateTime Birth
        {
            get { return birth; }
            set { birth = value; }
        }

        private string email;
        [DataMember]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public SystemUserDTO()
        {
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
