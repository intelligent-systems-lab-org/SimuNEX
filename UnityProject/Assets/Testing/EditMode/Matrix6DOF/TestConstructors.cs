using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Matrix6DOFTests
{
    public class TestConstructors
    {
        [Test]
        public void Constructor_Default_ShouldInitializeWithZeroValues()
        {
            // Act
            Matrix6DOF m = new();

            // Assert
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Assert.AreEqual(0, m[i, j], "Matrix element at [" + i + ", " + j + "] should be zero.");
                }
            }
        }

        [Test]
        public void Constructor_ValidMatrix_ShouldSucceed()
        {
            // Arrange
            Matrix validMatrix = new(new float[,]
            {
                { 1, 0, 0, 0, 0, 0 },
                { 0, 1, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0 },
                { 0, 0, 0, 0, 1, 0 },
                { 0, 0, 0, 0, 0, 1 }
            });

            // Act & Assert
            Assert.DoesNotThrow(() => new Matrix6DOF(validMatrix));
        }

        [Test]
        public void Constructor_InvalidSizeMatrix_ShouldThrowArgumentException()
        {
            // Arrange
            Matrix invalidSizeMatrix = new(5, 5);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Matrix6DOF(invalidSizeMatrix));
            Assert.That(ex.Message, Is.EqualTo("Invalid matrix size. Expected 6x6 matrix."));
        }

        [Test]
        public void Constructor_SingularMatrix_ShouldThrowArgumentException()
        {
            // Arrange
            // Singular matrix (last row all zeros)
            Matrix singularMatrix = new(new float[,]
            {
                { 1, 0, 0, 0, 0, 0 },
                { 0, 1, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0 },
                { 0, 0, 0, 0, 1, 0 },
                { 0, 0, 0, 0, 0, 0 } 
            });

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Matrix6DOF(singularMatrix));
            Assert.That(ex.Message, Is.EqualTo("Matrix is singular."));
        }

        [Test]
        public void CopyConstructor_ShouldCopyValuesCorrectly()
        {
            // Arrange
            Matrix6DOF original = Matrix6DOF.CreateMassMatrix(10f, new Vector3(1f, 2f, 3f));

            // Act
            Matrix6DOF copy = new(original);

            // Assert
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Assert.AreEqual(original[i, j], copy[i, j], "Matrix values do not match.");
                }
            }
        }

        [Test]
        public void CreateMassMatrix_ValidInputs_ShouldCreateCorrectMatrix()
        {
            // Arrange
            float mass = 10f;
            Vector3 inertiaTensor = new(1f, 2f, 3f);
            float[,] expectedValues = new float[,]
            {
                { mass, 0, 0, 0, 0, 0 },
                { 0, mass, 0, 0, 0, 0 },
                { 0, 0, mass, 0, 0, 0 },
                { 0, 0, 0, inertiaTensor.x, 0, 0 },
                { 0, 0, 0, 0, inertiaTensor.y, 0 },
                { 0, 0, 0, 0, 0, inertiaTensor.z }
            };

            // Act
            Matrix6DOF result = Matrix6DOF.CreateMassMatrix(mass, inertiaTensor);

            // Assert
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Assert.AreEqual(expectedValues[i, j], result[i, j], 
                        "Matrix values do not match expected values.");
                }
            }
        }

        [Test]
        public void Constructor_FromString_ShouldInitializeCorrectly()
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
            Matrix6DOF matrix6DOF = new Matrix6DOF(matrixString);

            // Assert
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Assert.AreEqual(i == j ? i + 1 : 0, matrix6DOF[i, j], 
                        $"Value at position [{i}, {j}] does not match.");
                }
            }
        }

        [Test]
        public void Constructor_FromString_InvalidString_ShouldThrowArgumentException()
        {
            // Arrange
            // Invalid because it's not a 6x6 matrix
            string invalidMatrixString = "[1 0 0; 0 2 0; 0 0 3]";

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _ = new Matrix6DOF(invalidMatrixString);
            }, "Constructor should throw an ArgumentException for an invalid matrix string.");
        }
    }
}