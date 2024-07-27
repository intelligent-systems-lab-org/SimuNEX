using NUnit.Framework;
using SimuNEX;

namespace CoreTests
{
    public class ModelOutputTests
    {
        [Test]
        public void TestOutputFcn()
        {
            float[] testVal = new float[] { 3.0f };

            ModelOutput testOutput = new("testOutput") { data = testVal };

            Assert.AreEqual(3.0f, testOutput.data[0]);
            Assert.AreEqual(testVal, testOutput.data);
        }
    }
}
