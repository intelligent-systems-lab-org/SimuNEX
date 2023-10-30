namespace SimuNEX
{
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
        /// Compares two matrices for equality.
        /// </summary>
        /// <param name="rhs">The matrix being compared.</param>
        /// <returns>True if the matrices are equal, false otherwise.</returns>
        public bool IsEqual(Matrix6DOF rhs) => _matrix.IsEqual(new Matrix(rhs));

        /// <summary>
        /// Compares two matrices if they are not equal.
        /// </summary>
        /// <param name="rhs">The matrix being compared.</param>
        /// <returns>True if the matrices are not equal, false otherwise.</returns>
        public bool IsNotEqual(Matrix rhs) => _matrix.IsNotEqual(new Matrix(rhs));

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

        /// <summary>
        /// Multiplies a <see cref="Matrix6DOF"/> with <see cref="Matrix"/>.
        /// </summary>
        /// <param name="matrix2">The right matrix operand.</param>
        /// <param name="matrix">The <see cref="Matrix6DOF"/>.</param>
        /// <returns>The result of multiplying the <see cref="Matrix6DOF"/> 
        /// with the <see cref="Matrix"/>.</returns>
        public static Matrix operator *(Matrix6DOF matrix, Matrix matrix2) => new Matrix(matrix) * matrix2;

        /// <summary>
        /// Multiplies a <see cref="Matrix"/> with a <see cref="Matrix6DOF"/>.
        /// </summary>
        /// <param name="matrix2">The left matrix operand.</param>
        /// <param name="matrix">The <see cref="Matrix6DOF"/>.</param>
        /// <returns>The result of multiplying the <see cref="Matrix"/> 
        /// with the <see cref="Matrix6DOF"/>.</returns>
        public static Matrix operator *(Matrix matrix2, Matrix6DOF matrix) => matrix2 * new Matrix(matrix);

        /// <summary>
        /// Multiplies a <see cref="Matrix6DOF"/> by a <see cref="Vector6DOF"/>
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="vector">The vector to multiply.</param>
        /// <returns>The result of multiplying the matrix with the vector.</returns>
        public static Vector6DOF operator *(Matrix6DOF matrix, Vector6DOF vector)
            => matrix._matrix * vector;
    }    
}