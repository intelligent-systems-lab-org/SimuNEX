using NUnit.Framework;

namespace Eigen3MatrixTests
{
    public class TestSerialization
    {
        [Test]
        public void OnBeforeSerialize_SerializesDataCorrectly()
        {
            // Arrange
            Matrix matrix = new(new float[,]
                {
                    { 1, 0, 1 },
                    { 3, 2, 5 }
                });

            // Act
            matrix.OnBeforeSerialize();

            // Assert
            Assert.IsNotNull(matrix._serializedData);

            Assert.AreEqual(1, matrix._serializedData[0]);
            Assert.AreEqual(0, matrix._serializedData[1]);
            Assert.AreEqual(1, matrix._serializedData[2]);
            Assert.AreEqual(3, matrix._serializedData[3]);
            Assert.AreEqual(2, matrix._serializedData[4]);
            Assert.AreEqual(5, matrix._serializedData[5]);

            Assert.AreEqual(2, matrix._serializedRows);
            Assert.AreEqual(3, matrix._serializedCols);
        }

        [Test]
        public void OnAfterDeserialize_DeserializesDataCorrectly()
        {
            // Arrange
            Matrix matrix = new(3, 2)
            {
                _serializedData = new float[] { 1, 2, 3, 4, 5, 6 },
                _serializedRows = 3,
                _serializedCols = 2
            };

            // Act
            matrix.OnAfterDeserialize();

            // Assert
            Assert.AreEqual(1, matrix[0, 0]);
            Assert.AreEqual(2, matrix[0, 1]);
            Assert.AreEqual(3, matrix[1, 0]);
            Assert.AreEqual(4, matrix[1, 1]);
            Assert.AreEqual(5, matrix[2, 0]);
            Assert.AreEqual(6, matrix[2, 1]);

            Assert.AreEqual(3, matrix._serializedRows);
            Assert.AreEqual(2, matrix._serializedCols);
        }
    }
}
