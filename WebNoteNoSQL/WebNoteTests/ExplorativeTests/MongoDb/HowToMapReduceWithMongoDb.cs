using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
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

        [Test]
        public void DoMapReduce()
        {
            string map = @"
                function() {
                    var logEntry = this;
                    emit(logEntry.Country, { count: 1, size: logEntry.Size });
                }";

            string reduce = @"        
                function(key, values) {
                    var result = {count: 0, size: 0 };

                    values.forEach(function(value){               
                        result.count += value.count;
                        result.size += value.size;
                    });

                    return result;
                }";

            string finalize = @"
                function(key, value){
      
                  value.averageSize = value.size / value.count;
                  return value;
                }";

            var collection = database.GetCollection(CollectionName);
            var options = new MapReduceOptionsBuilder();
            options.SetFinalize(finalize);
            options.SetOutput(MapReduceOutput.Inline);
            var results = collection.MapReduce(map, reduce, options);

            foreach (var result in results.GetResults())
            {
                Console.WriteLine(result.ToJson());
            }
            
        }

        [TestFixtureSetUp]
        public void StartAndInitialize()
        {
            string importScript = DatabaseStartScript.GetCurrentExecutingDirectory() + @"\..\..\ExplorativeTests\MongoDb\import_logs.cmd";
            Process.Start(new ProcessStartInfo(importScript));

            var client = new MongoClient(ConnectionString);
            server = client.GetServer();
            database = server.GetDatabase(DatabaseName);
        }

        [TestFixtureTearDown]
        public void DropAndDisconnect()
        {
            //database.GetCollection(CollectionName).Drop();

            if (server != null && server.State == MongoServerState.Connected)
            {
                server.Disconnect();
            }
        }
    }
}