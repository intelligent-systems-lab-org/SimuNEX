using NUnit.Framework;

namespace Eigen3MatrixTests
{
    public class TestConversions
    {
        [Test]
        public void TestToArrayRowMajorOrder()
        {
            // Initialize a 2x2 matrix with row-major order data
            var matrix = new Matrix(2, 2, new float[] { 1, 2, 3, 4 }, true);

            // Convert the matrix to an array in row-major order
            float[] rowMajorArray = matrix.ToArray(true);

            // Expected row-major order array
            float[] expectedRowMajor = new float[] { 1, 2, 3, 4 };

            // Assert that the array matches the expected values
            for (int i = 0; i < expectedRowMajor.Length; i++)
            {
                Assert.AreEqual(expectedRowMajor[i], rowMajorArray[i]);
            }
        }

        [Test]
        public void TestToArrayColumnMajorOrder()
        {
            // Initialize a 2x2 matrix with row-major order data (just for variety)
            var matrix = new Matrix(2, 2, new float[] { 1, 2, 3, 4 }, true);

            // Convert the matrix to an array in column-major order
            float[] colMajorArray = matrix.ToArray();

            // Expected column-major order array
            float[] expectedColMajor = new float[] { 1, 3, 2, 4 };

            // Assert that the array matches the expected values
            for (int i = 0; i < expectedColMajor.Length; i++)
            {
                Assert.AreEqual(expectedColMajor[i], colMajorArray[i]);
            }
        }

        [Test]
        public void TestMatrixToString()
        {
            var matrix = new Matrix(3, 3, new float[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, true);
            var expectedString = "1\t2\t3\n4\t5\t6\n7\t8\t9";
            Assert.AreEqual(expectedString, matrix.ToString());
        }

        [Test]
        public void TestMatrixTo2DArray_3x3()
        {
            var matrix = new Matrix(3, 3, new float[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            float[,] result = matrix.To2DArray();

            // Check dimensions
            Assert.AreEqual(3, result.GetLength(0));
            Assert.AreEqual(3, result.GetLength(1));

            // Check data
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Assert.AreEqual(matrix[i, j], result[i, j]);
                }
            }
        }

        [Test]
        public void TestMatrixTo2DArray_2x3()
        {
            var matrix = new Matrix(2, 3, new float[] { 1, 2, 3, 4, 5, 6 });
            float[,] result = matrix.To2DArray();

            // Check dimensions
            Assert.AreEqual(2, result.GetLength(0));
            Assert.AreEqual(3, result.GetLength(1));

            // Check data
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Assert.AreEqual(matrix[i, j], result[i, j]);
                }
            }
        }

        [Test]
        public void TestMatrixTo2DArray_3x2()
        {
            var matrix = new Matrix(3, 2, new float[] { 1, 2, 3, 4, 5, 6 });
            float[,] result = matrix.To2DArray();

            // Check dimensions
            Assert.AreEqual(3, result.GetLength(0));
            Assert.AreEqual(2, result.GetLength(1));

            // Check data
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Assert.AreEqual(matrix[i, j], result[i, j]);
                }
            }
        }
    }
}