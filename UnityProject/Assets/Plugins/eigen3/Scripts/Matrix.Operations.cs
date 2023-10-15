public partial class Matrix
{
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
    /// Add the matrix by other.
    /// </summary>
    /// <param name="other">The right operand.</param>
    /// <returns>Matrix sum of this + other.</returns>
    public Matrix Add(Matrix other)
    {
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
    public Matrix Subtract(Matrix other)
    {
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
}