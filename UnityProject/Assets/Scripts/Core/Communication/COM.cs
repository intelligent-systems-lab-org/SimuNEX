using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX.Communication
{
    public class COM : MonoBehaviour, IBlock
    {
        public SimuNEX simunex;

        [SerializeReference]
        public List<DataStream> streams = new();

        private List<DataStream> sendStreams = new();
        private List<DataStream> receiveStreams = new();

        public List<COMInput> dataInputs = new();
        public List<COMOutput> dataOutputs = new();
        public (ModelInput[], ModelOutput[]) modelPorts;

        public void AddPort(string name, bool isInput, int size = 1, int copies = 1)
        {
            if (copies == 1)
            {
                if (isInput)
                {
                    dataInputs.Add(new COMInput(name, size, this));
                }
                else
                {
                    dataOutputs.Add(new COMOutput(name, size, this));
                }
            }
            else
            {
                for (int i = 0; i < copies; i++)
                {
                    if (isInput)
                    {
                        dataInputs.Add(new COMInput($"{name}_{i + 1}", size, this));
                    }
                    else
                    {
                        dataOutputs.Add(new COMOutput($"{name}_{i + 1}", size, this));
                    }
                }
            }
        }

        public void Init()
        {
            sendStreams = streams.FindAll(m => m.direction is Streaming.S or Streaming.SR);
            receiveStreams = streams.FindAll(m => m.direction is Streaming.R or Streaming.SR);
        }

        protected void OnValidate()
        {
            if (simunex != null)
            {
                modelPorts = simunex.ports;
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
