using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Mathematics;

namespace SimuNEX
{
    /// <summary>
    /// Represents a 6-degree-of-freedom vector in 3D space.
    /// </summary>
    [Serializable]
    public partial struct Vector6DOF
    {
        /// <summary>
        /// Gets or sets the linear component of the vector.
        /// </summary>
        public float3 linear;

        /// <summary>
        /// Gets or sets the angular component of the vector.
        /// </summary>
        public float3 angular;

        /// <summary>
        /// Initializes a new instance of the Vector6DOF class with the specified linear and angular components.
        /// </summary>
        /// <param name="u">The linear component along the x-axis.</param>
        /// <param name="v">The linear component along the y-axis.</param>
        /// <param name="w">The linear component along the z-axis.</param>
        /// <param name="p">The angular component around the x-axis.</param>
        /// <param name="q">The angular component around the y-axis.</param>
        /// <param name="r">The angular component around the z-axis.</param>
        public Vector6DOF(float u = 0, float v = 0, float w = 0, float p = 0, float q = 0, float r = 0)
        {
            linear = new float3(u, v, w);
            angular = new float3(p, q, r);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector6DOF"/> class from a string representation of a 6-element vector.
        /// </summary>
        /// <param name="vectorString">A string representation of a 6-element vector.</param>
        public Vector6DOF(string vectorString)
        {
            Vector6DOF newVec6 = vectorString;
            linear = newVec6.linear;
            angular = newVec6.angular;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector6DOF"/> class with the specified linear and angular components.
        /// </summary>
        /// <param name="linear">The linear component of the vector.</param>
        /// <param name="angular">The angular component of the vector.</param>
        public Vector6DOF(Vector3 linear, Vector3 angular)
        {
            this.linear = linear;
            this.angular = angular;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector6DOF"/> class from a 6-element float array.
        /// </summary>
        /// <param name="v">A 6-element float array.</param>
        public Vector6DOF(float[] v)
        {
            if (v.Length != 6)
            {
                throw new ArgumentException("Number of elements must be 6.");
            }

            linear = new float3(v[0], v[1], v[2]);
            angular = new float3(v[3], v[4], v[5]);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector6DOF"/> class from a 6-element matrix.
        /// </summary>
        /// <param name="m">The 6-element <see cref="Matrix"/> to convert.</param>
        public Vector6DOF(Matrix m)
        {
            Vector6DOF v = m;
            linear = v.linear;
            angular = v.angular;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector6DOF"/> class from an enumerable of size 6.
        /// </summary>
        /// <param name="values">The enumerable of size 6.</param>
        /// <exception cref="ArgumentException">Thrown if the enumerable does not have exactly 6 elements.</exception>
        public Vector6DOF(IEnumerable<float> values)
        {
            float[] enumerable = values.ToArray();
            if (enumerable.Length != 6)
            {
                throw new ArgumentException("The enumerable must contain exactly 6 elements.");
            }

            linear = new float3(enumerable[0], enumerable[1], enumerable[2]);
            angular = new float3(enumerable[3], enumerable[4], enumerable[5]);
        }
    }
}
