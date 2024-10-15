using SimuNEX.Dynamics;

namespace SimuNEX.Mechanical
{
    public class DCMotorModel : DynamicModel, IModelInitialization
    {
        public float dcGain = 1.0f;
        public float timeConstant = 0.5f;

        public DCMotorModel()
        {
            outputs = new
            (
                new ModelOutput[] { new("speed", 1, Signal.Mechanical, this) }
            );

            inputs = new
            (
                new ModelInput[] { new("voltages", 1, Signal.Electrical, this) }
            );
        }

        public void Init()
        {
            stateSpace = new FirstOrderTF(timeConstant, dcGain);
        }

        protected override OutputFunction outputFunction =>
            (ModelInput[] _, ModelOutput[] outputs, StateSpace states) => outputs[0][0] = states.states[0, 0];

        protected override InputFunction inputFunction =>
            (ModelInput[] inputs, StateSpace states) => states.inputs[0, 0] = inputs[0][0];
    }
}
