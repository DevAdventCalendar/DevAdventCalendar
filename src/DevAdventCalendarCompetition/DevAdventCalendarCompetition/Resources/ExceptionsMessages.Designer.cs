﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DevAdventCalendarCompetition.Resources {
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
    public class ExceptionsMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ExceptionsMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DevAdventCalendarCompetition.Resources.ExceptionsMessages", typeof(ExceptionsMessages).Assembly);
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
        ///   Looks up a localized string similar to Wystąpił nieoczekiwany błąd podczas konfigurowania wiadomości e-mail dla użytkownika z identyfikatorem &apos;{0}&apos;..
        /// </summary>
        public static string ErrorDuringEmailConfiguration {
            get {
                return ResourceManager.GetString("ErrorDuringEmailConfiguration", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Problem z potwierdzeniem emaila.
        /// </summary>
        public static string ErrorDuringEmailConfirmation {
            get {
                return ResourceManager.GetString("ErrorDuringEmailConfirmation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wystąpił nieoczekiwany błąd podczas konfigurowania powiadomień email dla użytkownika z identyfikatorem {0}..
        /// </summary>
        public static string ErrorDuringEmailNotificationsPreferenceChange {
            get {
                return ResourceManager.GetString("ErrorDuringEmailNotificationsPreferenceChange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wystąpił nieoczekiwany błąd podczas konfigurowania powiadomień push dla użytkownika z identyfikatorem {0}..
        /// </summary>
        public static string ErrorDuringPushNotificationsPreferenceChange {
            get {
                return ResourceManager.GetString("ErrorDuringPushNotificationsPreferenceChange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Poprzedni test nie został zakończony.
        /// </summary>
        public static string PreviousTestIsNotDone {
            get {
                return ResourceManager.GetString("PreviousTestIsNotDone", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Test został uruchomiony.
        /// </summary>
        public static string TestAlreadyRun {
            get {
                return ResourceManager.GetString("TestAlreadyRun", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nie można załadować użytkownika z identyfikatorem {0}..
        /// </summary>
        public static string UserWithIdNotFound {
            get {
                return ResourceManager.GetString("UserWithIdNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Niepoprawny format daty zmiennej Configuration:EndDate lub Configuration:StartDate w appsettings.
        /// </summary>
        public static string WrongFormatOfDate {
            get {
                return ResourceManager.GetString("WrongFormatOfDate", resourceCulture);
            }
        }
    }
}
