using System;
using UnityEngine;

/// <summary>
/// Simple propeller model with constant thrust and torque coefficients.
/// </summary>
public class SimplePropeller : Propeller
{
    public float thrustCoefficient = 1.88865e-5f;
    public float torqueCoefficient = 1.1e-5f;

    protected override void Initialize()
    {
        force = new SimplePropellerForce(this);
    }

    public class SimplePropellerForce : Force
    {
        Func<float> thrustCoefficient;
        Func<float> torqueCoefficient;
        Func<Vector3> normal;
        Func<float> propellerSpeed;
        Func<Transform> transformCallback;

        public SimplePropellerForce(SimplePropeller propeller)
        {
            normal = () => propeller.normal;
            rb = propeller.rb;
            transformCallback = () => propeller.transform;
            thrustCoefficient = () => propeller.thrustCoefficient;
            torqueCoefficient = () => propeller.torqueCoefficient;
            propellerSpeed = () => propeller.motorOutput;
        }

        public override void ApplyForce()
        {
            float speed = propellerSpeed();
            Transform transform = transformCallback();
            rb.AddLinearForceAtPosition(normal() * thrustCoefficient() * speed * speed, transform.position);
            rb.AddTorque(normal() * torqueCoefficient() * Mathf.Abs(speed) * speed);
        }
    }
}