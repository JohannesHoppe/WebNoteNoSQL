namespace PostsharpAspects.Validation.ValidationImplementation
{
    using System;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    /// <summary>
    /// Wrapper for the MVC ModelStateDictionary
    /// Used to describe validation errors caused by a given property
    /// <para>
    /// Instead of using strings to name the property this implementation
    /// also supports strongly typed lambda expressions! (that's cool!)
    /// </para>
    /// </summary>
    public class ValidationResults : IValidationResults
    {
        /// <summary>
        /// Internally we will store all data to an "old school" ModelStateDictionary (which requires string-keys!)
        /// </summary>
        private readonly ModelStateDictionary modelStateDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResults"/> class.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        public ValidationResults(ModelStateDictionary modelState = null)
        {
            this.modelStateDictionary = modelState ?? new ModelStateDictionary();
        }

        /// <summary>
        /// Gets a value indicating whether this instance of the model-state dictionary is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        public bool IsValid
        {
            get { return this.modelStateDictionary.IsValid; }
        }

        /// <summary>
        /// Gets a ModelStateDictionary that can be merged with the Controllers ModelState
        /// </summary>
        public ModelStateDictionary AsModelStateDictionary
        {
            get
            {
                return this.modelStateDictionary;
            }
        }

        /// <summary>
        /// Adds the specified error message to the errors collection for the model-state
        /// dictionary that is associated with the specified expression
        /// </summary>
        /// <typeparam name="T">object that is holding the property.</typeparam>
        /// <param name="property">lambda expression that describes a property.</param>
        /// <param name="errorMessage">The error message.</param>
        public void AddError<T>(Expression<Func<T, object>> property, string errorMessage)
        {
            this.modelStateDictionary.AddModelError(GetExpressionName(property), errorMessage);
        }

        /// <summary>
        /// Adds the specified error message to the errors collection for the model-state
        /// dictionary that is associated with the specified key.
        /// </summary>
        /// <param name="key">key that should match a property name (don't use this "by hand", use lambdas whenever possible)</param>
        /// <param name="errorMessage">The error message.</param>
        public void AddError(string key, string errorMessage)
        {
            this.modelStateDictionary.AddModelError(key, errorMessage);
        }

        /// <summary>
        /// Determines whether the model-state dictionary contains the specified expression
        /// <para>
        /// If this is true, the expression caused an error
        /// </para>
        /// </summary>
        /// <typeparam name="T">object that is holding the property.</typeparam>
        /// <param name="property">lambda expression that describes a property.</param>
        /// <returns>
        ///     <c>true</c> if the expression was found; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsKey<T>(Expression<Func<T, object>> property)
        {
            return this.modelStateDictionary.ContainsKey(GetExpressionName(property));
        }

        /// <summary>
        /// Gets the name of a property that is wrapped in the lambda expression
        /// </summary>
        /// <typeparam name="T">object that is holding the property.</typeparam>
        /// <param name="property">lambda expression that describes a property.</param>
        /// <returns>name of property</returns>
        private static string GetExpressionName<T>(Expression<Func<T, object>> property)
        {
            return PropertyReflection.GetExpressionName(property);
        }
    }
}