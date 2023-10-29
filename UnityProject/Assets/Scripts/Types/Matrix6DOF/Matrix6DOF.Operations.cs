using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Matrix6DOF
{
    /// <summary>
    /// Sets the values of a specific row in the matrix.
    /// </summary>
    /// <param name="rowIndex">The index of the row to set.</param>
    /// <param name="values">The values to set for the row.</param>
    public void SetRow(int rowIndex, params float[] values)
    {
        for (int j = 0; j < 6; j++)
        {
            _matrix[rowIndex, j] = values[j];
        }
    }

    /// <summary>
    /// Sets the values of a specific column in the matrix.
    /// </summary>
    /// <param name="columnIndex">The index of the column to set.</param>
    /// <param name="values">The values to set for the column.</param>
    public void SetColumn(int columnIndex, params float[] values)
    {
        for (int i = 0; i < 6; i++)
        {
            _matrix[i, columnIndex] = values[i];
        }
    }

    /// <summary>
    /// Adds two Matrix6DOF objects together.
    /// </summary>
    /// <param name="matrix1">The first Matrix6DOF.</param>
    /// <param name="matrix2">The second Matrix6DOF.</param>
    /// <returns>A new Matrix6DOF representing the sum of the two matrices.</returns>
    public static Matrix6DOF operator +(Matrix6DOF matrix1, Matrix6DOF matrix2)
    {
        Matrix sum = matrix1._matrix + matrix2._matrix;
        return new Matrix6DOF { _matrix = sum };
    }

    /// <summary>
    /// Subtracts one Matrix6DOF from another.
    /// </summary>
    /// <param name="matrix1">The first Matrix6DOF.</param>
    /// <param name="matrix2">The second Matrix6DOF.</param>
    /// <returns>A new Matrix6DOF representing the difference of the two matrices.</returns>
    public static Matrix6DOF operator -(Matrix6DOF matrix1, Matrix6DOF matrix2)
    {
        Matrix difference = matrix1._matrix - matrix2._matrix;
        return new Matrix6DOF { _matrix = difference };
    }
}
