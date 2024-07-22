using UnityEngine;

namespace SimuNEX
{
    /// <summary>
    /// Models a single Rigidbody.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class RBModel : Model
    {
        public override IBehavioral behavorial => new RB(this);

        public RB rigidBody;

        protected void OnEnable()
        {
            rigidBody = behavorial as RB;
        }
    }
}
