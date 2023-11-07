using NUnit.Framework;
using SimuNEX.Faults.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FaultTests
{
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

    public class StictionFaultTest : FaultTests<Stiction>
    {
        protected override float expectedValue => Math.Abs(testValue) < faultInstance.stictionThreshold ? 0 : testValue;

        protected override void InitializeFaultInstance()
        {
            faultInstance = new(50f);
        }
    }

    public class QuantizationFaultTest : FaultTests<Quantization>
    {
        protected override float expectedValue => Mathf.Round(testValue / faultInstance.quantizationStep) * faultInstance.quantizationStep;

        protected override void InitializeFaultInstance()
        {
            faultInstance = new(0.0625f);
        }
    }

    public class GaussianFaultTest : FaultTests<Gaussian>
    {
        /// <summary>
        /// Override the expectedValue to use statistical approach
        /// </summary>
        protected override float expectedValue => testValue;
        protected override float tolerance => 0.5f;

        /// <summary>
        /// Implements a statistical test
        /// </summary>
        [Test]
        public override void TestFaultFunction()
        {
            // Arrange
            const int sampleSize = 10000;
            List<float> samples = new(sampleSize);
            System.Random rng = new(0);

            // Act
            for (int i = 0; i < sampleSize; i++)
            {
                // Replace Unity's random function with a controlled one for the test
                samples.Add(faultInstance.FaultFunction(testValue) + ((float)((rng.NextDouble() * 2.0) - 1.0) * faultInstance.variance));
            }

            float mean = samples.Average();
            float variance = samples.Sum(x => Mathf.Pow(x - mean, 2)) / sampleSize;

            // Assert
            Assert.AreEqual(expectedValue, mean, tolerance, "The mean of the results is not within the expected range.");
            Assert.AreEqual(faultInstance.variance, variance, tolerance, "The variance of the results is not within the expected range.");
        }

        protected override void InitializeFaultInstance()
        {
            faultInstance = new(0.5f);
        }
    }
}
