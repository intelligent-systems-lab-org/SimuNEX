using System.Collections.Generic;

namespace SimuNEX.Examples
{
    public class Multicopter : ModelSystem
    {
        public RBModel RB;
        public Adder adder;
        public List<PropellerModel> propellers;

        public Multicopter()
        {
            outputs = new
            (
                new ModelOutput[]
                    {
                        new("velocity", 3, Signal.Mechanical, this),
                        new("angular_velocity", 3,Signal.Mechanical, this),
                        new("position", 3, Signal.Mechanical, this),
                        new("angular_position", 4, Signal.Mechanical, this)
                    }
            );

            inputs = new
            (
                new ModelInput[]
                {
                    new("speed_BL", 1, Signal.Mechanical, this),
                    new("speed_BR", 1, Signal.Mechanical, this),
                    new("speed_FL", 1, Signal.Mechanical, this),
                    new("speed_FR", 1, Signal.Mechanical, this)
                }
            );
        }

        public override void Link()
        {
            models = new();
            models.AddRange(propellers);
            models.AddRange(new Model[] { adder, RB });

            adder.size = 6;

            adder.Create(new string[] { "force_BL", "force_BR", "force_FL", "force_BR" });

            outputMappings = new();
            inputMappings = new();
            internalMappings = new();

            // speed(s) => speed(s)
            MapInput(inputs[0], models[0].inports[0]);
            MapInput(inputs[1], models[1].inports[0]);
            MapInput(inputs[2], models[2].inports[0]);
            MapInput(inputs[3], models[3].inports[0]);

            // force(s) => force(s)
            MapInternal(models[0].outports[0], models[4].inports[0]);
            MapInternal(models[1].outports[0], models[4].inports[1]);
            MapInternal(models[2].outports[0], models[4].inports[2]);
            MapInternal(models[3].outports[0], models[4].inports[3]);

            // sum_outputs => forces
            MapInternal(models[4].outports[0], models[5].inports[0]);

            // velocity <= velocity
            MapOutput(models[5].outports[0], outputs[0]);

            // angular_velocity <= angular_velocity
            MapOutput(models[5].outports[1], outputs[1]);

            // position <= position
            MapOutput(models[5].outports[2], outputs[2]);

            // angular_position <= angular_position
            MapOutput(models[5].outports[3], outputs[3]);

            base.Link();
        }
    }
}
