using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Models a single Rigidbody.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class RBModel : Model
    {
        public override IModelPort[] ports => new IModelPort[]
        {
            new ModelOutput<Vector3>("velocity"),
            new ModelOutput<Vector3>("angular_velocity"),
            new ModelOutput<Vector3>("position"),
            new ModelOutput<Quaternion>("angular_position")
        };

        public override IBehavioral behavorial => new RB(this);

        public RB rigidBody;

        protected void OnEnable()
        {
            rigidBody = behavorial as RB;
        }
    }

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

        public void Step(params IModelPort[] ports)
        {
            (ports[0] as ModelOutput<Vector3>).data = body.velocity;
            (ports[1] as ModelOutput<Vector3>).data = body.angularVelocity;
            (ports[2] as ModelOutput<Vector3>).data = body.transform.position;
            (ports[3] as ModelOutput<Quaternion>).data = body.transform.rotation;
        }
    }
}
