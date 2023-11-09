using NUnit.Framework;
using SimuNEX.Faults;
using UnityEngine;

namespace FaultTests
{
    public class BaseTests
    {
        private const float bias = 5f;
        private const float tolerance = 1e-3f;

        private Bias biasFault;

        [Test]
        public void FaultFunctionVector3_ReturnsFaultedVector3()
        {
            biasFault = new(bias);

            // Arrange
            Vector3 testVec3 = new(1, 2, 3);

            // Act
            Vector3 resultVec3 = biasFault.FaultFunction(testVec3);

            // Assert
            Assert.AreEqual(testVec3.x + bias, resultVec3.x, tolerance);
            Assert.AreEqual(testVec3.y + bias, resultVec3.y, tolerance);
            Assert.AreEqual(testVec3.z + bias, resultVec3.z, tolerance);
        }

        [Test]
        public void FaultFunctionQuaternion_ReturnsFaultedQuaternion()
        {
            biasFault = new(bias);

            // Arrange
            Quaternion testQuat = Quaternion.identity;

            // Act
            Quaternion resultQuat = biasFault.FaultFunction(testQuat);

            // Assert
            Vector3 testEuler = testQuat.eulerAngles;
            Vector3 resultEuler = resultQuat.eulerAngles;

            Assert.AreEqual(testEuler.x + bias, resultEuler.x, tolerance);
            Assert.AreEqual(testEuler.y + bias, resultEuler.y, tolerance);
            Assert.AreEqual(testEuler.z + bias, resultEuler.z, tolerance);
        }
    }
}
