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
    public SimplePropellerForce(SimplePropeller propeller) : base(propeller)
    {
        parameters = new Func<float>[2]
        {
                () => propeller.thrustCoefficient,
                () => propeller.torqueCoefficient
        };
    }

    public override float[] PropellerFunction(Func<float> speed, Func<float>[] parameters)
    {
        float _speed = speed();
        float thrust = parameters[0]() * _speed * _speed;
        float torque = parameters[1]() * _speed * Mathf.Abs(_speed);
        return new float[] { thrust, torque };
    }
}