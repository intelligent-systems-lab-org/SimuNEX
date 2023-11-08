using NUnit.Framework;
using SimuNEX;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FactoryTests
{
    [SetUpFixture]
    public class TypeRegisterSetup<T>
    {
        private static readonly HashSet<Type> typesTested = new();

        public static void RegisterTest<U>() where U : T
        {
            _ = typesTested.Add(typeof(U));
        }

        [OneTimeTearDown]
        public void AfterAllTests()
        {
            // Retrieve all available fault types using the factory
            Type[] types = Factory<T>.GetAvailableTypes();

            // Check if all fault types have been registered/tested
            List<Type> untestedTypes = types.Where(t => !typesTested.Contains(t)).ToList();
            if (untestedTypes.Count > 0)
            {
                throw new Exception(
                    "The following fault types are not tested: " + string.Join(", ", untestedTypes.Select(t => t.Name)));
            }
        }
    }
}
