using SimuNEX.Communication;
using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX
{
    [DisallowMultipleComponent]
    public class SimuNEX : MonoBehaviour
    {
        /// <summary>
        /// The minimum timestep ran by <see cref="FixedUpdate"/>.
        /// </summary>
        [SerializeField]
        [Range(0.001f, 0.1f)]
        private float _sampleTime = 0.02f;

        /// <summary>
        /// <see cref="COM"/> system connected to SimuNEX.
        /// </summary>
        public COM communication;

        /// <summary>
        /// Collection of all models that will be simulated at runtime.
        /// </summary>
        [SerializeField]
        private List<Model> models;

        /// <summary>
        /// All input ports found, counting internal connections of <see cref="ModelSystem"/> entities.
        /// </summary>
        [SerializeField]
        private List<ModelInput> inports;

        /// <summary>
        /// All output ports found, counting internal connections of <see cref="ModelSystem"/> entities.
        /// </summary>
        [SerializeField]
        private List<ModelOutput> outports;

        /// <summary>
        /// (Property) All input ports found, counting internal connections of <see cref="ModelSystem"/> entities.
        /// </summary>
        public List<ModelInput> Inports => inports;

        /// <summary>
        /// (Property) All output ports found, counting internal connections of <see cref="ModelSystem"/> entities.
        /// </summary>
        public List<ModelOutput> Outports => outports;

        /// <summary>
        /// (Property) The minimum timestep ran by <see cref="FixedUpdate"/>.
        /// </summary>
        public float SampleTime
        {
            get => _sampleTime;

            set
            {
                _sampleTime = Mathf.Clamp(value, 0.001f, 0.1f);
                Time.fixedDeltaTime = _sampleTime;
            }
        }

        /// <summary>
        /// Initializes SimuNEX, locating all models and ports.
        /// </summary>
        public void Init()
        {
            models = new(GetComponentsInChildren<Model>());
            inports = new();
            outports = new();

            // Filter out models contained in ModelSystem instances
            List<Model> modelsToRemove = new();

            foreach (Model model in models)
            {
                model.Setup();
                if (model is ModelSystem modelSystem)
                {
                    modelSystem.Link();
                    modelsToRemove.AddRange(modelSystem.models);
                }

                inports.AddRange(model.inports);
                outports.AddRange(model.outports);
            }

            models.RemoveAll(m => modelsToRemove.Contains(m));

            Debug.Log($"Init Successful, found {models.Count} models (s).");

            if (communication != null)
            {
                communication.Init();
            }
        }

        protected void Start()
        {
            Time.fixedDeltaTime = _sampleTime; // Ensure initial setting is applied
            Init();
        }

        protected void FixedUpdate()
        {
            communication.ReceiveAll();

            foreach (Model model in models)
            {
                model.Step();
            }

            communication.SendAll();
        }
    }
}
