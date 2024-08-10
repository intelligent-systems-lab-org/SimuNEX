using ROS2;
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

            dataInputs = new
            (
                new COMInput[] { new("inputs", 17, this) }
            );

            dataOutputs = new
            (
                new COMOutput[] { new("outputs", 4, this) }
            );

            streams = new() { gameObject.AddComponent<DataStream>() };
            streams[0].Setup
            (
                this,
                new ROS2(),
                Streaming.SR,
                dataInputs[0],
                dataOutputs[0],
                new ModelOutput[] {
                    modelOutputs[9],// velocity
                    modelOutputs[10], // angular velocity
                    modelOutputs[11], // position
                    modelOutputs[12], // angular position
                    modelOutputs[0], // force_BL
                    modelOutputs[1], // force_BR
                    modelOutputs[2], // force_FL
                    modelOutputs[3] }, // force_FR
                new ModelInput[] { modelInputs[5], modelInputs[6], modelInputs[7], modelInputs[8] }
            );

            if (streams[0].protocol is ROS2 ros2)
            {
                ros2.Component = GetComponent<ROS2UnityComponent>();
                ros2.Initialize();
            }

            streams[0].Map
            (
                new DataMappings
                {
                    InputIndices = new (int, int)[]
                    {
                        (0, 0),
                        (0,  1),
                        (0, 2),
                        (1, 0),
                        (1, 1),
                        (1, 2),
                        (2, 0),
                        (2, 1),
                        (2, 2),
                        (3, 0),
                        (3, 1),
                        (3, 2),
                        (3, 3),
                        (4, 5),
                        (5, 5),
                        (6, 5),
                        (7, 5)
                    },
                    OutputIndices = new (int, int)[]
                    {
                        (0, 0), (1, 0), (2, 0), (3, 0)
                    }
                }
            );

            sendStreams = streams.FindAll(m => (m.direction is Streaming.S or Streaming.SR) && m.isMapped && m.enabled);
            receiveStreams = streams.FindAll(m => (m.direction is Streaming.R or Streaming.SR) && m.isMapped && m.enabled);
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
