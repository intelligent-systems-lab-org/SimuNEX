using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Models a single Rigidbody.
    /// </summary>
    public class RBModel : Model
    {
        /// <summary>
        /// Handles physics simulation.
        /// </summary>
        public Rigidbody body;

        /// <summary>
        /// Applied forces in the BCF in the current timestep.
        /// </summary>
        protected Vector6DOF appliedForce;

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
                new ModelOutput[]
                {
                    new("velocity", 3, Signal.Mechanical, this),
                    new("angular_velocity", 3,Signal.Mechanical, this),
                    new("position", 3, Signal.Mechanical, this),
                    new("angular_position", 4, Signal.Mechanical, this)
                }
            );

            inputs = new
            (
                new ModelInput[] { new("forces", 6, Signal.Mechanical, this) }
            );
        }

        protected override ModelFunction modelFunction =>
            (ModelInput[] inputs, ModelOutput[] outputs) =>
            {
                // Update states
                outputs[0].data[0] = body.velocity.x;
                outputs[0].data[1] = body.velocity.z;
                outputs[0].data[2] = body.velocity.y;

                outputs[1].data[0] = body.angularVelocity.x;
                outputs[1].data[1] = body.angularVelocity.z;
                outputs[2].data[2] = body.angularVelocity.y;

                outputs[2].data[0] = body.transform.position.x;
                outputs[2].data[1] = body.transform.position.z;
                outputs[2].data[2] = body.transform.position.y;

                outputs[3].data[0] = body.transform.rotation.x;
                outputs[3].data[1] = body.transform.rotation.z;
                outputs[3].data[2] = body.transform.rotation.y;
                outputs[3].data[3] = body.transform.rotation.w;

                // Synchronize inputs with applied force
                appliedForce.linear.x = inputs[0].data[0];
                appliedForce.linear.y = inputs[0].data[1];
                appliedForce.linear.z = inputs[0].data[2];

                appliedForce.angular.x = inputs[0].data[3];
                appliedForce.angular.y = inputs[0].data[4];
                appliedForce.angular.z = inputs[0].data[5];
            };

        protected virtual void PhysicsUpdate()
        {
            body.AddForce(appliedForce.linear);
            body.AddTorque(appliedForce.angular);
        }

        public override void Step()
        {
            modelFunction(inports, outports);
            PhysicsUpdate();
        }
    }
}
