using System;
using System.Linq;
using System.Reflection;

namespace SimuNEX
{
    /// <summary>
    /// Used for configuration of SimNEX types.
    /// </summary>
    /// <typeparam name="T">SimuNEX type.</typeparam>
    public static class Factory<T>
    {
        /// <summary>
        /// Creates an instance of type <see cref="T"/>.
        /// </summary>
        /// <param name="type">The type to be created.</param>
        /// <returns>An object of type <see cref="T"/>.</returns>
        public static T Create(Type type)
        {
            return (T)Activator.CreateInstance(type);
        }

        /// <summary>
        /// Returns all types derived from <see cref="T"/>.
        /// </summary>
        /// <param name="includeNested">Whether to include nested classes.</param>
        /// <param name="includeOnlyPublic">Whether to include only public classes.</param>
        /// <returns>List of all types derived from <see cref="T"/>.</returns>
        public static Type[] GetAvailableTypes(bool includeNested = true, bool includeOnlyPublic = false)
        {
            return Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(t => t.IsSubclassOf(typeof(T))
                    && !t.IsAbstract
                    && (!includeOnlyPublic || t.IsPublic)
                    && (includeNested || !t.IsNested))
                .ToArray();
        }
    }
}
