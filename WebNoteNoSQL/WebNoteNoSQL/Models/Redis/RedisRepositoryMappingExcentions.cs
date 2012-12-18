namespace WebNoteNoSQL.Models.Redis
{
    using System.Globalization;

    public static class RedisRepositoryMappingExcentions
    {
        public static string AsString(this long number)
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }
    }
}