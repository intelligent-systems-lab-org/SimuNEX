using UnityEngine;

public abstract class Propeller : MotorLoad
{
    private void Update()
    {
        // Scale Time.deltaTime based on _speed
        float scaledDeltaTime = Time.deltaTime * Mathf.Abs(_speed);

        // Handle rotation animation
        Quaternion increment = Quaternion.Euler(rad2deg * normal * _speed * scaledDeltaTime);
        spinnerObject.localRotation *= increment;
    }

}