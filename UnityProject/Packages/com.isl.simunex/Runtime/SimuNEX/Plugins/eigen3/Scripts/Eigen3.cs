using System;
using System.Runtime.InteropServices;

/// <summary>
/// eigen3 wrapper functions.
/// </summary>
internal static class Eigen3
{
    private const string EigenWrapperDll = "EigenWrapper.dll";

    [DllImport(EigenWrapperDll)]
    public static extern IntPtr CreateMatrix(int rows, int cols, float[] data, bool rowMajor = false);

    [DllImport(EigenWrapperDll)]
    public static extern void DeleteMatrix(IntPtr matrixPtr);

    [DllImport(EigenWrapperDll)]
    public static extern void AddMatrices(IntPtr matrix1, IntPtr matrix2, float[] result);

    [DllImport(EigenWrapperDll)]
    public static extern void SubtractMatrices(IntPtr matrix1, IntPtr matrix2, float[] result);

    [DllImport(EigenWrapperDll)]
    public static extern void MultiplyMatrices(IntPtr matrix1, IntPtr matrix2, float[] result);

    [DllImport(EigenWrapperDll)]
    public static extern int GetRowCount(IntPtr matrixPtr);

    [DllImport(EigenWrapperDll)]
    public static extern int GetColumnCount(IntPtr matrixPtr);

    [DllImport(EigenWrapperDll)]
    public static extern float GetElement(IntPtr matrixPtr, int row, int col);

    [DllImport(EigenWrapperDll)]
    public static extern float SetElement(IntPtr matrixPtr, int row, int col, float newEntry);

    [DllImport(EigenWrapperDll)]
    public static extern void Transpose(IntPtr matrixPtr, float[] result);

    [DllImport(EigenWrapperDll)]
    public static extern void TransposeInPlace(IntPtr matrixPtr);

    [DllImport(EigenWrapperDll)]
    public static extern IntPtr CreateIdentityMatrix(int size);

    [DllImport(EigenWrapperDll)]
    public static extern bool AreMatricesEqual(IntPtr matrixPtr1, IntPtr matrixPtr2);

    [DllImport(EigenWrapperDll)]
    public static extern float GetDeterminant(IntPtr matrixPtr);

    [DllImport(EigenWrapperDll)]
    public static extern void GetInverse(IntPtr matrixPtr, float[] result);

    [DllImport(EigenWrapperDll)]
    public static extern void MultiplyByScalar(IntPtr matrixPtr, float scalar, float[] result);

    [DllImport(EigenWrapperDll)]
    public static extern IntPtr CreateDiagonalMatrix(float[] floats, int size);
}
