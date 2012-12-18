namespace PostsharpAspects.Caching
{
    using System;
    using System.Reflection;
    using System.Text;

    using PostSharp.Aspects;

    using PostsharpAspects.Caching.CacheImplementation;

    /// <summary>
    /// Will cache the first argument that has the given Type
    /// </summary>
    [Serializable]
    public abstract class CacheBaseAspect : OnMethodBoundaryAspect
    {
        protected ICache Cache { get; set; }

        /// <summary>
        /// Method is invoked at build time to initialize the instance fields of the current aspect.
        /// </summary>
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            string prefix = method.DeclaringType.ToString();
            this.Cache = new Cache(prefix);
        }

        /// <summary>
        /// Creates a very unique key for the methods arguments
        /// uses the Type and value, returns an empty string if there are no args at all
        /// </summary>
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