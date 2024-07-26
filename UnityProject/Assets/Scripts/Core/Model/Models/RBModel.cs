using SimuNEX.Mechanical;
using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Models a single Rigidbody.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class RBModel : Model
    {
        public List<Force> forces = new();

        public Rigidbody body;

        protected void Awake()
        {
            if (TryGetComponent(out Rigidbody body))
            {
                this.body = body;
            }
        }

        public RBModel()
        {
            outputs = new
            (
                new IModelOutput[]
                {
                    new ModelOutput<Vector3>("velocity", Signal.Mechanical),
                    new ModelOutput<Vector3>("angular_velocity", Signal.Mechanical),
                    new ModelOutput<Vector3>("position", Signal.Mechanical),
                    new ModelOutput<Quaternion>("angular_position", Signal.Mechanical)
                }
            );

            inputs = new
            (
                new IModelInput[] { new ModelInput<Vector6DOF>("forces", Signal.Mechanical) }
            );
        }

        protected override ModelFunction modelFunction =>
            (IModelInput[] _, IModelOutput[] outputs) =>
            {
                outputs[0].data = body.velocity;
                outputs[1].data = body.angularVelocity;
                outputs[2].data = body.transform.position;
                outputs[3].data = body.transform.rotation;
            };

        /// <summary>
        /// Attaches a force to the <see cref="RBModel"/>.
        /// </summary>
        /// <param name="force">Force to be attached.</param>
        public void AttachForce(Force force)
        {
            forces.Add(force);
        }

        /// <summary>
        /// Removes a force to the <see cref="RBModel"/>.
        /// </summary>
        /// <param name="force">Force to be removed.</param>
        public void RemoveForce(Force force)
        {
            _ = forces.Remove(force);
        }
    }
}
