using SimuNEX.Mechanical;
using UnityEngine;

public class SimpleGustField : ForceField
{
    /// <summary>
    /// Location where the gust acts the strongest.
    /// </summary>
    public Vector3 sourcePosition;

    /// <summary>
    /// Direction of the wind speed.
    /// </summary>
    public Vector3 windDirection;

    /// <summary>
    /// Wind strength.
    /// </summary>
    public float windStrength;

    /// <summary>
    /// Variance for gaussian noise.
    /// </summary>
    public float variance;

    /// <summary>
    /// Handle to attached force object associated with this <see cref="ForceField"/> object.
    /// </summary>
    protected SimpleGust _force;

    public override void Apply(RigidBody rigidBody)
    {
        _force = rigidBody.gameObject.AddComponent<SimpleGust>();

        _force.sourcePosition = sourcePosition;
        _force.windDirection = windDirection;
        _force.windStrength = windStrength;
        _force.variance = variance;
    }

    public override void Remove(RigidBody rigidBody)
    {
        if (rigidBody != null)
        {
            rigidBody.RemoveForce(_force);
        }
    }

    public class SimpleGust : Force
    {
        /// <summary>
        /// Location where the gust acts the strongest.
        /// </summary>
        public Vector3 sourcePosition;

        /// <summary>
        /// Direction of the wind speed.
        /// </summary>
        public Vector3 windDirection;

        /// <summary>
        /// Wind strength.
        /// </summary>
        public float windStrength;

        /// <summary>
        /// Variance for gaussian noise.
        /// </summary>
        public float variance;

        public override void ApplyForce()
        {
            Vector3 value = (windStrength + (Random.Range(-1, 1) * variance)) * windDirection.normalized / Mathf.Pow(Vector3.Distance(sourcePosition, rigidBody.position), 2);
            rigidBody.AddLinearForce(value);
        }
    }

    /// <summary>
    /// Visualizes wind origin and direction.
    /// </summary>
    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(sourcePosition, 0.125f);

        // Length of the arrow represents the strength
        Vector3 endPosition = sourcePosition + windDirection.normalized;

        // Main line of the arrow
        Gizmos.DrawLine(sourcePosition, endPosition);

        // Arrowheads
        Gizmos.DrawRay(endPosition, Quaternion.LookRotation(windDirection) * Quaternion.Euler(180 + 30, 0, 0) * Vector3.forward * 0.125f);
        Gizmos.DrawRay(endPosition, Quaternion.LookRotation(windDirection) * Quaternion.Euler(180 - 30, 0, 0) * Vector3.forward * 0.125f);
    }
}
