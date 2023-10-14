using System.Runtime.InteropServices;
using System;

internal static class Eigen3
{
    private const string EigenWrapperDll = "EigenWrapper.dll";

    [DllImport(EigenWrapperDll)]
    public static extern IntPtr CreateMatrix(int rows, int cols, float[] data);

    [DllImport(EigenWrapperDll)]
    public static extern void DeleteMatrix(IntPtr matrixPtr);

    [DllImport(EigenWrapperDll)]
    public static extern void MultiplyMatrices(IntPtr matrix1, IntPtr matrix2, float[] result);

    [DllImport(EigenWrapperDll)]
    public static extern int GetRowCount(IntPtr matrixPtr);

    [DllImport(EigenWrapperDll)]
    public static extern int GetColumnCount(IntPtr matrixPtr);

    [DllImport(EigenWrapperDll)]
    public static extern float GetElement(IntPtr matrixPtr, int row, int col);
}
