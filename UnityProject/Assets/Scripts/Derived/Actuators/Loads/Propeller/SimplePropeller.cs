using System;
using UnityEngine;

/// <summary>
/// Simple propeller model with constant thrust and torque coefficients.
/// </summary>
public class SimplePropeller : Propeller
{
    /// <summary>
    /// Speed to thrust factor.
    /// </summary>
    public float thrustCoefficient = 1.88865e-5f;

    /// <summary>
    /// Speed to torque factor.
    /// </summary>
    public float torqueCoefficient = 1.1e-5f;

    protected override void Initialize()
    {
        force = rigidBody.gameObject.AddComponent<SimplePropellerForce>();
        (force as SimplePropellerForce).Initialize(this);
    }
}

/// <summary>
/// Implements the PropellerFunction for <see cref="SimplePropeller"/>
/// </summary>
public class SimplePropellerForce : PropellerForce
{
    /// <summary>
    /// Set up propeller specific parameters.
    /// </summary>
    /// <param name="propeller"><see cref="SimplePropeller"/> object that the force is being applied to.</param>
    public void Initialize(SimplePropeller propeller)
    {
        base.Initialize(propeller);
        parameters = () => new float[]
        {
            propeller.thrustCoefficient, propeller.torqueCoefficient
        };
    }

    public override float[] PropellerFunction(Func<float> speed, Func<float[]> parameters)
    {
        float _speed = speed();
        float thrust = parameters()[0] * _speed * Mathf.Abs(_speed);
        float torque = parameters()[1] * _speed * Mathf.Abs(_speed);
        return new float[] { thrust, torque };
    }
}