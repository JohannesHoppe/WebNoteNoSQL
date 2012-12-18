namespace PostsharpAspects.Validation
{
    using System;

    using PostSharp.Aspects;
    using PostSharp.Aspects.Advices;
    using PostSharp.Aspects.Dependencies;

    using PostsharpAspects.Validation.ValidationImplementation;

    [Serializable]
    [ProvideAspectRole(StandardRoles.Validation)]
    [IntroduceInterface(typeof(IValidator))]
    public class ValidatorIntroduceAspect : InstanceLevelAspect, IValidator
    {
        private IValidator validator;

        public IValidationResults Validate(object objectToValidate)
        {
            return this.validator.Validate(objectToValidate);
        }

        public bool IsValid(object objectToValidate)
        {
            return this.validator.IsValid(objectToValidate);
        }

        public override void CompileTimeInitialize(Type type, AspectInfo aspectInfo)
        {
            this.validator = new DataAnnotationsValidator();
        }
    }
}
