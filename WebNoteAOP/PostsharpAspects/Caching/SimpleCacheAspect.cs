namespace PostsharpAspects.Caching
{
    using System;
    using System.Reflection;
    using System.Text;

    using PostSharp.Aspects;

    using PostsharpAspects.Caching.CacheImplementation;

    [Serializable]
    public class SimpleCacheBaseAspect : OnMethodBoundaryAspect
    {
        protected ICache Cache { get; set; }

        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            string prefix = method.DeclaringType.ToString();
            this.Cache = new Cache(prefix);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            string cacheKey = GenerateCacheKey(args);
            object value = this.Cache.Get(cacheKey);

            if (value == null)
            {
                args.MethodExecutionTag = cacheKey;
                return;
            }

            args.ReturnValue = value;
            args.FlowBehavior = FlowBehavior.Return;
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            string cacheKey = (string)args.MethodExecutionTag;
            this.Cache.Insert(cacheKey, args.ReturnValue);
        }

        protected static string GenerateCacheKey(MethodExecutionArgs args)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < args.Arguments.Count; i++)
            {
                if (i > 0)
                {
                    stringBuilder.Append("|");
                }

                stringBuilder.AppendFormat(
                    "{0}_{1}",
                    args.Method.GetParameters()[i].Name,
                    args.Arguments.GetArgument(i) ?? "null");
            }

            return stringBuilder.ToString();
        }
    }
}