using System;

public class Matrix : IDisposable
{
    private IntPtr _matrixPtr;

    public Matrix(int rows, int cols, float[] data)
    {
        _matrixPtr = Eigen3.CreateMatrix(rows, cols, data);
    }

    public Matrix Multiply(Matrix other)
    {
        float[] resultData = new float[RowCount * other.ColCount];
        Eigen3.MultiplyMatrices(_matrixPtr, other._matrixPtr, resultData);
        return new Matrix(RowCount, other.ColCount, resultData);
    }

    public int RowCount => Eigen3.GetRowCount(_matrixPtr);
    public int ColCount => Eigen3.GetColumnCount(_matrixPtr);
    public float this[int row, int col] => Eigen3.GetElement(_matrixPtr, row, col);

    public void Dispose()
    {
        Eigen3.DeleteMatrix(_matrixPtr);
        _matrixPtr = IntPtr.Zero;
    }
}
