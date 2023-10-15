using NUnit.Framework;

namespace Eigen3MatrixTests
{
    public class TestProperties
    {
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
    }
}