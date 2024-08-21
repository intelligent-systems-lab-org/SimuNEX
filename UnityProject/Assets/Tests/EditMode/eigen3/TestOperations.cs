using NUnit.Framework;
using System;

namespace Eigen3MatrixTests
{
    public class TestOperations
    {
        [Test]
        public void TestMatrixAdd()
        {
            Matrix matrixA = new(new float[,] {
                { 2, 4 },
                { 5, 6 }
            });
            Matrix matrixB = new(new float[,] {
                { 1, 2 },
                { 4, 5 }
            });

            Matrix matrixC = matrixA.Add(matrixB);

            // test counts
            Assert.AreEqual(2, matrixC.RowCount);
            Assert.AreEqual(2, matrixC.ColCount);

            // test positions
            Assert.AreEqual(2 + 1, matrixC[0, 0]);
            Assert.AreEqual(4 + 2, matrixC[0, 1]);
            Assert.AreEqual(5 + 4, matrixC[1, 0]);
            Assert.AreEqual(6 + 5, matrixC[1, 1]);
        }

        [Test]
        public void TestMatrixAdd_DifferentDimensions_ThrowsException()
        {
            // Create two matrices with different dimensions
            Matrix matrixA = new(new float[,] {
                { 2, 4, 7 },
                { 5, 6, 8 }
            });
            Matrix matrixB = new(new float[,] {
                { 1, 2 },
                { 4, 5 }
            });

            // Expecting an InvalidOperationException
            _ = Assert.Throws<InvalidOperationException>(() =>
            {
                Matrix matrixC = matrixA.Add(matrixB);
            });
        }

        [Test]
        public void TestMatrixAddOperator()
        {
            Matrix matrixA = new(new float[,] {
                { 2, 4 },
                { 5, 6 }
            });
            Matrix matrixB = new(new float[,] {
                { 1, 2 },
                { 4, 5 }
            });

            Matrix matrixC = matrixA + matrixB;

            // test counts
            Assert.AreEqual(2, matrixC.RowCount);
            Assert.AreEqual(2, matrixC.ColCount);

            // test positions
            Assert.AreEqual(2 + 1, matrixC[0, 0]);
            Assert.AreEqual(4 + 2, matrixC[0, 1]);
            Assert.AreEqual(5 + 4, matrixC[1, 0]);
            Assert.AreEqual(6 + 5, matrixC[1, 1]);
        }

        [Test]
        public void TestMatrixSubtract()
        {
            Matrix matrixA = new(new float[,] {
                { 2, 4 },
                { 5, 6 }
            });
            Matrix matrixB = new(new float[,] {
                { 1, 2 },
                { 4, 5 }
            });

            Matrix matrixC = matrixA.Subtract(matrixB);

            // test counts
            Assert.AreEqual(2, matrixC.RowCount);
            Assert.AreEqual(2, matrixC.ColCount);

            // test positions
            Assert.AreEqual(2 - 1, matrixC[0, 0]);
            Assert.AreEqual(4 - 2, matrixC[0, 1]);
            Assert.AreEqual(5 - 4, matrixC[1, 0]);
            Assert.AreEqual(6 - 5, matrixC[1, 1]);
        }

        [Test]
        public void TestMatrixSubtract_DifferentDimensions_ThrowsException()
        {
            // Create two matrices with different dimensions
            Matrix matrixA = new(new float[,] {
                { 2, 4, 7 },
                { 5, 6, 8 }
            });
            Matrix matrixB = new(new float[,] {
                { 1, 2 },
                { 4, 5 }
            });

            // Expecting an InvalidOperationException
            _ = Assert.Throws<InvalidOperationException>(() =>
            {
                Matrix matrixC = matrixA.Subtract(matrixB);
            });
        }

        [Test]
        public void TestMatrixSubtractOperator()
        {
            Matrix matrixA = new(new float[,] {
                { 2, 4 },
                { 5, 6 }
            });
            Matrix matrixB = new(new float[,] {
                { 1, 2 },
                { 4, 5 }
            });

            Matrix matrixC = matrixA - matrixB;

            // test counts
            Assert.AreEqual(2, matrixC.RowCount);
            Assert.AreEqual(2, matrixC.ColCount);

            // test positions
            Assert.AreEqual(2 - 1, matrixC[0, 0]);
            Assert.AreEqual(4 - 2, matrixC[0, 1]);
            Assert.AreEqual(5 - 4, matrixC[1, 0]);
            Assert.AreEqual(6 - 5, matrixC[1, 1]);
        }

        [Test]
        public void TestMatrixMultiply()
        {
            Matrix matrixA = new(new float[,] {
                { 2, 4, 3 },
                { 5, 6, 1 }
            });
            Matrix matrixB = new(new float[,] {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            });

            Matrix matrixC = matrixA.Multiply(matrixB);

            // test counts
            Assert.AreEqual(2, matrixC.RowCount);
            Assert.AreEqual(3, matrixC.ColCount);

            // test positions
            Assert.AreEqual((2 * 1) + (4 * 4) + (3 * 7), matrixC[0, 0]);
            Assert.AreEqual((2 * 2) + (4 * 5) + (3 * 8), matrixC[0, 1]);
            Assert.AreEqual((2 * 3) + (4 * 6) + (3 * 9), matrixC[0, 2]);
            Assert.AreEqual((5 * 1) + (6 * 4) + (1 * 7), matrixC[1, 0]);
            Assert.AreEqual((5 * 2) + (6 * 5) + (1 * 8), matrixC[1, 1]);
            Assert.AreEqual((5 * 3) + (6 * 6) + (1 * 9), matrixC[1, 2]);
        }

