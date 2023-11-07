using NUnit.Framework;
using SimuNEX.Faults;
using System;
using System.Linq;

namespace FaultTests
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

    public class FaultFactoryTests
    {
        [Test]
        public void CreateFault_ReturnsCorrectFaultType()
        {
            // Arrange
            Type faultType = typeof(TestFault);

            // Act
            Fault faultInstance = FaultFactory.CreateFault(faultType);

            // Assert
            Assert.IsNotNull(faultInstance);
            Assert.IsInstanceOf(faultType, faultInstance);
        }

        [Test]
        public void GetAvailableFaultTypes_ReturnsOnlyNonAbstractFaultSubclasses()
        {
            // Act
            Type[] faultTypes = FaultFactory.GetAvailableFaultTypes();

            // Assert
            Assert.IsNotEmpty(faultTypes);
            Assert.IsTrue(faultTypes.All(t => typeof(Fault).IsAssignableFrom(t) && !t.IsAbstract));
        }
    }
}
