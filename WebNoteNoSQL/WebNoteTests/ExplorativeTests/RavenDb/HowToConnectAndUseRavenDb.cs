using System.Linq;
using NUnit.Framework;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;
using WebNoteNoSQL.Code.DbStartScript;
using WebNoteNoSQL.Models;
using WebNoteTests.ExplorativeTests.Infrastructure;

namespace WebNoteTests.ExplorativeTests.RavenDb
{
    /// <summary>
    /// This fires up the actual Raven.Server.exe which is only done for presentation purposes.
    /// For Your own unittests you should investigate using the in process hosting (embedded raven server)
    /// Which does not depend on processes running on the OS
    /// </summary>
    [TestFixture]
    public class HowToConnectAndUseRavenDb : ExploratoryTest
    {
        private const string DocumentStoreUrl = "http://localhost:8080";
        private const string Database = "WebNoteTest";
        private IDocumentStore store;

        public HowToConnectAndUseRavenDb()
            : base(DatabaseStartScript.ForRavenDb())
        {
        }

        [TestFixtureSetUp]
        public void Connect()
        {
            // A connection is done like this!
            store = new DocumentStore { Url = DocumentStoreUrl }.Initialize();

            // This method creates our database!
            store.DatabaseCommands.EnsureDatabaseExists(Database);
        }

        [Test]
        public void DoSomeActions()
        {
            // The session will now use only the specified database
            using (var session = store.OpenSession(Database))
            {
                session.Store(new Note { Title = "A Note", Message = "The Raven says Hello!" });
                session.Store(new Note { Title = "Another Note", Message = "Kraah! Kraaahh!!" });
                session.Store(
                    new Note
                        {
                            Title = "An interesting philosophy!",
                            Message = "All defaults in raven are configured so that you never shoot yourself in the foot!"
                        });

                // Raven uses the Unit-Of-Work Pattern (A PoEAA Pattern by Martin Fowler)!
                // See: http://martinfowler.com/eaaCatalog/unitOfWork.html
                // See: http://ravendb.net/documentation/client-api/unit-of-work
                // You can do whatever you want and then commit the changes to the db!
                session.SaveChanges();

                // Collections are documents of similar shape. This effectively queries the "Notes" collection!
                var notes = session.Query<Note>();
                Assert.That(notes.Count(), Is.EqualTo(3));
            }
        }

        [TestFixtureTearDown]
        public void CleanUp()
        {
            // Indexes can be defined using Linq. There are also default indexes,
            // such as Raven/DocumentsByEntityName or Temp/{EntityName} that can be used for convenience!

            // Droping everything in the Notes collection
            store.DatabaseCommands.ForDatabase(Database).DeleteByIndex("Raven/DocumentsByEntityName", new IndexQuery());
        }
    }
}