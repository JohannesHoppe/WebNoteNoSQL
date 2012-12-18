namespace PostsharpAspects.Caching
{
    using System;

    using PostSharp.Aspects;
    using PostSharp.Aspects.Dependencies;

    /// <summary>
    /// Will remove all cached items
    /// </summary>
    [Serializable]
    [ProvideAspectRole(StandardRoles.Caching)]
    public class CacheRemoveAspect : CacheBaseAspect
    {
        public override void OnExit(MethodExecutionArgs args)
        {
            this.Cache.RemoveAll();
        }
    }
}
