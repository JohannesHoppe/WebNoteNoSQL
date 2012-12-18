namespace PostsharpAspects.Validation.ValidationImplementation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using MvcValidator = System.ComponentModel.DataAnnotations.Validator;

    /// <summary>
    /// Validator that bases on DataAnnotations
    /// (uses the System.ComponentModel.DataAnnotations.Validator from ASP.NET MVC)
    /// </summary>
    [Serializable]
    public class DataAnnotationsValidator : IValidator
    {
        /// <summary>
        /// Determines whether the specified object is valid
        /// </summary>
        /// <param name="objectToValidate">The object to validate.</param>
        /// <returns>empty list if the object validates; otherwise, list with error messages.</returns>
        /// <exception cref="System.ArgumentNullException">instance is null.</exception>
        public IValidationResults Validate(object objectToValidate)
        {
            List<ValidationResult> rawResults = TryValidateObject(objectToValidate).ValidationResults;

            ValidationResults results = new ValidationResults();
            CopyModelErrors(rawResults, results);

            return results;
        }

        /// <summary>
        /// Determines whether the specified object is valid
        /// </summary>
        /// <param name="objectToValidate">The object to validate.</param>
        /// <returns>true if the object validates; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">instance is null.</exception>
        public bool IsValid(object objectToValidate)
        {
            return TryValidateObject(objectToValidate).IsValid;
        }

        /// <summary>
        /// Wrapper for Validator.TryValidateObject
        /// </summary>
        /// <exception cref="System.ArgumentNullException">instance is null</exception>
        private static CombinedValues TryValidateObject(object objectToValidate)
        {
            ValidationContext validationContext = new ValidationContext(objectToValidate, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool isValid = MvcValidator.TryValidateObject(objectToValidate, validationContext, validationResults, true);

            return new CombinedValues
            {
                IsValid = isValid,
                ValidationResults = validationResults
            };
        }

        /// <summary>
        /// Adds all results to the IValidationResults list
        /// </summary>
        private static void CopyModelErrors(IEnumerable<ValidationResult> fromList, IValidationResults toList)
        {
            foreach (var result in fromList)
            {
                string key = result.MemberNames.FirstOrDefault();

                // hint: good place for doing string manipulation (eg. translation)
                string errorMessage = result.ErrorMessage + " (own Validator!)";

                toList.AddError(key, errorMessage);
            }
        }

        /// <summary>
        /// used internally to return two values
        /// </summary>
        private class CombinedValues
        {
            public bool IsValid { get; set; }

            public List<ValidationResult> ValidationResults { get; set; }
        }
    }
}
