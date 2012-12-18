namespace WebNoteAOP.Tests.CacheImplementation
{
    /*
    [TestClass]
    public class CacheTest
    {
        private bool slowMethodCalled;
        private SimpleCache simpleCache;

        [TestInitialize]
        public void ResetValues()
        {
            this.slowMethodCalled = false;

            #region clean HttpRuntime.Cache

            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                HttpRuntime.Cache.Remove(enumerator.Key.ToString());
            }
            #endregion

            this.simpleCache = new SimpleCache("any_prefix");
        }

        [TestMethod]
        public void CanReturnData()
        {
            // Arrange
            YummySausageSalad expected = new YummySausageSalad { Key = 1, Name = "Wurstsalat" };

            // Act
            Func<YummySausageSalad> slowMethod = () => this.SlowMethod(expected.Key, expected.Name);
            YummySausageSalad actual = this.simpleCache.GetWithInsert(expected.Key.ToString(), slowMethod);

            // Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void CanCache()
        {
            // Arrange
            YummySausageSalad expected = new YummySausageSalad { Key = 1, Name = "Wurstsalat" };

            // Act
            Func<YummySausageSalad> slowMethod = () => this.SlowMethod(expected.Key, expected.Name);

            // 1st Assert - SlowMethod should be called
            YummySausageSalad actual = this.simpleCache.GetWithInsert(expected.Key.ToString(), slowMethod);
            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(this.slowMethodCalled, Is.True);


            // 2nd Assert - SlowMethod should NOT be called
            this.slowMethodCalled = false;
            YummySausageSalad actual2 = this.simpleCache.GetWithInsert(expected.Key.ToString(), slowMethod);
            Assert.That(actual2, Is.EqualTo(expected));
            Assert.That(this.slowMethodCalled, Is.False);
        }

        [TestMethod]
        public void CanOverride()
        {
            // Arrange
            this.simpleCache.GetWithInsert("1", () => this.SlowMethod(1, "Wurstsalat"));
            this.simpleCache.GetWithInsert("2", () => this.SlowMethod(2, "Wurstsalat2"));
            this.simpleCache.GetWithInsert("3", () => this.SlowMethod(3, "Wurstsalat3"));
            this.simpleCache.GetWithInsert("4", () => this.SlowMethod(4, "Wurstsalat4"));

            YummySausageSalad expected = new YummySausageSalad { Key = 3, Name = "Wurstsuppe" };

            // Act
            this.simpleCache.Insert(expected.Key.ToString(), expected);
            YummySausageSalad actual = HttpRuntime.Cache.Get("any_prefix_3") as YummySausageSalad;
            
            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }





        // this would be a slow method
        public YummySausageSalad SlowMethod(int key, string name)
        {
            this.slowMethodCalled = true;
            return new YummySausageSalad { Key = key, Name = name };
        }
    }
    */
}
