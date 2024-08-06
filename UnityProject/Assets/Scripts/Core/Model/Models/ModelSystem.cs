using System;
using System.Collections.Generic;
using System.Linq;

namespace SimuNEX
{
    public class ModelSystem : Model
    {
        public List<Model> models = new();
        public Dictionary<ModelOutput, List<ModelOutput>> outputMappings = new();
        public Dictionary<ModelInput, List<ModelInput>> inputMappings = new();
        public Dictionary<ModelOutput, List<ModelInput>> internalMappings = new();

        protected override ModelFunction modelFunction => throw new ArgumentException("ModelSystems do not have model functions.");

        public override void Step()
        {
            foreach (Model model in models)
            {
                model.Step();
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
                systemOutput.data = modelOutput.data; // Directly assign the reference
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
                modelInput.data = systemInput.data; // Directly assign the reference
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
                modelInput.data = modelOutput.data;
            }
        }

        /// <summary>
        /// Topological sort to determine the order of model processing.
        /// </summary>
        /// <returns>Sorted list of models based on dependencies.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public List<Model> TopologicalSort()
        {
            Dictionary<Model, int> inDegree = new();
            Dictionary<Model, List<Model>> adjList = new();

            foreach (Model model in models)
            {
                inDegree[model] = 0;
                adjList[model] = new List<Model>();
            }

            foreach (KeyValuePair<ModelOutput, List<ModelInput>> mapping in internalMappings)
            {
                foreach (ModelInput modelInput in mapping.Value)
                {
                    Model fromModel = models.First(m => m.outports.Contains(mapping.Key));
                    Model toModel = models.First(m => m.inports.Contains(modelInput));

                    adjList[fromModel].Add(toModel);
                    inDegree[toModel]++;
                }
            }

            Queue<Model> queue = new();
            foreach (Model model in inDegree.Where(kvp => kvp.Value == 0).Select(kvp => kvp.Key))
            {
                queue.Enqueue(model);
            }

            List<Model> sortedModels = new();
            while (queue.Count > 0)
            {
                Model model = queue.Dequeue();
                sortedModels.Add(model);

                foreach (Model dependentModel in adjList[model])
                {
                    inDegree[dependentModel]--;
                    if (inDegree[dependentModel] == 0)
                    {
                        queue.Enqueue(dependentModel);
                    }
                }
            }

            if (sortedModels.Count != models.Count)
            {
                throw new InvalidOperationException("The graph has a cycle.");
            }

            return sortedModels;
        }
    }
}
