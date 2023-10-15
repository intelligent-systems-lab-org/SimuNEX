using System;
using UnityEngine;

/// <summary>
/// Simple propeller model with constant thrust and torque coefficients.
/// </summary>
public class SimplePropeller : Propeller
{
    public float thrustCoefficient = 1.88865e-5f;
    public float torqueCoefficient = 1.1e-5f;

    protected override void Initialize(RigidBody rb)
    {
        force = new SimplePropellerForce(rb, () => transform, thrustCoefficient, torqueCoefficient, () => normal, () => motorOutput);
    }
}

[RequireComponent(typeof(SimplePropeller))]
public class SimplePropellerForce : Force
{
    float thrustCoefficient;
    float torqueCoefficient;
    Func<Vector3> normal;
    Func<float> propellerSpeed;
    Func<Transform> transformCallback;

    public SimplePropellerForce(RigidBody rb, Func<Transform> transformCallback, float thrustCoefficient, float torqueCoefficient,
        Func<Vector3> normal, Func<float> propellerSpeed)
    {
        this.thrustCoefficient = thrustCoefficient;
        this.torqueCoefficient = torqueCoefficient;
        this.normal = normal;
        this.propellerSpeed = propellerSpeed;
        this.rb = rb;
        this.transformCallback = transformCallback;
    }

    public override void ApplyForce()
    {
        float speed = propellerSpeed();
        Transform transform = transformCallback();
        rb.AddLinearForceAtPosition(normal() * thrustCoefficient * speed * speed, transform.position);
        rb.AddTorque(normal() * torqueCoefficient * Mathf.Abs(speed) * speed);
    }
}