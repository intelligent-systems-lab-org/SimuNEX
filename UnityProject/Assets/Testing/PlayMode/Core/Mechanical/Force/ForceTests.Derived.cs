using SimuNEX;
using SimuNEX.Mechanical;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace ForceTests
{
    public class ConstantForceTest : ForceTests<ContinuousForce, RigidBody>
    {
        protected override Vector6DOF expectedValue => forceInstance.forces.ToICF(rigidBody.transform);

        protected override void SetProperties()
        {
            rigidBody.transform.rotation = Quaternion.Euler(0, 45, 0);
            forceInstance.forces = new(1, 2, 3, 4, 5, 6);
        }

        [UnityTest]
        public virtual IEnumerator TestForceICF()
        {
            forceInstance.referenceFrame = CoordinateFrame.ICF;
            rigidBody.Step();

            yield return null;

            _ = IsForceApplied(forceInstance.forces);
        }
    }

    public class LinearDragTest : ForceTests<LinearDrag, RigidBody>
    {
        protected override Vector6DOF expectedValue => forceInstance.dragCoefficients * rigidBody.velocity * -1;

        protected override void SetProperties()
        {
            forceInstance.dragCoefficients = Matrix6DOF.CreateMassMatrix(1, Vector3.one);
        }
    }

    public class QuadraticDragTest : ForceTests<QuadraticDrag, RigidBody>
    {
        protected override Vector6DOF expectedValue
            => forceInstance.dragCoefficients * rigidBody.velocity.Apply(v => Mathf.Abs(v) * v) * -1;

        protected override void SetProperties()
        {
            forceInstance.dragCoefficients = Matrix6DOF.CreateMassMatrix(1, Vector3.one);
        }
    }

    public class SimpleGravityTest : ForceTests<SimpleGravity, RigidBody>
    {
        protected override Vector6DOF expectedValue
        {
            get
            {
                Vector3 force = new(0, -rigidBody.mass * forceInstance.acceleration, 0);
                Vector3 torque = Vector3.Cross(
                    force,
                    rigidBody.transform.InverseTransformPoint(forceInstance.point.position));
                return new(force, torque);
            }
        }

        protected override void SetProperties()
        {
            forceInstance.acceleration = 10f;
        }
    }

    public class SimpleBuoyancyTest : ForceTests<SimpleBuoyancy, RigidBodyF>
    {
        protected override Vector6DOF expectedValue
        {
            get
            {
                Vector3 force =
            (forceInstance.fluidDensity
                * forceInstance.simpleGravity.acceleration
                * rigidBody._displacedVolumeFactor
                * rigidBody._volume
                * Vector3.up)
                + (forceInstance.simpleGravity.weight * Vector3.down);

                Vector3 torque = Vector3.Cross(
                    force,
                    rigidBody.transform.InverseTransformPoint(forceInstance.point.position));

                return new(force, torque);
            }
        }

        protected override void SetProperties()
        {
            forceInstance.fluidDensity = 1000f;
        }
    }
}