        [Test]
        public void TestMatrixMultiplyWithIncompatibleDimensions_ThrowsException()
        {
            Matrix matrixA = new(new float[,] {
                { 2, 4, 3 },
                { 5, 6, 1 }
            });
            Matrix matrixB = new(new float[,] {
                { 1, 2 },
                { 3, 4 }
            });

            // Expecting an InvalidOperationException
            _ = Assert.Throws<InvalidOperationException>(() =>
            {
                Matrix matrixC = matrixA.Multiply(matrixB);
            });
        }

        [Test]
        public void TestMatrixMultiplyOperator()
        {
            Matrix matrixA = new(new float[,] {
                { 2, 4, 3 },
                { 5, 6, 1 }
            });
            Matrix matrixB = new(new float[,] {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            });

            Matrix matrixC = matrixA * matrixB;

            // test counts
            Assert.AreEqual(2, matrixC.RowCount);
            Assert.AreEqual(3, matrixC.ColCount);

            // test positions
            Assert.AreEqual((2 * 1) + (4 * 4) + (3 * 7), matrixC[0, 0]);
            Assert.AreEqual((2 * 2) + (4 * 5) + (3 * 8), matrixC[0, 1]);
            Assert.AreEqual((2 * 3) + (4 * 6) + (3 * 9), matrixC[0, 2]);
            Assert.AreEqual((5 * 1) + (6 * 4) + (1 * 7), matrixC[1, 0]);
            Assert.AreEqual((5 * 2) + (6 * 5) + (1 * 8), matrixC[1, 1]);
            Assert.AreEqual((5 * 3) + (6 * 6) + (1 * 9), matrixC[1, 2]);
        }

        [Test]
        public void TestMatrixMultiplyByScalar()
        {
            Matrix matrixA = new(new float[,] {
                { 2, 4 },
                { 5, 6 }
            });

            const float scalar = 3.0f;
            Matrix result = matrixA.Multiply(scalar);

            // test counts
            Assert.AreEqual(2, result.RowCount);
            Assert.AreEqual(2, result.ColCount);

            // test positions
            Assert.AreEqual(2 * 3, result[0, 0]);
            Assert.AreEqual(4 * 3, result[0, 1]);
            Assert.AreEqual(5 * 3, result[1, 0]);
            Assert.AreEqual(6 * 3, result[1, 1]);
        }

        [Test]
        public void TestMatrixMultiplyByRightScalarOperator()
        {
            Matrix matrixA = new(new float[,] {
                { 2, 4 },
                { 5, 6 }
            });

            const float scalar = 3.0f;
            Matrix result = matrixA * scalar;

            // test counts
            Assert.AreEqual(2, result.RowCount);
            Assert.AreEqual(2, result.ColCount);

            // test positions
            Assert.AreEqual(2 * 3, result[0, 0]);
            Assert.AreEqual(4 * 3, result[0, 1]);
            Assert.AreEqual(5 * 3, result[1, 0]);
            Assert.AreEqual(6 * 3, result[1, 1]);
        }

        [Test]
        public void TestMatrixMultiplyByLeftScalarOperator()
        {
            Matrix matrixA = new(new float[,] {
                { 2, 4 },
                { 5, 6 }
            });

            const float scalar = 3.0f;
            Matrix result = scalar * matrixA;

            // test counts
            Assert.AreEqual(2, result.RowCount);
            Assert.AreEqual(2, result.ColCount);

            // test positions
            Assert.AreEqual(2 * 3, result[0, 0]);
            Assert.AreEqual(4 * 3, result[0, 1]);
            Assert.AreEqual(5 * 3, result[1, 0]);
            Assert.AreEqual(6 * 3, result[1, 1]);
        }

        [Test]
        public void TestMatrixTranspose()
        {
            Matrix matrix = new(new float[,] {
                { 1, 2, 3 },
                { 4, 5, 6 }
            });
            matrix.Transpose();

            // test counts
            Assert.AreEqual(3, matrix.RowCount);
            Assert.AreEqual(2, matrix.ColCount);

            // test positions
            Assert.AreEqual(1, matrix[0, 0]);
            Assert.AreEqual(4, matrix[0, 1]);
            Assert.AreEqual(2, matrix[1, 0]);
            Assert.AreEqual(5, matrix[1, 1]);
            Assert.AreEqual(3, matrix[2, 0]);
            Assert.AreEqual(6, matrix[2, 1]);
        }

        [Test]
        public void TestIsEqual_SameData_ReturnsTrue()
        {
            // Arrange
            float[] data = { 1, 2, 3, 4 };
            Matrix matrix1 = new(2, 2, data);
            Matrix matrix2 = new(2, 2, data);

            // Act
            bool result = matrix1.IsEqual(matrix2);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void TestIsEqual_DifferentData_ReturnsFalse()
        {
            // Arrange
            float[] data1 = { 1, 2, 3, 4 };
            float[] data2 = { 5, 6, 7, 8 };
            Matrix matrix1 = new(2, 2, data1);
            Matrix matrix2 = new(2, 2, data2);

            // Act
            bool result = matrix1.IsEqual(matrix2);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void TestIsNotEqual_SameData_ReturnsFalse()
        {
            // Arrange
            float[] data = { 1, 2, 3, 4 };
            Matrix matrix1 = new(2, 2, data);
            Matrix matrix2 = new(2, 2, data);

            // Act
            bool result = matrix1.IsNotEqual(matrix2);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void TestIsNotEqual_DifferentData_ReturnsTrue()
        {
            // Arrange
            float[] data1 = { 1, 2, 3, 4 };
            float[] data2 = { 5, 6, 7, 8 };
            Matrix matrix1 = new(2, 2, data1);
            Matrix matrix2 = new(2, 2, data2);

            // Act
            bool result = matrix1.IsNotEqual(matrix2);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
