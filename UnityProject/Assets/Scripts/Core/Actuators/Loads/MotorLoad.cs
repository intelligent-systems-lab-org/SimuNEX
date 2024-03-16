using System;
using UnityEngine;

namespace SimuNEX.Loads
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
        public float _speed;

        /// <summary>
        /// <see cref="Mechanical.Force"/> associated with the load.
        /// </summary>
        protected Mechanical.Force _force;

        [Parameter]
        /// <summary>
        /// The inertia of the load attached to the <see cref="Motor"/> in kg.m^2.
        /// </summary>
        public float loadInertia = 0.5f;

        [Parameter]
        /// <summary>
        /// The damping coefficient of the load in N.m.s/rad.
        /// </summary>
        public float loadDamping;

        /// <summary>
        /// Turns SFX on or off.
        /// </summary>
        [SFX]
        public bool enableSFX;

        /// <summary>
        /// Set up forces, properties, etc. for simulation.
        /// </summary>
        protected abstract void Initialize();

        protected void Update()
        {
            HandleAnimation();
            HandleSFX();
        }

        /// <summary>
        /// Animates spinner associated with the <see cref="MotorLoad"/>.
        /// </summary>
        protected virtual void HandleAnimation()
        {
            // Scale Time.deltaTime based on motorSpeed
            float scaledDeltaTime = Time.deltaTime * Mathf.Abs(_speed);

            // Handle rotation animation
            Quaternion increment = Quaternion.Euler(_speed * rad2deg * scaledDeltaTime * spinnerNormal);
            spinnerObject.localRotation *= increment;
        }

        /// <summary>
        /// Handles SFX associated with the <see cref="MotorLoad"/>.
        /// </summary>
        protected virtual void HandleSFX() { }

        /// <summary>
        /// Attaches a <see cref="Force"/> to the <see cref="RigidBody"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the force was not initialized in <see cref="Initialize"/></exception>
        public void Activate()
        {
            if (rigidBody != null)
            {
                Initialize();
                if (_force == null)
                {
                    throw new InvalidOperationException($"The force component for '{nameof(rigidBody)}' was not initialized properly.");
                }
            }
        }

        /// <summary>
        /// Detaches associated <see cref="Force"/> from the <see cref="RigidBody"/>.
        /// </summary>
        public void Deactivate()
        {
            if (rigidBody != null)
            {
                rigidBody.RemoveForce(_force);
            }
        }

        protected void OnEnable()
        {
            FindSpinnerTransforms();
            Activate();
        }

        protected void OnValidate()
        {
            FindSpinnerTransforms();
        }

        protected void OnDisable()
        {
            Deactivate();
        }

        /// <summary>
        /// Locates mesh of spinning object which should be an immediate child of the GameObject this script would be attached to.
        /// </summary>
        protected void FindSpinnerTransforms()
        {
            Transform[] transforms = GetComponentsInChildren<Transform>();
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
            get
            {
                float angleInDegrees = 0f;

                switch (spinAxis)
                {
                    case Direction.Up:
                        {
                            angleInDegrees = spinnerObject.localEulerAngles.y;
                            break;
                        }

                    case Direction.Down:
                        {
                            angleInDegrees = -spinnerObject.localEulerAngles.y;
                            break;
                        }

                    case Direction.Left:
                        {
                            angleInDegrees = spinnerObject.localEulerAngles.x;
                            break;
                        }

                    case Direction.Right:
                        {
                            angleInDegrees = -spinnerObject.localEulerAngles.x;
                            break;
                        }

                    case Direction.Forward:
                        {
                            angleInDegrees = spinnerObject.localEulerAngles.z;
                            break;
                        }

                    case Direction.Backward:
                        {
                            angleInDegrees = -spinnerObject.localEulerAngles.z;
                            break;
                        }
                }

                return NormalizeAngle(angleInDegrees * Mathf.Deg2Rad);
            }

            set
            {
                Vector3 newEuler = spinnerObject.localEulerAngles;

                switch (spinAxis)
                {
                    case Direction.Up:
                        {
                            newEuler.y = value * Mathf.Rad2Deg;
                            break;
                        }

                    case Direction.Down:
                        {
                            newEuler.y = -value * Mathf.Rad2Deg;
                            break;
                        }

                    case Direction.Left:
                        {
                            newEuler.x = value * Mathf.Rad2Deg;
                            break;
                        }

                    case Direction.Right:
                        {
                            newEuler.x = -value * Mathf.Rad2Deg;
                            break;
                        }

                    case Direction.Forward:
                        {
                            newEuler.z = value * Mathf.Rad2Deg;
                            break;
                        }

                    case Direction.Backward:
                        {
                            newEuler.z = -value * Mathf.Rad2Deg;
                            break;
                        }
                }

                spinnerObject.localEulerAngles = newEuler;
            }
        }

        /// <summary>
        /// Converts angles in the range 0 to 2pi to -pi to pi.
        /// </summary>
        /// <param name="angleInRadians">Angle in the range of 0 to 2pi.</param>
        /// <returns>Converted angle between -pi to pi.</returns>
        private float NormalizeAngle(float angleInRadians)
        {
            while (angleInRadians <= -Mathf.PI)
            {
                angleInRadians += 2 * Mathf.PI;
            }

            while (angleInRadians > Mathf.PI)
            {
                angleInRadians -= 2 * Mathf.PI;
            }

            return angleInRadians;
        }
    }
}
