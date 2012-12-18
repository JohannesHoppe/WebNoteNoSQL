namespace PostsharpAspects.ExceptionHandling
{
    using System;

    using PostSharp.Aspects;
    using PostSharp.Aspects.Dependencies;

    [Serializable]
    [ProvideAspectRole(StandardRoles.ExceptionHandling)]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.Before, StandardRoles.Caching)]
    public class ConvertExceptionAspect : OnExceptionAspect 
    {
        public override void OnException(MethodExecutionArgs args)
        {
            throw new MyException("Fehler", args.Exception);
        }
    }
}
