using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX
{
    public abstract class Model : MonoBehaviour, IModel
    {
        /// <summary>
        /// Returns all output ports.
        /// </summary>
        public IModelOutput[] outports => outputs.ToArray();

        /// <summary>
        /// Returns all input ports.
        /// </summary>
        public IModelInput[] inports => inputs.ToArray();

        /// <summary>
        /// The outputs of the <see cref="Model"/>.
        /// </summary>
        protected List<IModelOutput> outputs = new();

        /// <summary>
        /// The inputs to the <see cref="Model"/>.
        /// </summary>
        protected List<IModelInput> inputs = new();

        /// <summary>
        /// Models the relationship between the model's inputs and outputs.
        /// </summary>
        /// <param name="inputs">The inputs (read-only) to the function.</param>
        /// <param name="outputs">The outputs (read and write) to the function.</param>
        public delegate void ModelFunction(IModelInput[] inputs, IModelOutput[] outputs);

        /// <summary>
        /// Defines the relationship of the <see cref="Model"/> with respect to its inputs and outputs.
        /// </summary>
        protected abstract ModelFunction modelFunction { get; }

        /// <summary>
        /// Gets a test model for testing .
        /// </summary>
        /// <returns>TestModel instance.</returns>
        public TestModel GetTestModel()
        {
            return new TestModel(this);
        }

        public class TestModel
        {
            private readonly Model model;

            internal TestModel(Model model)
            {
                this.model = model;
            }

            /// <summary>
            /// Tests the model function.
            /// </summary>
            public void TestModelFunction()
            {
                model.modelFunction(model.inports, model.outports);
            }
        }
    }
}
