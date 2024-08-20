using System.Collections.Generic;
using System;
using UnityEngine;

namespace SimuNEX.Communication
{
    public class COM : MonoBehaviour
    {
        public List<DataStream> streams = new();
        private List<DataStream> sendStreams = new();
        private List<DataStream> receiveStreams = new();

        public List<COMInput> dataInputs = new();
        public List<COMOutput> dataOutputs = new();

        public ModelInput[] modelInputs;
        public ModelOutput[] modelOutputs;

        public void AddPort(string name, bool isInput, int size = 1, int copies = 1)
        {
            if (copies == 1)
            {
                if (isInput)
                {
                    dataInputs.Add(new COMInput(name, size));
                }
                else
                {
                    dataOutputs.Add(new COMOutput(name, size));
                }
            }
            else
            {
                for (int i = 0; i < copies; i++)
                {
                    if (isInput)
                    {
                        dataInputs.Add(new COMInput($"{name}_{i + 1}", size));
                    }
                    else
                    {
                        dataOutputs.Add(new COMOutput($"{name}_{i + 1}", size));
                    }
                }
            }
        }

        public void Init()
        {
            streams = new(GetComponents<DataStream>());

            dataInputs = new();
            dataOutputs = new();

            foreach (DataStream stream in streams)
            {
                if (stream.protocol is IProtocolInitialization protocol)
                {
                    protocol.Initialize();
                }

                if (stream.direction is Streaming.S or Streaming.SR)
                {
                    dataInputs.Add(stream.inputData);
                }

                if (stream.direction is Streaming.S or Streaming.SR)
                {
                    dataOutputs.Add(stream.outputData);
                }
            }

            LinkPorts(modelInputs, m => m.inports);
            LinkPorts(modelOutputs, m => m.outports);

            sendStreams = streams.FindAll(m => (m.direction is Streaming.S or Streaming.SR) && m.isMapped && m.enabled);
            receiveStreams = streams.FindAll(m => (m.direction is Streaming.R or Streaming.SR) && m.isMapped && m.enabled);
        }

        private void LinkPorts<T>(T[] ports, Func<Model, T[]> getPorts) where T : ModelPort
        {
            for (int i = 0; i < ports.Length; i++)
            {
                T port = ports[i];
                Model model = port.connectedModel;

                if (model != null)
                {
                    T matched = Array.Find(getPorts(model), p => p.name == port.name && p.connectedModel == port.connectedModel);

                    if (matched != null)
                    {
                        port.data = matched.data;
                    }
                }
            }
        }

        public void SendAll()
        {
            foreach (DataStream dataStream in sendStreams)
            {
                dataStream.Send();
            }
        }

        public void ReceiveAll()
        {
            foreach (DataStream dataStream in receiveStreams)
            {
                dataStream.Receive();
            }
        }
    }
}
