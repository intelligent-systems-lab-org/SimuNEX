using System;
using static StateSpaceTypes;

public class PMSMotor : Motor
{
    /// <summary>
    /// The q-axis input voltage for the PMSM.
    /// </summary>
    public float qAxisVoltage = 0;

    /// <summary>
    /// The d-axis input voltage for the PMSM.
    /// </summary>
    public float dAxisVoltage = 0;

    /// <summary>
    /// The stator winding resistance in ohms.
    /// </summary>
    public float resistance = 2.875f;

    /// <summary>
    /// The stator winding inductance in henries.
    /// </summary>
    public float inductance = 8.5e-2f;

    /// <summary>
    /// The number of poles in the PMSM.
    /// </summary>
    public int poles = 2;

    /// <summary>
    /// The magnet flux linkage in webers.
    /// </summary>
    public float flux = 0.175f;

    /// <summary>
    /// The inertia of the load attached to the motor in kg.m^2.
    /// </summary>
    public float loadInertia = 8e-4f;

    /// <summary>
    /// The damping coefficient of the load in N.m.s/rad.
    /// </summary>
    public float loadDamping = 0.0021f;

    /// <summary>
    /// <see cref="LinearStateSpace"/> which defines the transfer function.
    /// </summary>
    private LinearStateSpace stateSpace;

    public override float[] GetInput() => new float[] { qAxisVoltage, dAxisVoltage };

    public override void SetInput(float[] value)
    {
        qAxisVoltage = value[0];
        dAxisVoltage = value[1];
    }

    protected override void Initialize()
    {
        parameters = new Func<float>[]
        {
            () => resistance,
            () => inductance,
            () => poles,
            () => flux,
            () => loadInertia,
            () => loadDamping
        };

        inputs = new Func<float>[]
        {
            () => qAxisVoltage,
            () => dAxisVoltage
        };

        stateSpace = new LinearStateSpace
        (
            A: () =>
            {
                float R = parameters[0]();
                float L = parameters[1]();
                float P = parameters[2]();
                float wb = parameters[3]();
                float J = parameters[4]();
                float D = parameters[5]();

                return new(new float[,]
               {
                    { -R/L,        0,  wb*P/L },
                    { 0,          -R/L,  0    },
                    { 1.5f*P*wb/L, 0,  -D/J   }
               });
            },
            B: () =>
            {
                float L = parameters[1]();
                return new(new float[,]
                {
                    { 1/L, 0  },
                    { 0,  1/L },
                    { 0,   0  }
                });
            }
        );

        MF = (inputs, parameters) =>
        {
            stateSpace.inputs[0, 0] = inputs[0]();
            stateSpace.inputs[1, 0] = inputs[1]();
            stateSpace.Compute();
            return stateSpace.outputs[2, 0];
        };
    }
}