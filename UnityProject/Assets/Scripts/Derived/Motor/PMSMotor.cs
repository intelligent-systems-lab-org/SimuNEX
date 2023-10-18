using System;

public class PMSMotor : Motor
{
    /// <summary>
    /// The q-axis input voltage for the PMSM.
    /// </summary>
    public float voltage_q = 0;

    /// <summary>
    /// The d-axis input voltage for the PMSM.
    /// </summary>
    public float voltage_d = 0;

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
    /// <see cref="StateSpace"/> which defines the transfer function.
    /// </summary>
    private StateSpace stateSpace = new();

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
            () => voltage_q,
            () => voltage_d
        };

        stateSpace.Initialize
        (
            3,
            inputs.Length,
            new Matrix(3, 1, new float[3]),
            integrator: new Integrators.RK4()
        );

        stateSpace.DerivativeFcn = (states, inputs) =>
        {
            float R = parameters[0]();
            float L = parameters[1]();
            float P = parameters[2]();
            float wb = parameters[3]();
            float J = parameters[4]();
            float D = parameters[5]();

            Matrix A = new(new float[,]
           {
                    { -R/L,        0,  wb*P/L },
                    { 0,          -R/L,  0    },
                    { 1.5f*P*wb/L, 0,  -D/J   }
           });
            Matrix B = new(new float[,]
            {
                    { 1/L, 0  },
                    { 0,  1/L },
                    { 0,   0  }
            });
            return A * states + B * inputs;
        };

        MF = (inputs, parameters) =>
        {
            stateSpace.inputs[0, 0] = inputs[0]();
            stateSpace.inputs[1, 0] = inputs[1]();
            stateSpace.Compute();
            return stateSpace.states[2, 0];
        };
    }
}
