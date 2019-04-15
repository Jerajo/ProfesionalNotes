using System;
using System.IO;
using System.Web;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace PN.Services
{
    public class LanguagesService : IDisposable
    {
        public LanguagesService()
        {
            InitializeComponent();
        }

        #region SETTERS AND GETTERS

        public static List<string> Languages { get; private set; }
        public string Language { get; private set; }

        public static Dictionary<string, string> HomeTitles { get; private set; }
        public string HomeTitle { get; private set; }

        public static Dictionary<string, string> ForumTitles { get; private set; } 
        public string ForumTitle { get; private set; }

        public static Dictionary<string, string> TagTitles { get; private set; }
        public string TagTitle { get; private set; }

        public static Dictionary<string, string> PostTitles { get; private set; }
        public string PostTitle { get; private set; }

        public static Dictionary<string, string> UserTitles { get; private set; }
        public string UserTitle { get; private set; }

        public static Dictionary<string, string> AccountTitles { get; private set; }
        public string AccountTitle { get; private set; }

        public static Dictionary<string, string> PoliciesTitles { get; private set; }
        public string PoliciesTitle { get; private set; }

        public bool IsDisposing { get; private set; }

        #endregion

        #region Métodos

        #region Publicos

        public static string GetCurrentLanguage()
        {
            try
            {
                var currentCulture = Thread.CurrentThread.CurrentCulture;
                return currentCulture.TwoLetterISOLanguageName;
            }
            catch (Exception) { throw; }
        }

        public static void RegisterLanguages()
        {
            try
            {
                var jDocument = GetAllRouteValues();

                Languages = GetAllLanguages();
                HomeTitles = jDocument["HomeTitles"].ToObject<Dictionary<string,string>>();
                ForumTitles = jDocument["ForumTitles"].ToObject<Dictionary<string, string>>();
                TagTitles = jDocument["TagTitles"].ToObject<Dictionary<string, string>>();
                PostTitles = jDocument["PostTitles"].ToObject<Dictionary<string, string>>();
                UserTitles = jDocument["UserTitles"].ToObject<Dictionary<string, string>>();
                AccountTitles = jDocument["AccountTitles"].ToObject<Dictionary<string, string>>();
                PoliciesTitles = jDocument["PoliciesTitles"].ToObject<Dictionary<string, string>>();
            }
            catch (Exception) { throw; }
        }

        public void Dispose(bool isDisposing)
        {
            IsDisposing = isDisposing;
            if (IsDisposing) Dispose();
        }

        public void Dispose()
        {
            Language = null;
            HomeTitle = null;
            ForumTitle = null;
            PostTitle = null;
            TagTitle = null;
            UserTitle = null;
            AccountTitle = null;
            PoliciesTitle = null;
        }

        #endregion

        #region AuxiliaryMethods

        private static List<string> GetAllLanguages()
        {
            var fullPath = GetFullPath("App_Data/Languages/");
            var allFiles = Directory.GetFiles(fullPath, "*.resx");
            var languages = new List<string>();

            foreach (var file in allFiles)
            {
                var lang = file.Substring(file.IndexOf('.') + 1, 2);
                if (lang != "re") languages.Add(lang);
                else languages.Add("es");
            }

            return languages;
        }

        private static JObject GetAllRouteValues()
        {
            using (var fileServer = new FileService())
            {
                return fileServer.Read(GetFullPath("App_Data/routes.json")) as JObject;
            }
        }

        private static string GetFullPath(string relativePath = "")
        {
            return Path.Combine(HttpRuntime.AppDomainAppPath, relativePath);
        }

        private void InitializeComponent()
        {
            Language = GetCurrentLanguage();
            HomeTitle = HomeTitles[Language];
            ForumTitle = ForumTitles[Language];
            PostTitle = PostTitles[Language];
            TagTitle = TagTitles[Language];
            UserTitle = UserTitles[Language];
            AccountTitle = AccountTitles[Language];
            PoliciesTitle = PoliciesTitles[Language];
            IsDisposing = false;
        }

        #endregion

        #endregion
    }
}