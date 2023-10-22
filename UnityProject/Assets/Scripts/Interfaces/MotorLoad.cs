using System;
using UnityEngine;

/// <summary>
/// Generalizable class for motor loads such as <see cref="Propeller"/>.
/// </summary>
public abstract class MotorLoad : Load
{
    /// <summary>
    /// Radians to Degrees conversion factor.
    /// </summary>
    protected readonly float rad2deg = Mathf.Rad2Deg;

    /// <summary>
    /// Location of the propeller mesh.
    /// </summary>
    public Transform spinnerObject;

    /// <summary>
    /// Propeller axis of rotation.
    /// </summary>
    public SpinAxis spinAxis;

    /// <summary>
    /// Propeller speed value.
    /// </summary>
    public float _speed = 0;

    /// <summary>
    /// Force associated with the load.
    /// </summary>
    protected Force force;

    /// <summary>
    /// Set up forces, properties, etc. for simulation.
    /// </summary>
    protected abstract void Initialize();

    /// <summary>
    /// Attaches a <see cref="Force"/> to the <see cref="RigidBody"/>.
    /// </summary>
    public void Activate()
    {
        Initialize();
        rb.AttachForce(force);
    }

    /// <summary>
    /// Detaches associated <see cref="Force"/> from the <see cref="RigidBody"/>.
    /// </summary>
    public void Deactivate()
    {
        rb.RemoveForce(force);
    }

    private void OnEnable()
    {
        Activate();
    }

    private void OnValidate()
    {
        FindSpinnerTransforms();
    }

    private void OnDisable()
    {
        Deactivate();
    }

    /// <summary>
    /// Locates mesh of spinning object which should be an immediate child of the GameObject this script would be attached to.
    /// </summary>
    private void FindSpinnerTransforms()
    {
        var transforms = GetComponentsInChildren<Transform>();
        if (transforms.Length > 1)
        {
            spinnerObject = transforms[1];
        }
        else
        {
            Debug.LogError("No spinner object found!", this);
        }
    }

    /// <summary>
    /// Updates speed based on the motor output.
    /// </summary>
    public float motorOutput
    {
        get
        {
            if (actuatorFunction != null)
            {
                _speed = actuatorFunction();
            }
            // Use input speed if motor is not connected
            return _speed;
        }
    }


    /// <summary>
    /// Obtains the local normal vector of rotation.
    /// </summary>
    public Vector3 normal
    {
        get
        {
            Vector3 normal = Vector3.zero;
            switch (spinAxis)
            {
                case SpinAxis.Up:
                    normal = transform.up;
                    break;
                case SpinAxis.Down:
                    normal = -transform.up;
                    break;
                case SpinAxis.Left:
                    normal = -transform.right;
                    break;
                case SpinAxis.Right:
                    normal = transform.right;
                    break;
                case SpinAxis.Forward:
                    normal = transform.forward;
                    break;
                case SpinAxis.Backward:
                    normal = -transform.forward;
                    break;
            }
            return normal;
        }
    }

    /// <summary>
    /// Obtains the normal vector of rotation.
    /// </summary>
    public Vector3 spinnerNormal
    {
        get
        {
            Vector3 normal = Vector3.zero;
            switch (spinAxis)
            {
                case SpinAxis.Up:
                    normal = Vector3.up;
                    break;
                case SpinAxis.Down:
                    normal = -Vector3.up;
                    break;
                case SpinAxis.Left:
                    normal = -Vector3.right;
                    break;
                case SpinAxis.Right:
                    normal = Vector3.right;
                    break;
                case SpinAxis.Forward:
                    normal = Vector3.forward;
                    break;
                case SpinAxis.Backward:
                    normal = -Vector3.forward;
                    break;
            }
            return normal;
        }
    }
}

/// <summary>
/// Axis of rotation.
/// </summary>
public enum SpinAxis
{
    Up, Down, Left, Right, Forward, Backward
}
