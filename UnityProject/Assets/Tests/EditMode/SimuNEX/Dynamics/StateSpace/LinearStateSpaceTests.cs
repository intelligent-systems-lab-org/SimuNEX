using NUnit.Framework;
using SimuNEX.Dynamics;

namespace StateSpaceTests
{
    /// <summary>
    /// Tests for the <see cref="LinearStateSpace"/> class.
    /// </summary>
    public class LinearStateSpaceTests
    {
        private static readonly Matrix A_matrix = new(new float[,] { { 1, 2 }, { 3, 4 } });
        private static readonly Matrix B_matrix = new(new float[,] { { 5 }, { 6 } });
        private static readonly Matrix C_matrix = new(new float[,] { { 1, 0 }, { 0, 1 } });
        private static readonly Matrix D_matrix = new(new float[,] { { 7 }, { 8 } });

        [Test]
        public void Constructor_InitializesCorrectly()
        {
            // Arrange
            float[] initialConditions = { 1, 0 };

            // Act
            LinearStateSpace lss = new(
                A_matrix,
                B_matrix,
                C_matrix,
                D_matrix,
                initialConditions);

            // Assert
            Assert.AreEqual(A_matrix, lss.A);
            Assert.AreEqual(B_matrix, lss.B);
            Assert.AreEqual(C_matrix, lss.C);
            Assert.AreEqual(D_matrix, lss.D);
            Assert.AreEqual(initialConditions[0], lss.states[0, 0]);
            Assert.AreEqual(initialConditions[1], lss.states[1, 0]);
        }

        [Test]
        public void OutputProperty_ReturnsCorrectValues()
        {
            // Arrange
            float[] initialConditions = { 0, 0 };
            LinearStateSpace lss = new(
                A_matrix,
                B_matrix,
                C_matrix,
                D_matrix,
                initialConditions);
            ForwardEuler solver = new();

            // Set a known input
            lss.inputs[0, 0] = 1;

            // Act - Compute one step
            solver.Step(lss);

            // Assert
            Matrix expectedOutput = (C_matrix * lss.states) + (D_matrix * lss.inputs);
            Assert.IsTrue(expectedOutput.IsEqual(lss.outputs));
        }

        [Test]
        public void OutputProperty_ReturnsCorrectValuesWithoutDMatrix()
        {
            // Arrange
            float[] initialConditions = { 0, 0 };
            LinearStateSpace lss = new(
                A_matrix,
                B_matrix,
                C_matrix,
                initialConditions: initialConditions);
            ForwardEuler solver = new();

            // Set a known input
            lss.inputs[0, 0] = 1;

            // Act - Compute one step
            solver.Step(lss);

            // Assert
            Assert.IsNull(lss.D);

            Matrix expectedOutput = C_matrix * lss.states;
            Assert.IsTrue(expectedOutput.IsEqual(lss.outputs));
        }
    }
}
