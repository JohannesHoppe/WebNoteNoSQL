using System;
using NUnit.Framework;
using ServiceStack.CacheAccess;
using ServiceStack.Redis;
using WebNoteNoSQL.Code.DbStartScript;
using WebNoteTests.ExplorativeTests.Infrastructure;

namespace WebNoteTests.ExplorativeTests.Redis
{
    [TestFixture]
    public class HowToUseRedisAsCache : ExploratoryTest
    {
        private const string RedisHost = "localhost";

        public HowToUseRedisAsCache()
            : base(DatabaseStartScript.ForRedis())
        {
        }

        /// <summary>
        /// The RedisClient implements ICacheClient, a generic interface
        /// The following servicestack cache providers implement this interface, too:
        /// - Memcached
        /// - Redis
        /// - In Memory Cache
        /// - FileAndCacheTextManager
        /// </summary>
        [Test]
        public void CacheDemo()
        {
            const int CachedValue = 42;

            ICacheClient redisClient = new RedisClient(RedisHost);

            // store some value in cache for one hour
            redisClient.Set("cache_key", CachedValue, TimeSpan.FromSeconds(3600));

            // get typed value from cache
            int valueFromCache = redisClient.Get<int>("cache_key");

            Assert.That(valueFromCache, Is.EqualTo(CachedValue));
        }
    }
}