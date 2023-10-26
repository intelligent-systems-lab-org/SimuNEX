using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class Vector6DOF
{
    /// <summary>
    /// Adds two <see cref="Vector6DOF"/> instances together.
    /// </summary>
    /// <param name="v1">The first <see cref="Vector6DOF"/> to add.</param>
    /// <param name="v2">The second <see cref="Vector6DOF"/> to add.</param>
    /// <returns>A new <see cref="Vector6DOF"/> representing the sum of the two <see cref="Vector6DOF"/> instances.</returns>
    public static Vector6DOF operator +(Vector6DOF v1, Vector6DOF v2)
    {
        return new Vector6DOF
        {
            linear = v1.linear + v2.linear,
            angular = v1.angular + v2.angular
        };
    }

    /// <summary>
    /// Subtracts one <see cref="Vector6DOF"/> instance from another.
    /// </summary>
    /// <param name="v1">The <see cref="Vector6DOF"/> to subtract from.</param>
    /// <param name="v2">The <see cref="Vector6DOF"/> to subtract.</param>
    /// <returns>A new <see cref="Vector6DOF"/> representing the difference between the two <see cref="Vector6DOF"/> instances.</returns>
    public static Vector6DOF operator -(Vector6DOF v1, Vector6DOF v2)
    {
        return new Vector6DOF
        {
            linear = v1.linear - v2.linear,
            angular = v1.angular - v2.angular
        };
    }

    /// <summary>
    /// Multiplies a <see cref="Vector6DOF"/> instance by a scalar value.
    /// </summary>
    /// <param name="v">The <see cref="Vector6DOF"/> to multiply.</param>
    /// <param name="scalar">The scalar value to multiply by.</param>
    /// <returns>A new <see cref="Vector6DOF"/> representing the result of multiplying the <see cref="Vector6DOF"/> by the scalar value.</returns>
    public static Vector6DOF operator *(Vector6DOF v, float scalar)
    {
        return new Vector6DOF
        {
            linear = v.linear * scalar,
            angular = v.angular * scalar
        };
    }

    /// <summary>
    /// Multiplies a scalar value by a <see cref="Vector6DOF"/> instance.
    /// </summary>
    /// <param name="scalar">The scalar value to multiply.</param>
    /// <param name="v">The <see cref="Vector6DOF"/> to multiply.</param>
    /// <returns>A new <see cref="Vector6DOF"/> representing the result of multiplying the scalar value by the <see cref="Vector6DOF"/>.</returns>
    public static Vector6DOF operator *(float scalar, Vector6DOF v) => v * scalar;

    /// <summary>
    /// Divides a <see cref="Vector6DOF"/> instance by a scalar value.
    /// </summary>
    /// <param name="v">The <see cref="Vector6DOF"/> to divide.</param>
    /// <param name="scalar">The scalar value to divide by.</param>
    /// <returns>A new <see cref="Vector6DOF"/> representing the result of dividing the <see cref="Vector6DOF"/> by the scalar value.</returns>
    public static Vector6DOF operator /(Vector6DOF v, float scalar)
    {
        return new Vector6DOF
        {
            linear = v.linear / scalar,
            angular = v.angular / scalar
        };
    }

    /// <summary>
    /// Divides a scalar value by a <see cref="Vector6DOF"/> instance.
    /// </summary>
    /// <param name="scalar">The scalar value to divide.</param>
    /// <param name="v">The <see cref="Vector6DOF"/> to divide by.</param>
    /// <returns>A new <see cref="Vector6DOF"/> representing the result of dividing the scalar value by the <see cref="Vector6DOF"/>.</returns>
    public static Vector6DOF operator /(float scalar, Vector6DOF v)
    {
        Vector3 reciprocalLinear = new(1f / v.linear.x, 1f / v.linear.y, 1f / v.linear.z);
        Vector3 reciprocalAngular = new(1f / v.angular.x, 1f / v.angular.y, 1f / v.angular.z);

        return new Vector6DOF
        {
            linear = scalar * reciprocalLinear,
            angular = scalar * reciprocalAngular
        };
    }

    /// <summary>
    /// Multiplies two <see cref="Vector6DOF"/> instances component-wise.
    /// </summary>
    /// <param name="v1">The first <see cref="Vector6DOF"/>.</param>
    /// <param name="v2">The second <see cref="Vector6DOF"/>.</param>
    /// <returns>A new <see cref="Vector6DOF"/> representing the component-wise multiplication of the vectors.</returns>
    public static Vector6DOF operator *(Vector6DOF v1, Vector6DOF v2)
    {
        return new Vector6DOF
        {
            linear = new Vector3
            (
                v1.linear.x * v2.linear.x,
                v1.linear.y * v2.linear.y,
                v1.linear.z * v2.linear.z
            ),
            angular = new Vector3
            (
                v1.angular.x * v2.angular.x,
                v1.angular.y * v2.angular.y,
                v1.angular.z * v2.angular.z
            )
        };
    }

    /// <summary>
    /// Divides two <see cref="Vector6DOF"/> instances component-wise.
    /// </summary>
    /// <param name="v1">The numerator <see cref="Vector6DOF"/>.</param>
    /// <param name="v2">The denominator <see cref="Vector6DOF"/>.</param>
    /// <returns>A new <see cref="Vector6DOF"/> representing the component-wise division of the numerator by the denominator.</returns>
    public static Vector6DOF operator /(Vector6DOF v1, Vector6DOF v2)
    {
        return new Vector6DOF
        {
            linear = new Vector3
            (
                v1.linear.x / v2.linear.x,
                v1.linear.y / v2.linear.y,
                v1.linear.z / v2.linear.z
            ),
            angular = new Vector3
            (
                v1.angular.x / v2.angular.x,
                v1.angular.y / v2.angular.y,
                v1.angular.z / v2.angular.z
            )
        };
    }

    /// <summary>
    /// Determines whether two <see cref="Vector6DOF"/> instances are equal.
    /// </summary>
    /// <param name="v1">The first <see cref="Vector6DOF"/>.</param>
    /// <param name="v2">The second <see cref="Vector6DOF"/>.</param>
    /// <returns>True if the two vectors are equal; otherwise, false.</returns>
    public static bool operator ==(Vector6DOF v1, Vector6DOF v2)
    {
        return v1.linear == v2.linear && v1.angular == v2.angular;
    }

    /// <summary>
    /// Determines whether two <see cref="Vector6DOF"/> instances are not equal.
    /// </summary>
    /// <param name="v1">The first <see cref="Vector6DOF"/>.</param>
    /// <param name="v2">The second <see cref="Vector6DOF"/>.</param>
    /// <returns>True if the two vectors are not equal; otherwise, false.</returns>
    public static bool operator !=(Vector6DOF v1, Vector6DOF v2) => !(v1 == v2);

    /// <summary>
    /// Multiplies a <see cref="Matrix"/> by a <see cref="Vector6DOF"/>.
    /// </summary>
    /// <param name="m">The <see cref="Matrix"/> to multiply.</param>
    /// <param name="v">The <see cref="Vector6DOF"/> to multiply.</param>
    /// <returns>A new <see cref="Vector6DOF"/> representing the result of the matrix-vector multiplication.</returns>
    public static Vector6DOF operator *(Matrix m, Vector6DOF v)
    {
        Matrix result = m * new Matrix(6, 1, v);
        return new Vector6DOF(result.ToArray());
    }

    /// <summary>
    /// Converts the <see cref="Vector6DOF"/> to the body frame defined by the specified <see cref="Transform"/>.
    /// </summary>
    /// <param name="transform">The <see cref="Transform"/> defining the body frame.</param>
    /// <returns>A new <see cref="Vector6DOF"/> transformed to the body frame.</returns>
    public Vector6DOF ToBodyFrame(Transform transform) =>
        new(transform.InverseTransformDirection(linear), transform.InverseTransformDirection(angular));

    /// <summary>
    /// Applies a specified function to each component of the <see cref="Vector6DOF"/>.
    /// </summary>
    /// <param name="func">The function to apply to each component of the <see cref="Vector6DOF"/>.</param>
    /// <returns>A new <see cref="Vector6DOF"/> with the function applied to each component.</returns>
    public Vector6DOF Apply(Func<float, float> func)
    {
        return new Vector6DOF
        {
            linear = new Vector3(func(linear.x), func(linear.y), func(linear.z)),
            angular = new Vector3(func(angular.x), func(angular.y), func(angular.z))
        };
    }

    /// <summary>
    /// Applies specified functions to the linear and angular components of the <see cref="Vector6DOF"/>.
    /// </summary>
    /// <param name="linearFunc">The function to apply to each component of the linear part of the <see cref="Vector6DOF"/>.</param>
    /// <param name="angularFunc">The function to apply to each component of the angular part of the <see cref="Vector6DOF"/>.</param>
    /// <returns>A new <see cref="Vector6DOF"/> with the corresponding functions applied to the linear and angular components.</returns>
    public Vector6DOF Apply(Func<float, float> linearFunc, Func<float, float> angularFunc)
    {
        return new Vector6DOF
        {
            linear = new Vector3(linearFunc(linear.x), linearFunc(linear.y), linearFunc(linear.z)),
            angular = new Vector3(angularFunc(angular.x), angularFunc(angular.y), angularFunc(angular.z))
        };
    }
}
