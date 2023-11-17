using SimuNEX.Actuators;

namespace ActuatorTests.Motors
{
    public class DCMotorTests : MotorTests<DCMotor>
    {
        public override void InitializeMotor()
        {
            testMotor.backEMFConstant = 1f;
            testMotor.torqueConstant = 0.1f;
            testMotor.armatureResistance = 20f;

            actuatorSystem.inputs = new float[1] { 100f };
        }
    }

    public class PMSMotorTests : MotorTests<PMSMotor>
    {
        public override void InitializeMotor()
        {
            testMotor.poles = 2;
            testMotor.resistance = 2.875f;
            testMotor.inductance = 8.5e-2f;
            testMotor.flux = 0.175f;

            actuatorSystem.inputs = new float[2] { 100f, 0 };
        }
    }
}
