using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using NUnit.Framework;
using WebNoteNoSQL.Code.DbStartScript;
using WebNoteNoSQL.Models;
using WebNoteTests.ExplorativeTests.Infrastructure;

namespace WebNoteTests.ExplorativeTests.MongoDb
{
    [TestFixture]
    public class HowToReadAndWriteWithMongoDb : ExploratoryTest
    {
        private const string ConnectionString = "mongodb://localhost";
        private const string DatabaseName = "WebNoteTest";
        private const string CollectionName = "Notes";

        private MongoDatabase database;
        private MongoServer server;

        // Fire up the server!
        public HowToReadAndWriteWithMongoDb()
            : base(DatabaseStartScript.ForMongoDb())
        {
        }

        [Test]
        public void CreateACollectionWithSomeData()
        {
            CreateCollectionIfNecessary(database);

            MongoCollection<Note> notes = database.GetCollection<Note>(CollectionName);

            // Let's create a couple of documents!
            var note = new Note
                           {
                               Title = "Mongo is fun for developers!",
                               Message = "With Mongo Documents, there is hardly any need for excessive OR Mapping, since documents resemble objects closer that relations do!"
                           };

            var note2 = new Note
                            {
                                Title = "Hello",
                                Message = "Welcome to VSone!"
                            };

            var noteWithCategories = new NoteWithCategories
                                         {
                                             Title = "RavenDb",
                                             Message = "RavenDb is also pretty cool!",
                                             Categories = new[] { new Category { Name = "Very Important" } }
                                         };

            // Insert
            notes.Insert(note);
            notes.Insert(note2);
            notes.Insert(noteWithCategories);

            // Let's find them
            List<Note> allNotes = notes.FindAllAs<Note>().ToList();
            Assert.That(allNotes.Count, Is.EqualTo(3));

            // You can also marshal them as the subclass, but for this example only one will have the cargories set!
            List<NoteWithCategories> allNotesWithcategories = notes.FindAllAs<NoteWithCategories>().ToList();
            Assert.That(allNotesWithcategories.Count, Is.EqualTo(3));
            Assert.That(allNotesWithcategories.Count(n => n.Categories != null && n.Categories.Any()), Is.EqualTo(1));
        }

        [TestFixtureSetUp]
        public void StartAndInitialize()
        {
            var client = new MongoClient(ConnectionString);
            server = client.GetServer();
            database = server.GetDatabase(DatabaseName);
        }

        [TestFixtureTearDown]
        public void DropAndDisconnect()
        {
            database.GetCollection(CollectionName).Drop();

            if (server != null && server.State == MongoServerState.Connected)
            {
                server.Disconnect();
            }
        }


        private static void CreateCollectionIfNecessary(MongoDatabase database)
        {
            if (!database.CollectionExists(CollectionName))
            {
                database.CreateCollection(CollectionName);
            }
        }
    }
}