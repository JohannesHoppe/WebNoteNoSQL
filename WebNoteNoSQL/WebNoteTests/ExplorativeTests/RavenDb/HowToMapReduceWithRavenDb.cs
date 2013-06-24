using System;
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
    [TestFixture]
    public class HowToMapReduceWithRavenDb : ExploratoryTest
    {
        private const string DocumentStoreUrl = "http://localhost:8080";
        private const string Database = "logs";
        private IDocumentStore store;

        public HowToMapReduceWithRavenDb()
            : base(DatabaseStartScript.ForRavenDb())
        {
        }

        [TestFixtureSetUp]
        public void Connect()
        {
            store = new DocumentStore { Url = DocumentStoreUrl }.Initialize();
            store.DatabaseCommands.EnsureDatabaseExists(Database);
        }

        [Test]
        [Ignore]
        public void DoMapReduce()
        {
            new SearchIndex().Execute(store);

            using (var session = store.OpenSession(Database))
            {
                var reducedResults = session.Query<SearchIndex.Result, SearchIndex>()
                    .ToList();
                
                foreach (var result in reducedResults)
                {
                    Console.WriteLine("Country: " + result.Country + "\tCount: " + result.Count + "\tSize:" + result.Size);
                }
            }
        }

        [Test]
        [Ignore]
        public void EnsureDatabaseHasLogEntries()
        {
            using (var session = store.OpenSession(Database))
            {
                var notes = session.Query<LogEntry>();
                Assert.That(notes.Count(), Is.EqualTo(5322), "Ups! Please import the logentries.");
            }
        }
    }
}