using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX.Mechanical
{
    [RequireComponent(typeof(Rigidbody))]
    [DisallowMultipleComponent]
    public class RigidBody : MonoBehaviour, IDynamics
    {
        /// <summary>
        /// Handles physics simulation.
        /// </summary>
        public Rigidbody body;

        /// <summary>
        /// Center of mass of the <see cref="RigidBody"/>.
        /// </summary>
        public Transform COM = default;

        /// <summary>
        /// Active forces that are attached to the <see cref="RigidBody"/>
        /// </summary>
        public List<Force> forces = new();

        /// <summary>
        /// Accumulated forces in the current timestep.
        /// </summary>
        protected Vector6DOF _forces = new();

        /// <summary>
        /// Applied forces in the BCF in the current timestep.
        /// </summary>
        public Vector6DOF appliedForce = new();

        /// <summary>
        /// Velocity in the BCF at the current timestep.
        /// </summary>
        [SerializeField]
        protected Vector6DOF _velocity = new();

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

        /// <summary>
        /// Velocity at the start of simulation.
        /// </summary>
        public Vector6DOF initialVelocity = new();

        /// <summary>
        /// Pose at the start of simulation.
        /// </summary>
        public Pose initialPose = new();

        protected void OnEnable()
        {
            Setup();
            Initialize();
        }

        /// <summary>
        /// Applies initial conditions at the start of the physics simulation.
        /// </summary>
        public void Initialize()
        {
            body.AddForce(initialVelocity.linear, ForceMode.VelocityChange);
            body.AddTorque(initialVelocity.angular, ForceMode.VelocityChange);
        }

        protected void OnValidate()
        {
            Setup();
        }

        /// <summary>
        /// Configures the underlying physics engine for starting simulation.
        /// </summary>
        protected void Setup()
        {
            body = GetComponent<Rigidbody>();
            body.useGravity = false;
            body.drag = 0;
            body.angularDrag = 0;

            if (COM != null) 
            { 
                body.centerOfMass = COM.position;
            }

            initialPose = new(position, angularPosition);
        }

        public float mass
        {
            get => body.mass;

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }

                body.mass = value;
            }
        }

        /// <summary>
        /// Attaches a _force to the <see cref="RigidBody"/>.
        /// </summary>
        /// <param name="force">Force to be attached.</param>
        public void AttachForce(Force force)
        {
            forces.Add(force);
        }

        /// <summary>
        /// Removes a _force to the <see cref="RigidBody"/>.
        /// </summary>
        /// <param name="force">Force to be removed.</param>
        public void RemoveForce(Force force)
        {
            _ = forces.Remove(force);
        }

        /// <summary>
        /// Adds translational _force to the current timestep.
        /// </summary>
        /// <param name="f">3D _force to be applied.</param>
        public void AddLinearForce(Vector3 f)
        {
            _forces.linear += f;
        }

        /// <summary>
        /// Adds torque to the current timestep.
        /// </summary>
        /// <param name="tau">3D torque to be applied.</param>
        public void AddTorque(Vector3 tau)
        {
            _forces.angular += tau;
        }

        /// <summary>
        /// Adds _force (linear and angular) to the current timestep.
        /// </summary>
        /// <param name="F">6DOF _force to be applied.</param>
        public void AddForce(Vector6DOF F)
        {
            _forces += F;
        }

        /// <summary>
        /// Applies a linear _force at a position.
        /// </summary>
        /// <param name="f">The 3D _force to be applied.</param>
        /// <param name="pos">The position at which the _force acts.</param>
        public void AddLinearForceAtPosition(Vector3 f, Vector3 pos)
        {
            AddLinearForce(f);
            AddTorque(Vector3.Cross(f, transform.InverseTransformPoint(pos)));
        }

        public void Step()
        {
            _velocity = new Vector6DOF(body.velocity, body.angularVelocity).ToBCF(transform);
            _kineticEnergy = kineticEnergy;
            _potentialEnergy = potentialEnergy;
            _power = power;

            if (forces?.Count > 0)
            {
                foreach (Force force in forces)
                {
                    force.ApplyForce();
                }
            }

            UpdatePhysics();
            appliedForce = _forces;

            // Reset forces before the next timestep
            _forces = Vector6DOF.zero;
        }

        /// <summary>
        /// Updates the physics engine given the current values.
        /// </summary>
        protected virtual void UpdatePhysics()
        {
            body.AddForce(_forces.linear);
            body.AddTorque(_forces.angular);
        }

        /// <summary>
        /// 6DOF velocity of the <see cref="Rigidbody"/>.
        /// </summary>
        public Vector6DOF velocity => _velocity;

        /// <summary>
        /// Angular position of the <see cref="RigidBody"/> expressed as a <see cref="Quaternion"/>.
        /// </summary>
        public Quaternion angularPosition => transform.rotation;

        /// <summary>
        /// Linear position of the <see cref="RigidBody"/>.
        /// </summary>
        public Vector3 position => transform.position;

        /// <summary>
        /// Updates the kinetic energy of the <see cref="RigidBody"/>.
        /// </summary>
        /// <returns>The value of the kinetic energy.</returns>
        public virtual float kineticEnergy
        {
            get
            {
                float linearKE = 0.5f * mass * _velocity.linear.sqrMagnitude;

                float rotationalKE = 0.5f
                    * (
                        (body.inertiaTensor.x * _velocity.angular.x * _velocity.angular.x) +
                            (body.inertiaTensor.y * _velocity.angular.y * _velocity.angular.y) +
                            (body.inertiaTensor.z * _velocity.angular.z * _velocity.angular.z)
                        );

                return linearKE + rotationalKE;
            }
        }

        /// <summary>
        /// Updates the potential energy property of the <see cref="RigidBody"/>.
        /// Returns 0 if gravity or spring forces are absent.
        /// </summary>
        /// <returns>The value of the potential energy.</returns>
        public float potentialEnergy => TryGetComponent(out SimpleGravity simpleGravity) && simpleGravity.enabled
            ? simpleGravity.weight * transform.position.y
            : 0;

        /// <summary>
        /// Average power of the <see cref="RigidBody"/>.
        /// </summary>
        public float power
            => Vector3.Dot(appliedForce.linear, _velocity.linear) +
                Vector3.Dot(appliedForce.angular, _velocity.angular);

        /// <summary>
        /// Resets position, rotation, and velocities to their defaults.
        /// </summary>
        public void ResetAll()
        {
            body.velocity = initialVelocity.linear;
            body.angularVelocity = initialVelocity.angular;
            transform.SetPositionAndRotation(initialPose.position, initialPose.rotation);
        }
    }
}
