using NUnit.Framework;
using System;

namespace Eigen3MatrixTests
{
    public class TestProperties
    {
        [Test]
        public void TestMatrixIndexerGetSet()
        {
            Matrix matrix = new(3, 3, new float[9]);
            matrix[0, 0] = 1.0f;
            matrix[1, 1] = 2.0f;
            matrix[2, 2] = 3.0f;

            Assert.AreEqual(1.0f, matrix[0, 0]);
            Assert.AreEqual(2.0f, matrix[1, 1]);
            Assert.AreEqual(3.0f, matrix[2, 2]);
        }

        [Test]
        public void TestMatrixIndexerGetSetInvalidIndices_ThrowsException()
        {
            Matrix matrix = new(2, 2);

            matrix[1, 1] = 2.0f;

            _ = Assert.Throws<ArgumentOutOfRangeException>(() => matrix[-1, 0] = 1.0f);
            _ = Assert.Throws<ArgumentOutOfRangeException>(() => matrix[0, -1] = 2.0f);
            _ = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                float elem = matrix[-1, 0];
            });
            _ = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                float elem = matrix[0, -1];
            });
        }

        [Test]
        public void TestMatrixTransposeProperty()
        {
            Matrix originalMatrix = new(new float[,] {
                { 1, 2, 3 },
                { 4, 5, 6 }
            });
            Matrix transposedMatrix = originalMatrix.Transposed;

            // test counts
            Assert.AreEqual(3, transposedMatrix.RowCount);
            Assert.AreEqual(2, transposedMatrix.ColCount);

            // test positions
            Assert.AreEqual(1, transposedMatrix[0, 0]);
            Assert.AreEqual(4, transposedMatrix[0, 1]);
            Assert.AreEqual(2, transposedMatrix[1, 0]);
            Assert.AreEqual(5, transposedMatrix[1, 1]);
            Assert.AreEqual(3, transposedMatrix[2, 0]);
            Assert.AreEqual(6, transposedMatrix[2, 1]);
        }

        [Test]
        public void TestMatrixInverseProperty()
        {
            Matrix originalMatrix = new(new float[,] {
                { 1, 2 },
                { 3, 4 }
            });
            Matrix inverse = originalMatrix.Inverse;

            // test counts
            Assert.AreEqual(2, inverse.RowCount);
            Assert.AreEqual(2, inverse.ColCount);

            // test positions
            const float tol = 1e-5f;
            Assert.AreEqual(-2.0f, inverse[0, 0], tol);
            Assert.AreEqual(1.0f, inverse[0, 1], tol);
            Assert.AreEqual(1.5f, inverse[1, 0], tol);
            Assert.AreEqual(-0.5f, inverse[1, 1], tol);
        }

        [Test]
        public void TestMatrixInversePropertyNonSquare_ThrowsException()
        {
            Matrix originalMatrix = new(new float[,] {
                { 1, 2, 0 },
                { 3, 4, 1 }
            });

            _ = Assert.Throws<InvalidOperationException>(() =>
            {
                Matrix inverse = originalMatrix.Inverse;
            });
        }

        [Test]
        public void TestMatrixInversePropertySingular_ThrowsException()
        {
            Matrix originalMatrix = new(new float[,] {
                { 1, 2 },
                { 1, 2 }
            });

            _ = Assert.Throws<InvalidOperationException>(() =>
            {
                Matrix inverse = originalMatrix.Inverse;
            });
        }

        [Test]
        public void TestMatrixDeterminantProperty()
        {
            const float tol = 1e-5f;
            Matrix originalMatrix = new(new float[,] {
                { 1, 2 },
                { 3, 4 }
            });

            float determinant = originalMatrix.Determinant;

            Assert.AreEqual(4 - 6, determinant, tol);
        }

        [Test]
        public void TestMatrixDeterminantPropertyNonSquare_ThrowsException()
        {
            Matrix originalMatrix = new(new float[,] {
                { 1, 2, 0 },
                { 3, 4, 1 }
            });

            _ = Assert.Throws<InvalidOperationException>(() =>
            {
                float determinant = originalMatrix.Determinant;
            });
        }
    }
}
