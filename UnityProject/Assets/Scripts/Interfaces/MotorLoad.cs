using UnityEngine;

namespace SimuNEX 
{
    /// <summary>
    /// Generalizable class for motor loads such as <see cref="Propeller"/>.
    /// </summary>
    public abstract class MotorLoad : Load
    {
        /// <summary>
        /// Radians to Degrees conversion factor.
        /// </summary>
        protected readonly float rad2deg = Mathf.Rad2Deg;

        /// <summary>
        /// Location of the propeller mesh.
        /// </summary>
        public Transform spinnerObject;

        /// <summary>
        /// Propeller axis of rotation.
        /// </summary>
        public Direction spinAxis;

        /// <summary>
        /// Propeller speed value.
        /// </summary>
        public float _speed = 0;

        /// <summary>
        /// Force associated with the load.
        /// </summary>
        protected Force force;

        /// <summary>
        /// The inertia of the load attached to the <see cref="Motor"/> in kg.m^2.
        /// </summary>
        public float loadInertia = 0.5f;

        /// <summary>
        /// The damping coefficient of the load in N.m.s/rad.
        /// </summary>
        public float loadDamping = 0;

        /// <summary>
        /// Set up forces, properties, etc. for simulation.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Attaches a <see cref="Force"/> to the <see cref="RigidBody"/>.
        /// </summary>
        public void Activate()
        {
            Initialize();
            rigidBody.AttachForce(force);
        }

        /// <summary>
        /// Detaches associated <see cref="Force"/> from the <see cref="RigidBody"/>.
        /// </summary>
        public void Deactivate()
        {
            rigidBody.RemoveForce(force);
        }

        private void OnEnable()
        {
            Activate();
        }

        private void OnValidate()
        {
            FindSpinnerTransforms();
        }

        private void OnDisable()
        {
            Deactivate();
        }

        /// <summary>
        /// Locates mesh of spinning object which should be an immediate child of the GameObject this script would be attached to.
        /// </summary>
        private void FindSpinnerTransforms()
        {
            var transforms = GetComponentsInChildren<Transform>();
            if (transforms.Length > 1)
            {
                spinnerObject = transforms[1];
            }
            else
            {
                Debug.LogError("No spinner object found!", this);
            }
        }

        /// <summary>
        /// Updates speed based on the motor output.
        /// </summary>
        public float motorOutput
        {
            get
            {
                if (actuatorFunction != null)
                {
                    _speed = actuatorFunction();
                }
                // Use input speed if motor is not connected
                return _speed;
            }
        }


        /// <summary>
        /// Obtains the local normal vector of rotation.
        /// </summary>
        public Vector3 normal => transform.TransformDirection(spinAxis.ToVector());

        /// <summary>
        /// Obtains the normal vector of rotation.
        /// </summary>
        public Vector3 spinnerNormal => spinAxis.ToVector();

        /// <summary>
        /// Normalizes the angle of the rotating object based on the spin axis.
        /// </summary>
        /// <returns>Normalized angle in radians.</returns>
        public float normalizedAngle
        {
            get {

                float angleInDegrees = 0f;

                switch (spinAxis) 
                {
                    case Direction.Up:
                        angleInDegrees = spinnerObject.localEulerAngles.y;
                        break;

                    case Direction.Down:
                        angleInDegrees = -spinnerObject.localEulerAngles.y;
                        break;

                    case Direction.Left:
                        angleInDegrees = spinnerObject.localEulerAngles.x;
                        break;

                    case Direction.Right:
                        angleInDegrees = -spinnerObject.localEulerAngles.x;
                        break;

                    case Direction.Forward:
                        angleInDegrees = spinnerObject.localEulerAngles.z;
                        break;

                    case Direction.Backward:
                        angleInDegrees = -spinnerObject.localEulerAngles.z;
                        break;
                }

                return NormalizeAngle(angleInDegrees * Mathf.Deg2Rad);
            }
        }

        /// <summary>
        /// Converts angles in the range 0 to 2pi to -pi to pi.
        /// </summary>
        /// <param name="angleInRadians">Angle in the range of 0 to 2pi.</param>
        /// <returns>Converted angle between -pi to pi.</returns>
        private float NormalizeAngle(float angleInRadians)
        {
            while (angleInRadians <= -Mathf.PI) angleInRadians += 2 * Mathf.PI;
            while (angleInRadians > Mathf.PI) angleInRadians -= 2 * Mathf.PI;
            return angleInRadians;
        }
    }
}