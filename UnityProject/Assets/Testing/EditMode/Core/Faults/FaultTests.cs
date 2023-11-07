using NUnit.Framework;
using SimuNEX;
using System;
using System.Collections.Generic;
using System.Linq;
using static SimuNEX.FaultTypes;

namespace FaultTests
{
    [SetUpFixture]
    public class FaultTestsSetup
    {
        private static readonly HashSet<Type> faultTypesTested = new();

        public static void RegisterFaultTest<T>() where T : Fault
        {
            _ = faultTypesTested.Add(typeof(T));
        }

        [OneTimeTearDown]
        public void AfterAllTests()
        {
            // Retrieve all available fault types using the factory
            Type[] faultTypes = FaultFactory.GetAvailableFaultTypes();

            // Check if all fault types have been registered/tested
            List<Type> untestedFaultTypes = faultTypes.Where(t => !faultTypesTested.Contains(t)).ToList();
            if (untestedFaultTypes.Count > 0)
            {
                throw new Exception(
                    "The following fault types are not tested: " + string.Join(", ", untestedFaultTypes.Select(t => t.Name)));
            }
        }
    }

    public abstract class FaultTests<TFault> where TFault : Fault, new()
    {
        protected TFault faultInstance;

        protected virtual float testValue => 10f;
        protected abstract float expectedValue { get; }

        [SetUp]
        public void SetUp()
        {
            faultInstance = new TFault();
            // Register the fault type as being tested
            FaultTestsSetup.RegisterFaultTest<TFault>();
        }

        [Test]
        public void TestFaultFunction()
        {
            float actualValue = faultInstance.FaultFunction(testValue);
            Assert.IsTrue(
                actualValue == expectedValue,
                $"FaultFunction for {faultInstance.GetType().Name} did not return the expected value.");
        }
    }

    /// <summary>
    /// Example of a concrete test class
    /// </summary>
    public class ConstantFaultTest : FaultTests<Constant>
    {
        /// <summary>
        /// Expected value for the test
        /// </summary>
        protected override float expectedValue => 0f;
    }
}
