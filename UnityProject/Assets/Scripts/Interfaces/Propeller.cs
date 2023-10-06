using UnityEngine;

public class Propeller : MonoBehaviour
{
    /// <summary>
    /// Radians to Degrees conversion factor.
    /// </summary>
    private float rad2deg = Mathf.Rad2Deg;

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

    /// <summary>
    /// Propeller mesh should be an immediate child of the GameObject this script would be attached to.
    /// </summary>
    private void OnEnable()
    {
        spinnerObject = GetComponentsInChildren<Transform>()[1];
    }

    private void Update()
    {
        // Handle rotation animation
        spinnerObject.localRotation = Quaternion.Slerp(spinnerObject.localRotation,
            Quaternion.Euler(spinnerObject.localRotation.eulerAngles + (rad2deg * spinnerNormal * _speed * Time.deltaTime)), 0.5f);
    }
}

public enum SpinAxis
{
    Up, Down, Left, Right, Forward, Backward
}