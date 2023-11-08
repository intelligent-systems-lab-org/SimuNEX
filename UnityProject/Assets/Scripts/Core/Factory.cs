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
        public static T Create(Type type)
        {
            return (T)Activator.CreateInstance(type);
        }

        public static Type[] GetAvailableTypes()
        {
            return Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract)
                .ToArray();
        }
    }
}
