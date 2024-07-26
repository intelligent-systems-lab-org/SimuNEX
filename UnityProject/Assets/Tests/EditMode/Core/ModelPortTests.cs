using SimuNEX;
using NUnit.Framework;
using System.Collections;
using UnityEngine;

namespace CoreTests
{
    public static class ModelPortTestCases
    {
        public static IEnumerable TestCases
        {
            get
            {
                ModelOutput<float[]> float5 = new("float5") { data = new float[5] };

                yield return new TestCaseData(new ModelOutput<Vector3>("vector3")).Returns(3).SetName("Vector3");
                yield return new TestCaseData(new ModelOutput<Quaternion>("quaternion")).Returns(4).SetName("Quaternion");
                yield return new TestCaseData(new ModelOutput<int>("int")).Returns(1).SetName("Int");
                yield return new TestCaseData(new ModelOutput<float>("float")).Returns(1).SetName("Float");
                yield return new TestCaseData(float5).Returns(5).SetName("FloatArray_5");
                yield return new TestCaseData(new ModelOutput<Vector6DOF>("vector6")).Returns(6).SetName("Vector6");
            }
        }
    }

    public class ModelOutputTests
    {
        [Test]
        public void TestOutputFcn()
        {
            const float testVal = 3.0f;

            ModelOutput<float> testOutput = new("testOutput") { data = testVal };

            Assert.AreEqual(3.0f, testOutput.data);
            Assert.AreEqual(testVal, testOutput.data);
        }

        [TestCaseSource(typeof(ModelPortTestCases), nameof(ModelPortTestCases.TestCases))]
        public int TestSize(IModelPort modelPort)
        {
            return modelPort.size;
        }
    }
}
