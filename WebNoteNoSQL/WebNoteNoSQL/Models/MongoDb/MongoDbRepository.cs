using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

using Builders = MongoDB.Driver.Builders;

namespace WebNoteNoSQL.Models.MongoDb
{
    /// <summary>
    /// Sample Repository - here we use Generics and the default C# driver serializer
    /// </summary>
    public class MongoDbRepository : IWebNoteRepository
    {
        private const string ConnectionStringName = "MongoDB";
        private const string DatabaseName = "WebNote";
        private const string CollectionName = "Notes";
        private const string CollectionNameCategories = "Categories";

        private readonly MongoDatabase database;
        private readonly MongoServer server;
        private readonly MongoCollection<BsonDocument> notes;
        private readonly MongoCollection<BsonDocument> categories;

        public MongoDbRepository()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            server = MongoServer.Create(connectionString);
            database = server.GetDatabase(DatabaseName);
            notes = database.GetCollection(CollectionName);
            categories = database.GetCollection(CollectionNameCategories);
        }

        public void Create(Note noteToAdd, IEnumerable<Category> newCategories)
        {
            NoteWithCategories note = NoteWithCategories.Convert(noteToAdd, newCategories);
            notes.Insert(note);
        }

        public NoteWithCategories Read(string id)
        {
            var query = Query.EQ("_id", ObjectId.Parse(id));
            return notes.FindOneAs<NoteWithCategories>(query);
        }

        public IEnumerable<NoteWithCategories> ReadAll()
        {
            var sortBy = SortBy.Descending("_id");
            return notes.FindAllAs<NoteWithCategories>().SetSortOrder(sortBy).ToList();
        }

        public void Update(Note noteToEdit, IEnumerable<Category> newCategories)
        {
            NoteWithCategories note = Read(noteToEdit.Id);

            note.Title = noteToEdit.Title;
            note.Message = noteToEdit.Message;
            note.Categories = newCategories;

            var query = Query.EQ("_id", ObjectId.Parse(noteToEdit.Id));
            notes.Update(query, Builders.Update.Replace(note));
        }

        public void Delete(string id)
        {
            notes.Remove(Query.EQ("_id", ObjectId.Parse(id)));
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return categories.FindAllAs<Category>().ToList();
        }

        public IEnumerable<Category> GetAllCategories(string[] categoryColors)
        { 
            var colorArray = new BsonArray(categoryColors);
            var query = Query.In("Color", colorArray);

            return categories.FindAs<Category>(query).ToList();
        }

        public void Dispose()
        {
            if (server != null && server.State == MongoServerState.Connected)
            {
                server.Disconnect();
            }
        }

        #region Install Demo Content

        public void TryInstallDemoContent()
        {
            if (notes.Count() > 0)
            {
                return;
            }

            var newNotes = DemoContent.GetNodesWithCategories("MongoDB", () => null);
            notes.InsertBatch(newNotes);

            var newCategories = DemoContent.GetCategories(() => null);
            categories.InsertBatch(newCategories);
        }

        #endregion
    }
}