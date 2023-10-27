using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidBody : Dynamics
{
    /// <summary>
    /// Handles physics simulation.
    /// </summary>
    public Rigidbody body;

    /// <summary>
    /// Active forces that are attached to the <see cref="RigidBody"/>
    /// </summary>
    protected List<Force> forces = new();

    /// <summary>
    /// Accumulated forces in the current timestep.
    /// </summary>
    protected Vector6DOF _forces = Vector6DOF.zero;

    /// <summary>
    /// Applied forces in the BCF in the current timestep.
    /// </summary>
    public Vector6DOF appliedForce;

    /// <summary>
    /// Velocity in the BCF at the current timestep.
    /// </summary>
    [SerializeField]
    protected Vector6DOF _velocity;

    /// <summary>
    /// Kinetic energy at the current timestep.
    /// </summary>
    [SerializeField]
    protected float _kineticEnergy;

    /// <summary>
    /// Potential energy at the current timestep. Depends on the presence of gravity and spring forces.
    /// </summary>
    [SerializeField]
    protected float _potentialEnergy;

    /// <summary>
    /// Average power at the current timestep.
    /// </summary>
    [SerializeField]
    protected float _power;

    private void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Configures the system at the start of the physics simulation.
    /// </summary>
    protected override void Initialize()
    {
        _forces = Vector6DOF.zero; 
    }

    private void OnValidate()
    {
        body = GetComponent<Rigidbody>();
        body.useGravity = false;
        body.drag = 0;
        body.angularDrag = 0;
    }

    public float mass
    {
        get => body.mass;
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentException();
            }
            body.mass = mass;
        }
    }

    /// <summary>
    /// Attaches a force to the <see cref="RigidBody"/>.
    /// </summary>
    /// <param name="force">Force to be attached.</param>
    public void AttachForce(Force force)
    {
        forces.Add(force);
    }

    /// <summary>
    /// Removes a force to the <see cref="RigidBody"/>.
    /// </summary>
    /// <param name="force">Force to be removed.</param>
    public void RemoveForce(Force force)
    {
        forces.Remove(force);
    }

    /// <summary>
    /// Adds translational force to the current timestep.
    /// </summary>
    /// <param name="f">3D force to be applied.</param>
    public void AddLinearForce(Vector3 f) => _forces.linear += f;

    /// <summary>
    /// Adds torque to the current timestep.
    /// </summary>
    /// <param name="tau">3D torque to be applied.</param>
    public void AddTorque(Vector3 tau) => _forces.angular += tau;

    /// <summary>
    /// Adds force (linear and angular) to the current timestep.
    /// </summary>
    /// <param name="F">6DOF force to be applied.</param>
    public void AddForce(Vector6DOF F) => _forces += F;

    /// <summary>
    /// Applies a linear force at a position.
    /// </summary>
    /// <param name="f">The 3D force to be applied.</param>
    /// <param name="pos">The position at which the force acts.</param>
    public void AddLinearForceAtPosition(Vector3 f, Vector3 pos)
    {
        AddLinearForce(f);
        AddTorque(Vector3.Cross(f, pos));
    }

    public override void Step()
    {
        _velocity = new Vector6DOF(body.velocity, body.angularVelocity).ToBodyFrame(transform);
        _kineticEnergy = kineticEnergy;
        _potentialEnergy = potentialEnergy;
        _power = power;

        if (forces != null && forces.Count > 0)
        {
            foreach (Force force in forces)
            {
                force.ApplyForce();
            }
        }

        body.AddForce(_forces.linear);
        body.AddTorque(_forces.angular);

        appliedForce = _forces;

        // Reset forces before the next timestep
        _forces = Vector6DOF.zero;
    }

    /// <summary>
    /// 6DOF velocity of the <see cref="Rigidbody"/>.
    /// </summary>
    public Vector6DOF velocity => new(body.velocity, body.angularVelocity);

    /// <summary>
    /// Angular position of the <see cref="RigidBody"/> expressed as a <see cref="Quaternion"/>.
    /// </summary>
    public Quaternion angularPosition => body.rotation;

    /// <summary>
    /// Linear position of the <see cref="RigidBody"/>.
    /// </summary>
    public Vector3 position => body.position;

    /// <summary>
    /// Updates the kinetic energy of the <see cref="RigidBody"/>.
    /// </summary>
    /// <returns>The value of the kinetic energy.</returns>
    public virtual float kineticEnergy
    {
        get 
        {
            float linearKE = 0.5f * mass * _velocity.linear.sqrMagnitude;

            float rotationalKE = 0.5f * (
                body.inertiaTensor.x * _velocity.angular.x * _velocity.angular.x +
                body.inertiaTensor.y * _velocity.angular.y * _velocity.angular.y +
                body.inertiaTensor.z * _velocity.angular.z * _velocity.angular.z
            );

            return linearKE + rotationalKE;
        }
    }

    /// <summary>
    /// Updates the potential energy property of the <see cref="RigidBody"/>. 
    /// Returns 0 if gravity or spring forces are absent.
    /// </summary>
    /// <returns>The value of the potential energy.</returns>
    public float potentialEnergy
    {
        get
        {
            if (TryGetComponent<SimpleGravity>(out var simpleGravity))
            {
                return simpleGravity.weight * transform.position.y;
            }
            else
            {
                return 0; 
            }
        }
    }

    /// <summary>
    /// Average power of the <see cref="RigidBody"/>.
    /// </summary>
    public float power
        => Vector3.Dot(appliedForce.linear, _velocity.linear) + 
        Vector3.Dot(appliedForce.angular, _velocity.angular);
}