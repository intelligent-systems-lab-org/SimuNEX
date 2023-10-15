using NUnit.Framework;

namespace Eigen3MatrixTests
{
    public class TestOperations
    {
        [Test]
        public void TestMatrixMultiply()
        {
            var matrixA = new Matrix(new float[,] {
            { 2, 4, 3 },
            { 5, 6, 1}
        }
            );
            var matrixB = new Matrix(new float[,] {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        });

            var matrixC = matrixA.Multiply(matrixB);

            // test counts
            Assert.AreEqual(2, matrixC.RowCount);
            Assert.AreEqual(3, matrixC.ColCount);

            // test positions
            Assert.AreEqual(2 * 1 + 4 * 4 + 3 * 7, matrixC[0, 0]);
            Assert.AreEqual(2 * 2 + 4 * 5 + 3 * 8, matrixC[0, 1]);
            Assert.AreEqual(2 * 3 + 4 * 6 + 3 * 9, matrixC[0, 2]);
            Assert.AreEqual(5 * 1 + 6 * 4 + 1 * 7, matrixC[1, 0]);
            Assert.AreEqual(5 * 2 + 6 * 5 + 1 * 8, matrixC[1, 1]);
            Assert.AreEqual(5 * 3 + 6 * 6 + 1 * 9, matrixC[1, 2]);
        }

        [Test]
        public void TestMatrixTranspose()
        {
            var matrix = new Matrix(new float[,] { 
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

    }
}