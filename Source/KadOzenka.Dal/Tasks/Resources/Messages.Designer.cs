//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KadOzenka.Dal.Tasks.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("KadOzenka.Dal.Tasks.Resources.Messages", typeof(Messages).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Корректирующий фактор &apos;{0}&apos; уже добавлен в качестве Базового или Корректируемого.
        /// </summary>
        public static string InheritanceCorrectingFactorAlreadyExists {
            get {
                return ResourceManager.GetString("InheritanceCorrectingFactorAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не выбран Корректируемый фактор.
        /// </summary>
        public static string InheritanceCorrectingFactorIsEmpty {
            get {
                return ResourceManager.GetString("InheritanceCorrectingFactorIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Фактор &apos;{0}&apos; уже добавлен в качестве Базового или Корректируемого.
        /// </summary>
        public static string InheritanceFactorAlreadyExists {
            get {
                return ResourceManager.GetString("InheritanceFactorAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не выбран Фактор.
        /// </summary>
        public static string InheritanceFactorIsEmpty {
            get {
                return ResourceManager.GetString("InheritanceFactorIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Базовый и Корректируемый факторы должны быть разными.
        /// </summary>
        public static string InheritanceFactorsAreTheSame {
            get {
                return ResourceManager.GetString("InheritanceFactorsAreTheSame", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Типы факторов не совпадают. Один - ЗУ, второй ОКС.
        /// </summary>
        public static string InheritanceFactorsTypeMismatch {
            get {
                return ResourceManager.GetString("InheritanceFactorsTypeMismatch", resourceCulture);
            }
        }
    }
}
