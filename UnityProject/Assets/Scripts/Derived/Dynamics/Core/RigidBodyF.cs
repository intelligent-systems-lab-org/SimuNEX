using System;
using UnityEngine;

/// <summary>
/// Simulates rigidbodies that include added mass forces.
/// </summary>
public class RigidBodyF : RigidBody
{
    /// <summary>
    /// Added mass coefficients.
    /// </summary>
    public Matrix6DOF addedMass = new();

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
    public float _displacedVolumeFactor = 1f;

    protected override void UpdatePhysics()
    {
        _acceleration = MassMatrix.inverse * _forces;
        body.AddForce(_acceleration.linear, ForceMode.Acceleration);
        body.AddTorque(_acceleration.angular, ForceMode.Acceleration);
    }

    /// <summary>
    /// 6 x 6 mass matrix.
    /// </summary>
    public Matrix6DOF MassMatrix => Matrix6DOF.CreateMassMatrix(mass, body.inertiaTensor) - addedMass;

    public override float kineticEnergy
    {
        get {
            Matrix momentum = MassMatrix * _velocity;
            return 0.5f * (_velocity.transpose * momentum)[0, 0];
        }
    }        
}
