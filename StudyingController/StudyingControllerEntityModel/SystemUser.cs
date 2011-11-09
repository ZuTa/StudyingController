using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace StudyingControllerEntityModel
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

    public partial class SystemUser
    {
        [DataMember]
        public UserRoles UserRole
        {
            get
            {
                return (UserRoles)_iUserRole;
            }
            set
            {
                _iUserRole = (int)value;
            }
        }

    }
}
