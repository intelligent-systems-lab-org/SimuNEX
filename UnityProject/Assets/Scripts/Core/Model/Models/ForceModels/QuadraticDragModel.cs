using UnityEngine;

namespace SimuNEX
{
    public class QuadraticDragModel : LinearDragModel
    {
        protected override ModelFunction modelFunction =>
            (ModelInput[] inputs, ModelOutput[] outputs) =>
            {
                _velocities.u = inputs[0].data[0];
                _velocities.v = inputs[0].data[1];
                _velocities.w = inputs[0].data[2];
                _velocities.p = inputs[0].data[3];
                _velocities.q = inputs[0].data[4];
                _velocities.r = inputs[0].data[5];

                _forces = dragCoefficients * _velocities.Apply(v => Mathf.Abs(v) * v) * -1;

                outputs[0].data[0] = _forces.u;
                outputs[0].data[1] = _forces.v;
                outputs[0].data[2] = _forces.w;
                outputs[0].data[3] = _forces.p;
                outputs[0].data[4] = _forces.q;
                outputs[0].data[5] = _forces.r;
            };
    }
}
