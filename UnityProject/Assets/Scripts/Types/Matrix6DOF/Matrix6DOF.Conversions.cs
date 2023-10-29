using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Matrix6DOF
{
    /// <summary>
    /// Converts the <see cref="Matrix6DOF"/> to its string representation.
    /// </summary>
    /// <returns>The string representation of the <see cref="Matrix6DOF"/>.</returns>
    public override string ToString() => _matrix.ToString();

    /// <summary>
    /// Implicitly converts a <see cref="Matrix6DOF"/> to a <see cref="Matrix"/>.
    /// </summary>
    /// <param name="m">The <see cref="Matrix6DOF"/> to convert.</param>
    /// <returns>The converted <see cref="Matrix"/>.</returns>
    public static implicit operator Matrix(Matrix6DOF m) => m._matrix;
}
