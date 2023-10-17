using UnityEngine;

namespace IntegratorTests
{
    public class TestHeun : BaseTests<Integrators.Heun>
    {
        protected override TestConfig Config => new(Time.fixedDeltaTime, 1f, false, 5e-2f);
    }
}