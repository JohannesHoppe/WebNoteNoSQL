namespace PostsharpAspects
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    using PostSharp.Aspects;

    public static class NewDateTime
    {
        private static DateTime newDateTime = DateTime.MinValue;

        public static DateTime DateTime
        {
            get
            {
                return newDateTime;
            }

            set
            {
                newDateTime = value;
            }
        }
    }

    /// <summary>
    /// Used to override System.DateTime.Now
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    [Serializable]
    public class DateTimeNowOverrideAspect : OnMethodBoundaryAspect
    {
        public override void OnExit(MethodExecutionArgs args)
        {
            args.ReturnValue = NewDateTime.DateTime != DateTime.MinValue ?
                NewDateTime.DateTime :
                DateTime.Now;
        }

        public override bool CompileTimeValidate(MethodBase method)
        {
            return method.Name == "get_Now";
        }
    }
}