using System;

public partial class Matrix
{
    /// <summary>
    /// Multiplies the matrix by other.
    /// </summary>
    /// <param name="other">The right operand.</param>
    /// <returns>Matrix product of this * other.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the matrix dimensions 
    /// are incompatible for multiplication.</exception>
    public Matrix Multiply(Matrix other)
    {
        if (ColCount != other.RowCount)
        {
            throw new InvalidOperationException(@"The number of rows of the second matrix 
                must match the number of columns of the first matrix.");
        }

        float[] resultData = new float[RowCount * other.ColCount];
        Eigen3.MultiplyMatrices(_matrixPtr, other._matrixPtr, resultData);
        return new Matrix(RowCount, other.ColCount, resultData);
    }

    /// <summary>
    /// Multiplies the matrix by a scalar.
    /// </summary>
    /// <param name="scalar">The scalar to multiply by.</param>
    /// <returns>Matrix product of this * scalar.</returns>
    public Matrix Multiply(float scalar)
    {
        int rows = RowCount;
        int cols = ColCount;
        float[] resultData = new float[rows * cols];
        Eigen3.MultiplyByScalar(_matrixPtr, scalar, resultData);
        return new Matrix(rows, cols, resultData);
    }

    /// <summary>
    /// Multiply by a scalar.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <param name="scalar">The scalar to multiply by.</param>
    /// <returns>Matrix product of matrix * scalar.</returns>
    public static Matrix operator *(Matrix matrix, float scalar) => matrix.Multiply(scalar);

    /// <summary>
    /// Multiply scalar by matrix.
    /// </summary>
    /// <param name="scalar">The scalar to multiply by.</param>
    /// <param name="matrix">The matrix.</param>
    /// <returns>Matrix product of scalar * matrix.</returns>
    public static Matrix operator *(float scalar, Matrix matrix) => matrix.Multiply(scalar);

    /// <summary>
    /// Add the matrix by other.
    /// </summary>
    /// <param name="other">The right operand.</param>
    /// <returns>Matrix sum of this + other.</returns>
    /// <exception cref="InvalidOperationException">Thrown when matrix dimensions do not match.</exception>
    public Matrix Add(Matrix other)
    {
        if (RowCount != other.RowCount || ColCount != other.ColCount)
        {
            throw new InvalidOperationException("The dimensions of both matrices must match.");
        }

        float[] resultData = new float[RowCount * ColCount];
        Eigen3.AddMatrices(_matrixPtr, other._matrixPtr, resultData);
        return new Matrix(RowCount, ColCount, resultData);
    }

    /// <summary>
    /// Performs matrix addition.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>Matrix sum of left + right.</returns>
    public static Matrix operator +(Matrix left, Matrix right) => left.Add(right);

    /// <summary>
    /// Subtract the matrix by other.
    /// </summary>
    /// <param name="other">The right operand.</param>
    /// <returns>Matrix difference of this - other.</returns>
    /// <exception cref="InvalidOperationException">Thrown when matrix dimensions do not match.</exception>
    public Matrix Subtract(Matrix other)
    {
        if (RowCount != other.RowCount || ColCount != other.ColCount)
        {
            throw new InvalidOperationException("The dimensions of both matrices must match.");
        }

        float[] resultData = new float[RowCount * ColCount];
        Eigen3.SubtractMatrices(_matrixPtr, other._matrixPtr, resultData);
        return new Matrix(RowCount, ColCount, resultData);
    }

    /// <summary>
    /// Performs matrix subtraction
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>Matrix difference of left - right.</returns>
    public static Matrix operator -(Matrix left, Matrix right) => left.Subtract(right);

    /// <summary>
    /// Performs matrix multiplication.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>Matrix product of left * right.</returns>
    public static Matrix operator *(Matrix left, Matrix right) => left.Multiply(right);

    /// <summary>
    /// Transposes the matrix in place.
    /// </summary>
    public void Transpose() => Eigen3.TransposeInPlace(_matrixPtr);

    /// <summary>
    /// Determines if the current matrix is equal to the provided matrix.
    /// </summary>
    /// <param name="other">Another matrix to compare with.</param>
    /// <returns>true if the matrices are equal; otherwise, false.</returns>
    public bool IsEqual(Matrix other) => Eigen3.AreMatricesEqual(_matrixPtr, other._matrixPtr);

    /// <summary>
    /// Determines if the current matrix is not equal to the provided matrix.
    /// </summary>
    /// <param name="other">Another matrix to compare with.</param>
    /// <returns>true if the matrices are not equal; otherwise, false.</returns>
    public bool IsNotEqual(Matrix other) => !IsEqual(other);
}