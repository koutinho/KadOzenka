//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KadOzenka.Dal.LongProcess._Common {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("KadOzenka.Dal.LongProcess._Common.Messages", typeof(Messages).Assembly);
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
        ///   Looks up a localized string similar to Не удалось расчитать цену.
        /// </summary>
        public static string CadastralPriceCalculationError {
            get {
                return ResourceManager.GetString("CadastralPriceCalculationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Не найдена активная модель.
        /// </summary>
        public static string NoActiveModelInCadasralPriceCalculation {
            get {
                return ResourceManager.GetString("NoActiveModelInCadasralPriceCalculation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to У ЕО не заполнены данные по атрибутам.
        /// </summary>
        public static string NotAllUnitFactorsAreFullToCalculateCadastralPrice {
            get {
                return ResourceManager.GetString("NotAllUnitFactorsAreFullToCalculateCadastralPrice", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to У ЕО нет факторов.
        /// </summary>
        public static string UnitDoesNotHaveFactorsToCalculateCandastralPrice {
            get {
                return ResourceManager.GetString("UnitDoesNotHaveFactorsToCalculateCandastralPrice", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Установлена нулевая цена.
        /// </summary>
        public static string ZeroCadastralPrice {
            get {
                return ResourceManager.GetString("ZeroCadastralPrice", resourceCulture);
            }
        }
    }
}
