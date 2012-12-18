namespace PostsharpAspects.Logging
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    using NLog;

    using PostSharp.Aspects;
    using PostSharp.Aspects.Dependencies;

    [Serializable]
    [ProvideAspectRole(StandardRoles.PerformanceInstrumentation)]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.After, StandardRoles.Caching)]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.After, StandardRoles.ExceptionHandling)]
    [AspectRoleDependency(AspectDependencyAction.Commute, StandardRoles.PerformanceInstrumentation)]
    public class LogTimeAspect : OnMethodBoundaryAspect
    {
        private const int SlowTotalMilliseconds = 250;

        private static readonly Logger Logger = LogManager.GetLogger("LogTimeAspect");
        private static readonly Stopwatch Stopwatch = new Stopwatch();
        private string instanceName;

        static LogTimeAspect()
        {
            Stopwatch.Start();
        }

        /// <summary>
        /// Method executed at build time.
        /// </summary>
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            this.instanceName = method.DeclaringType.FullName + "." + method.Name;
        }

        /// <summary>
        /// Saves the time method start 
        /// </summary>
        [DebuggerStepThrough]
        public override void OnEntry(MethodExecutionArgs args)
        {
            args.MethodExecutionTag = Stopwatch.ElapsedTicks;
        }

        /// <summary>
        /// Stops the time on method end
        /// </summary>
        [DebuggerStepThrough]
        public override void OnExit(MethodExecutionArgs args)
        {
            long timeInTicks = Stopwatch.ElapsedTicks - (long)args.MethodExecutionTag;
            double totalMilliseconds = TimeSpan.FromTicks(timeInTicks).TotalMilliseconds;

            if (totalMilliseconds > SlowTotalMilliseconds)
            {
                Logger.Trace(String.Format("{0}ms\t- {1}", totalMilliseconds, this.instanceName));
            }

            base.OnExit(args);
        }
    }
}