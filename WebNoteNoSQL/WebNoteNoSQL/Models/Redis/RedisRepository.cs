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

            using (var notes = redisClient.GetTypedClient<NoteWithCategories>())
            {
                note.Id = notes.GetNextSequence().AsString();
                notes.Store(note);
            }
        }

        public NoteWithCategories Read(string id)
        {
            using (var notes = redisClient.GetTypedClient<NoteWithCategories>())
            {
                return notes.GetById(id);
            }
        }

        public IEnumerable<NoteWithCategories> ReadAll()
        {
            using (var notes = redisClient.GetTypedClient<NoteWithCategories>())
            {
                return notes.GetAll();
            }
        }

        public void Update(Note noteToEdit, IEnumerable<Category> newCategories)
        {
            using (var notes = redisClient.GetTypedClient<NoteWithCategories>())
            {
                NoteWithCategories note = Read(noteToEdit.Id);

                note.Title = noteToEdit.Title;
                note.Message = noteToEdit.Message;
                note.Categories = newCategories;

                notes.Store(note);
            }
        }

        public void Delete(string id)
        {
            using (var notes = redisClient.GetTypedClient<NoteWithCategories>())
            {
                notes.DeleteById(id);
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            using (var categories = redisClient.GetTypedClient<Category>())
            {
                return categories.GetAll();
            }
        }

        public IEnumerable<Category> GetAllCategories(string[] categoryIds)
        {
            using (var categories = redisClient.GetTypedClient<Category>())
            {
                return categories.GetByIds(categoryIds);
            }
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