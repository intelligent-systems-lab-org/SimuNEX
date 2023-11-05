using NUnit.Framework;
using SimuNEX;
using UnityEngine;

namespace DirectionTests
{
    public class TestExtensionMethods
    {
        [Test]
        public void ToVectorReturnsCorrespondingVector3s()
        {
            Assert.AreEqual(Vector3.left, Direction.Left.ToVector());
            Assert.AreEqual(Vector3.right, Direction.Right.ToVector());
            Assert.AreEqual(Vector3.forward, Direction.Forward.ToVector());
            Assert.AreEqual(Vector3.back, Direction.Backward.ToVector());
            Assert.AreEqual(Vector3.up, Direction.Up.ToVector());
            Assert.AreEqual(Vector3.down, Direction.Down.ToVector());
            Assert.AreEqual(Vector3.zero, ((Direction)(-1)).ToVector());
        }
    }
}
