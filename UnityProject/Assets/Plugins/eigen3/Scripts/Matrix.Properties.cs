using System;

public partial class Matrix
{
    /// <summary>
    /// Get number of rows.
    /// </summary>
    public int RowCount => Eigen3.GetRowCount(_matrixPtr);
    /// <summary>
    /// Get number of columns.
    /// </summary>
    public int ColCount => Eigen3.GetColumnCount(_matrixPtr);

    /// <summary>
    /// Gets or sets the matrix element at the specified row and column.
    /// </summary>
    /// <param name="row">The zero-based row index of the element to get or set.</param>
    /// <param name="col">The zero-based column index of the element to get or set.</param>
    /// <returns>The matrix element at the specified row and column.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when a negative index is entered.</exception>
    public float this[int row, int col]
    {
        get
        {
            if (row < 0 || col < 0)
            {
                throw new ArgumentOutOfRangeException("Row and column indices must be non-negative.");
            }
            return Eigen3.GetElement(_matrixPtr, row, col);
        }
        set
        {
            if (row < 0 || col < 0)
            {
                throw new ArgumentOutOfRangeException("Row and column indices must be non-negative.");
            }
            Eigen3.SetElement(_matrixPtr, row, col, value);
        }
    }

    /// <summary>
    /// Returns the transposed matrix.
    /// </summary>
    public Matrix Transposed
    {
        get
        {
            float[] resultData = new float[RowCount * ColCount];
            Eigen3.Transpose(_matrixPtr, resultData);
            return new Matrix(ColCount, RowCount, resultData);
        }
    }

    /// <summary>
    /// Returns the inverse of the matrix.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when matrix is
    /// singular or non-square.</exception>
    public Matrix Inverse
    {
        get
        {
            if (RowCount != ColCount)
            {
                throw new InvalidOperationException("Inverse is undefined for non-square matrices.");
            }

            if (Eigen3.GetDeterminant(_matrixPtr) == 0)
            {
                throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
            }

            float[] resultData = new float[RowCount * ColCount];
            Eigen3.GetInverse(_matrixPtr, resultData);
            return new Matrix(ColCount, RowCount, resultData);
        }
    }

    /// <summary>
    /// Returns the determinant of the matrix.
    /// </summary>
    public float Determinant
    {
        get {
            if (RowCount != ColCount)
            {
                throw new InvalidOperationException("Determinant is undefined for non-square matrices");
            }
            return Eigen3.GetDeterminant(_matrixPtr);
        }
    }
}
