using SimuNEX;
using SimuNEX.Loads;
using UnityEngine;

namespace LoadTests.Propellers
{
    public class SimplePropellerTests : PropellerTests<SimplePropeller, SimplePropeller.SimplePropellerForce>
    {
        public override void InitializePropeller()
        {
            testPropeller.thrustCoefficient = 0.5f;
            testPropeller.torqueCoefficient = -0.05f;

            testPropeller._speed = 100f;
            testPropeller.spinAxis = Direction.Up;
        }

        public class ForceTest : PropellerForceTest
        {
            protected override Vector6DOF expectedValue
            {
                get
                {
                    float speedSigned = Mathf.Abs(testPropeller._speed) * testPropeller._speed;
                    Vector3 force = new(0, testPropeller.thrustCoefficient * speedSigned, 0);
                    Vector3 momentTorque = Vector3.Cross(
                        force,
                        rigidBody.transform.InverseTransformPoint(testPropeller.transform.position));
                    Vector3 reactionTorque = new(0, testPropeller.torqueCoefficient * speedSigned, 0);
                    return new(force, momentTorque + reactionTorque);
                }
            }

            protected override void SetProperties()
            {
            }
        }
    }

    public class UnidirectionalPropellerTests :
        PropellerTests<UnidirectionalPropeller, UnidirectionalPropeller.UnidirectionalPropellerForce>
    {
        public override void InitializePropeller()
        {
            testPropeller.thrustCoefficient = 0.5f;
            testPropeller.torqueCoefficient = -0.05f;

            testPropeller._speed = 100f;
            testPropeller.spinAxis = Direction.Up;
        }

        public class ForceTest : PropellerForceTest
        {
            protected override Vector6DOF expectedValue
            {
                get
                {
                    float speedSigned = Mathf.Abs(testPropeller._speed) * testPropeller._speed;
                    Vector3 force = new(0, testPropeller.thrustCoefficient * Mathf.Abs(speedSigned), 0);
                    Vector3 momentTorque = Vector3.Cross(
                        force,
                        rigidBody.transform.InverseTransformPoint(testPropeller.transform.position));
                    Vector3 reactionTorque = new(0, testPropeller.torqueCoefficient * speedSigned, 0);
                    return new(force, momentTorque + reactionTorque);
                }
            }

            protected override void SetProperties()
            {
            }
        }
    }
}
