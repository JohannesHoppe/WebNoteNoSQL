using Mongo2Go;
using MongoDB.Driver;

namespace WebNoteTests
{
    public class MongoIntegrationTest
    {
        internal static MongoDbRunner _runner;
        internal static MongoCollection<TestDocument> _collection;

        internal static void CreateConnection()
        {
            _runner = MongoDbRunner.Start();

            MongoServer server = new MongoClient(_runner.ConnectionString).GetServer();
            MongoDatabase database = server.GetDatabase("IntegrationTest");
            _collection = database.GetCollection<TestDocument>("TestCollection");
        }
    }
}
