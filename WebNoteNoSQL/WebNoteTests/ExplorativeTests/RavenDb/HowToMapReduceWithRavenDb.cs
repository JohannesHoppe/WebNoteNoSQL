using System;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using NUnit.Framework;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;
using Raven.Client.Indexes;
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
        public void DoMapReduce()
        {
            //// A cleaner way --> AbstractIndexCreationTask<TDocument,TReduceResult>
            // var assembly = typeof(Logs_SearchIndex).Assembly;
            // var catalog = new CompositionContainer(new AssemblyCatalog(assembly));
            // IndexCreation.CreateIndexes(catalog, store.DatabaseCommands.ForDatabase(Database), store.Conventions);

            store.DatabaseCommands.ForDatabase(Database).PutIndex(
                "Logs/SearchIndex",
                new IndexDefinitionBuilder<LogEntry, Logs_SearchIndex.Result>
                {
                    Map = logEntries => from entry in logEntries
                                        select new Logs_SearchIndex.Result
                                        { 
                                            Country = entry.Country,
                                            Size = entry.Size,
                                            Count = 1
                                         },

                    Reduce = results => from entry in results
                                        group entry by entry.Country into e
                                        select new Logs_SearchIndex.Result
                                        {
                                            Country = e.Key,
                                            Count = e.Sum(x => x.Count),
                                            Size = e.Sum(x => x.Size),
                                        }
                }, true);


            using (var session = store.OpenSession(Database))
            {
                var reducedResults = session.Query<Logs_SearchIndex.Result, Logs_SearchIndex>().ToList();

                Console.WriteLine("{0,25} {1,10} {2,15}", "Country", "Count", "Size");
                foreach (var result in reducedResults)
                {
                    Console.WriteLine("{0,25} {1,10} {2,15}", result.Country, result.Count, result.Size);
                }
            }
        }

        [Test]
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