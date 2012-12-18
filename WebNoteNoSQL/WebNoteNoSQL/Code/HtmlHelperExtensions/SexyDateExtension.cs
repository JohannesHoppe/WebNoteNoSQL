using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.Mvc;

namespace WebNoteNoSQL.Code.HtmlHelperExtensions
{
    /// <summary>
    /// Some HTML + CSS3 == Sexy
    /// </summary>
    public static class SexyDateExtension
    {
        private const string Format = @"
<div class=""sexy_date"">
    <h1 class=""day"">{0:dd}</h1> 
    <h1 class=""month"">{0:MMM}</h1> 
    <h1 class=""year"">{0:yyyy}</h1>
</div>";

        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlHelper", Justification = "Extension Method")]
        public static string SexyDate(this HtmlHelper htmlHelper, DateTime date)
        {
            return String.Format(CultureInfo.InvariantCulture, Format, date);
        }
    }
}