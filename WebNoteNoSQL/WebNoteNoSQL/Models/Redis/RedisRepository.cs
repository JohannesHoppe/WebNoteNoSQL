using System;
using System.Collections.Generic;
using System.Configuration;
using ServiceStack.Redis;

namespace WebNoteNoSQL.Models.Redis
{
    public class RedisRepository : IWebNoteRepository
    {
        private const string ConnectionStringName = "Redis";
        private readonly IRedisClient redisClient;

        public RedisRepository()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            redisClient = new RedisClient(connectionString);
        }

        public void Create(Note noteToAdd, IEnumerable<Category> newCategories)
        {
            NoteWithCategories note = NoteWithCategories.Convert(noteToAdd, newCategories);

            throw new NotImplementedException();
        }

        public NoteWithCategories Read(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NoteWithCategories> ReadAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Note noteToEdit, IEnumerable<Category> newCategories)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAllCategories(string[] categoryIds)
        {
            throw new NotImplementedException();
        }

        #region Install Demo Content

        public void TryInstallDemoContent()
        {
            using (var notes = redisClient.GetTypedClient<NoteWithCategories>())
            {
                if (redisClient.ContainsKey("urn:notewithcategories:1")) { return; }

                var newNotes = DemoContent.GetNodesWithCategories("Redis", () => notes.GetNextSequence().AsString());
                notes.StoreAll(newNotes);
            }

            using (var categories = redisClient.GetTypedClient<Category>())
            {
                var newCategories = DemoContent.GetCategories(() => categories.GetNextSequence().AsString());
                categories.StoreAll(newCategories);
            }
        }

        #endregion
    }
}