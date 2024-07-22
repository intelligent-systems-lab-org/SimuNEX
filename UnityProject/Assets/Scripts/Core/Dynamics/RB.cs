using UnityEngine;

namespace SimuNEX
{
    public class RB : Dynamics
    {
        protected Rigidbody body;

        public RB(RBModel model)
        {
            GameObject gameObject = model.gameObject;

            if (gameObject.TryGetComponent(out Rigidbody rigidbody))
            {
                body = rigidbody;
            }
            else
            {
                body = gameObject.AddComponent<Rigidbody>();
            }
        }

        public override IModelOutput[] states => new IModelOutput[]
        {
            new ModelOutput<Vector3>("velocity", Signal.Mechanical),
            new ModelOutput<Vector3>("angular_velocity", Signal.Mechanical),
            new ModelOutput<Vector3>("position", Signal.Mechanical),
            new ModelOutput<Quaternion>("angular_position", Signal.Mechanical)
        };

        public override IModelInput[] inputs { get; }

        public override void Step(IModelOutput[] states, IModelInput[] inputs)
        {
            states[0].data = body.velocity;
            states[1].data = body.angularVelocity;
            states[2].data = body.transform.position;
            states[3].data = body.transform.rotation;
        }
    }
}
