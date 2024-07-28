namespace SimuNEX
{
    /// <summary>
    /// Defines behavior where the outputs are a sum of the inputs.
    /// </summary>
    public class Summer : Model
    {
        /// <summary>
        /// Creates a <see cref="Summer"/> model.
        /// </summary>
        public Summer()
        {
            outputs = new
            (
                new ModelOutput[] { new("output") }
            );
        }

        protected override ModelFunction modelFunction =>
            (ModelInput[] inputs, ModelOutput[] outputs) =>
            {
                outputs[0].data[0] = inputs[0].data[0];

                for (int i = 1; i < inputs.Length; ++i)
                {
                    outputs[0].data[0] += inputs[i].data[0];
                }
            };

        /// <summary>
        /// Adds inputs to the <see cref="Summer"/>.
        /// </summary>
        /// <param name="inputs">Inputs to add.</param>
        public void Add(params ModelInput[] inputs)
        {
            this.inputs.AddRange(inputs);
        }
    }
}
