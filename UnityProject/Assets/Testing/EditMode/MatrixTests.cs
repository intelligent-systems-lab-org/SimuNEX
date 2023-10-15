using NUnit.Framework;

public class MatrixTests
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
    public void TestMatrixMultiply()
    {
        var matrixA = new Matrix(2, 3, new float[] { 2, 4, 3, 5, 6, 1 });
        var matrixB = new Matrix(3, 3, new float[] { 1, 4, 7, 2, 5, 8, 3, 6, 9 });

        var matrixC = matrixA.Multiply(matrixB);

        // test counts
        Assert.AreEqual(2, matrixC.RowCount);
        Assert.AreEqual(3, matrixC.ColCount);

        // test positions
        Assert.AreEqual(2 * 1 + 3 * 4 + 6 * 7, matrixC[0, 0]);
        Assert.AreEqual(2 * 2 + 3 * 5 + 6 * 8, matrixC[0, 1]);
        Assert.AreEqual(2 * 3 + 3 * 6 + 6 * 9, matrixC[0, 2]);
        Assert.AreEqual(4 * 1 + 5 * 4 + 1 * 7, matrixC[1, 0]);
        Assert.AreEqual(4 * 2 + 5 * 5 + 1 * 8, matrixC[1, 1]);
        Assert.AreEqual(4 * 3 + 5 * 6 + 1 * 9, matrixC[1, 2]);
    }

    [Test]
    public void TestMatrixToString()
    {
        var matrix = new Matrix(2, 2, new float[] { 1, 2, 3, 4 });
        var expectedString = "1\t3\n2\t4";
        Assert.AreEqual(expectedString, matrix.ToString());
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