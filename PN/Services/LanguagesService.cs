using System;
using PN.Models;
using System.IO;
using System.Web;
using IP2Country;
using System.Threading;
using IP2Country.IpToAsn;
using Newtonsoft.Json.Linq;
using System.Globalization;
using IP2Country.Datasources;
using System.Collections.Generic;

namespace PN.Services
{
    public class LanguagesService : IDisposable
    {
        public LanguagesService()
        {
            InitializeComponent();
        }

        #region SETTERS AND GETTERS

        public string Region { get; private set; }

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

        public string GetCurrentRegion()
        {
            try
            {
                var currentCulture = Thread.CurrentThread.CurrentCulture;
                var currentRegion = new RegionInfo(currentCulture.Name);
                return currentRegion.TwoLetterISORegionName;
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

        public string GetUserHostRegion(string userHostAddress)
        {
            var resolver = new IP2CountryResolver(new IIP2CountryDataSource[] {
                new IpToAsnCSVFileSource(GetFullPath("App_Data/ip2country-v4.tsv.gz")),
                new IpToAsnCSVFileSource(GetFullPath("App_Data/ip2country-v6.tsv.gz")),
            });

            var resoult = resolver.Resolve(userHostAddress);

            return resoult.Country;
        }

        public void Dispose(bool isDisposing)
        {
            IsDisposing = isDisposing;
            if (IsDisposing) Dispose();
        }

        public void Dispose()
        {
            Language = null;
            Region = null;
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
            Region = GetCurrentRegion();
            HomeTitle = HomeTitles[Language];
            ForumTitle = ForumTitles[Language];
            PostTitle = PostTitles[Language];
            TagTitle = TagTitles[Language];
            UserTitle = UserTitles[Language];
            AccountTitle = AccountTitles[Language];
            PoliciesTitle = PoliciesTitles[Language];
            IsDisposing = false;
        }

        private void InsertCultureInfoInDB()
        {
            using (var db = new AppDbContext())
            {
                var fileServer = new FileService();
                var jObjet = fileServer.Read(GetFullPath("App_Data/cultures.json")) as JObject;
                var languages = jObjet["languages"].ToObject<List<string>>();
                var countries = jObjet["countries"].ToObject<List<string>>();
                var laguageList = new List<Language>();
                var countryList = new List<Country>();

                foreach (var value in languages)
                {

                    var culture = new CultureInfo(value);

                    var language = new Language
                    {
                        ISOLanguage = value,
                        NativeLanguage = culture.NativeName,
                    };
                    laguageList.Add(language);
                }

                db.Language.AddRange(laguageList);
                db.SaveChanges();

                foreach (var value in countries)
                {
                    var regionInfo = new RegionInfo(value);

                    var country = new Country
                    {
                        ISORegion = value,
                        NativeRegion = regionInfo.NativeName
                    };

                    countryList.Add(country);
                }

                db.Country.AddRange(countryList);
                db.SaveChanges();
            }
        }

        #endregion

        #endregion
    }
}