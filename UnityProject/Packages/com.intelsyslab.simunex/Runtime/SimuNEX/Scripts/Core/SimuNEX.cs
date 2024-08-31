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
        private float _tickRate = 0.02f;

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
        public float TickRate
        {
            get => _tickRate;

            set
            {
                _tickRate = Mathf.Clamp(value, 0.001f, 0.1f);
                Time.fixedDeltaTime = _tickRate;
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

            AdjustSampleTimes();

            // Filter out models contained in ModelSystem instances
            List<Model> modelsToRemove = new();

            foreach (Model model in models)
            {
                model.Setup();

                if (model is IModelInitialization modelInitialization)
                {
                    modelInitialization.Init();
                }

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

            InitializeSimulation();
        }

        protected void Start()
        {
            Time.fixedDeltaTime = _tickRate; // Ensure initial setting is applied
            Init();
        }

        protected void FixedUpdate() => simulateStep();

        public delegate void SimulationStep();

        private SimulationStep simulateStep;

        private void InitializeSimulation()
        {
            if (communication == null)
            {
                simulateStep = () =>
                {
                    foreach (Model model in models)
                    {
                        model.Step(TickRate);
                    }
                };
            }
            else
            {
                communication.Init();

                simulateStep = () =>
                {
                    communication.ReceiveAll();

                    foreach (Model model in models)
                    {
                        model.Step(TickRate);
                    }

                    communication.SendAll();
                };
            }
        }

        /// <summary>
        /// Adjusts the sample time of each model to ensure it's a valid multiple of the SimuNEX sample time.
        /// </summary>
        private void AdjustSampleTimes()
        {
            foreach (Model model in models)
            {
                float modelSampleTime = model.sampleTime;

                // Saturate if the model's sample time exceeds SimuNEX's sample time
                if (modelSampleTime < TickRate)
                {
                    model.sampleTime = TickRate;
                }
                else
                {
                    // Round to the closest higher multiple of SimuNEX's sample time
                    float multiplier = Mathf.Ceil(modelSampleTime / TickRate);
                    model.sampleTime = multiplier * TickRate;
                }
            }
        }
    }
}
