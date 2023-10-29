using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Matrix6DOF
{
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
