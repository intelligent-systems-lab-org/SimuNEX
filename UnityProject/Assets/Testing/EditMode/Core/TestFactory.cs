using NUnit.Framework;
using SimuNEX.Faults;
using System;
using System.Linq;
using SimuNEX;

namespace FactoryTests
{
    public abstract class FactoryTests<T>
    {
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

    public class FaultFactoryTests : FactoryTests<Fault>
    {
        /// <summary>
        /// Identity <see cref="Fault"/> class for testing.
        /// </summary>
        public class TestFault : Fault
        {
            public override float FaultFunction(float val)
            {
                return val;
            }
        }

        [SetUp]
        public void SetUp()
        {
            testClass = new TestFault();
        }
    }
}
