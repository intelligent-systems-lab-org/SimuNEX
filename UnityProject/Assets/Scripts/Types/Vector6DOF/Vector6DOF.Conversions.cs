using System;
using System.Linq;

namespace SimuNEX
{
    public partial class Vector6DOF
    {
        /// <summary>
        /// Returns a string representation of the vector in the format of a 6-element float vector.
        /// </summary>
        /// <returns>A string representation of the vector.</returns>
        public override string ToString()
        {
            return $"({linear.x} {linear.y} {linear.z} {angular.x} {angular.y} {angular.z})";
        }

        /// <summary>
        /// Implicitly converts a <see cref="Vector6DOF"/> instance to a <see cref="float[]"/>.
        /// </summary>
        /// <param name="vector">The <see cref="Vector6DOF"/> instance to convert.</param>
        /// <returns>A <see cref="float[]"/> representing the values of the <see cref="Vector6DOF"/>.</returns>
        public static implicit operator float[](Vector6DOF vector)
        {
            float[] result = new float[6];
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
        public static implicit operator Matrix(Vector6DOF vector)
        {
            return new(6, 1, vector);
        }

        /// <summary>
        /// Provides an implicit conversion from a Matrix to a Vector6DOF.
        /// </summary>
        /// <param name="matrix">The matrix to be converted.</param>
        /// <returns>A Vector6DOF constructed from the given matrix.</returns>
        /// <exception cref="ArgumentException">Thrown when the matrix is not 1x6 or 6x1.</exception>
        public static implicit operator Vector6DOF(Matrix matrix)
        {
            if (matrix.RowCount == 1 && matrix.ColCount == 6)
            {
                return new Vector6DOF
                (
                    matrix[0, 0],
                    matrix[0, 1],
                    matrix[0, 2],
                    matrix[0, 3],
                    matrix[0, 4],
                    matrix[0, 5]
                );
            }
            else if (matrix.RowCount == 6 && matrix.ColCount == 1)
            {
                return new Vector6DOF
                (
                    matrix[0, 0],
                    matrix[1, 0],
                    matrix[2, 0],
                    matrix[3, 0],
                    matrix[4, 0],
                    matrix[5, 0]
                );
            }
            else
            {
                // Throw an exception if the matrix is not 1x6 or 6x1
                throw new ArgumentException("Matrix must be 1x6 or 6x1 to convert to Vector6DOF.");
            }
        }

        /// <summary>
        /// Converts a string representation of a <see cref="Vector6DOF"/> to a <see cref="Vector6DOF"/> instance.
        /// </summary>
        /// <param name="vectorString">The string representation of the <see cref="Vector6DOF"/>.</param>
        /// <returns>A new <see cref="Vector6DOF"/> instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the vector string does not contain 6 elements.</exception>
        public static implicit operator Vector6DOF(string vectorString)
        {
            string[] values = vectorString.Trim('[', ']').Split(';');
            if (values.Length != 6)
            {
                throw new InvalidOperationException("Invalid vector string. Expected 6 elements.");
            }

            float[] vectorValues = values.Select(float.Parse).ToArray();
            return new Vector6DOF(vectorValues);
        }
    }
}
