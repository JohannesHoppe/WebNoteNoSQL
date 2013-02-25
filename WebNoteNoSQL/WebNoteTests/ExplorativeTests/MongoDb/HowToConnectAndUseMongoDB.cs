using System.Linq;
using MongoDB.Driver;
using NUnit.Framework;
using WebNoteNoSQL.Code.DbStartScript;
using WebNoteTests.ExplorativeTests.Infrastructure;

namespace WebNoteTests.ExplorativeTests.MongoDb
{
    /// <summary>
    /// This shows how to connect to the server and create a database
    /// </summary>
    [TestFixture]
    public class HowToConnectAndUseMongoDb : ExploratoryTest
    {
        private const string ConnectionString = "mongodb://localhost";
        private const string DatabaseName = "WebNoteTest";
        private const string CollectionName = "Notes";

        // Fire up the server!
        public HowToConnectAndUseMongoDb()
            : base(DatabaseStartScript.ForMongoDb())
        {
        }

        [Test]
        public void DoSomeActions()
        {
            // A connection is done like this!
            MongoClient client = new MongoClient(ConnectionString);
            var server = client.GetServer();

            // A new database does not exist. 
            Assert.That(server.DatabaseExists(DatabaseName), Is.False);
            Assert.That(server.GetDatabaseNames().Contains(DatabaseName), Is.False);

            // To create a database, just tell mongo to its name from now on!
            var database = server.GetDatabase(DatabaseName);

            // When you create a collection, the database is created
            database.CreateCollection(CollectionName);

            // Now the Database exists
            Assert.That(server.DatabaseExists(DatabaseName), Is.True);
            Assert.That(server.GetDatabaseNames().Contains(DatabaseName), Is.True);
            Assert.That(database.Name, Is.EqualTo(DatabaseName));
        }

        [Test]
        public void CleanUpAgain()
        {
            var client = new MongoClient(ConnectionString);
            var server = client.GetServer();

            // Remove the database
            server.DropDatabase(DatabaseName);
        }
    }
}
