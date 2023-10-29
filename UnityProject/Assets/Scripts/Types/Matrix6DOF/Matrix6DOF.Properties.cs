using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Matrix6DOF
{
    /// <summary>
    /// Gets or sets the value at the specified row and column of the mass matrix.
    /// </summary>
    /// <param name="i">The row index.</param>
    /// <param name="j">The column index.</param>
    /// <returns>The value at the specified row and column.</returns>
    public float this[int i, int j]
    {
        get => _matrix[i, j];
        set => _matrix[i, j] = value;
    }
}
