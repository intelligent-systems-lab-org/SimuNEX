namespace SimuNEX.Mechanical
{
    public class LinearDragModel : Model
    {
        /// <summary>
        /// Drag coefficients defined as a <see cref="Matrix6DOF"/>,
        /// where each row refers to a _force applied to a DOF and each column refers to a velocity DOF.
        /// </summary>
        public Matrix6DOF dragCoefficients;

        protected Vector6DOF _velocities = new();
        protected Vector6DOF _forces = new();

        public LinearDragModel()
        {
            outputs = new
            (
                new ModelOutput[] { new("drag_forces", 6, Signal.Mechanical, this) }
            );

            inputs = new
            (
                new ModelInput[] { new("body_velocities", 6, Signal.Mechanical, this) }
            );
        }

        protected override ModelFunction modelFunction =>
            (ModelInput[] inputs, ModelOutput[] outputs) =>
            {
                _velocities.u = inputs[0][0];
                _velocities.v = inputs[0][1];
                _velocities.w = inputs[0][2];
                _velocities.p = inputs[0][3];
                _velocities.q = inputs[0][4];
                _velocities.r = inputs[0][5];

                _forces = dragCoefficients * _velocities * -1;

                outputs[0][0] = _forces.u;
                outputs[0][1] = _forces.v;
                outputs[0][2] = _forces.w;
                outputs[0][3] = _forces.p;
                outputs[0][4] = _forces.q;
                outputs[0][5] = _forces.r;
            };
    }
}
