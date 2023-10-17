using UnityEngine;

namespace IntegratorTests
{
    public class TestEuler : BaseTests<Integrators.ForwardEuler>
    {
        protected override TestConfig Config => new(Time.fixedDeltaTime, 1f, true, 5e-2f);
    }
}