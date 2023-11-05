using NUnit.Framework;
using SimuNEX;
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

        [Test]
        public void OperatorMultiply_Matrix6DOFWithMatrix_ShouldReturnCorrectProduct()
        {
            // Arrange
            Matrix6DOF matrix6DOF = Matrix6DOF.CreateMassMatrix(2.0f, new Vector3(2, 2, 2));
            Matrix matrix = new(new float[,]
            {
                { 1, 0, 0, 0, 0, 0 },
                { 0, 1, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0 },
                { 0, 0, 0, 0, 1, 0 },
                { 0, 0, 0, 0, 0, 1 }
            });

            // Act
            Matrix product = matrix6DOF * matrix;

            // Assert
            Assert.AreEqual(2.0f, product[0, 0], "Product of Matrix6DOF and Matrix is incorrect.");
            Assert.AreEqual(2.0f, product[4, 4], "Product of Matrix6DOF and Matrix is incorrect.");
        }

        [Test]
        public void OperatorMultiply_MatrixWithMatrix6DOF_ShouldReturnCorrectProduct()
        {
            // Arrange
            Matrix matrix = new(new float[,]
            {
                { 1, 0, 0, 0, 0, 0 },
                { 0, 1, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0 },
                { 0, 0, 0, 0, 1, 0 },
                { 0, 0, 0, 0, 0, 1 }
            });
            Matrix6DOF matrix6DOF = Matrix6DOF.CreateMassMatrix(3.0f, new Vector3(3, 3, 3));

            // Act
            Matrix product = matrix * matrix6DOF;

            // Assert
            Assert.AreEqual(3.0f, product[0, 0], "Product of Matrix and Matrix6DOF is incorrect.");
            Assert.AreEqual(3.0f, product[3, 3], "Product of Matrix and Matrix6DOF is incorrect.");
        }

        [Test]
        public void OperatorMultiply_Matrix6DOFWithVector6DOF_ShouldReturnCorrectProduct()
        {
            // Arrange
            Matrix6DOF matrix6DOF = Matrix6DOF.CreateMassMatrix(2.0f, new Vector3(2, 2, 2));
            Vector6DOF vector6DOF = new(new Vector3(1, 2, 3), new Vector3(4, 5, 6));

            // Act
            Vector6DOF product = matrix6DOF * vector6DOF;

            // Assert
            Assert.AreEqual(2.0f, product[0],
                "Product of Matrix6DOF and Vector6DOF is incorrect for linear component.");
            Assert.AreEqual(4.0f, product[1],
                "Product of Matrix6DOF and Vector6DOF is incorrect for linear component.");
            Assert.AreEqual(6.0f, product[2],
                "Product of Matrix6DOF and Vector6DOF is incorrect for linear component.");
            Assert.AreEqual(8.0f, product[3],
                "Product of Matrix6DOF and Vector6DOF is incorrect for angular component.");
            Assert.AreEqual(10.0f, product[4],
                "Product of Matrix6DOF and Vector6DOF is incorrect for angular component.");
            Assert.AreEqual(12.0f, product[5],
                "Product of Matrix6DOF and Vector6DOF is incorrect for angular component.");
        }
    }
}
