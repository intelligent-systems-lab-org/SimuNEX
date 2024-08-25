using SimuNEX.Dynamics;

namespace ODESolverTests
{
    public class TestEuler : SolverTests<ForwardEuler>
    {
    }

    public class TestHeun : SolverTests<Heun>
    {
    }

    public class TestRK4 : SolverTests<RK4>
    {
    }
}
