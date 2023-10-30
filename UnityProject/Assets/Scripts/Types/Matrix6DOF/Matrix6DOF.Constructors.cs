using System;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Represents a 6 x 6 matrix used in dynamics calculations.
    /// </summary>
    [Serializable]
    public partial class Matrix6DOF
    {
        /// <summary>
        /// A 6x6 matrix.
        /// </summary>
        [SerializeField]
        private Matrix _matrix;

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix6DOF"/> class with an empty 6x6 matrix.
        /// </summary>
        public Matrix6DOF() => _matrix = new Matrix(6, 6);

        /// <summary>
        /// Copy constructor. Initializes a new instance of the <see cref="Matrix6DOF"/> class with the same values as another instance.
        /// </summary>
        /// <param name="other">The <see cref="Matrix6DOF"/> instance to copy from.</param>
        public Matrix6DOF(Matrix6DOF other)
        {
            _matrix = new Matrix(other._matrix);
        }

        /// <summary>
        /// Constructs a mass matrix with the specified mass and inertia tensor.
        /// </summary>
        /// <param name="mass">The mass value.</param>
        /// <param name="inertiaTensor">The inertia tensor.</param>
        /// <returns>A new <see cref="Matrix6DOF"/> with mass and inertia values along the diagonals.</returns>
        public static Matrix6DOF CreateMassMatrix(float mass, Vector3 inertiaTensor)
            => Matrix.CreateDiagonal(mass, mass, mass, 
                inertiaTensor.x, inertiaTensor.y, inertiaTensor.z);

        /// <summary>
        /// Validates the <see cref="Matrix6DOF"/>.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the matrix has an invalid size or is singular.</exception>
        public void Validate6DOFMatrix()
        {
            if (_matrix.RowCount != 6 || _matrix.ColCount != 6)
            {
                throw new ArgumentException("Invalid matrix size. Expected 6x6 matrix.");
            }

            if (_matrix.Determinant == 0)
            {
                throw new ArgumentException("Matrix is singular.");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix6DOF"/> class with the given <see cref="Matrix"/>.
        /// </summary>
        /// <param name="matrix">The matrix to initialize with.</param>
        /// <exception cref="ArgumentException">Thrown if the matrix has an invalid size or is singular.</exception>
        public Matrix6DOF(Matrix matrix)
        {
            _matrix = matrix;
            Validate6DOFMatrix();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix6DOF"/> class from a string representation.
        /// </summary>
        /// <param name="matrixString">A string representation of the matrix.</param>
        public Matrix6DOF(string matrixString)
        {
            Matrix6DOF newVec6 = matrixString;
            _matrix = newVec6;
        }
    }    
}