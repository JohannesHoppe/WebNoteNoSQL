using Raven.Imports.Newtonsoft.Json;

namespace WebNoteTests.ExplorativeTests.RavenDb
{
    public class LogEntry
    {
        [JsonProperty("IP Address")]
        public string IpAddress { get; set; }
        public string Logname { get; set; }
        public string Date { get; set; }
        public string Request { get; set; }
        public int Code { get; set; }
        public int Size { get; set; }
        public string Country { get; set; }
        public string Referer { get; set; }
        public string UserAgent  { get; set; }
    }
}
