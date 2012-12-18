namespace PostsharpAspects.Logging
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    using NLog;

    using PostSharp;
    using PostSharp.Aspects;
    using PostSharp.Aspects.Dependencies;
    using PostSharp.Extensibility;

    [Serializable]
    [ProvideAspectRole(StandardRoles.PerformanceInstrumentation)]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.After, StandardRoles.Caching)]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.After, StandardRoles.ExceptionHandling)]
    [AspectRoleDependency(AspectDependencyAction.Commute, StandardRoles.PerformanceInstrumentation)]
    public class StackOverflowDetectionAspect : OnMethodBoundaryAspect
    {
        private const int CriticalFrameCount = 50;
        private static readonly Logger Logger = LogManager.GetLogger("StackOverflowDetectionAspect");
        private static bool showWarningOnlyOnce;
        private string instanceName;

        /// <summary>Method executed at build time.</summary>
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            this.instanceName = method.DeclaringType.FullName + "." + method.Name;

            if (showWarningOnlyOnce)
            {
                return;
            }

            showWarningOnlyOnce = true;
            Message.Write(
                MessageLocation.Of(method),
                SeverityType.Warning,
                "StackOverflowDetectionAspect",
                "Getting a StackTrace is very expensive. This aspect should only be used to debug a known issue and should be removed after the issue is located, Method: {0} and other!",
                method.DeclaringType.Name);
        }

        /// <summary>
        /// Method invoked before the execution of the method to which the current aspect is applied.
        /// </summary>
        [DebuggerStepThrough]
        public override void OnEntry(MethodExecutionArgs args)
        {
            StackTrace st = new StackTrace();
            int frameCount = st.FrameCount;

            if (frameCount > CriticalFrameCount)
            {
                Logger.Trace(string.Format("{0}x\t- {1}", frameCount, this.instanceName));
            }
        }
    }
}