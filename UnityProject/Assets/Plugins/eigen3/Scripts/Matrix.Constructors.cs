using System;

/// <summary>
/// Matrix class that utilizes the eigen3 C++ library.
/// </summary>
[Serializable]
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
    private Matrix(in IntPtr ptr)
    {
        _matrixPtr = ptr;
    }

    /// <summary>
    /// Creates an Eigen matrix from a 1D array.
    /// </summary>
    /// <param name="rows">Number of rows.</param>
    /// <param name="cols">Number of columns.</param>
    /// <param name="data">Input array.
    /// If not given, creates a zero matrix by the specified size.</param>
    /// <param name="rowMajor">Assume order in row-major if true. Column-major by default.</param>
    /// <exception cref="ArgumentException">If rows or cols is zero,
    /// or the length of the data array is not equal to rows * cols.</exception>
    public Matrix(int rows, int cols, float[] data = null, bool rowMajor = false)
    {
        if (rows <= 0 || cols <= 0)
        {
            throw new ArgumentException("Number of rows and columns must be greater than 0.");
        }

        data ??= new float[rows * cols];

        if (rows * cols != data.Length)
        {
            throw new ArgumentException($"The provided data array has a length of {data.Length}, " +
                $"but it should be {rows * cols} based on the specified number of rows and columns.");
        }

        _matrixPtr = Eigen3.CreateMatrix(rows, cols, data, rowMajor);
    }

    /// <summary>
    /// Creates an Eigen matrix from a 2D array.
    /// </summary>
    /// <param name="data">2D array representing the matrix.</param>
    public Matrix(float[,] data)
    {
        if (data == null)
        {
            throw new ArgumentNullException(nameof(data), "Input 2D array cannot be null.");
        }

        int rows = data.GetLength(0);
        int cols = data.GetLength(1);

        float[] flatData = new float[rows * cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                // Flatten in column-major order
                flatData[(j * rows) + i] = data[i, j];
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
    /// <returns>An identity matrix of the given size.</returns>
    public static Matrix Eye(int size)
    {
        IntPtr ptr = Eigen3.CreateIdentityMatrix(size);
        return new(ptr);
    }

    /// <summary>
    /// Creates a matrix with given entries along the diagonals.
    /// </summary>
    /// <param name="values">Values along the diagonals of the matrix.</param>
    /// <returns>A diagonal matrix with the given values.</returns>
    public static Matrix CreateDiagonal(params float[] values)
    {
        IntPtr ptr = Eigen3.CreateDiagonalMatrix(values, values.Length);
        return new(ptr);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="Matrix"/> and optionally releases the managed resources.
    /// </summary>
    public void Dispose()
    {
        Eigen3.DeleteMatrix(_matrixPtr);
        _matrixPtr = IntPtr.Zero;
    }
}
