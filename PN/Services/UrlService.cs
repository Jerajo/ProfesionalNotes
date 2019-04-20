using PN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

/* Autor:  Afzaal Ahmad Zeeshan
 * Conde From: https://www.c-sharpcorner.com/UploadFile/201fc1/what-is-random-urls-and-how-to-creating-them-in-Asp-Net/
 * Published: Feb 21 2015
 */

namespace PN.Services
{
    public class URLService
    {
        #region SETTERS AND GETTERS

        private static readonly List<int> Numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };

        private static readonly List<char> Characters = new List<char>()
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',  'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '-', '_'
        };

        private static readonly Random Randomizer = new Random();

        #endregion

        public static string GetRootURL() => HttpRuntime.AppDomainAppVirtualPath;

        public static string GetRelativeURL() => HttpContext.Current.Request.Url.PathAndQuery;

        public static string GetAbsoluteURL() => HttpContext.Current.Request.Url.AbsolutePath;

        public static string GetRandomURL()
        {
            var tempURL = "";
            var language = (new LanguagesService()).Language;

            using (var db = new AppDbContext())
            {
                do
                {
                    // run the loop till I get a string of 10 Characters  
                    for (int i = 0; i < 11; i++)
                    {
                        // Get random Numbers, to get either a character or a number...  
                        int random = Randomizer.Next(0, 3);
                        if (random == 1)
                        {
                            // use a number  
                            random = Randomizer.Next(0, Numbers.Count);
                            tempURL += Numbers[random].ToString();
                        }
                        else
                        {
                            random = Randomizer.Next(0, Characters.Count);
                            tempURL += Characters[random].ToString();
                        }
                    }
                } while (db.RandomLink.Any(m => m.ShortedLink.Contains(tempURL)));
            }

            return GetRootURL() + $"{language}/{tempURL}";
        }
    }
}