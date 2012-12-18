namespace PostsharpAspects.Caching
{
    using System;

    using PostSharp.Aspects;
    using PostSharp.Aspects.Dependencies;

    /// <summary>
    /// Will cache the first argument that has the given Type
    /// </summary>
    [Serializable]
    [ProvideAspectRole(StandardRoles.Caching)]
    public class CacheGetAspect : CacheBaseAspect
    {
        /// <summary>
        /// Method executed <b>before</b> the body of methods to which this aspect is applied.
        /// </summary>
        public override void OnEntry(MethodExecutionArgs args)
        {
            string cacheKey = GenerateCacheKey(args);
            object value = this.Cache.Get(cacheKey);

            if (value == null)
            {
                args.MethodExecutionTag = cacheKey; // so we don't have to call GenerateKey again
                return;
            }

            // data is already cached, we we will not call OnSuccess and return the value immediately
            args.ReturnValue = value;
            args.FlowBehavior = FlowBehavior.Return;        
        }

        /// <summary>
        /// Method will only be called when the data wasn't cached before
        /// </summary>
        public override void OnSuccess(MethodExecutionArgs args)
        {
            string cacheKey = (string)args.MethodExecutionTag;
            this.Cache.Insert(cacheKey, args.ReturnValue);
        }
    }
}