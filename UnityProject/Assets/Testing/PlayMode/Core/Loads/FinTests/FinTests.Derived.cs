using SimuNEX;
using SimuNEX.Loads;
using UnityEngine;

namespace LoadTests.Fins
{
    public class SimpleFinTests : FinTests<SimpleFin, SimpleFin.SimpleFinForce>
    {
        public override void InitializeFin()
        {
            testFin.forceCoefficient = 1.5f;
            testFin.torqueCoefficient = 0.5f;
            testFin.flowDirection = Direction.Forward;
            testFin.spinAxis = Direction.Right;
            testFin.normalizedAngle = Mathf.PI / 4;

            sharedRigidBody.initialVelocity.linear = new Vector3(0, 0, 20f);
        }

        public class ForceTests : FinForceTest
        {
            protected override Vector6DOF expectedValue
            {
                get
                {
                    float flowSpeedSqr = Mathf.Pow(rigidBody.velocity.w, 2);
                    Vector3 force = new(0, testFin.forceCoefficient * flowSpeedSqr * testFin.normalizedAngle, 0);
                    Vector3 momentTorque = Vector3.Cross(
                        force,
                        rigidBody.transform.InverseTransformPoint(testFin.transform.position));
                    Vector3 torque = new(testFin.torqueCoefficient * flowSpeedSqr * testFin.normalizedAngle, 0, 0);
                    return new(force, momentTorque + torque);
                }
            }

            protected override void SetProperties()
            {
            }
        }
    }
}
