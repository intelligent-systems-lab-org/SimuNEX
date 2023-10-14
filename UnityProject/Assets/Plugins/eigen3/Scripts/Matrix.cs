using System;
using System.Runtime.InteropServices;

public class EigenMatrix : IDisposable
{
    private IntPtr _matrixPtr;

    [DllImport("EigenWrapper.dll")]
    private static extern IntPtr CreateMatrix(int rows, int cols, double[] data);

    [DllImport("EigenWrapper.dll")]
    private static extern void DeleteMatrix(IntPtr matrixPtr);

    [DllImport("EigenWrapper.dll")]
    private static extern void MultiplyMatrices(IntPtr matrix1, IntPtr matrix2, double[] result);

    [DllImport("EigenWrapper.dll")]
    private static extern int GetRowCount(IntPtr matrixPtr);

    [DllImport("EigenWrapper.dll")]
    private static extern int GetColumnCount(IntPtr matrixPtr);

    [DllImport("EigenWrapper.dll")]
    private static extern float GetElement(IntPtr matrixPtr, int row, int col);

    public EigenMatrix(int rows, int cols, double[] data)
    {
        _matrixPtr = CreateMatrix(rows, cols, data);
    }

    public EigenMatrix Multiply(EigenMatrix other)
    {
        double[] resultData = new double[RowCount * other.ColCount];
        MultiplyMatrices(_matrixPtr, other._matrixPtr, resultData);
        return new EigenMatrix(RowCount, other.ColCount, resultData);
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
