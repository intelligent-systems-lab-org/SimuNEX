using UnityEngine;

public class Propeller : MonoBehaviour
{
    private float rad2deg = Mathf.Rad2Deg;
    public Transform spinnerObject;
    public SpinAxis spinAxis;
    public float _speed = 0;

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

    private void OnEnable()
    {
        spinnerObject = GetComponentsInChildren<Transform>()[1];
    }

    private void Update()
    {
        spinnerObject.localRotation = Quaternion.Slerp(spinnerObject.localRotation,
            Quaternion.Euler(spinnerObject.localRotation.eulerAngles + (rad2deg * spinnerNormal * _speed * Time.fixedDeltaTime)), 0.5f);
    }
}

public enum SpinAxis
{
    Up, Down, Left, Right, Forward, Backward
}