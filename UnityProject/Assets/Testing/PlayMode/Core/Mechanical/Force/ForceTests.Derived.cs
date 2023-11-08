using SimuNEX;
using SimuNEX.Mechanical.Forces;

namespace ForceTests
{
    public class ConstantForceTest : ForceTests<ContinuousForce>
    {
        protected override Vector6DOF expectedValue => forceInstance.forces;

        protected override void SetProperties()
        {
            forceInstance.forces = new(1, 2, 3, 4, 5, 6);
        }
    }
}
