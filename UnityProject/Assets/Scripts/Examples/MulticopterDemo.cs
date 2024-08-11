using SimuNEX.Communication;
using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX.Examples
{
    [RequireComponent(typeof(SimuNEX))]
    [RequireComponent(typeof(Rigidbody))]
    public class MulticopterDemo : SimuNEXDemo
    {
        [Header("Locations")]
        public GameObject Systems;

        public GameObject Network;
        private Multicopter _multicopter;
        private COM _com;

        public override void Init()
        {
            if (Systems != null)
            {
                _multicopter = Systems.TryGetComponent(out Multicopter multicopter) ? multicopter
                    : Systems.AddComponent<Multicopter>();
                _multicopter.Link();
            }

            if (Network != null)
            {
                _com = Network.TryGetComponent(out COM com) ? com : Network.AddComponent<COM>();

                List<ModelOutput> modelOutputs = new(_multicopter.outports)
                {
                    _multicopter.models[0].outports[0],
                    _multicopter.models[1].outports[0],
                    _multicopter.models[2].outports[0],
                    _multicopter.models[3].outports[0]
                };

                _com.modelInputs = _multicopter.inports;
                _com.modelOutputs = modelOutputs.ToArray();

                _com.dataInputs = new
                (
                    new COMInput[] { new("inputs", 17) }
                );

                _com.dataOutputs = new
                (
                    new COMOutput[] { new("outputs", 4) }
                );

                DataStream dataStream = Network.TryGetComponent(out DataStream ds) ? ds : Network.AddComponent<DataStream>();
                COMProtocol protocol = dataStream.protocol ?? new SelfConnect();

                dataStream.Setup
                (
                    _com,
                    Streaming.SR,
                    _com.dataInputs[0],
                    _com.dataOutputs[0],
                    new ModelOutput[] {
                        _com.modelOutputs[0], // force_BL
                        _com.modelOutputs[1], // force_BR
                        _com.modelOutputs[2], // force_FL
                        _com.modelOutputs[3], // force_FR
                        _com.modelOutputs[4],// velocity
                        _com.modelOutputs[5], // angular velocity
                        _com.modelOutputs[6], // position
                        _com.modelOutputs[7] // angular position
                    },
                    new ModelInput[] { _com.modelInputs[0], _com.modelInputs[1], _com.modelInputs[2], _com.modelInputs[3] },
                    protocol
                );

                dataStream.Map
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
            }

            Debug.Log("Demo Setup Successful.");
        }
    }
}
