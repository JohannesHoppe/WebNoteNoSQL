namespace PostsharpAspects.Validation
{
    using System;
    using System.Reflection;

    using PostSharp.Aspects;
    using PostSharp.Aspects.Dependencies;

    using PostsharpAspects.Validation.ValidationImplementation;

    /// <summary>
    /// Will throw a DataNotValidException if any method is called with anvalid data
    /// </summary>
    [Serializable]
    [ProvideAspectRole(StandardRoles.Validation)]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.Before, StandardRoles.ExceptionHandling)]
    [AspectRoleDependency(AspectDependencyAction.Order, AspectDependencyPosition.Before, StandardRoles.Caching)]
    public class ValidationGuardAspect : OnMethodBoundaryAspect
    {
        private string methodName;
        private IValidator validator = new DataAnnotationsValidator();

        /// <summary>
        /// Checks for validitity
        /// </summary>
        public override void OnEntry(MethodExecutionArgs args)
        {
            int amountOfParameters = args.Arguments.Count;

            for (int i = 0; i < amountOfParameters; i++)
            {
                object argument = args.Arguments.GetArgument(i);
  
                if (argument != null &&
                    !this.validator.IsValid(argument))
                {
                    throw new DataNotValidException(
                        String.Format(
                            "The paramter number '{0}' in '{1}' contained invalid data!",
                            GetParametersName(args, i),
                            this.methodName));
                }
            }
        }

        /// <summary>
        /// Method executed at build time.
        /// </summary>
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            this.methodName = method.DeclaringType.FullName + "." + method.Name;
            this.validator = new DataAnnotationsValidator();
        }

        /// <summary>
        /// Guard will ignore Properties
        /// </summary>
        public override bool CompileTimeValidate(MethodBase method)
        {
            return !(method.Name.StartsWith("set_") || method.Name.StartsWith("get_"));
        }

        /// <summary>
        /// Gets the name of the parameter at the given index positions
        /// </summary>
        private static string GetParametersName(MethodExecutionArgs args, int index)
        {
            ParameterInfo[] parameters = args.Method.GetParameters();
            return parameters[index].Name;
        }
    }
}
