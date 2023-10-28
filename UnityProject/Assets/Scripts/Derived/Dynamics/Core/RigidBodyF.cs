using System;
using UnityEngine;

/// <summary>
/// Simulates rigidbodies that include added mass forces.
/// </summary>
public class RigidBodyF : RigidBody
{
    /// <summary>
    /// 6 x 6 mass matrix.
    /// </summary>
    Func<Matrix> massMatrix;

    /// <summary>
    /// Acceleration at the current timestep.
    /// </summary>
    [SerializeField]
    private Vector6DOF _acceleration;

    /// <summary>
    /// Volume of the body. Can be set independent of dimensions for now.
    /// </summary>
    public float _volume = 10f;
    
    /// <summary>
    /// Factor of fluid displaced between 0 and 1.
    /// </summary>
    public float _displacedFactor = 1f;

    protected override void Initialize()
    {
        massMatrix = () => new Matrix(new float[,]
        {
            { mass, 0, 0, 0, 0, 0 },
            { 0, mass, 0, 0, 0, 0, },
            { 0, 0, mass, 0, 0, 0, },
            { 0, 0, 0, body.inertiaTensor.x, 0, 0 },
            { 0, 0, 0, 0, body.inertiaTensor.y, 0 },
            { 0, 0, 0, 0, 0, body.inertiaTensor.z }
        });
    }

    //public override float kineticEnergy
    //    => 0.5f * (_velocity.transpose * massMatrix() * _velocity)[0];        
}
