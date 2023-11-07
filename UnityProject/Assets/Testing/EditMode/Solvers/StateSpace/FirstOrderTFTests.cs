using NUnit.Framework;
using static SimuNEX.StateSpaceTypes;
using static SimuNEX.ODESolvers;

namespace StateSpaceTests
{
    /// <summary>
    /// Tests for the <see cref="FirstOrderTF"/> class.
    /// </summary>
    public class FirstOrderTFTests
    {
        private const float tolerance = 1e-3f;

        [Test]
        public void Constructor_InitializesCorrectly()
        {
            // Arrange
            static float timeConstant()
            {
                return 2f;
            }

            static float dcGain()
            {
                return 5f;
            }

            // Act
            FirstOrderTF firstOrder = new(timeConstant, dcGain, initialState: 1);

            // Assert
            Assert.AreEqual(5f, firstOrder.DCGain());
            Assert.AreEqual(2f, firstOrder.TimeConstant());
            Assert.AreEqual(1f, firstOrder.output);
            Assert.IsInstanceOf<ForwardEuler>(firstOrder.solver);
        }

        [Test]
        public void Constructor_InitializesWithDefaultStepperMethod()
        {
            // Arrange
            static float timeConstant()
            {
                return 1f;
            }

            static float dcGain()
            {
                return 1f;
            }

            // Act
            FirstOrderTF firstOrder = new(timeConstant, dcGain);

            // Assert
            Assert.AreEqual(1f, firstOrder.DCGain());
            Assert.AreEqual(1f, firstOrder.TimeConstant());
            Assert.AreEqual(0f, firstOrder.output);
            Assert.IsInstanceOf<ForwardEuler>(firstOrder.solver);
        }

        [Test]
        public void InputProperty_RespondsToStepChange()
        {
            // Arrange
            static float timeConstant()
            {
                return 2f;
            }

            static float dcGain()
            {
                return 3f;
            }

            FirstOrderTF firstOrder = new(timeConstant, dcGain) { input = 1f };

            // Act
            firstOrder.Compute();

            // Assert
            Assert.AreEqual(1f, firstOrder.input);

            // The expected change in output is based on the time constant, DC gain, and the step input.
            float expectedChange = 1 / timeConstant() * ((dcGain() * 1f) - 0f) * firstOrder.solver.stepSize;
            Assert.AreEqual(expectedChange, firstOrder.output, tolerance);
        }
    }
}
