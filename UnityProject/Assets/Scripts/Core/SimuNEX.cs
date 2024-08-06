using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX
{
    [DisallowMultipleComponent]
    public class SimuNEX : MonoBehaviour
    {
        [SerializeField]
        [Range(0.001f, 0.1f)]
        private float _sampleTime = 0.02f;

        [SerializeField]
        private List<Model> models;

        [SerializeField]
        private List<ModelInput> inports;

        [SerializeField]
        private List<ModelOutput> outports;


        public float SampleTime
        {
            get => _sampleTime;

            set
            {
                _sampleTime = Mathf.Clamp(value, 0.001f, 0.1f);
                Time.fixedDeltaTime = _sampleTime;
            }
        }

        public (ModelInput[], ModelOutput[]) ports => (inports.ToArray(), outports.ToArray());

        protected void OnValidate()
        {
            models = new(GetComponentsInChildren<Model>());
            inports = new();
            outports = new();

            // Filter out models contained in ModelSystem instances
            List<Model> modelsToRemove = new();

            foreach (Model model in models)
            {
                if (model is ModelSystem modelSystem)
                {
                    modelsToRemove.AddRange(modelSystem.models);
                }

                inports.AddRange(model.inports);
                outports.AddRange(model.outports);
            }

            models.RemoveAll(m => modelsToRemove.Contains(m));
        }

        protected void Start()
        {
            Time.fixedDeltaTime = _sampleTime; // Ensure initial setting is applied
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
