using System;
using System.Text;

public class Matrix : IDisposable
{
    /// <summary>
    /// Handle to matrix data
    /// </summary>
    private IntPtr _matrixPtr;

    /// <summary>
    /// Creates an Eigen matrix from a 2D array.
    /// </summary>
    /// <param name="data">2D array representing the matrix.</param>
    public Matrix(int rows, int cols, float[] data, bool rowMajor = false)
    {
        _matrixPtr = Eigen3.CreateMatrix(rows, cols, data, rowMajor);
    }

    /// <summary>
    /// Converts the matrix to a flat array.
    /// </summary>
    /// <param name="rowMajor">If true, the output array will be in row-major order; otherwise, it will be in column-major order.</param>
    /// <returns>A flat array representation of the matrix.</returns>
    public float[] ToArray(bool rowMajor = false)
    {
        float[] data = new float[RowCount * ColCount];
        if (rowMajor)
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
                {
                    data[i * ColCount + j] = this[i, j];
                }
            }
        }
        else
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColCount; j++)
                {
                    data[j * RowCount + i] = this[i, j];
                }
            }
        }
        return data;
    }

    /// <summary>
    /// Multiplies the matrix by other.
    /// </summary>
    /// <param name="other">The right operand.</param>
    /// <returns>Matrix product of this * other.</returns>
    public Matrix Multiply(Matrix other)
    {
        float[] resultData = new float[RowCount * other.ColCount];
        Eigen3.MultiplyMatrices(_matrixPtr, other._matrixPtr, resultData);
        return new Matrix(RowCount, other.ColCount, resultData);
    }

    /// <summary>
    /// Performs matrix multiplication.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>Matrix product of left * right.</returns>
    public static Matrix operator *(Matrix left, Matrix right) => left.Multiply(right);

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
    public float this[int row, int col]
    {
        get => Eigen3.GetElement(_matrixPtr, row, col);
        set => Eigen3.SetElement(_matrixPtr, row, col, value);
    }

    /// <summary>
    /// Returns a formatted string of this matrix.
    /// </summary>
    public override string ToString()
    {
        StringBuilder sb = new();
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColCount; j++)
            {
                sb.Append(this[i, j]);
                if (j < ColCount - 1)
                {
                    sb.Append("\t");
                }
            }
            if (i < RowCount - 1)
            {
                sb.Append("\n");
            }
        }
        return sb.ToString();
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
            return new Matrix(RowCount, ColCount, resultData);
        }
    }

    /// <summary>
    /// Transposes the matrix in place.
    /// </summary>
    public void Transpose() => Eigen3.TransposeInPlace(_matrixPtr);

    /// <summary>
    /// Releases the unmanaged resources used by the Matrix and optionally releases the managed resources.
    /// </summary>
    public void Dispose()
    {
        Eigen3.DeleteMatrix(_matrixPtr);
        _matrixPtr = IntPtr.Zero;
    }
}
