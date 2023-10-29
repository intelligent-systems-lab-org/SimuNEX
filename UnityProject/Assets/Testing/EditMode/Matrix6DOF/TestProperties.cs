using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Matrix6DOFTests
{
    public class TestProperties
    {
        [Test]
        public void Indexer_GetterSetter_ShouldSetAndGetValuesCorrectly()
        {
            // Arrange
            Matrix6DOF matrix6DOF = new();
            float expectedValue = 5f;

            // Act
            // Setting a value
            matrix6DOF[2, 3] = expectedValue;
            // Getting the value
            float actualValue = matrix6DOF[2, 3];

            // Assert
            Assert.AreEqual(expectedValue, actualValue, 
                "The value set and retrieved using the indexer does not match.");
        }

        [Test]
        public void TestMatrixInverseProperty()
        {
            var originalMatrix = new Matrix6DOF
            (
                "[2 0 0 0 0 0; " +
                 "0 2 0 0 0 0; " +
                 "0 0 2 0 0 0; " +
                 "0 0 0 5 0 0; " +
                 "0 0 0 0 1 0; " +
                 "0 0 0 0 0 4]"
            );

            var inverse = originalMatrix.inverse;

            // test positions
            float tol = 1e-5f;
            Assert.AreEqual(0.5f,  inverse[0, 0], tol);
            Assert.AreEqual(0.5f,  inverse[1, 1], tol);
            Assert.AreEqual(0.5f,  inverse[2, 2], tol);
            Assert.AreEqual(0.2f,  inverse[3, 3], tol);
            Assert.AreEqual(1.0f,  inverse[4, 4], tol);
            Assert.AreEqual(0.25f, inverse[5, 5], tol);
        }
    }
}
