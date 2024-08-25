using UnityEngine;

namespace SimuNEX.Mechanical
{
    /// <summary>
    /// Models a single Rigidbody with added mass dynamics.
    /// </summary>
    public class RBFModel : RBModel, IModelInitialization
    {
        /// <summary>
        /// Added mass coefficients.
        /// </summary>
        public Matrix6DOF addedMass = new();

        /// <summary>
        /// Rigidbody mass and inertia tensor as a 6-diagonal matrix.
        /// </summary>
        private Matrix6DOF RBMass = new();

        /// <summary>
        /// Acceleration at the current timestep.
        /// </summary>
        [SerializeField]
        private Vector6DOF _acceleration = new();

        /// <summary>
        /// 6 x 6 mass matrix.
        /// </summary>
        public Matrix6DOF MassMatrix => RBMass - addedMass;

        public void Init()
        {
            RBMass = Matrix6DOF.CreateMassMatrix(body.mass, body.inertiaTensor);
        }

        protected override void PhysicsUpdate()
        {
            _acceleration = MassMatrix.inverse * appliedForce;
            body.AddForce(_acceleration.linear, ForceMode.Acceleration);
            body.AddTorque(_acceleration.angular, ForceMode.Acceleration);
        }
    }
}
