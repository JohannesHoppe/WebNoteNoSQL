using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebNoteNoSQL.Code
{
    using System.Configuration;
    using System.Web.Configuration;

    public static class WebConfigManipulation
    {
        public static void ChangeEngine()
        {
            var currentKey = WebConfigurationManager.AppSettings["DbEngine"];

            string[] keys = { "EntityFramework", "Redis", "MongoDB", "RavenDB" };
            string nextKey = keys.FindNextOrFirst(currentKey);

            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");

            config.AppSettings.Settings["DbEngine"].Value = nextKey;
            config.Save(ConfigurationSaveMode.Modified);            
        }

        /// <summary>
        /// Returns the NEXT item in list or the first one when the last item matched
        /// "cycles" through the list
        /// </summary>
        public static string FindNextOrFirst(this IEnumerable<string> keys, string currentKey)
        {
            string firstItem = null;
            bool match = false;
            
            foreach (string key in keys)
            {
                if (firstItem == null)
                {
                    firstItem = key;
                }

                if (match)
                {
                    return key;
                }

                if (key == currentKey)
                {
                    match = true;
                }
            }

            return firstItem;
        }
    }
}