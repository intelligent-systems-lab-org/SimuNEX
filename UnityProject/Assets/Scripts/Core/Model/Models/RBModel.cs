using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Models a single Rigidbody.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class RBModel : Model
    {
        /// <summary>
        /// Handles physics simulation.
        /// </summary>
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
                    new ModelOutput("velocity", 3, Signal.Mechanical),
                    new ModelOutput("angular_velocity", 3,Signal.Mechanical),
                    new ModelOutput("position", 3, Signal.Mechanical),
                    new ModelOutput("angular_position", 4, Signal.Mechanical)
                }
            );

            inputs = new
            (
                new IModelInput[] { new ModelInput("forces", 6, Signal.Mechanical) }
            );
        }

        protected override ModelFunction modelFunction =>
            (IModelInput[] _, IModelOutput[] outputs) =>
            {
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
            };
    }
}
