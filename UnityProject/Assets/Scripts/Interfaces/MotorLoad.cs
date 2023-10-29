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
    public Direction spinAxis;

    /// <summary>
    /// Propeller speed value.
    /// </summary>
    public float _speed = 0;

    /// <summary>
    /// Force associated with the load.
    /// </summary>
    protected Force force;

    /// <summary>
    /// The inertia of the load attached to the <see cref="Motor"/> in kg.m^2.
    /// </summary>
    public float loadInertia = 0.5f;

    /// <summary>
    /// The damping coefficient of the load in N.m.s/rad.
    /// </summary>
    public float loadDamping = 0;

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
        rigidBody.AttachForce(force);
    }

    /// <summary>
    /// Detaches associated <see cref="Force"/> from the <see cref="RigidBody"/>.
    /// </summary>
    public void Deactivate()
    {
        rigidBody.RemoveForce(force);
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
                case Direction.Up:
                    normal = transform.up;
                    break;
                case Direction.Down:
                    normal = -transform.up;
                    break;
                case Direction.Left:
                    normal = -transform.right;
                    break;
                case Direction.Right:
                    normal = transform.right;
                    break;
                case Direction.Forward:
                    normal = transform.forward;
                    break;
                case Direction.Backward:
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
                case Direction.Up:
                    normal = Vector3.up;
                    break;
                case Direction.Down:
                    normal = -Vector3.up;
                    break;
                case Direction.Left:
                    normal = -Vector3.right;
                    break;
                case Direction.Right:
                    normal = Vector3.right;
                    break;
                case Direction.Forward:
                    normal = Vector3.forward;
                    break;
                case Direction.Backward:
                    normal = -Vector3.forward;
                    break;
            }
            return normal;
        }
    }
}

/// <summary>
/// Represents a direction.
/// </summary>
public enum Direction
{
    Up, Down, Left, Right, Forward, Backward
}
