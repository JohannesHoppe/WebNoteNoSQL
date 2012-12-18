using System;
using System.Runtime.Remoting.Messaging;
using Microsoft.Practices.Unity;

namespace WebNoteNoSQL.Code.Unity
{
    /// <summary>
    /// A singleton with the skope and livetime of one web request
    /// </summary>
    /// <see>http://stackoverflow.com/questions/1151201/singleton-per-call-context-web-request-in-unity</see>
    public class PerCallContextLifetimeManager : LifetimeManager
    {
        private readonly string key;

        /// <summary>
        /// Initializes a new instance of the <see cref="PerCallContextLifetimeManager"/> class.
        /// </summary>
        public PerCallContextLifetimeManager()
        {
            key = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Retrieve a value from the backing store associated with this Lifetime policy.
        /// </summary>
        /// <returns>
        /// the object desired, or null if no such object is currently stored.
        /// </returns>
        public override object GetValue()
        {
            return CallContext.GetData(key);
        }

        /// <summary>
        /// Stores the given value into backing store for retrieval later.
        /// </summary>
        /// <param name="newValue">The object being stored.</param>
        public override void SetValue(object newValue)
        {
            CallContext.SetData(key, newValue);
        }

        /// <summary>
        /// Remove the given object from backing store.
        /// </summary>
        /// <remarks>No code, because Unity nevery call RemoveValue directly</remarks>
        public override void RemoveValue()
        {
        }
    }
}