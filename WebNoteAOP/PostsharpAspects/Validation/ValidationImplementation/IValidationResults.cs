namespace PostsharpAspects.Validation.ValidationImplementation
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    /// <summary>
    /// Used to describe validation errors
    /// </summary>
    public interface IValidationResults
    {
        /// <summary>
        /// Gets a value indicating whether this instance of the model-state dictionary is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        bool IsValid { get; }

        /// <summary>
        /// Gets a ModelStateDictionary that can be merged with the Controllers ModelState
        /// </summary>
        ModelStateDictionary AsModelStateDictionary { get; }

        /// <summary>
        /// Adds the specified error message to the errors collection for the model-state
        /// dictionary that is associated with the specified expression
        /// </summary>
        /// <typeparam name="T">object that is holding the property.</typeparam>
        /// <param name="property">lambda expression that describes a property.</param>
        /// <param name="errorMessage">The error message.</param>
        void AddError<T>(Expression<Func<T, object>> property, string errorMessage);

        /// <summary>
        /// Adds the specified error message to the errors collection for the model-state
        /// dictionary that is associated with the specified key.
        /// </summary>
        /// <param name="key">key that should match a property name (don't use this "by hand", use lambdas where possible)</param>
        /// <param name="errorMessage">The error message.</param>
        void AddError(string key, string errorMessage);

        /// <summary>
        /// Determines whether the model-state dictionary contains an error for the specified expression
        /// </summary>
        /// <typeparam name="T">object that is holding the property.</typeparam>
        /// <param name="property">lambda expression that describes a property.</param>
        /// <returns>
        /// <c>true</c> if the expression was found; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsKey<T>(Expression<Func<T, object>> property);
    }
}