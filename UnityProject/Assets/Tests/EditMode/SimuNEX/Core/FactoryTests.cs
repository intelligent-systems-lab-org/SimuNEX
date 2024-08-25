using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using SimuNEX;

namespace SimuNEXTests
{
    [SetUpFixture]
    public class TypeRegisterSetup<T>
    {
        public virtual bool includeOnlyPublic => false;
        public virtual bool includeNested => true;

        private static readonly HashSet<Type> typesTested = new();

        public static void RegisterTest<U>() where U : T
        {
            _ = typesTested.Add(typeof(U));
        }

        [OneTimeTearDown]
        public void AfterAllTests()
        {
            // Retrieve all available types using the factory
            Type[] types = Factory<T>.GetAvailableTypes(includeNested, includeOnlyPublic);

            // Check if all types have been registered/tested
            List<Type> untestedTypes = types.Where(t => !typesTested.Contains(t)).ToList();
            if (untestedTypes.Count > 0)
            {
                throw new Exception(
                    "The following types are not tested: " + string.Join(", ", untestedTypes.Select(t => t.Name)));
            }
        }
    }

    /// <summary>
    /// Generic tests for <see cref="Factory{T}"/>.
    /// </summary>
    /// <typeparam name="T">Abstract type to be tested.</typeparam>
    public abstract class FactoryTests<T>
    {
        /// <summary>
        /// A concrete class of <see cref="T"/>.
        /// </summary>
        protected T testClass;

        [Test]
        public void Create_ReturnsCorrectType()
        {
            // Arrange
            Type testType = testClass.GetType();

            // Act
            T testInstance = Factory<T>.Create(testType);

            // Assert
            Assert.IsNotNull(testInstance);
            Assert.IsInstanceOf(testType, testInstance);
        }

        [Test]
        public void GetAvailableTypes_ReturnsOnlyNonAbstractSubclasses()
        {
            // Act
            Type[] types = Factory<T>.GetAvailableTypes();

            // Assert
            Assert.IsNotEmpty(types);
            Assert.IsTrue(types.All(t => typeof(T).IsAssignableFrom(t) && !t.IsAbstract));
        }
    }
}
