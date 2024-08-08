using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX.Communication
{
    /// <summary>
    /// A communication entity used to send or receive data in and out of SimuNEX.
    /// </summary>
    public class DataStream : MonoBehaviour
    {
        /// <summary>
        /// The communication system this stream is managed by.
        /// </summary>
        public COM communication;

        /// <summary>
        /// Sets the mode of streaming.
        /// </summary>
        /// <remarks>Send (S), Receive (R), Send/Receive (SR)</remarks>
        public Streaming direction;

        /// <summary>
        /// Maps stream outputs to COM outputs.
        /// </summary>
        private readonly Dictionary<int, int> _comOutputMappings = new();

        /// <summary>
        /// Maps stream inputs to COM inputs.
        /// </summary>
        private readonly Dictionary<int, int> _comInputMappings = new();


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

        public bool isMapped => direction switch
        {
            Streaming.S => mappings.InputIndices != null,
            Streaming.R => mappings.OutputIndices != null,
            _ => mappings.OutputIndices != null && mappings.InputIndices != null
        };

        /// <summary>
        /// Setups the <see cref="DataStream"/> with the given settings.
        /// </summary>
        /// <param name="communication">The component used to manage the stream.</param>
        /// <param name="protocol">The protocol used to handle communcation.</param>
        /// <param name="direction">The direction of streaming.</param>
        /// <param name="inputData">The input port this stream associates with.</param>
        /// <param name="outputData">The output port this stream associates with.</param>
        /// <param name="modelOutputs">The model output ports the stream uses for access.</param>
        /// <param name="modelInputs">The model input ports the stream uses for access.</param>
        /// <exception cref="ArgumentException">Throws if required ports are missing.</exception>
        public void Setup
        (
            COM communication,
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

            this.communication = communication;
            this.protocol = protocol;
            this.direction = direction;

            this.inputData = inputData;
            this.outputData = outputData;

            if (modelOutputs != null)
            {
                for (int i = 0; i < modelOutputs.Length; i++)
                {
                    _comOutputMappings[i] = Array.IndexOf(communication.modelOutputs, modelOutputs[i]);
                }
            }

            if (modelInputs != null)
            {
                for (int i = 0; i < modelInputs.Length; i++)
                {
                    _comInputMappings[i] = Array.IndexOf(communication.modelInputs, modelInputs[i]);
                }
            }
        }

        public void Map(DataMappings mappings) => this.mappings = mappings;

        public void Send()
        {
            for (int i = 0; i < inputData.data.Length; i++)
            {
                (int outputIdx, int subIdx) = mappings.InputIndices[i];
                inputData.data[i] = communication.modelOutputs[_comOutputMappings[outputIdx]].data[subIdx];
            }

            protocol.Send(inputData.data);
        }

        public void Receive()
        {
            protocol.Receive(outputData.data);

            for (int i = 0; i < outputData.data.Length; i++)
            {
                (int inputIdx, int subIdx) = mappings.OutputIndices[i];
                communication.modelInputs[_comInputMappings[inputIdx]].data[subIdx] = outputData.data[i];
            }
        }
    }

    [Serializable]
    public struct DataMappings
    {
        public (int, int)[] InputIndices { get; set; }
        public (int, int)[] OutputIndices { get; set; }
    }
}
