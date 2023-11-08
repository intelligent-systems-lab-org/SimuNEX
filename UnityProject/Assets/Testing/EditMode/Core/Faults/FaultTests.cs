using FactoryTests;
using NUnit.Framework;
using SimuNEX.Faults;
using UnityEngine;

namespace FaultTests
{
    public class FaultTestsSetup : TypeRegisterSetup<Fault>
    {
    }

    public abstract class FaultTests<TFault> where TFault : Fault, new()
    {
        protected TFault faultInstance;

        /// <summary>
        /// Test tolerance value.
        /// </summary>
        protected virtual float tolerance => 0.01f;

        /// <summary>
        /// Input value for the test.
        /// </summary>
        protected virtual float testValue => 10f;

        /// <summary>
        /// Expected value for the test.
        /// </summary>
        protected abstract float expectedValue { get; }

        /// <summary>
        /// Initializes the fault instance with the necessary parameters for construction.
        /// </summary>
        protected abstract void InitializeFaultInstance();

        [SetUp]
        public void SetUp()
        {
            faultInstance = new TFault();
            InitializeFaultInstance();

            // Register the fault type as being tested
            FaultTestsSetup.RegisterTest<TFault>();
        }

        [Test]
        public virtual void TestFaultFunction()
        {
            float actualValue = faultInstance.FaultFunction(testValue);
            Assert.IsTrue(
                Mathf.Abs(actualValue - expectedValue) < tolerance,
                $"FaultFunction for {faultInstance.GetType().Name} did not return the expected value.");
        }
    }
}
