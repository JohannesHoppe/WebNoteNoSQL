namespace PostsharpAspects.Validation.ValidationImplementation
{
    public interface IValidator
    {
        /// <summary>
        /// Determines whether the specified object is valid
        /// </summary>
        /// <param name="objectToValidate">The object to validate.</param>
        /// <returns>empty list if the object validates; otherwise, list with error messages.</returns>
        /// <exception cref="System.ArgumentNullException">instance is null.</exception>
        IValidationResults Validate(object objectToValidate);

        /// <summary>
        /// Determines whether the specified object is valid
        /// </summary>
        /// <param name="objectToValidate">The object to validate.</param>
        /// <returns>true if the object validates; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">instance is null.</exception>
        bool IsValid(object objectToValidate);
    }
}