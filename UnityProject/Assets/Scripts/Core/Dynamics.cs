using UnityEngine;

namespace SimuNEX
{
    public abstract class Dynamics
    {
        public abstract void Init(Model model);
        public abstract void Step(Model model);
    }

    public class NoDynamics : Dynamics
    {
        public override void Init(Model model)
        {
            // Does nothing
        }

        public override void Step(Model model)
        {
            // Does nothing
        }
    }

    public class RigidBody : Dynamics
    {
        public Rigidbody body;

        public override void Init(Model model)
        {
            if (model.gameObject.TryGetComponent(out Rigidbody rigidbody))
            {
                body = rigidbody;
            }
            else
            {
                body = model.gameObject.AddComponent<Rigidbody>();
            }
        }

        public override void Step(Model model)
        {
            throw new System.NotImplementedException();
        }
    }
}
