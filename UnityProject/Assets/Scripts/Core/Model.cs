using UnityEngine;

namespace SimuNEX
{
    public class Model : MonoBehaviour
    {
        [SerializeReference]
        protected Dynamics dynamics;

        protected void OnEnable()
        {
            Init();
        }

        public void Init()
        {
            dynamics.Init(this);
        }
    }
}
