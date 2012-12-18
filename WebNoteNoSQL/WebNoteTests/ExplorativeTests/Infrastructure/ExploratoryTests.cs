using NUnit.Framework;

using WebNoteNoSQL.Code.DbStartScript;

namespace WebNoteTests.ExplorativeTests.Infrastructure
{
    public class ExploratoryTest
    {
        private readonly DatabaseStartScript script;

        public ExploratoryTest(DatabaseStartScript script)
        {
            this.script = script;
        }

        [TestFixtureSetUp]
        public void StartTheServer()
        {
            new DatabaseServerProcess(script).Start();
        }
    }
}