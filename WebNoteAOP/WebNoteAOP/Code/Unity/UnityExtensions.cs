using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.Practices.Unity;

namespace WebNoteAOP.Code.Unity
{
    /// <summary>
    /// This is very ugly, but Unity lacks a TryResolve feature... :-(
    /// </summary>
    public static class UnityExtensions
    {
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Reviewed")]
        public static T TryResolve<T>(this IUnityContainer container)
        {
            try
            {
                return container.Resolve<T>();
            }
            catch (Exception)
            {
                return default(T); 
            }
        }
    }
}