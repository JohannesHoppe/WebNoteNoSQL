namespace PostsharpAspects.Caching.CacheImplementation
{
    public interface ICache
    {
        /// <summary>
        /// Returns an instance
        /// </summary>
        object Get(string cacheKey);

        /// <summary>
        /// Inserts/Updates a cached item
        /// </summary>
        void Insert(string cacheKey, object item);

        /// <summary>
        /// Removes a cached item
        /// </summary>
        void Remove(string cacheKey);

        /// <summary>
        /// Clears all entries
        /// </summary>
        void RemoveAll();
    }
}