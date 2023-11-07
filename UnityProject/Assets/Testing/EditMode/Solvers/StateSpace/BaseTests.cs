using NUnit.Framework;
using SimuNEX;
using System;
using static SimuNEX.ODESolvers;

namespace StateSpaceTests
{
    /// <summary>
    /// Tests the methods specific to the base class.
    /// </summary>
    public class BaseTests
    {
        [Test]
        public void Constructor_ThrowsException_InvalidInitialConditions()
        {
            // Act
            StateSpace ss = new();

            // Assert
            // Invalid rows specified in matrix constructor.
            _ = Assert.Throws<ArgumentException>(() => ss.Initialize(3, 1, new Matrix(1, 1, new float[] { 1 })));

            // Invalid columns specified in matrix constructor.
            _ = Assert.Throws<ArgumentException>(() => ss.Initialize(1, 1, new Matrix(1, 2, new float[] { 1, 2 })));
        }

        [Test]
        public void InputsSetter_ThrowsException_InvalidValueSize()
        {
            // Act
            StateSpace ss = new();

            // Arrange
            ss.Initialize(4, 2, new Matrix(4, 1, new float[4]));

            // Assert
            // Invalid rows.
            _ = Assert.Throws<ArgumentException>(() => ss.inputs = new Matrix(4, 1));

            // Invalid columns.
            _ = Assert.Throws<ArgumentException>(() => ss.inputs = new Matrix(2, 2));
        }

        [Test]
        public void InputsSetter_AssignsCorrectly()
        {
            // Act
            StateSpace ss = new();

            // Arrange
            ss.Initialize(4, 2, new Matrix(4, 1, new float[] { 4, 2, 1, 3 }));
            ss.inputs = new Matrix(2, 1, new float[] { 5, 6 });

            // Assert
            Assert.AreEqual(5, ss.inputs[0, 0]);
            Assert.AreEqual(6, ss.inputs[1, 0]);
        }
    }

    public class StepperCreationTests
    {
        [Test]
        public void CreateStepper_WithEulerMethod_ReturnsForwardEulerSolver()
        {
            ODESolver solver = StateSpace.CreateStepper(StepperMethod.Euler);
            Assert.IsInstanceOf<ForwardEuler>(solver);
        }

        [Test]
        public void CreateStepper_WithHeunMethod_ReturnsHeunSolver()
        {
            ODESolver solver = StateSpace.CreateStepper(StepperMethod.Heun);
            Assert.IsInstanceOf<Heun>(solver);
        }

        [Test]
        public void CreateStepper_WithRK4Method_ReturnsRK4Solver()
        {
            ODESolver solver = StateSpace.CreateStepper(StepperMethod.RK4);
            Assert.IsInstanceOf<RK4>(solver);
        }

        [Test]
        public void CreateStepper_WithUnsupportedMethod_ThrowsArgumentOutOfRangeException()
        {
            const StepperMethod invalidMethod = (StepperMethod)(-1);

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
                StateSpace.CreateStepper(invalidMethod));

            Assert.That(ex.ParamName, Is.EqualTo("stepperMethod"));
            Assert.That(ex.Message, Does.Contain($"Not expected stepper method: {invalidMethod}"));
        }
    }
}
