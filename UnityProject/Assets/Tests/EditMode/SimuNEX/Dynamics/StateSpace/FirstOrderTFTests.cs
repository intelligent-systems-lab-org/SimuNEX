using NUnit.Framework;
using SimuNEX.Dynamics;

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
            const float timeConstant = 2f;
            const float dcGain = 5f;

            // Act
            FirstOrderTF firstOrder = new(timeConstant, dcGain, initialState: 1);

            // Assert
            Assert.AreEqual(5f, firstOrder.DCGain);
            Assert.AreEqual(2f, firstOrder.TimeConstant);
            Assert.AreEqual(1f, firstOrder.output);
        }

        [Test]
        public void Constructor_InitializesWithDefaultStepperMethod()
        {
            // Arrange
            const float timeConstant = 1f;
            const float dcGain =  1f;

            // Act
            FirstOrderTF firstOrder = new(timeConstant, dcGain);

            // Assert
            Assert.AreEqual(1f, firstOrder.DCGain);
            Assert.AreEqual(1f, firstOrder.TimeConstant);
            Assert.AreEqual(0f, firstOrder.output);
        }

        [Test]
        public void InputProperty_RespondsToStepChange()
        {
            // Arrange
            const float timeConstant = 2f;
            const  float dcGain = 3f;

            FirstOrderTF firstOrder = new(timeConstant, dcGain) { input = 1f };
            ForwardEuler solver = new();

            // Act
            solver.Step(firstOrder);

            // Assert
            Assert.AreEqual(1f, firstOrder.input);

            // The expected change in output is based on the time constant, DC gain, and the step input.
            float expectedChange = 1 / timeConstant * ((dcGain * 1f) - 0f) * solver.stepSize;
            Assert.AreEqual(expectedChange, firstOrder.output, tolerance);
        }
    }
}
