using System;
using UnityEngine;

namespace SimuNEX
{
    public partial struct Vector6DOF
    {
        /// <summary>
        /// Returns the transpose of the vector as a row matrix.
        /// </summary>
        /// <returns>The transpose of the vector as a row matrix.</returns>
        public Matrix transpose => new(1, 6, new float[] { u, v, w, p, q, r });

        /// <summary>
        /// Returns the magnitudes of the linear and angular components as a tuple.
        /// </summary>
        /// <returns>A tuple containing the magnitudes of the linear and angular components.</returns>
        public readonly (float, float) magnitude => (((Vector3)linear).magnitude, ((Vector3)angular).magnitude);

        /// <summary>
        /// Determines whether the current <see cref="Vector6DOF"/> instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>True if the object is a <see cref="Vector6DOF"/> and is equal to the current instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is Vector6DOF other && this == other;
        }

        /// <summary>
        /// Generates a hash code for the current <see cref="Vector6DOF"/> instance.
        /// </summary>
        /// <returns>An integer hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(linear, angular);
        }

        /// <summary>
        /// Gets or sets the value of the component at the specified index.
        /// </summary>
        /// <param name="index">The index of the component to get or set.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if the index is out of range.</exception>
        /// <returns>The value of the component at the specified index.</returns>
        public float this[int index]
        {
            get => index switch
            {
                0 => linear.x,
                1 => linear.y,
                2 => linear.z,
                3 => angular.x,
                4 => angular.y,
                5 => angular.z,
                _ => throw new IndexOutOfRangeException()
            };

            set
            {
                switch (index)
                {
                    case 0:
                        {
                            linear.x = value;
                            break;
                        }

                    case 1:
                        {
                            linear.y = value;
                            break;
                        }

                    case 2:
                        {
                            linear.z = value;
                            break;
                        }

                    case 3:
                        {
                            angular.x = value;
                            break;
                        }

                    case 4:
                        {
                            angular.y = value;
                            break;
                        }

                    case 5:
                        {
                            angular.z = value;
                            break;
                        }

                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of the x component of the linear part of the <see cref="Vector6DOF"/>.
        /// </summary>
        public float u
        {
            get => linear.x;
            set => linear.x = value;
        }

        /// <summary>
        /// Gets or sets the value of the y component of the linear part of the <see cref="Vector6DOF"/>.
        /// </summary>
        public float v
        {
            get => linear.y;
            set => linear.y = value;
        }

        /// <summary>
        /// Gets or sets the value of the z component of the linear part of the <see cref="Vector6DOF"/>.
        /// </summary>
        public float w
        {
            get => linear.z;
            set => linear.z = value;
        }

        /// <summary>
        /// Gets or sets the value of the x component of the angular part of the <see cref="Vector6DOF"/>.
        /// </summary>
        public float p
        {
            get => angular.x;
            set => angular.x = value;
        }

        /// <summary>
        /// Gets or sets the value of the y component of the angular part of the <see cref="Vector6DOF"/>.
        /// </summary>
        public float q
        {
            get => angular.y;
            set => angular.y = value;
        }

        /// <summary>
        /// Gets or sets the value of the z component of the angular part of the <see cref="Vector6DOF"/>.
        /// </summary>
        public float r
        {
            get => angular.z;
            set => angular.z = value;
        }

        /// <summary>
        /// Represents a <see cref="Vector6DOF"/> instance with all components set to zero.
        /// </summary>
        public static Vector6DOF zero => new();

        /// <summary>
        /// Selects specific components from the <see cref="Vector6DOF"/> based on the given query.
        /// </summary>
        /// <param name="query">The query specifying which components to select.</param>
        /// <returns>A <see cref="Vector{T}"/> containing the selected components.</returns>
        /// <exception cref="NotSupportedException"></exception>
        public float[] Select(string query)
        {
            string[] components = query.Split(' ');
            float[] result = new float[components.Length];

            for (int i = 0; i < components.Length; i++)
            {
                result[i] = components[i] switch
                {
                    "u" => linear.x,
                    "v" => linear.y,
                    "w" => linear.z,
                    "p" => angular.x,
                    "q" => angular.y,
                    "r" => angular.z,
                    _ => throw new NotSupportedException($"Invalid component: {components[i]}"),
                };
            }

            return result;
        }

        /// <summary>
        /// Alters the components of the <see cref="Vector6DOF"/> based on the given query and array.
        /// </summary>
        /// <param name="query">The query specifying which components to alter.</param>
        /// <param name="value">The array containing the new values for the selected components.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public void Alter(string query, float[] value)
        {
            string[] components = query.Split(' ');

            if (components.Length != value.Length)
            {
                throw new InvalidOperationException("Size mismatch: Number of components in the query must match the size of the array.");
            }

            for (int i = 0; i < components.Length; i++)
            {
                switch (components[i])
                {
                    case "u":
                        {
                            linear.x = value[i];
                            break;
                        }

                    case "v":
                        {
                            linear.y = value[i];
                            break;
                        }

                    case "w":
                        {
                            linear.z = value[i];
                            break;
                        }

                    case "p":
                        {
                            angular.x = value[i];
                            break;
                        }

                    case "q":
                        {
                            angular.y = value[i];
                            break;
                        }

                    case "r":
                        {
                            angular.z = value[i];
                            break;
                        }

                    default:
                        throw new NotSupportedException($"Invalid component: {components[i]}");
                }
            }
        }

        /// <summary>
        /// Sets the value of a specific component of the <see cref="Vector6DOF"/> using the given query.
        /// </summary>
        /// <param name="query">The query specifying the component to set.</param>
        /// <returns>The value of the specified component in string representation.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public string this[string query]
        {
            set
            {
                string[] components = query.Split(' ');

                string[] vectorValues = value.Trim('[', ']').Split(';');
                if (vectorValues.Length != components.Length)
                {
                    throw new InvalidOperationException($"Invalid vector string. Expected {components.Length} elements.");
                }

                for (int i = 0; i < components.Length; i++)
                {
                    switch (components[i])
                    {
                        case "u":
                            {
                                linear.x = float.Parse(vectorValues[i]);
                                break;
                            }

                        case "v":
                            {
                                linear.y = float.Parse(vectorValues[i]);
                                break;
                            }

                        case "w":
                            {
                                linear.z = float.Parse(vectorValues[i]);
                                break;
                            }

                        case "p":
                            {
                                angular.x = float.Parse(vectorValues[i]);
                                break;
                            }

                        case "q":
                            {
                                angular.y = float.Parse(vectorValues[i]);
                                break;
                            }

                        case "r":
                            {
                                angular.z = float.Parse(vectorValues[i]);
                                break;
                            }

                        default:
                            throw new NotSupportedException($"Invalid component: {components[i]}");
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new <see cref="Vector6DOF"/> object with all components set to one.
        /// </summary>
        public static Vector6DOF one => new(1, 1, 1, 1, 1, 1);
    }
}
