using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX.Communication
{
    public class COM : MonoBehaviour, IBlock
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

        public void Init(SimuNEX simuNEX)
        {
            modelInputs = simuNEX.Inports.ToArray();
            modelOutputs = simuNEX.Outports.ToArray();

            // test code
            dataInputs = new(new COMInput[] { new("test1", 3, this) });
            dataOutputs = new(new COMOutput[] { new("test1", 3, this) });

            while (streams.Count < 2)
            {
                streams.Add(gameObject.AddComponent<DataStream>());
            }

            streams[0].Setup(
                this,
                new SelfConnect(),
                Streaming.S,
                inputData: dataInputs[0],
                modelOutputs: modelOutputs
                );
            streams[0].Map(new DataMappings { InputIndices = new (int, int)[] { (2, 0), (2, 1), (2, 2) } });

            streams[1].Setup(
                this,
                new SelfConnect(),
                Streaming.R,
                outputData: dataOutputs[0],
                modelInputs: modelInputs
                );
            streams[1].Map(new DataMappings { OutputIndices = new (int, int)[] { (0, 0), (0, 1), (0, 2) } });

            sendStreams = streams.FindAll(m => (m.direction is Streaming.S or Streaming.SR) && m.isMapped);
            receiveStreams = streams.FindAll(m => (m.direction is Streaming.R or Streaming.SR) && m.isMapped);
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
