using System;

/// <summary>
/// Matrix class that utilizes the eigen3 C++ library.
/// </summary>
public partial class Matrix : IDisposable
{
    /// <summary>
    /// Handle to matrix data
    /// </summary>
    private IntPtr _matrixPtr;

    /// <summary>
    /// Private constructor to initialize matrix from a pointer.
    /// </summary>
    /// <param name="ptr">Pointer to the Eigen matrix.</param>
    private Matrix(IntPtr ptr)
    {
        _matrixPtr = ptr;
    }

    /// <summary>
    /// Creates an Eigen matrix from a 1D array.
    /// </summary>
    /// <param name="rows">Number of rows.</param>
    /// <param name="cols">Number of columns.</param>
    /// <param name="data">Input array.</param>
    /// <param name="rowMajor">Assume order in row-major if true. Column-major by default.</param>
    public Matrix(int rows, int cols, float[] data, bool rowMajor = false)
    {
        _matrixPtr = Eigen3.CreateMatrix(rows, cols, data, rowMajor);
    }

    /// <summary>
    /// Creates an Eigen matrix from a 2D array.
    /// </summary>
    /// <param name="data">2D array representing the matrix.</param>
    public Matrix(float[,] data)
    {
        int rows = data.GetLength(0);
        int cols = data.GetLength(1);

        float[] flatData = new float[rows * cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                // Flatten in column-major order
                flatData[j * rows + i] = data[i, j];
            }
        }

        _matrixPtr = Eigen3.CreateMatrix(rows, cols, flatData);
    }

    /// <summary>
    /// Copy constructor for Matrix.
    /// </summary>
    /// <param name="other">Another matrix to copy from.</param>
    public Matrix(Matrix other)
    {
        int rows = other.RowCount;
        int cols = other.ColCount;
        float[] data = other.ToArray();

        _matrixPtr = Eigen3.CreateMatrix(rows, cols, data);
    }

    /// <summary>
    /// Creates an identity matrix of given size.
    /// </summary>
    /// <param name="size">Size of the matrix (number of rows/columns).</param>
    public static Matrix Eye(int size)
    {
        var ptr = Eigen3.CreateIdentityMatrix(size);
        return new Matrix(ptr);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the Matrix and optionally releases the managed resources.
    /// </summary>
    public void Dispose()
    {
        Eigen3.DeleteMatrix(_matrixPtr);
        _matrixPtr = IntPtr.Zero;
    }
}
