namespace WebNoteAOP.Tests.PostsharpAspects.CacheImpTest
{
    using global::PostsharpAspects.Caching.CacheImplementation;
    
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NUnit.Framework;

    using Assert = NUnit.Framework.Assert;

    [TestClass]
    public class CacheTest
    {
        private ICache cache;

        [TestInitialize]
        public void ResetValues()
        {
            this.cache = new Cache("x");
            ((IUnitTestableCache)this.cache).CleanCompleteCache();
        }

        [TestMethod]
        public void CanReturnData()
        {
            // Arrange
            CacheTestObject expected = new CacheTestObject { Key = 1, Name = "Test" };

            // Act
            this.cache.Insert("1", expected);
            CacheTestObject actual = this.cache.Get("1") as CacheTestObject;

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestMethod]
        public void CanOverride()
        {
            // Arrange
            this.cache.Insert("1", new CacheTestObject { Key = 1, Name = "Test" });
            this.cache.Insert("2", new CacheTestObject { Key = 2, Name = "Test2" });
            this.cache.Insert("3", new CacheTestObject { Key = 3, Name = "Test3" });
            this.cache.Insert("4", new CacheTestObject { Key = 4, Name = "Test4" });

            CacheTestObject expected = new CacheTestObject { Key = 3, Name = "New" };

            // Act
            this.cache.Insert("3", expected);
            CacheTestObject actual = this.cache.Get("3") as CacheTestObject;
            
            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
