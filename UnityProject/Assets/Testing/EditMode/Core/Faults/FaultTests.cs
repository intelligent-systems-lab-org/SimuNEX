using NUnit.Framework;
using SimuNEX.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        /// <summary>
        /// Test tolerance value.
        /// </summary>
        protected virtual float tolerance => 0.01f;

        /// <summary>
        /// Input value for the test.
        /// </summary>
        protected virtual float testValue => 10f;

        /// <summary>
        /// Expected value for the test
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
            FaultTestsSetup.RegisterFaultTest<TFault>();
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
