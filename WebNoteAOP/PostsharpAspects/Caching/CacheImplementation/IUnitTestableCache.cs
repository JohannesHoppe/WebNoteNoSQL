namespace PostsharpAspects.Caching.CacheImplementation
{
    public interface IUnitTestableCache
    {
        void CleanCompleteCache();

        object GetFirstItemFromCache();
    }
}
