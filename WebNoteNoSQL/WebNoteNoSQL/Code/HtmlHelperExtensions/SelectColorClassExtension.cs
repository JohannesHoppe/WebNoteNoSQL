using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using WebNoteNoSQL.Models;

namespace WebNoteNoSQL.Code.HtmlHelperExtensions
{
    public static class SelectColorClassExtension
    {
        private static readonly string[] ColorPreference = new[] { "red", "green", "gray" };

        /// <summary>
        /// Returns a color class name or an empty string
        /// Prefers red, green, gray in the given order!
        /// </summary>
        public static string SelectColorClass(this HtmlHelper html, IEnumerable<Category> categories)
        {
            if (categories != null && categories.Any())
            {
                List<string> allAvaliableColors = categories.Select(category => category.Color).ToList();

                foreach (string preferredColor in ColorPreference.Where(allAvaliableColors.Contains))
                {
                    return preferredColor + "Color";
                }
            }

            return String.Empty;
        }
    }
}