using System;
using UnityEngine;

namespace SimuNEX.Communication
{
    /// <summary>
    /// A communication entity used to send or receive data in and out of SimuNEX.
    /// </summary>
    public class DataStream : MonoBehaviour
    {
        /// <summary>
        /// Sets the mode of streaming.
        /// </summary>
        /// <remarks>Send (S), Receive (R), Send/Receive (SR)</remarks>
        public Streaming direction;

        /// <summary>
        /// The <see cref="ModelOutput"/> ports the stream uses for access.
        /// </summary>
        [SerializeField]
        private ModelOutput[] modelOutputs;

        /// <summary>
        /// The <see cref="ModelInput"/> ports the stream uses for access.
        /// </summary>
        [SerializeField]
        private ModelInput[] modelInputs;

        /// <summary>
        /// The <see cref="COMInput"/> port this stream associates with.
        /// </summary>
        [SerializeField]
        private COMInput inputData;

        /// <summary>
        /// The <see cref="COMOutput"/> port this stream associates with.
        /// </summary>
        [SerializeField]
        private COMOutput outputData;

        /// <summary>
        /// Maps COM indices to ModelPort indices.
        /// </summary>
        private DataMappings mappings = new();


        /// <summary>
        /// The <see cref="COMProtocol"/> the stream uses for data communication.
        /// </summary>
        [SerializeReference]
        public COMProtocol protocol;

        public void Setup
        (
            COMProtocol protocol,
            Streaming direction,
            COMInput inputData = null,
            COMOutput outputData = null,
            ModelOutput[] modelOutputs = null,
            ModelInput[] modelInputs = null
        )
        {
            // Validate the constraints
            if (inputData == null && outputData == null)
            {
                throw new ArgumentException("inputData and outputData cannot both be null. At least one must be specified.");
            }

            if (inputData != null && (modelOutputs == null || modelOutputs.Length == 0))
            {
                throw new ArgumentException("modelOutputs cannot be null or empty if inputData is specified.");
            }

            if (outputData != null && (modelInputs == null || modelInputs.Length == 0))
            {
                throw new ArgumentException("modelInputs cannot be null or empty if outputData is specified.");
            }

            this.protocol = protocol ?? throw new ArgumentNullException(nameof(protocol));
            this.direction = direction;

            this.inputData = inputData;
            this.outputData = outputData;
            this.modelOutputs = modelOutputs;
            this.modelInputs = modelInputs;
        }

        public void Map(DataMappings mappings) => this.mappings = mappings;

        public void Send()
        {
            for (int i = 0; i < inputData.data.Length; i++)
            {
                (int outputIdx, int subIdx) = mappings.InputIndices[i];
                inputData.data[i] = modelOutputs[outputIdx].data[subIdx];
            }

            protocol.Send(inputData.data);
        }

        public void Receive()
        {
            for (int i = 0; i < outputData.data.Length; i++)
            {
                (int inputIdx, int subIdx) = mappings.OutputIndices[i];
                modelInputs[inputIdx].data[subIdx] = outputData.data[i];
            }

            protocol.Receive(outputData.data);
        }
    }

    [Serializable]
    public struct DataMappings
    {
        public (int, int)[] InputIndices { get; set; }
        public (int, int)[] OutputIndices { get; set; }
    }
}
