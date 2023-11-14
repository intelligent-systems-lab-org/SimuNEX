using FactoryTests;
using NUnit.Framework;
using SimuNEX;
using SimuNEX.Mechanical;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace ForceFieldTests
{
    public class ForceFieldTestsSetup : TypeRegisterSetup<ForceField>
    {
    }

    public abstract class ForceFieldTests<TFField, TForce>
        where TFField : ForceField
        where TForce : Force
    {
        protected GameObject gameObject;
        protected RigidBody rigidBody;
        protected BoxCollider rigidCollider;
        protected Environment environment;
        protected TFField testField;

        protected const int throwawayBoxCounts = 3;
        private const float timeout = 2f;

        private float startTime;

        [SetUp]
        public void Setup()
        {
            gameObject = new("Test");
            rigidBody = gameObject.AddComponent<RigidBody>();
            gameObject.AddComponent<TForce>();
            rigidCollider = gameObject.AddComponent<BoxCollider>();

            environment = new GameObject("Environment").AddComponent<Environment>();
            environment.transform.position = Vector3.zero;

            // Pre-existing boxes
            for (int i = 0; i < throwawayBoxCounts; ++i)
            {
                _ = environment.gameObject.AddComponent<BoxCollider>();
            }

            environment.boxSize = 10f * Vector3.one;
            environment.center = new(0, environment.boxSize.y / 2, 0);

            testField = environment.gameObject.AddComponent<TFField>();

            ForceFieldTestsSetup.RegisterTest<TFField>();
        }

        [UnityTest]
        public IEnumerator TestField()
        {
            environment.Initialize();

            // Test if boxes were removed
            BoxCollider[] colliders = environment.GetComponents<BoxCollider>();
            Assert.IsFalse(colliders.Length is throwawayBoxCounts and not 1);

            // No other field should exist
            ForceField[] fields = environment.GetComponents<ForceField>();
            Assert.IsTrue(fields.Length == 1);

            // Inside field
            rigidBody.body.transform.position = Vector3.zero;
            startTime = Time.realtimeSinceStartup;

            yield return new WaitUntil(
                () => gameObject.GetComponent<TForce>() != null || (Time.realtimeSinceStartup - startTime) > timeout);

            Assert.IsNotNull(gameObject.GetComponent<TForce>());

            // Outside field
            rigidBody.body.transform.position = environment.boxSize + (2f * Vector3.one);
            startTime = Time.realtimeSinceStartup;

            yield return new WaitUntil(
                () => gameObject.GetComponent<TForce>() == null || (Time.realtimeSinceStartup - startTime) > timeout);

            Assert.IsNull(gameObject.GetComponent<TForce>());
        }
    }
}
