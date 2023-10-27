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
    private List<Force> forces = new();

    /// <summary>
    /// Accumulated forces in the current timestep.
    /// </summary>
    private Vector6DOF _forces = Vector6DOF.zero;

    /// <summary>
    /// Velocity at the current timestep.
    /// </summary>
    [SerializeField]
    private Vector6DOF _velocity;

    /// <summary>
    /// Potential energy at the current timestep. Depends on the presence of gravity and spring forces.
    /// </summary>
    [SerializeField]
    private float _potentialEnergy;

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
    /// <param name="CF">Coordinate frame in which the force acts.</param>
    public void AddLinearForce(Vector3 f, CoordinateFrame CF = CoordinateFrame.BCF)
    {
        _forces.linear += CF switch
        {
            CoordinateFrame.ICF => f,
            _ => transform.InverseTransformDirection(f)
        };
    }

    /// <summary>
    /// Adds torque to the current timestep.
    /// </summary>
    /// <param name="tau">3D torque to be applied.</param>
    /// <param name="CF">Coordinate frame in which the torque acts.</param>
    public void AddTorque(Vector3 tau, CoordinateFrame CF = CoordinateFrame.BCF)
    {
        _forces.linear += CF switch
        {
            CoordinateFrame.ICF => tau,
            _ => transform.InverseTransformDirection(tau)
        };
    }

    /// <summary>
    /// Adds force (linear and angular) to the current timestep.
    /// </summary>
    /// <param name="F">6DOF force to be applied.</param>
    /// <param name="CF">Coordinate frame in which the forces acts.</param>
    public void AddForce(Vector6DOF F, CoordinateFrame CF = CoordinateFrame.BCF)
    {
        _forces += CF switch
        {
            CoordinateFrame.ICF => F,
            _ => F.ToBodyFrame(transform)
        };
    }

    /// <summary>
    /// Applies a linear force at a position.
    /// </summary>
    /// <param name="f">The 3D force to be applied.</param>
    /// <param name="pos">The position at which the force acts.</param>
    /// <param name="CF">Coordinate frame in which the force acts.</param>
    public void AddLinearForceAtPosition(Vector3 f, Vector3 pos, CoordinateFrame CF = CoordinateFrame.BCF)
    {
        AddLinearForce(f, CF);
        AddTorque(Vector3.Cross(f, pos), CF);
    }

    public override void Step()
    {
        _velocity = new Vector6DOF(body.velocity, body.angularVelocity);
        _potentialEnergy = UpdatePotentialEnergy();

        if (forces != null && forces.Count > 0)
        {
            foreach (Force force in forces)
            {
                force.ApplyForce();
            }
        }
        body.AddForce(_forces.linear);
        body.AddTorque(_forces.angular);

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
    /// Updates the potential energy property of the <see cref="RigidBody"/>. 
    /// Returns 0 if gravity or spring forces are absent.
    /// </summary>
    public float UpdatePotentialEnergy()
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