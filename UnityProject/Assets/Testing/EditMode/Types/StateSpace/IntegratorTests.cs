using NUnit.Framework;
using static SimuNEX.StateSpaceTypes;
using static SimuNEX.Steppers;

namespace StateSpaceTests
{
    /// <summary>
    /// Tests the methods specific to <see cref="Integrator"/>.
    /// </summary>
    public class IntegratorTests
    {
        const float tolerance = 1e-3f;

        [Test]
        public void Constructor_InitializesCorrectly()
        {
            // Act and Arrange
            Integrator integrator = new(() => 5f, 2);

            // Assert
            Assert.AreEqual(5f, integrator.Gain());
            Assert.AreEqual(2f, integrator.output);
            Assert.IsInstanceOf<ForwardEuler>(integrator.solver);
        }

        [Test]
        public void Constructor_InitializesDefault()
        {
            // Act and Arrange
            Integrator integrator = new();

            // Assert
            Assert.AreEqual(1f, integrator.Gain());
            Assert.AreEqual(0, integrator.output);
            Assert.IsInstanceOf<ForwardEuler>(integrator.solver);
        }

        [Test]
        public void InputProperty_IntegrationOfConstantValue()
        {
            // Act
            Integrator integrator = new()
            {
                // Arrange
                input = 1f
            };

            // Assert
            Assert.AreEqual(1f, integrator.input);
            integrator.Compute();
            Assert.AreEqual(1f * integrator.solver.stepSize, integrator.output, tolerance);
        }
    }
}
