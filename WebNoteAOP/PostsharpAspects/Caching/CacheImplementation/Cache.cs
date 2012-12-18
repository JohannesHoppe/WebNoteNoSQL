// ReSharper disable FieldCanBeMadeReadOnly.Local
namespace PostsharpAspects.Caching.CacheImplementation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Web;

    /// <summary>
    /// Wrapper around HttpRuntime.Cache
    /// </summary>
    [Serializable]
    public class Cache : ICache, IUnitTestableCache
    {
        // prefix that should be unique for one type of cached data
        private string cacheKeyPrefix;
        
        public Cache(string cacheKeyPrefix)
        {
            this.cacheKeyPrefix = cacheKeyPrefix + "_";
        }

        /// <summary>
        /// Returns an instance
        /// </summary>
        public object Get(string cacheKey)
        {
            return HttpRuntime.Cache.Get(this.GetFullCacheKey(cacheKey));
        }

        /// <summary>
        /// Inserts/Updates a cached item
        /// </summary>
        public void Insert(string cacheKey, object value)
        {
            HttpRuntime.Cache.Insert(this.GetFullCacheKey(cacheKey), value);
        }

        /// <summary>
        /// Removes a cached item
        /// </summary>
        public void Remove(string cacheKey)
        {
            HttpRuntime.Cache.Remove(this.GetFullCacheKey(cacheKey));
        }

        /// <summary>
        /// Clears all entries
        /// </summary>
        public void RemoveAll()
        {
            List<string> itemsToRemove = new List<string>();
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().StartsWith(this.cacheKeyPrefix))
                {
                    itemsToRemove.Add(enumerator.Key.ToString());
                }
            }

            foreach (string itemToRemove in itemsToRemove)
            {
                HttpRuntime.Cache.Remove(itemToRemove);
            }
        }

        #region implements IUnitTestableCache
        
        public void CleanCompleteCache()
        {
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                HttpRuntime.Cache.Remove(enumerator.Key.ToString());
            }
        }

        public object GetFirstItemFromCache()
        {
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                return enumerator.Entry.Value;
            }

            return null;
        }

        #endregion

        private string GetFullCacheKey(string cacheKey)
        {
            return this.cacheKeyPrefix + cacheKey;
        }
    }
}

// ReSharper restore FieldCanBeMadeReadOnly.Local