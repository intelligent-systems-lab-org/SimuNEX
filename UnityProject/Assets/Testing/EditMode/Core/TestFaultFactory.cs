using NUnit.Framework;
using SimuNEX;
using System;
using System.Linq;

namespace FaultTests
{
    public class FaultFactoryTests
    {
        private class TestFault : Fault
        {
            public override float FaultFunction(float val)
            {
                return val;
            }
        }

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
