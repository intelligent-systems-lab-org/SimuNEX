using UnityEngine;

namespace SimuNEX
{
    public class PropellerModel : Model
    {
        /// <summary>
        /// The <see cref="RBModel"/> body where forces are applied.
        /// </summary>
        public RBModel RB;

        /// <summary>
        /// Location of the propeller mesh.
        /// </summary>
        public Transform spinnerObject;

        /// <summary>
        /// Propeller axis of rotation.
        /// </summary>
        public Direction spinAxis;

        /// <summary>
        /// Speed to thrust factor.
        /// </summary>
        public float thrustCoefficient = 1.88865e-5f;

        /// <summary>
        /// Speed to torque factor.
        /// </summary>
        public float torqueCoefficient = 1.1e-5f;

        public PropellerModel()
        {
            outputs = new
            (
                new ModelOutput[] { new("forces", 6, Signal.Mechanical, this) }
            );

            inputs = new
            (
                new ModelInput[] { new("speed", 1, Signal.Mechanical, this) }
            );
        }

        protected override ModelFunction modelFunction =>
            (ModelInput[] inputs, ModelOutput[] outputs) =>
            {
                float speedProd = inputs[0].data[0] * Mathf.Abs(inputs[0].data[0]);
                Vector3 normal = transform.TransformDirection(spinAxis.ToVector());

                Vector6DOF totalForce = ForceAtPosition(Mathf.Abs(speedProd) * thrustCoefficient * normal, transform.position);

                // use propeller normal for reaction torque direction for now
                totalForce.angular +=  torqueCoefficient * speedProd * normal;

                totalForce = totalForce.Apply(x => Mathf.Abs(x) < 1e-6 ? 0f : x);

                outputs[0].data[0] = totalForce.u;
                outputs[0].data[1] = totalForce.v;
                outputs[0].data[2] = totalForce.w;
                outputs[0].data[3] = totalForce.p;
                outputs[0].data[4] = totalForce.q;
                outputs[0].data[5] = totalForce.r;
            };

        protected Vector6DOF ForceAtPosition(Vector3 force, Vector3 pos) => new()
        {
            linear = force,
            angular = Vector3.Cross(force, RB.transform.InverseTransformPoint(pos))
        };
    }
}
