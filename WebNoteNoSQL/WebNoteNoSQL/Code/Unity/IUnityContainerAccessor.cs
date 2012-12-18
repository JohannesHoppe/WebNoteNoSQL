using Microsoft.Practices.Unity;

namespace WebNoteNoSQL.Code.Unity
{
    /// <summary>
    /// used to cast the MvcApplication
    /// </summary>
    public interface IUnityContainerAccessor
    {
        /// <summary>
        /// Gets the Unity container. 
        /// </summary>
        IUnityContainer Container { get; }
    }
}