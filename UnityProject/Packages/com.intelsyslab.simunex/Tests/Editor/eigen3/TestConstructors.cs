using NUnit.Framework;
using System;

namespace Eigen3MatrixTests
{
    public class TestConstructors
    {
        [Test]
        public void TestMatrixInitialization()
        {
            Matrix matrix = new(2, 2, new float[] { 1, 2, 3, 4 });

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
        public void TestMatrixInitializationWithNullData()
        {
            // Passing null for data, which defaults to zero matrix
            Matrix matrix = new(2, 2);

            // test counts
            Assert.AreEqual(2, matrix.RowCount);
            Assert.AreEqual(2, matrix.ColCount);

            // test all positions are zeros
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Assert.AreEqual(0, matrix[i, j]);
                }
            }
        }

        [Test]
        public void TestMatrixInitializationWithMismatchedDataSize()
        {
            // Expecting an exception due to mismatched data size
            _ = Assert.Throws<ArgumentException>(() =>
            {
                Matrix matrix = new(3, 3, new float[] { 1, 2, 3, 4 });
            });
        }

        [Test]
        public void TestMatrixInitializationWithZeroRowsOrCols()
        {
            // Expecting an exception due to zero rows
            _ = Assert.Throws<ArgumentException>(() =>
            {
                Matrix matrix = new(0, 2, new float[] { 1, 2 });
            });

            // Expecting an exception due to zero columns
            _ = Assert.Throws<ArgumentException>(() =>
            {
                Matrix matrix = new(2, 0, new float[] { 1, 2 });
            });
        }

        [Test]
        public void TestMatrixInitializationRowMajorOrder()
        {
            Matrix matrix = new(2, 2, new float[] { 1, 2, 3, 4 }, true);

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
            Matrix matrix = new(initData);

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
            Matrix matrix = new(initData);

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
        public void TestMatrixInitializationNullFrom2DArray()
        {
            float[,] initData = null;
            _ = Assert.Throws<ArgumentNullException>(() =>
            {
                Matrix matrix = new(initData);
            });
        }

        [Test]
        public void TestMatrixCopy()
        {
            Matrix originalMatrix = new(2, 2, new float[] { 1, 2, 3, 4 });
            Matrix copiedMatrix = new(originalMatrix);

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
            Matrix matrix = Matrix.Eye(3);

            // test counts
            Assert.AreEqual(3, matrix.RowCount);
            Assert.AreEqual(3, matrix.ColCount);

            // test positions
            for (int i = 0; i < matrix.RowCount; ++i)
            {
                for (int j = 0; j < matrix.ColCount; ++j)
                {
                    if (i == j)
                    {
                        Assert.AreEqual(1, matrix[i, j]);
                    }
                    else
                    {
                        Assert.AreEqual(0, matrix[i, j]);
                    }
                }
            }
        }

        [Test]

        public void TestDiagonal()
        {
            float[] values = new float[] { 1, 2, 3, 4, 5, 6 };
            Matrix matrix = Matrix.CreateDiagonal(values);

            // test counts
            Assert.AreEqual(6, matrix.RowCount);
            Assert.AreEqual(6, matrix.ColCount);

            // test positions
            for (int i = 0; i < matrix.RowCount; ++i)
            {
                for (int j = 0; j < matrix.ColCount; ++j)
                {
                    if (i == j)
                    {
                        Assert.AreEqual(values[i], matrix[i, j]);
                    }
                    else
                    {
                        Assert.AreEqual(0, matrix[i, j]);
                    }
                }
            }
        }

        [Test]
        public void TestMatrixDispose()
        {
            Matrix matrix = new(2, 2, new float[] { 1, 2, 3, 4 });
            matrix.Dispose();
            // Cannot directly test if the matrix is disposed as there is no public indication of the matrix's state post-disposal.
            // However, the test should ensure that calling Dispose() doesn't lead to any unexpected errors.
            // Ideally, you would make further calls on a disposed object throw an exception, which you can then test for.
        }
    }
}
