using NUnit.Framework;
using SimuNEX.Dynamics;

namespace StateSpaceTests
{
    /// <summary>
    /// Tests the methods specific to <see cref="Integrator"/>.
    /// </summary>
    public class IntegratorTests
    {
        private const float tolerance = 1e-3f;

        [Test]
        public void Constructor_InitializesCorrectly()
        {
            // Act and Arrange
            Integrator integrator = new(5f, 2);

            // Assert
            Assert.AreEqual(5f, integrator.Gain);
            Assert.AreEqual(2f, integrator.output);
        }

        [Test]
        public void Constructor_InitializesDefault()
        {
            // Act and Arrange
            Integrator integrator = new();

            // Assert
            Assert.AreEqual(1f, integrator.Gain);
            Assert.AreEqual(0, integrator.output);
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
            ForwardEuler solver = new();

            // Assert
            Assert.AreEqual(1f, integrator.input);
            solver.Step(integrator);
            Assert.AreEqual(1f * solver.stepSize, integrator.output, tolerance);
        }
    }
}
