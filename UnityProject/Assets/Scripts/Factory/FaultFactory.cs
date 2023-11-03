using System;
using System.Linq;
using System.Reflection;

namespace SimuNEX
{
    /// <summary>
    /// Used for configuration of <see cref="Fault"/> types.
    /// </summary>
    public static class FaultFactory
    {
        public static Fault CreateFault(Type faultType)
        {
            return (Fault)Activator.CreateInstance(faultType);
        }

        public static Type[] GetAvailableFaultTypes()
        {
            return Assembly.GetAssembly(typeof(Fault)).GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Fault)) && !t.IsAbstract)
                .ToArray();
        }
    }
}
