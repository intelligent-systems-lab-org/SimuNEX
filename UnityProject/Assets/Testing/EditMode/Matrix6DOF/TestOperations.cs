using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Matrix6DOFTests
{
    public class TestOperations
    {
        [Test]
        public void SetRow_ValidRow_ShouldSetCorrectValues()
        {
            Matrix6DOF m = new();
            m.SetRow(2, 1, 2, 3, 4, 5, 6);

            for (int j = 0; j < 6; j++)
            {
                Assert.AreEqual(j + 1, m[2, j]);
            }
        }

        [Test]
        public void SetColumn_ValidColumn_ShouldSetCorrectValues()
        {
            Matrix6DOF m = new();
            m.SetColumn(3, 1, 2, 3, 4, 5, 6);

            for (int i = 0; i < 6; i++)
            {
                Assert.AreEqual(i + 1, m[i, 3]);
            }
        }

        [Test]
        public void IsEqual_SameMatrix_ShouldReturnTrue()
        {
            Matrix6DOF m1 = new
            (
                "[1 2 3 4 5 6; " +
                "1 2 3 4 5 6; " +
                "1 2 3 4 5 6; " +
                "1 2 3 4 5 6; " +
                "1 2 3 4 5 6; " +
                "1 2 3 4 5 6]"
            );
            Matrix6DOF m2 = m1;

            Assert.IsTrue(m1.IsEqual(m2));
        }

        [Test]
        public void IsNotEqual_DifferentMatrix_ShouldReturnTrue()
        {
            Matrix6DOF m1 = new
            (
                "[1 2 3 4 5 6; " +
                "1 2 3 4 5 6; " +
                "1 2 3 4 5 6; " +
                "1 2 3 4 5 6; " +
                "1 2 3 4 5 6; " +
                "1 2 3 4 5 6]"
            );
            Matrix6DOF m2 = new
            (
                "[1 1 1 1 1 1; " +
                "1 2 3 4 5 6; " +
                "1 2 3 4 5 6; " +
                "1 2 3 4 5 6; " +
                "1 2 3 4 5 6; " +
                "1 2 3 4 5 6]"
            );

            Assert.IsTrue(m1.IsNotEqual(m2));
        }

        [Test]
        public void OperatorAdd_TwoMatrices_ShouldReturnCorrectSum()
        {
            // Arrange
            Matrix6DOF matrix1 = Matrix6DOF.CreateMassMatrix(1.0f, new Vector3(1, 1, 1));
            Matrix6DOF matrix2 = Matrix6DOF.CreateMassMatrix(2.0f, new Vector3(2, 2, 2));

            // Act
            Matrix6DOF sum = matrix1 + matrix2;

            // Assert
            Assert.AreEqual(3.0f, sum[0, 0], "Sum of matrices is incorrect.");
            Assert.AreEqual(3.0f, sum[4, 4], "Sum of matrices is incorrect.");
        }

        [Test]
        public void OperatorSubtract_TwoMatrices_ShouldReturnCorrectDifference()
        {
            // Arrange
            Matrix6DOF matrix1 = Matrix6DOF.CreateMassMatrix(3.0f, new Vector3(3, 3, 3));
            Matrix6DOF matrix2 = Matrix6DOF.CreateMassMatrix(1.0f, new Vector3(1, 1, 1));

            // Act
            Matrix6DOF difference = matrix1 - matrix2;

            // Assert
            Assert.AreEqual(2.0f, difference[0, 0], "Difference of matrices is incorrect.");
            Assert.AreEqual(2.0f, difference[3, 3], "Difference of matrices is incorrect.");
        }
    }
}
