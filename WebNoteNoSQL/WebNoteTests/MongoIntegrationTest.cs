using FluentAssertions;
using Machine.Specifications;
using Mongo2Go;
using MongoDB.Driver;

// ReSharper disable UnusedMember.Local
namespace WebNoteTests
{
    [Subject("Hello Mongo2Go")]
    public class When_deserializing_TestDocument
    {
        static MongoDbRunner runner;
        static MongoCollection<TestDocument> collection;
        static TestDocument actual;

        Establish context = () =>
        {
            runner = MongoDbRunner.Start(); // just one line, that's all we have to do!

            collection = new MongoClient(runner.ConnectionString)
                .GetServer()
                .GetDatabase("TestDatabase")
                .GetCollection<TestDocument>("TestCollection");

            collection.Insert(TestDocument.GetDummyData());
        };

        Because of = () => actual = collection.FindOneAs<TestDocument>();

        It should_hava_equal_data = () => actual.ShouldHave().AllPropertiesBut(d => d.Id).EqualTo(TestDocument.GetDummyData());
    }
}
// ReSharper restore UnusedMember.Local
