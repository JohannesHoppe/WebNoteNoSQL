using System.Web.Mvc;

namespace WebNoteNoSQL.Code.HtmlHelperExtensions
{
    public static class EncodeLineBreaksExtension
    {
        public static string EncodeLineBreaks(this HtmlHelper html, string text)
        {
            return html.Encode(text).Replace("\r\n", "<br />");
        }

        public static string EncodeLineBreaks(this HtmlHelper html, object text)
        {
            return html.Encode(text).Replace("\r\n", "<br />");
        }
    }
}