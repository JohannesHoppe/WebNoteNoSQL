namespace WebNoteNoSQL.Models.EntityFramework
{
    using System;

    public static class EntityFrameworkRepositoryMappingExcentions
    {
        public static int AsInt32(this string number)
        {
            return Convert.ToInt32(number);
        }
    }
}