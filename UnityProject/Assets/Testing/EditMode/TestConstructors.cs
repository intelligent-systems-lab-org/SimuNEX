using NUnit.Framework;

namespace Eigen3MatrixTests
{
    public class TestConstructors
    {
        [Test]
        public void TestMatrixInitialization()
        {
            var matrix = new Matrix(2, 2, new float[] { 1, 2, 3, 4 });

            // test counts
            Assert.AreEqual(2, matrix.RowCount);
            Assert.AreEqual(2, matrix.ColCount);

            // test positions
            Assert.AreEqual(1, matrix[0, 0]);
            Assert.AreEqual(2, matrix[1, 0]);
            Assert.AreEqual(3, matrix[0, 1]);
            Assert.AreEqual(4, matrix[1, 1]);
        }

        [Test]
        public void TestMatrixInitializationRowMajorOrder()
        {
            var matrix = new Matrix(2, 2, new float[] { 1, 2, 3, 4 }, true);

            // test counts
            Assert.AreEqual(2, matrix.RowCount);
            Assert.AreEqual(2, matrix.ColCount);

            // test positions
            Assert.AreEqual(1, matrix[0, 0]);
            Assert.AreEqual(2, matrix[0, 1]);
            Assert.AreEqual(3, matrix[1, 0]);
            Assert.AreEqual(4, matrix[1, 1]);
        }

        [Test]
        public void TestMatrixInitialization3x3From2DArray()
        {
            // Initialize a 3x3 matrix using a 2D array
            float[,] initData = {
        { 1, 2, 3 },
        { 4, 5, 6 },
        { 7, 8, 9 }
    };
            var matrix = new Matrix(initData);

            // Assert counts
            Assert.AreEqual(3, matrix.RowCount);
            Assert.AreEqual(3, matrix.ColCount);

            // Assert positions
            Assert.AreEqual(1, matrix[0, 0]);
            Assert.AreEqual(2, matrix[0, 1]);
            Assert.AreEqual(3, matrix[0, 2]);
            Assert.AreEqual(4, matrix[1, 0]);
            Assert.AreEqual(5, matrix[1, 1]);
            Assert.AreEqual(6, matrix[1, 2]);
            Assert.AreEqual(7, matrix[2, 0]);
            Assert.AreEqual(8, matrix[2, 1]);
            Assert.AreEqual(9, matrix[2, 2]);
        }

        [Test]
        public void TestMatrixInitialization2x3From2DArray()
        {
            // Initialize a 2x3 matrix using a 2D array
            float[,] initData = {
            { 1, 2, 3 },
            { 4, 5, 6 }
        };
            var matrix = new Matrix(initData);

            // Assert counts
            Assert.AreEqual(2, matrix.RowCount);
            Assert.AreEqual(3, matrix.ColCount);

            // Assert positions
            Assert.AreEqual(1, matrix[0, 0]);
            Assert.AreEqual(2, matrix[0, 1]);
            Assert.AreEqual(3, matrix[0, 2]);
            Assert.AreEqual(4, matrix[1, 0]);
            Assert.AreEqual(5, matrix[1, 1]);
            Assert.AreEqual(6, matrix[1, 2]);
        }

        [Test]
        public void TestMatrixCopy()
        {
            var originalMatrix = new Matrix(2, 2, new float[] { 1, 2, 3, 4 });
            var copiedMatrix = new Matrix(originalMatrix);

            // test counts
            Assert.AreEqual(2, copiedMatrix.RowCount);
            Assert.AreEqual(2, copiedMatrix.ColCount);

            // test data
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Assert.AreEqual(originalMatrix[i, j], copiedMatrix[i, j]);
                }
            }
        }

        [Test]
        public void TestIdentity()
        {
            var matrix = Matrix.Eye(3);

            // test counts
            Assert.AreEqual(3, matrix.RowCount);
            Assert.AreEqual(3, matrix.ColCount);

            // test positions
            Assert.AreEqual(1, matrix[0, 0]);
            Assert.AreEqual(0, matrix[0, 1]);
            Assert.AreEqual(0, matrix[0, 2]);
            Assert.AreEqual(0, matrix[1, 0]);
            Assert.AreEqual(1, matrix[1, 1]);
            Assert.AreEqual(0, matrix[1, 2]);
            Assert.AreEqual(0, matrix[2, 0]);
            Assert.AreEqual(0, matrix[2, 1]);
            Assert.AreEqual(1, matrix[2, 2]);
        }

        [Test]
        public void TestMatrixDispose()
        {
            var matrix = new Matrix(2, 2, new float[] { 1, 2, 3, 4 });
            matrix.Dispose();
            // Cannot directly test if the matrix is disposed as there is no public indication of the matrix's state post-disposal.
            // However, the test should ensure that calling Dispose() doesn't lead to any unexpected errors.
            // Ideally, you would make further calls on a disposed object throw an exception, which you can then test for.
        }
    }
}