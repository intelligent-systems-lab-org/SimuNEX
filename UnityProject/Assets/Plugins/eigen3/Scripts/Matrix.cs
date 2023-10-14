using System;
using System.Runtime.InteropServices;

public class Matrix : IDisposable
{
    private IntPtr _matrixPtr;

    [DllImport("EigenWrapper.dll")]
    private static extern IntPtr CreateMatrix(int rows, int cols, float[] data);

    [DllImport("EigenWrapper.dll")]
    private static extern void DeleteMatrix(IntPtr matrixPtr);

    [DllImport("EigenWrapper.dll")]
    private static extern void MultiplyMatrices(IntPtr matrix1, IntPtr matrix2, float[] result);

    [DllImport("EigenWrapper.dll")]
    private static extern int GetRowCount(IntPtr matrixPtr);

    [DllImport("EigenWrapper.dll")]
    private static extern int GetColumnCount(IntPtr matrixPtr);

    [DllImport("EigenWrapper.dll")]
    private static extern float GetElement(IntPtr matrixPtr, int row, int col);

    public Matrix(int rows, int cols, float[] data)
    {
        _matrixPtr = CreateMatrix(rows, cols, data);
    }

    public Matrix Multiply(Matrix other)
    {
        float[] resultData = new float[RowCount * other.ColCount];
        MultiplyMatrices(_matrixPtr, other._matrixPtr, resultData);
        return new Matrix(RowCount, other.ColCount, resultData);
    }

    public int RowCount => GetRowCount(_matrixPtr);
    public int ColCount => GetColumnCount(_matrixPtr);
    public float this[int row, int col] => GetElement(_matrixPtr, row, col);

    public void Dispose()
    {
        DeleteMatrix(_matrixPtr);
        _matrixPtr = IntPtr.Zero;
    }
}
