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
    public class EmbeddedModeRavenDb
    {
        [Test]
        public void DoSomeActions()
        {
            using (var documentStore = new EmbeddableDocumentStore{ RunInMemory = true}.Initialize())
            {
                using (var session = documentStore.OpenSession())
                {
                    // Run complex test scenarious
                }
            }
        }
    }
}