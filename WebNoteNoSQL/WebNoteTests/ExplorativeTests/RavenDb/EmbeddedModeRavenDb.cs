using NUnit.Framework;
using Raven.Client.Embedded;

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