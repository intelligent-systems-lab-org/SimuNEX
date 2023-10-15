using UnityEngine;

public abstract class Propeller : MotorLoad
{
    /// <summary>
    /// The propeller function (PF) that computes output values (force and torque) based on the provided inputs (e.g. propeller speed and other parameters).
    /// </summary>
    /// <param name="speed">Current propeller speed.</param>
    /// <param name="parameters">Parameters specific to the propeller.</param>
    /// <returns>An array of float values where the first element is force and the second is torque.</returns>
    protected delegate float[] PropellerFunction(float speed, float[] parameters);

    private void Update()
    {
        // Scale Time.deltaTime based on _speed
        float scaledDeltaTime = Time.deltaTime * Mathf.Abs(_speed);

        // Handle rotation animation
        Quaternion increment = Quaternion.Euler(rad2deg * spinnerNormal * _speed * scaledDeltaTime);
        spinnerObject.localRotation *= increment;
    }

}