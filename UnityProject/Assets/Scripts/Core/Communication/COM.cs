using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX.Communication
{
    public class COM : MonoBehaviour, IBlock
    {
        public SimuNEX simunex;

        [SerializeReference]
        public List<DataStream> streams = new();

        [SerializeReference]
        private List<DataStream> sendStreams = new();

        [SerializeReference]
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
            sendStreams = streams.FindAll(m => m.streaming == Streaming.S || m.streaming == Streaming.SR);
            receiveStreams = streams.FindAll(m => m.streaming == Streaming.R || m.streaming == Streaming.SR);
        }

        protected void OnValidate()
        {
            if (simunex != null)
            {
                modelPorts = simunex.ports;
            }

            // Test code
            if (streams.Count == 0)
            {
                dataInputs.Add(new("test1", 1, this));
                dataInputs.Add(new("test2", 1, this));

                streams.Add(gameObject.AddComponent<DataStream>());
                streams.Add(gameObject.AddComponent<DataStream>());
                streams[0].Setup(new SelfConnect(), Streaming.S, dataInputs[0], modelPorts.Item2, new (int, int)[] { (1, 1) });
                streams[1].Setup(new SelfConnect(), Streaming.SR, dataInputs[1], modelPorts.Item2, new (int, int)[] { (1, 1) });
            }

            Init();
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
