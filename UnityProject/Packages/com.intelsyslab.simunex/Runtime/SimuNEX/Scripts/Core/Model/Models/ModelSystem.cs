using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimuNEX
{
    public class ModelSystem : Model
    {
        public List<Model> models = new();
        public Dictionary<ModelOutput, List<ModelOutput>> outputMappings = new();
        public Dictionary<ModelInput, List<ModelInput>> inputMappings = new();
        public Dictionary<ModelOutput, List<ModelInput>> internalMappings = new();

        protected override ModelFunction modelFunction => throw new ArgumentException("ModelSystems do not have model functions.");

        public virtual void Link()
        {
            // Assign references for output mappings
            foreach (KeyValuePair<ModelOutput, List<ModelOutput>> kvp in outputMappings)
            {
                foreach (ModelOutput systemOutput in kvp.Value)
                {
                    systemOutput.data = kvp.Key.data;
                }
            }

            // Assign references for input mappings
            foreach (KeyValuePair<ModelInput, List<ModelInput>> kvp in inputMappings)
            {
                foreach (ModelInput modelInput in kvp.Value)
                {
                    modelInput.data = kvp.Key.data;
                }
            }

            // Assign references for internal mappings
            foreach (KeyValuePair<ModelOutput, List<ModelInput>> kvp in internalMappings)
            {
                foreach (ModelInput modelInput in kvp.Value)
                {
                    modelInput.data = kvp.Key.data;
                }
            }
        }

        public override void Step(float tickRate = Mathf.Infinity)
        {
            foreach (Model model in models)
            {
                model.Step(tickRate);
            }
        }

        public void AddPorts(params ModelPort[] ports)
        {
            foreach(ModelPort port in ports)
            {
                if (port is  ModelInput input)
                {
                    inputs.Add(input);
                }
                else if (port is ModelOutput output)
                {
                    outputs.Add(output);
                }
            }
        }

        private bool IsPartOfModelSystem(ModelPort port)
        {
            return models.SelectMany(model => model.inports).Contains(port)
                || models.SelectMany(model => model.outports).Contains(port);
        }

        private bool IsPartOfThisSystem(ModelPort port)
        {
            return inputs.Contains(port) || outputs.Contains(port);
        }

        /// <summary>
        /// Map a model output to one or more system outputs.
        /// </summary>
        /// <param name="modelOutput">A output port of a <see cref="Model"/> belonging to the <see cref="ModelSystem"/>.</param>
        /// <param name="systemOutputs">An outport port belong to the <see cref="ModelSystem"/>.</param>
        /// <exception cref="InvalidOperationException">Throws if an output of the <see cref="ModelSystem"/> is already mapped or sizes do not match.</exception>
        public void MapOutput(ModelOutput modelOutput, params ModelOutput[] systemOutputs)
        {
            if (!IsPartOfModelSystem(modelOutput))
            {
                throw new InvalidOperationException("The model output does not belong to this ModelSystem.");
            }

            foreach (ModelOutput systemOutput in systemOutputs)
            {
                if (!IsPartOfThisSystem(systemOutput))
                {
                    throw new InvalidOperationException("The system output does not belong to this ModelSystem.");
                }

                if (systemOutput.size != modelOutput.size)
                {
                    throw new InvalidOperationException("The output sizes do not match.");
                }

                // Ensure system output is not already mapped
                if (outputMappings.Any(kvp => kvp.Value.Contains(systemOutput)))
                {
                    throw new InvalidOperationException("This system output is already mapped to another model output.");
                }

                if (!outputMappings.ContainsKey(modelOutput))
                {
                    outputMappings[modelOutput] = new List<ModelOutput>();
                }

                outputMappings[modelOutput].Add(systemOutput);
            }
        }

        /// <summary>
        /// Unmap a model output from all system outputs.
        /// </summary>
        /// <param name="modelOutput">A output port of a <see cref="Model"/> belonging to the <see cref="ModelSystem"/>.</param>
        /// <exception cref="InvalidOperationException">Throws if <paramref name="modelOutput"/> does not belong to this <see cref="ModelSystem"/>.</exception>
        public void UnmapOutput(ModelOutput modelOutput)
        {
            if (!IsPartOfModelSystem(modelOutput))
            {
                throw new InvalidOperationException("The model output does not belong to this ModelSystem.");
            }

            if (outputMappings.ContainsKey(modelOutput))
            {
                foreach (ModelOutput systemOutput in outputMappings[modelOutput])
                {
                    systemOutput.data = new float[systemOutput.data.Length]; // Clear the reference
                }

                _ = outputMappings.Remove(modelOutput);
            }
        }

        /// <summary>
        /// Map a system input to one or more model inputs.
        /// </summary>
        /// <param name="systemInput">An input port belonging to the <see cref="ModelSystem"/>.</param>
        /// <param name="modelInputs">Input ports belonging to the <see cref="Model"/>s within the <see cref="ModelSystem"/>.</param>
        /// <exception cref="InvalidOperationException">Throws if a model input is already mapped or sizes do not match.</exception>
        public void MapInput(ModelInput systemInput, params ModelInput[] modelInputs)
        {
            if (!IsPartOfThisSystem(systemInput))
            {
                throw new InvalidOperationException("The system input does not belong to this ModelSystem.");
            }

            foreach (ModelInput modelInput in modelInputs)
            {
                if (!IsPartOfModelSystem(modelInput))
                {
                    throw new InvalidOperationException("The model input does not belong to any Model within this ModelSystem.");
                }

                if (systemInput.size != modelInput.size)
                {
                    throw new InvalidOperationException("The input sizes do not match.");
                }

                // Ensure model input is not already mapped
                if (inputMappings.Any(kvp => kvp.Value.Contains(modelInput)) 
                    || internalMappings.Any(kvp => kvp.Value.Contains(modelInput)))
                {
                    throw new InvalidOperationException("This model input is already mapped to another system input or model output.");
                }

                if (!inputMappings.ContainsKey(systemInput))
                {
                    inputMappings[systemInput] = new List<ModelInput>();
                }

                inputMappings[systemInput].Add(modelInput);
            }
        }

        /// <summary>
        /// Unmap a system input from all model inputs.
        /// </summary>
        /// <param name="systemInput">An input port belonging to the <see cref="ModelSystem"/>.</param>
        /// <exception cref="InvalidOperationException">Throws if <paramref name="modelOutput"/> does not belong to this <see cref="ModelSystem"/>.</exception>
        public void UnmapInput(ModelInput systemInput)
        {
            if (!IsPartOfThisSystem(systemInput))
            {
                throw new InvalidOperationException("The system input does not belong to this ModelSystem.");
            }

            if (inputMappings.ContainsKey(systemInput))
            {
                foreach (ModelInput modelInput in inputMappings[systemInput])
                {
                    modelInput.data = new float[modelInput.data.Length]; // Clear the reference
                }

                _ = inputMappings.Remove(systemInput);
            }
        }

        /// <summary>
        /// Map an internal model output to one or more model inputs.
        /// </summary>
        /// <param name="modelOutput">An output port belonging to a <see cref="Model"/>s within the <see cref="ModelSystem"/>.</param>
        /// <param name="modelInputs">Input ports belonging to the <see cref="Model"/>s within the <see cref="ModelSystem"/>.</param>
        /// <exception cref="InvalidOperationException">Throws if a model input is already mapped or sizes do not match.</exception>
        public void MapInternal(ModelOutput modelOutput, params ModelInput[] modelInputs)
        {
            if (!IsPartOfModelSystem(modelOutput))
            {
                throw new InvalidOperationException("The model output does not belong to any Model within this ModelSystem.");
            }

            foreach (ModelInput modelInput in modelInputs)
            {
                if (!IsPartOfModelSystem(modelInput))
                {
                    throw new InvalidOperationException("The model input does not belong to any Model within this ModelSystem.");
                }

                if (modelOutput.size != modelInput.size)
                {
                    throw new InvalidOperationException("The input and output sizes do not match.");
                }

                // Ensure model input is not already mapped
                if (inputMappings.Any(kvp => kvp.Value.Contains(modelInput))
                    || internalMappings.Any(kvp => kvp.Value.Contains(modelInput)))
                {
                    throw new InvalidOperationException("This model input is already mapped to another system input or model output.");
                }

                if (!internalMappings.ContainsKey(modelOutput))
                {
                    internalMappings[modelOutput] = new List<ModelInput>();
                }

                internalMappings[modelOutput].Add(modelInput);
            }
        }
    }
}
