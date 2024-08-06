using System.Collections.Generic;
using System;
using UnityEngine;

namespace SimuNEX.Communication
{
    public class DataStream : MonoBehaviour
    {
        [SerializeField]
        private ModelOutput[] modelOutputs;

        [SerializeField]
        private COMInput inputData;

        private readonly Dictionary<int, (int, int)> inputMappings = new();

        [SerializeReference]
        public COMProtocol protocol;

        public Streaming streaming;

        public void Setup(COMProtocol protocol, Streaming streaming, COMInput inputData, ModelOutput[] modelOutputs, (int, int)[] indices)
        {
            this.protocol = protocol;
            this.modelOutputs = modelOutputs;
            this.inputData = inputData;
            this.streaming = streaming;

            for (int i = 0; i < indices.Length; i++)
            {
                inputMappings[i] = indices[i];
            }
        }

        public void Send()
        {
            for (int i = 0; i < inputData.data.Length; i++)
            {
                (int outputIdx, int subIdx) = inputMappings[i];
                inputData.data[i] = modelOutputs[outputIdx].data[subIdx];
            }

            protocol.Send(inputData.data);
        }

        public void Receive() => throw new NotImplementedException();
    }
}
