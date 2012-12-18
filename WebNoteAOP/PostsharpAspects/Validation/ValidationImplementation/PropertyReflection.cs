namespace PostsharpAspects.Validation.ValidationImplementation
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Generic helper methods for working with lambda expressions in the style:  w => w.Name
    /// </summary>
    public static class PropertyReflection
    {
        /// <summary>
        /// Gets the name of a property that is wrapped in a lambda expression
        /// </summary>
        /// <typeparam name="T">object that is holding the property.</typeparam>
        /// <param name="property">lambda expression that describes a property.</param>
        /// <returns>name of property</returns>
        public static string GetExpressionName<T>(Expression<Func<T, object>> property)
        {
            // we have a primitive type (enum, int, double...)
            if (property.Body is UnaryExpression)
            {
                return GetUnaryExpressionName(property);
            }

            // its not primitive, so it is a complex type
            return GetMemberExpressionName(property);
        }

        /// <summary>
        /// 1. Gets the name of a property the is wrapped in a lambda expression
        /// 2. uses reflection to extract the data from the given item of the same (!) type
        /// </summary>
        /// <typeparam name="T">object that is holding the property.</typeparam>
        /// <param name="property">lambda expression that describes a property.</param>
        /// <param name="item">instance of the object</param>
        /// <returns>value of the property</returns>
        public static object ExpressionToValue<T>(
            Expression<Func<T, object>> property,
            T item)
        {
            // 1.
            string propertyName = GetExpressionName(property);

            // 2. since we are working on "T" only, the usage of strings is save here
            PropertyInfo pi = typeof(T).GetProperty(propertyName);
            return pi.GetValue(item, null);
        }

        /// <summary>
        /// Gets the name of a UNARYEXPRESSION property (holding a primitive type) that is wrapped in a lambda expression
        /// </summary>
        /// <typeparam name="T">object that is holding the property.</typeparam>
        /// <param name="property">lambda expression that describes a property.</param>
        /// <returns>name of property</returns>
        private static string GetUnaryExpressionName<T>(Expression<Func<T, object>> property)
        {
            return ((UnaryExpression)property.Body).Operand.ToString().Split('.')[1];
        }

        /// <summary>
        /// Gets the name of a MEMBEREXPRESSION property that is wrapped in a lambda expression
        /// </summary>
        /// <typeparam name="T">object that is holding the property.</typeparam>
        /// <param name="property">lambda expression that describes a property.</param>
        /// <returns>name of property</returns>
        private static string GetMemberExpressionName<T>(Expression<Func<T, object>> property)
        {
            return ((MemberExpression)property.Body).Member.Name;
        }
    }
}