using NUnit.Framework;
using SimuNEX;
using System;
using UnityEngine;

namespace Matrix6DOFTests
{
    public class TestConversions
    {
        [Test]
        public void ToString_ShouldReturnCorrectStringRepresentation()
        {
            // Arrange
            Matrix6DOF matrix6DOF = new();
            string expectedStringRepresentation = matrix6DOF.ToString();

            // Act
            string actualStringRepresentation = matrix6DOF.ToString();

            // Assert
            Assert.AreEqual(expectedStringRepresentation, actualStringRepresentation,
                "The string representation does not match.");
        }

        [Test]
        public void ImplicitConversion_ToMatrix_ShouldConvertCorrectly()
        {
            // Arrange
            Matrix6DOF matrix6DOF = Matrix6DOF.CreateMassMatrix(10f, new Vector3(1f, 2f, 3f));

            // Act
            Matrix convertedMatrix = matrix6DOF; // Implicit conversion

            // Assert
            Assert.AreEqual(matrix6DOF.ToString(), convertedMatrix.ToString(),
                "Conversion to Matrix did not maintain values.");
        }

        [Test]
        public void ImplicitConversion_FromMatrix_ShouldConvertCorrectly()
        {
            // Arrange
            Matrix matrix = new(new float[,]
            {
                { 1, 0, 0, 0, 0, 0 },
                { 0, 2, 0, 0, 0, 0 },
                { 0, 0, 3, 0, 0, 0 },
                { 0, 0, 0, 4, 0, 0 },
                { 0, 0, 0, 0, 5, 0 },
                { 0, 0, 0, 0, 0, 6 }
            });

            // Act
            // Implicit conversion
            Matrix6DOF convertedMatrix6DOF = matrix;

            // Assert
            Assert.AreEqual(matrix.ToString(), convertedMatrix6DOF.ToString(),
                "Conversion from Matrix did not maintain values.");
        }

        [Test]
        public void ImplicitConversion_FromString_ShouldConvertCorrectly()
        {
            // Arrange
            string matrixString =
               "[1 0 0 0 0 0; " +
                   "0 2 0 0 0 0; " +
                   "0 0 3 0 0 0; " +
                   "0 0 0 4 0 0; " +
                   "0 0 0 0 5 0; " +
                   "0 0 0 0 0 6]";

            // Act
            // Implicit conversion
            Matrix6DOF convertedMatrix6DOF = matrixString;

            // Assert
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Assert.AreEqual(i == j ? i + 1 : 0, convertedMatrix6DOF[i, j],
                        $"Value at position [{i}, {j}] does not match.");
                }
            }
        }

        [Test]
        public void ImplicitConversion_FromString_InvalidString_ShouldThrowArgumentException()
        {
            // Arrange
            // Invalid because it's not a 6x6 matrix
            string invalidMatrixString = "[1 0 0; 0 2 0; 0 0 3]";

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                Matrix6DOF _ = invalidMatrixString;
            }, "Conversion should throw an ArgumentException for an invalid matrix string.");
        }

        [Test]
        public void ImplicitConversion_FromString_InvalidColumns_ShouldThrowArgumentException()
        {
            // Arrange
            // Invalid because it's not a 6x6 matrix
            string invalidMatrixString =
                "[1 0 0 0 0; " +
                    "0 2 0 0 0; " +
                    "0 0 3 0 0; " +
                    "0 0 0 4 0; " +
                    "0 0 0 0 5; " +
                    "0 0 0 0 0]";

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                Matrix6DOF _ = invalidMatrixString;
            }, "Conversion should throw an ArgumentException for an invalid matrix string.");
        }
    }
}
