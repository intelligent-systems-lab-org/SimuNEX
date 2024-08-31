using UnityEngine;

namespace SimuNEX.Sensors
{
    public class LIDARModel : Model
    {
        private Camera lidarCamera;
        private RenderTexture depthTexture;
        private Shader lidarDepthShader;

        /// <summary>
        /// The frequency in Hz at which the sensor will generate data, and the revolutions per second that the LiDAR spins at.
        /// </summary>
        public float rotateFrequency = 20f;

        /// <summary>
        /// Distance of ray in metres (m).
        /// </summary>
        public Limits range = new()
        {
            min = 0.5f,
            max = 90f
        };

        /// <summary>
        /// Maximum and minimum horizontal scan angle in radians.
        /// </summary>
        public Limits horizontalFOV = new()
        {
            min = -Mathf.PI,
            max = Mathf.PI
        };

        /// <summary>
        /// Maximum and minimum vertical scan angle in radians.
        /// </summary>
        public Limits verticalFOV = new()
        {
            min = -21.2f * Mathf.Deg2Rad,
            max = 21.2f * Mathf.Deg2Rad
        };

        /// <summary>
        /// The number of vertical sample steps per horizontal step. A value of 1 results in a planar LiDAR.
        /// </summary>
        public int verticalSamples = 32;

        /// <summary>
        /// The number of horizontal sample steps per complete laser sweep cycle.
        /// </summary>
        public int horizontalSamples = 512;
        public int cameraWidth = 512;
        public float rangeResolution = 0.01f;
        private float h_delta_angle;
        private float v_delta_angle;
        private float[] h_angles;
        private float[] v_angles;

        protected override ModelFunction modelFunction => throw new System.NotImplementedException();

        protected void Start()
        {
            lidarDepthShader = Shader.Find("Custom/LidarDepthShader");

            // Create a render texture to store depth information
            // We use a square resolution based on the camera
            depthTexture = new(cameraWidth, cameraWidth, 24, RenderTextureFormat.ARGB32) { enableRandomWrite = true };
            _ = depthTexture.Create();

            lidarCamera = TryGetComponent(out Camera camera) ? camera : gameObject.AddComponent<Camera>();

            lidarCamera.enabled = false;  // We don't want to render this to the screen
            lidarCamera.targetTexture = depthTexture;
            lidarCamera.clearFlags = CameraClearFlags.SolidColor;
            lidarCamera.backgroundColor = Color.clear;
            lidarCamera.orthographic = false;

            // Calculate the ideal virtual camera FOV based on the total vertical FOV of the LiDAR
            float fov = Mathf.CeilToInt(Mathf.Rad2Deg * (Mathf.Abs(verticalFOV.min) + verticalFOV.max));

            if (fov % 2 != 0)
            {
                fov++;
            }

            lidarCamera.usePhysicalProperties = true;

            float f_x = cameraWidth / (2.0f * Mathf.Tan(Mathf.Deg2Rad * fov / 2.0f));
            float f_y = cameraWidth / (2.0f * Mathf.Tan(Mathf.Deg2Rad * fov / 2.0f));

            lidarCamera.focalLength = f_x;

            lidarCamera.sensorSize = lidarCamera.focalLength * cameraWidth * new Vector2(1 / f_x, 1 / f_y);

            lidarCamera.lensShift = new
            (
                -((0.5f * cameraWidth) - (cameraWidth / 2f)) / cameraWidth,
                ((0.5f * cameraWidth) - (cameraWidth / 2f)) / cameraWidth
            );

            GenerateCoords();
        }

        protected void Update()
        {
            // Rotate the camera based on LIDAR's rotational frequency
            lidarCamera.transform.Rotate(0, Time.deltaTime * rotateFrequency * 360.0f, 0);

            lidarCamera.RenderWithShader(lidarDepthShader, "");
        }

        private void GenerateCoords()
        {
            h_delta_angle = (horizontalFOV.max - horizontalFOV.min) / (horizontalSamples - 1);
            v_delta_angle = (Mathf.Abs(verticalFOV.min) + verticalFOV.max) / (verticalSamples - 1);

            h_angles = Linspace(horizontalFOV.min, horizontalFOV.max - h_delta_angle, horizontalSamples);
            v_angles = Linspace(verticalFOV.min, verticalFOV.max, verticalSamples);
        }

        public float[] Linspace(float start, float end, int n)
        {
            float increment = (end - start) / (n - 1);

            float[] result = new float[n];

            for (int i = 0; i < n; i++)
            {
                result[i] = start + (i * increment);
            }

            return result;
        }

        protected void OnGUI()
        {
            GUI.DrawTexture(new Rect(0, 0, 512, 512), depthTexture, ScaleMode.ScaleToFit, false);
        }
    }
}
