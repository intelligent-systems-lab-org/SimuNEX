using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX
{
    public class SimuNEX : MonoBehaviour
    {
        [SerializeField]
        [Range(0.001f, 0.1f)]
        private float _sampleTime = 0.02f;

        [SerializeField]
        private List<Model> models;

        public float SampleTime
        {
            get => _sampleTime;

            set
            {
                _sampleTime = Mathf.Clamp(value, 0.001f, 0.1f);
                Time.fixedDeltaTime = _sampleTime;
            }
        }

        protected void Start()
        {
            models = new(GetComponentsInChildren<Model>());
            Time.fixedDeltaTime = _sampleTime; // Ensure initial setting is applied

            // Filter out models contained in ModelSystem instances
            List<Model> modelsToRemove = new();

            foreach (Model model in models)
            {
                if (model is ModelSystem modelSystem)
                {
                    modelsToRemove.AddRange(modelSystem.models);
                }
            }

            models.RemoveAll(m => modelsToRemove.Contains(m));
        }

        protected void FixedUpdate()
        {
            foreach (Model model in models)
            {
                model.Step();
            }
        }
    }
}
