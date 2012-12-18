using MongoDB.Bson.Serialization.Attributes;

namespace WebNoteNoSQL.Models
{
    [BsonIgnoreExtraElements]
    public class Category
    {
        public Category()
        {
            Name = string.Empty;
            Color = string.Empty;
        }

        // Documents within documents usally do not need an identifier, later on we will use the Color property to identify it
        [BsonIgnore]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }
    }
}