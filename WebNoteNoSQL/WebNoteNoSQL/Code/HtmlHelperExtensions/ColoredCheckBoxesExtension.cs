using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using WebNoteNoSQL.Models;

namespace WebNoteNoSQL.Code.HtmlHelperExtensions
{
    public static class ColoredCheckBoxesExtension
    {
        private const string Format = @"
            <li>
                <input type=""checkbox"" name=""{4}"" value=""{0}"" id=""label_{0}""{1} />
                <label for=""label_{0}"" style=""color:{2}"">{3}</label>
            </li>";

        /// <summary>
        /// Will show all available categories
        /// </summary>
        public static string ColoredCheckBoxes(this HtmlHelper htmlHelper, object allAvailableCategories, string checkBoxName)
        {
            return ColoredCheckBoxes(htmlHelper, null, allAvailableCategories, checkBoxName);
        }

        /// <summary>
        /// Will show all available categories,
        /// if category is existing in the model the checkbox will be checked
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlHelper", Justification = "Extension Method")]
        public static string ColoredCheckBoxes(this HtmlHelper htmlHelper, NoteWithCategories model, object allAvailableCategories, string checkBoxName)
        {
            IEnumerable<Category> allCategories = allAvailableCategories as IEnumerable<Category>;
            if (allCategories != null)
            {
                List<string> modelCategoryColors = (model != null && model.Categories.Any()) ?
                    model.Categories.Select(color => color.Color).ToList() :
                    new List<string>();
                
                StringBuilder tmp = new StringBuilder();
                foreach (Category category in allCategories)
                {
                    tmp.AppendFormat(
                        CultureInfo.InvariantCulture,
                        Format,
                        !String.IsNullOrEmpty(category.Id) ? category.Id : category.Color,
                        (modelCategoryColors.Contains(category.Color) ? " checked=\"checked\"" : String.Empty),
                        category.Color,
                        category.Name, 
                        checkBoxName); 
                }

                return "<ul>" + tmp + "</ul>";
            } 

            return "Wrong Data";
        }
    }
}