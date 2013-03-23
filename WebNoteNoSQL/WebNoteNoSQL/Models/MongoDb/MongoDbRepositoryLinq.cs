using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Builders = MongoDB.Driver.Builders;

namespace WebNoteNoSQL.Models.MongoDb
{
    /// <summary>
    /// Sample Repository - shows the C# driver serializer with the new Linq syntax
    /// </summary>
    public class MongoDbRepositoryLinq : IWebNoteRepository
    {
        private const string ConnectionStringName = "MongoDB";
        private const string DatabaseName = "WebNote";
        private const string CollectionName = "Notes";
        private const string CollectionNameCategories = "Categories";

        private readonly MongoDatabase database;
        private readonly MongoServer server;
        private readonly MongoCollection<NoteWithCategories> notes;
        private readonly MongoCollection<Category> categories;

        public MongoDbRepositoryLinq()
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            MongoClient client = new MongoClient(connectionString);
            server = client.GetServer();
            database = server.GetDatabase(DatabaseName);
            notes = database.GetCollection<NoteWithCategories>(CollectionName);
            categories = database.GetCollection<Category>(CollectionNameCategories);
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

        public IEnumerable<Category> GetAllCategories(string[] categoryColors)
        {
            throw new NotImplementedException();
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