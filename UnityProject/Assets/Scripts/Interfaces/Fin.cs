using System;
using UnityEngine;

/// <summary>
/// Interface for implementing fin-like/control surface systems.
/// </summary>
public abstract class Fin : MotorLoad
{
    private void Update()
    {
        // Scale Time.deltaTime based on _speed
        float scaledDeltaTime = Time.deltaTime * Mathf.Abs(_speed);

        // Handle rotation animation
        Quaternion increment = Quaternion.Euler(_speed * rad2deg * scaledDeltaTime * spinnerNormal);
        spinnerObject.localRotation *= increment;
    }
}

/// <summary>
/// Specialized forces for fins.
/// </summary>
public abstract class FinForce : Force
{
    /// <summary>
    /// Fin angle.
    /// </summary>
    protected Func<float> finAngle;

    /// <summary>
    /// Parameters specific to the fin.
    /// </summary>
    protected Func<float[]> parameters;

    /// <summary>
    /// Axis of fin rotation.
    /// </summary>
    protected Func<Vector3> normal;

    /// <summary>
    /// Fin thrust and torques stored in an array.
    /// </summary>
    protected float[] outputs = new float[2];

    /// <summary>
    /// The fin function (FF) that computes output values based on the provided inputs (e.g. fin angle and other parameters).
    /// </summary>
    /// <param name="finAngle">Current fin angle.</param>
    /// <param name="parameters">Parameters specific to the fin.</param>
    /// <returns>An array of float values where the first element is force and the second is torque.</returns>
    public abstract float[] FinFunction(Func<float> finAngle, Func<float[]> parameters);

    public override void ApplyForce()
    {
        var _normal = normal();
        outputs = FinFunction(finAngle, parameters);
        rigidBody.AddLinearForce(_normal * outputs[0]);
        rigidBody.AddTorque(_normal * outputs[1]);
    }

    /// <summary>
    /// Connects fin object to its associated transforms and <see cref="RigidBody"/>.
    /// </summary>
    /// <param name="fin"><see cref="Fin"/> object that the force is being applied to.</param>
    public void Initialize(Fin fin)
    {
        normal = () => fin.normal;
        finAngle = () => 
        {
            float angleInDegrees = 0f;

            switch (fin.spinAxis) 
            {
                case Direction.Up:
                    angleInDegrees = fin.spinnerObject.localEulerAngles.y;
                    break;

                case Direction.Down:
                    angleInDegrees = -fin.spinnerObject.localEulerAngles.y;
                    break;

                case Direction.Left:
                    angleInDegrees = fin.spinnerObject.localEulerAngles.x;
                    break;

                case Direction.Right:
                    angleInDegrees = -fin.spinnerObject.localEulerAngles.x;
                    break;

                case Direction.Forward:
                    angleInDegrees = fin.spinnerObject.localEulerAngles.z;
                    break;

                case Direction.Backward:
                    angleInDegrees = -fin.spinnerObject.localEulerAngles.z;
                    break;
            }
            return NormalizeAngle(angleInDegrees * Mathf.Deg2Rad);
        };
    }

    /// <summary>
    /// Converts angles in the range 0 to 2pi to -pi to pi.
    /// </summary>
    /// <param name="angleInRadians">Angle in the range of 0 to 2pi.</param>
    /// <returns>Converted angle between -pi to pi.</returns>
    private float NormalizeAngle(float angleInRadians)
    {
        while (angleInRadians <= -Mathf.PI) angleInRadians += 2 * Mathf.PI;
        while (angleInRadians > Mathf.PI) angleInRadians -= 2 * Mathf.PI;
        return angleInRadians;
    }
}