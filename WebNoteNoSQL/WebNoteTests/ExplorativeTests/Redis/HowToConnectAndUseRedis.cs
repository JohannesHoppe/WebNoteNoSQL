using System.Globalization;
using System.Linq;
using NUnit.Framework;
using ServiceStack.Redis;
using WebNoteNoSQL.Code.DbStartScript;
using WebNoteNoSQL.Models;
using WebNoteTests.ExplorativeTests.Infrastructure;

namespace WebNoteTests.ExplorativeTests.Redis
{
    using WebNoteNoSQL.Models.Redis;

    /// <summary>
    /// This fires up the actual redis-server.exe which is only done for presentation purposes.
    /// Please note: Redis was primarily developed on and for *NIX operating systems!
    /// </summary>
    [TestFixture]
    public class HowToConnectAndUseRedis : ExploratoryTest
    {
        private const string RedisHost = "localhost";

        public HowToConnectAndUseRedis()
            : base(DatabaseStartScript.ForRedis())
        {
        }

        [Test]
        public void DoSomeActions()
        {
            IRedisClient redisClient = new RedisClient(RedisHost);

            // In Redis, databases are identified by an integer index, not by a database name.
            // By default, a client is connected to database 0.
            redisClient.Db = 1;
            
            using (var notes = redisClient.GetTypedClient<Note>())
            {
                var newNote = new Note 
                {
                        Id = notes.GetNextSequence().AsString(),
                        Title = "Test",
                        Message = "Hello World"
                };

                notes.Store(newNote);
        
                // Assert
                var allNotes = notes.GetAll();
                Assert.That(allNotes.Count(), Is.GreaterThanOrEqualTo(1));
            }
        }

        [TestFixtureTearDown]
        public void CleanUp()
        {
            IRedisClient redisClient = new RedisClient(RedisHost);
            redisClient.Db = 1;

            using (var notes = redisClient.GetTypedClient<Note>())
            {
                notes.DeleteAll();
            }
        }
    }
}