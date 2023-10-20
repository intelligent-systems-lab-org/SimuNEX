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
    private Vector3 _forces;

    /// <summary>
    /// Accumulated torques in the current timestep.
    /// </summary>
    private Vector3 _torques;

    private void Start()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        Step();
    }

    /// <summary>
    /// Configures the system at the start of the physics simulation.
    /// </summary>
    protected void Initialize()
    {
        _forces = Vector3.zero;
        _torques = Vector3.zero;  
    }

    private void OnValidate()
    {
        body = GetComponent<Rigidbody>();
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
        _forces += CF switch
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
        _torques += CF switch
        {
            CoordinateFrame.ICF => tau,
            _ => transform.InverseTransformDirection(tau)
        };
    }

    /// <summary>
    /// Adds force (linear and angular) to the current timestep.
    /// </summary>
    /// <param name="f">3D force to be applied.</param>
    /// <param name="tau">3D torque to be applied.</param>
    /// <param name="CF">Coordinate frame in which the forces acts.</param>
    public void AddForce(Vector3 f, Vector3 tau, CoordinateFrame CF = CoordinateFrame.BCF)
    {
        AddLinearForce(f, CF);
        AddTorque(tau, CF);
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

    protected override void Step()
    {
        if (forces != null && forces.Count > 0)
        {
            foreach (Force force in forces)
            {
                force.ApplyForce();
            }
        }
        body.AddForce(_forces);
        body.AddTorque(_torques);

        // Reset forces before the next timestep
        _forces = Vector3.zero;
        _torques = Vector3.zero;
    }
}

/// <summary>
/// Represents the coordinate frame used for a vector in a 6DOF space.
/// </summary>
public enum CoordinateFrame
{
    /// <summary>
    /// Body-Centered Frame (BCF) represents the coordinate frame relative to the body or local frame.
    /// </summary>
    BCF,

    /// <summary>
    /// Inertial Coordinate Frame (ICF) represents the coordinate frame fixed in an inertial reference frame.
    /// </summary>
    ICF
}