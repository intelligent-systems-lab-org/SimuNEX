using System;
using System.Linq;

public partial class Vector6DOF
{
    /// <summary>
    /// Returns a string representation of the vector in the format of a 6-element float vector.
    /// </summary>
    /// <returns>A string representation of the vector.</returns>
    public override string ToString() => $"({linear.x} {linear.y} {linear.z} {angular.x} {angular.y} {angular.z})";

    /// <summary>
    /// Implicitly converts a <see cref="Vector6DOF"/> instance to a <see cref="float[]"/>.
    /// </summary>
    /// <param name="vector">The <see cref="Vector6DOF"/> instance to convert.</param>
    /// <returns>A <see cref="float[]"/> representing the values of the <see cref="Vector6DOF"/>.</returns>
    public static implicit operator float[](Vector6DOF vector)
    {
        var result = new float[6];
        result[0] = vector.linear.x;
        result[1] = vector.linear.y;
        result[2] = vector.linear.z;
        result[3] = vector.angular.x;
        result[4] = vector.angular.y;
        result[5] = vector.angular.z;
        return result;
    }

    /// <summary>
    /// Implicitly converts a <see cref="Vector6DOF"/> instance to a <see cref="Matrix"/>.
    /// </summary>
    /// <param name="vector">The <see cref="Vector6DOF"/> instance to convert.</param>
    /// <returns>A <see cref="Matrix"/> representing the values of the <see cref="Vector6DOF"/>.</returns>
    public static implicit operator Matrix(Vector6DOF vector) => new(6, 1, vector);

    /// <summary>
    /// Converts a string representation of a <see cref="Vector6DOF"/> to a <see cref="Vector6DOF"/> instance.
    /// </summary>
    /// <param name="vectorString">The string representation of the <see cref="Vector6DOF"/>.</param>
    /// <returns>A new <see cref="Vector6DOF"/> instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the vector string does not contain 6 elements.</exception>
    public static implicit operator Vector6DOF(string vectorString)
    {
        var values = vectorString.Trim('[', ']').Split(';');
        if (values.Length != 6)
        {
            throw new InvalidOperationException("Invalid vector string. Expected 6 elements.");
        }

        float[] vectorValues = values.Select(float.Parse).ToArray();
        return new Vector6DOF(vectorValues);
    }
}
