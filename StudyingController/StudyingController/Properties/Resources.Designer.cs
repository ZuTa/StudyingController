﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudyingController.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("StudyingController.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to StudyingController.
        /// </summary>
        internal static string AppDataFolderName {
            get {
                return ResourceManager.GetString("AppDataFolderName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Увага.
        /// </summary>
        internal static string DefaultMessageText {
            get {
                return ResourceManager.GetString("DefaultMessageText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Використовуються недопустимі символи.
        /// </summary>
        internal static string ErrorBadCharsUsed {
            get {
                return ResourceManager.GetString("ErrorBadCharsUsed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поле не може бути порожнім.
        /// </summary>
        internal static string ErrorFieldEmpty {
            get {
                return ResourceManager.GetString("ErrorFieldEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Логін повинен складатись лише з символів a-z(A-Z)0-9_.
        /// </summary>
        internal static string ErrorLoginBadChars {
            get {
                return ResourceManager.GetString("ErrorLoginBadChars", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поле логін не може бути порожнім.
        /// </summary>
        internal static string ErrorLoginEmpty {
            get {
                return ResourceManager.GetString("ErrorLoginEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Логін повинен мати не менше 3 символів.
        /// </summary>
        internal static string ErrorLoginLength {
            get {
                return ResourceManager.GetString("ErrorLoginLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Порт за межами (0-65535).
        /// </summary>
        internal static string ErrorPortBorder {
            get {
                return ResourceManager.GetString("ErrorPortBorder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поле порт не може бути порожнім.
        /// </summary>
        internal static string ErrorPortEmpty {
            get {
                return ResourceManager.GetString("ErrorPortEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Порт повинен бути цілим числом.
        /// </summary>
        internal static string ErrorPortNotInt {
            get {
                return ResourceManager.GetString("ErrorPortNotInt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Використано неприпустимі символи для сервера.
        /// </summary>
        internal static string ErrorServerBadChars {
            get {
                return ResourceManager.GetString("ErrorServerBadChars", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Поле сервер не може бути порожнім.
        /// </summary>
        internal static string ErrorServerEmpty {
            get {
                return ResourceManager.GetString("ErrorServerEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Потрібно вибрати структуру.
        /// </summary>
        internal static string ErrorStructureNotFound {
            get {
                return ResourceManager.GetString("ErrorStructureNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Помилка.
        /// </summary>
        internal static string ErrorTxt {
            get {
                return ResourceManager.GetString("ErrorTxt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Адміністратори факультетів.
        /// </summary>
        internal static string FacultyAdminsTxt {
            get {
                return ResourceManager.GetString("FacultyAdminsTxt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Секретарі факультетів.
        /// </summary>
        internal static string FacultySecretariesTxt {
            get {
                return ResourceManager.GetString("FacultySecretariesTxt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Адміністратори інститутів.
        /// </summary>
        internal static string InstituteAdminsTxt {
            get {
                return ResourceManager.GetString("InstituteAdminsTxt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Секретарі інститутів.
        /// </summary>
        internal static string InstituteSecretariesTxt {
            get {
                return ResourceManager.GetString("InstituteSecretariesTxt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Новий інститут.
        /// </summary>
        internal static string InstituteViewHeaderText {
            get {
                return ResourceManager.GetString("InstituteViewHeaderText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to loginConfig.xml.
        /// </summary>
        internal static string LoginConfigFileName {
            get {
                return ResourceManager.GetString("LoginConfigFileName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Заповніть коректно всі поля!.
        /// </summary>
        internal static string LoginDataError {
            get {
                return ResourceManager.GetString("LoginDataError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Головні адміністратори.
        /// </summary>
        internal static string MainAdminsTxt {
            get {
                return ResourceManager.GetString("MainAdminsTxt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Головні секретарі.
        /// </summary>
        internal static string MainSecretariesTxt {
            get {
                return ResourceManager.GetString("MainSecretariesTxt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ви дійсно хочете видалити?.
        /// </summary>
        internal static string RemoveEntityTxt {
            get {
                return ResourceManager.GetString("RemoveEntityTxt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ControllerService.svc.
        /// </summary>
        internal static string Service {
            get {
                return ResourceManager.GetString("Service", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Студенти.
        /// </summary>
        internal static string StudentsTxt {
            get {
                return ResourceManager.GetString("StudentsTxt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Викладачі.
        /// </summary>
        internal static string TeachersTxt {
            get {
                return ResourceManager.GetString("TeachersTxt", resourceCulture);
            }
        }
    }
}
