using NUnit.Framework;
using SimuNEX.Dynamics;
using System;

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
}
