using UnityEngine;

namespace IntegratorTests
{
    public class TestRK4 : BaseTests<Integrators.RK4>
    {
        protected override TestConfig Config => new(Time.fixedDeltaTime, 1f, false, 5e-2f);
    }
}