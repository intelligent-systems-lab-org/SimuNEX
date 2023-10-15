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