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
        public void TestFaultFunction()
        {
            float actualValue = faultInstance.FaultFunction(testValue);
            Assert.IsTrue(
                actualValue == expectedValue,
                $"FaultFunction for {faultInstance.GetType().Name} did not return the expected value.");
        }
    }

    public class ConstantFaultTest : FaultTests<Constant>
    {
        protected override float expectedValue => faultInstance._value;

        protected override void InitializeFaultInstance()
        {
            faultInstance = new(4f);
        }
    }

    public class BiasFaultTest : FaultTests<Bias>
    {
        protected override float expectedValue => testValue + faultInstance._value;

        protected override void InitializeFaultInstance()
        {
            faultInstance = new(5f);
        }
    }

    public class ScaleFaultTest : FaultTests<Scale>
    {
        protected override float expectedValue => testValue * faultInstance.gain;

        protected override void InitializeFaultInstance()
        {
            faultInstance = new(2f);
        }
    }

    public class DeadZoneFaultTest : FaultTests<DeadZone>
    {
        protected override float expectedValue =>
                testValue > faultInstance.range.min && testValue < faultInstance.range.max ? 0f : testValue;

        protected override void InitializeFaultInstance()
        {
            faultInstance = new(-1, 1);
        }

        [Test]
        public void SingleFloatConstructor_InitializesCorrectly()
        {
            faultInstance = new(10);

            Assert.AreEqual(10, faultInstance.range.max);
            Assert.AreEqual(-10, faultInstance.range.min);
        }
    }
}
