using System;
using System.Linq;

namespace SimuNEX
{
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

        /// <summary>
        /// Implicitly converts a <see cref="Matrix"/> to a <see cref="Matrix6DOF"/>.
        /// </summary>
        /// <param name="m">The <see cref="Matrix"/> to convert.</param>
        /// <returns>The converted <see cref="Matrix6DOF"/>.</returns>
        public static implicit operator Matrix6DOF(Matrix m) => new(m);

        /// <summary>
        /// Implicitly converts a matrix string representation to a <see cref="Matrix6DOF"/>.
        /// </summary>
        /// <param name="matrixString">The matrix string representation to convert.</param>
        /// <returns>The converted <see cref="Matrix6DOF"/>.</returns>
        /// <exception cref="ArgumentException">Thrown if the matrix string is invalid.</exception>
        public static implicit operator Matrix6DOF(string matrixString)
        {
            Matrix6DOF matrix = new();
            string[] rows = matrixString.Trim('[', ']').Split(';');
            if (rows.Length != 6)
            {
                throw new ArgumentException("Invalid matrix string. Expected 6 rows.");
            }

            for (int i = 0; i < 6; i++)
            {
                string[] values = rows[i].Trim().Split(' ');
                if (values.Length != 6)
                {
                    throw new ArgumentException("Invalid matrix string. Expected 6 columns in each row.");
                }

                float[] rowValues = values.Select(float.Parse).ToArray();
                matrix.SetRow(i, rowValues);
            }

            return matrix;
        }
    }
}
