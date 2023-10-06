using UnityEngine;

public class Propeller : MotorLoad
{
    /// <summary>
    /// Radians to Degrees conversion factor.
    /// </summary>
    private float rad2deg = Mathf.Rad2Deg;

    private void Update()
    {
        // Handle rotation animation
        spinnerObject.localRotation = Quaternion.Slerp(spinnerObject.localRotation,
            Quaternion.Euler(spinnerObject.localRotation.eulerAngles + (rad2deg * spinnerNormal * _speed * Time.deltaTime)), 0.5f);
    }
}