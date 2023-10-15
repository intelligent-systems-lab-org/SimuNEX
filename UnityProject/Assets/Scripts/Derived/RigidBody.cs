using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidBody : Dynamics
{
    public Rigidbody body;

    private List<Force> forces = new();
    private Vector3 _forces;
    private Vector3 _torques;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        foreach (Force force in forces)
        {
            force.ApplyForce();
        }
        Step();
    }

    protected void Initialize()
    {
        _forces = Vector3.zero;
        _torques = Vector3.zero;
        body = GetComponent<Rigidbody>();
    }

    public void AttachForce(Force force)
    {
        forces.Add(force);
    }

    public void RemoveForce(Force force)
    {
        forces.Remove(force);
    }

    public void AddLinearForce(Vector3 f, CoordinateFrame CF = CoordinateFrame.BCF)
    {
        _forces += CF switch
        {
            CoordinateFrame.ICF => f,
            _ => transform.InverseTransformDirection(f)
        };
    }

    public void AddTorque(Vector3 tau, CoordinateFrame CF = CoordinateFrame.BCF)
    {
        _torques += CF switch
        {
            CoordinateFrame.ICF => tau,
            _ => transform.InverseTransformDirection(tau)
        };
    }

    public void AddForce(Vector3 f, Vector3 tau, CoordinateFrame CF = CoordinateFrame.BCF)
    {
        AddLinearForce(f, CF);
        AddTorque(tau, CF);
    }

    public void AddLinearForceAtPosition(Vector3 f, Vector3 pos, CoordinateFrame CF = CoordinateFrame.BCF)
    {
        AddLinearForce(f, CF);
        AddTorque(Vector3.Cross(pos, f), CF);
    }

    protected override void Step()
    {
        body.AddForce(_forces);
        body.AddTorque(_torques);
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