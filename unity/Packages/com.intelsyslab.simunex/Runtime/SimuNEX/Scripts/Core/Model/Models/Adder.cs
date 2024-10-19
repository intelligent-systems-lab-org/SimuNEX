using System;

namespace SimuNEX
{
    /// <summary>
    /// Defines behavior where the outputs are a sum of the inputs.
    /// </summary>
    public class Adder : Model
    {
        private int _dataSize = 1;

        public int size
        {
            get => _dataSize;

            set
            {
                _dataSize = value;
                outputs = new
                (
                    new ModelOutput[] { new("output", _dataSize, outputs[0].signal, this) }
                );
            }
        }

        /// <summary>
        /// Creates a <see cref="Adder"/> model.
        /// </summary>
        public Adder()
        {
            outputs = new
            (
                new ModelOutput[] { new("output", _dataSize, Signal.Virtual, this) }
            );
        }

        protected override ModelFunction modelFunction =>
            (ModelInput[] inputs, ModelOutput[] outputs) =>
            {
                outputs[0].Clear(); // Clear the output data array

                for (int i = 0; i < inputs.Length; ++i)
                {
                    for (int j = 0; j < outputs[0].size; ++j)
                    {
                        outputs[0][j] += inputs[i][j];
                    }
                }
            };

        /// <summary>
        /// Adds inputs to the <see cref="Adder"/>.
        /// </summary>
        /// <param name="inputs">Inputs to add.</param>
        /// <exception cref="ArgumentException">Thrown when input dimensions do not match output dimensions.</exception>
        public void Create(params string[] inputs)
        {
            this.inputs = new();

            foreach (string input in inputs)
            {
                this.inputs.Add(new(input, size, Signal.Virtual, this));
            }
        }
    }
}
