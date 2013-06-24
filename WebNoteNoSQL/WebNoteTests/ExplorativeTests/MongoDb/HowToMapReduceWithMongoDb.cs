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
    public class HowToMapReduceWithMongoDb : ExploratoryTest
    {
        private const string ConnectionString = "mongodb://localhost";
        private const string DatabaseName = "logs";
        private const string CollectionName = "logs";

        private MongoDatabase database;
        private MongoServer server;

        // Fire up the server!
        public HowToMapReduceWithMongoDb()
            : base(DatabaseStartScript.ForMongoDb())
        {
        }

        [TestFixtureSetUp]
        public void StartAndInitialize()
        {
            string importScript = DatabaseStartScript.GetCurrentExecutingDirectory() + @"..\..\ExplorativeTests\MongoDb\import_logs.cmd";

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
    }
}