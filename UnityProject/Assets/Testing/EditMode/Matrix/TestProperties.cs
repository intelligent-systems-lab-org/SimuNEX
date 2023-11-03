using NUnit.Framework;
using System;

namespace Eigen3MatrixTests
{
    public class TestProperties
    {
        [Test]
        public void TestMatrixIndexerGetSet()
        {
            var matrix = new Matrix(3, 3, new float[9]);
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
            var matrix = new Matrix(2, 2);
            
            matrix[1, 1] = 2.0f;

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                matrix[-1, 0] = 1.0f;
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                matrix[0, -1] = 2.0f;
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var elem = matrix[-1, 0];
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var elem = matrix[0, -1];
            });
        }

        [Test]
        public void TestMatrixTransposeProperty()
        {
            var originalMatrix = new Matrix(new float[,] {
                { 1, 2, 3 },
                { 4, 5, 6 }
            });
            var transposedMatrix = originalMatrix.Transposed;

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
            var originalMatrix = new Matrix(new float[,] {
                { 1, 2 },
                { 3, 4 }
            });
            var inverse = originalMatrix.Inverse;

            // test counts
            Assert.AreEqual(2, inverse.RowCount);
            Assert.AreEqual(2, inverse.ColCount);

            // test positions
            float tol = 1e-5f;
            Assert.AreEqual(-2.0f, inverse[0, 0], tol);
            Assert.AreEqual(1.0f, inverse[0, 1], tol);
            Assert.AreEqual(1.5f, inverse[1, 0], tol);
            Assert.AreEqual(-0.5f, inverse[1, 1], tol);
        }

        [Test]
        public void TestMatrixInversePropertyNonSquare_ThrowsException()
        {
            var originalMatrix = new Matrix(new float[,] {
                { 1, 2, 0 },
                { 3, 4, 1 }
            });
            
            Assert.Throws<InvalidOperationException>(() =>
            {
                var inverse = originalMatrix.Inverse;
            });
        }

        [Test]
        public void TestMatrixInversePropertySingular_ThrowsException()
        {
            var originalMatrix = new Matrix(new float[,] {
                { 1, 2 },
                { 1, 2 }
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                var inverse = originalMatrix.Inverse;
            });
        }

        [Test]
        public void TestMatrixDeterminantProperty()
        {
            float tol = 1e-5f;
            var originalMatrix = new Matrix(new float[,] {
                { 1, 2 },
                { 3, 4 }
            });

            float determinant = originalMatrix.Determinant;

            Assert.AreEqual(4 - 6, determinant, tol);
        }

        [Test]
        public void TestMatrixDeterminantPropertyNonSquare_ThrowsException()
        {
            var originalMatrix = new Matrix(new float[,] {
                { 1, 2, 0 },
                { 3, 4, 1 }
            });
            
            Assert.Throws<InvalidOperationException>(() =>
            {
                var determinant = originalMatrix.Determinant;
            });
        }
    }
}