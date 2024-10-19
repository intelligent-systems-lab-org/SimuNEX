using System.Collections.Generic;
using UnityEngine;

namespace SimuNEX.Sensors
{
    public class LIDARModel : Model
    {
        private Camera lidarCamera;
        private RenderTexture depthTexture;
        private Shader lidarDepthShader;
        public float rotateFrequency = 20f;
        public Limits range = new() { min = 0.5f, max = 90f };
        public Limits horizontalFOV = new() { min = -180, max = 180 };
        public Limits verticalFOV = new() { min = -21.2f, max = 21.2f };
        public int verticalSamples = 32;
        public int horizontalSamples = 512;
        public int cameraWidth = 512;
        public float rangeResolution = 0.01f;
        private float h_delta_angle;
        private float v_delta_angle;
        private float[] h_angles;
        private float[] v_angles;
        private readonly List<Vector3> polar_to_cartesian_LUT = new();
        private float sensor_sum_rotation_angle;
        private float sensor_cur_rotation_angle;
        private float sensor_prev_rotation_angle;
        private float target_fov;

        protected override ModelFunction modelFunction => throw new System.NotImplementedException();

        protected void Start()
        {
            lidarDepthShader = Shader.Find("Custom/LidarDepthShader");
            depthTexture = new RenderTexture(cameraWidth, cameraWidth, 24, RenderTextureFormat.ARGB32) { enableRandomWrite = true };
            _ = depthTexture.Create();

            SetupCamera();
            GenerateCoords();
        }

        public void SetupCamera()
        {
            lidarCamera = TryGetComponent(out Camera camera) ? camera : gameObject.AddComponent<Camera>();

            lidarCamera.enabled = false;
            lidarCamera.targetTexture = depthTexture;
            lidarCamera.clearFlags = CameraClearFlags.SolidColor;
            lidarCamera.backgroundColor = Color.clear;
            lidarCamera.orthographic = false;

            target_fov = Mathf.CeilToInt(Mathf.Abs(verticalFOV.min) + verticalFOV.max);

            if (target_fov % 2 != 0)
            {
                target_fov++;
            }

            SetFOV(target_fov);
        }

        protected void Update()
        {
            // Calculate the angle of rotation for this frame
            float deltaRotationAngle = rotateFrequency * Time.deltaTime * 360.0f;
            sensor_sum_rotation_angle += deltaRotationAngle;

            // If the accumulated rotation angle exceeds the threshold for a single scan step
            if (sensor_sum_rotation_angle > h_delta_angle)
            {
                float currentFOV = target_fov;

                // Adjust the FOV dynamically based on the current accumulated rotation
                if (sensor_sum_rotation_angle > currentFOV)
                {
                    currentFOV = Mathf.CeilToInt(Mathf.Min(sensor_sum_rotation_angle + (2 * h_delta_angle), 178.0f));
                }

                // Ensure the FOV is even and not below 90 degrees
                if (currentFOV % 2 != 0)
                {
                    currentFOV++;
                }

                if (currentFOV < 90)
                {
                    currentFOV = 90;
                }

                SetFOV(currentFOV);

                // Update the rotation angle of the camera based on the current rotation
                lidarCamera.transform.localEulerAngles = new Vector3
                (
                    0,
                    Mathf.Repeat
                    (
                        sensor_cur_rotation_angle
                            + sensor_prev_rotation_angle
                            + (currentFOV / 2),
                        360f
                    ),
                    0
                );

                // Update the current rotation angle for the next frame
                sensor_cur_rotation_angle = Mathf.Repeat(sensor_cur_rotation_angle + sensor_prev_rotation_angle, 360f);

                // Limit the sum of rotation angles if it exceeds the current FOV
                if (sensor_sum_rotation_angle > currentFOV)
                {
                    sensor_sum_rotation_angle = currentFOV;
                }

                lidarCamera.RenderWithShader(lidarDepthShader, "");

                // Set the previous rotation angle for the next frame
                sensor_prev_rotation_angle = sensor_sum_rotation_angle;
                sensor_sum_rotation_angle = 0;
            }
        }

        public void SetFOV(float fov)
        {
            float f_x = cameraWidth / (2.0f * Mathf.Tan(Mathf.Deg2Rad * fov / 2.0f));
            lidarCamera.focalLength = f_x;
            lidarCamera.sensorSize = new Vector2(cameraWidth / f_x, cameraWidth / f_x) * lidarCamera.focalLength;
        }

        private void GenerateCoords()
        {
            h_delta_angle = (horizontalFOV.max - horizontalFOV.min) / (horizontalSamples - 1);
            v_delta_angle = (Mathf.Abs(verticalFOV.min) + verticalFOV.max) / (verticalSamples - 1);

            h_angles = Linspace(horizontalFOV.min, horizontalFOV.max - h_delta_angle, horizontalSamples);
            v_angles = Linspace(verticalFOV.min, verticalFOV.max, verticalSamples);

            for (int h_idx = 0; h_idx < horizontalSamples; h_idx++)
            {
                float angle = Mathf.Deg2Rad * h_angles[h_idx];
                for (int v_idx = 0; v_idx < verticalSamples; v_idx++)
                {
                    polar_to_cartesian_LUT.Add(new Vector3(
                        Mathf.Cos(Mathf.Deg2Rad * v_angles[v_idx]) * Mathf.Cos(angle),
                        Mathf.Cos(Mathf.Deg2Rad * v_angles[v_idx]) * Mathf.Sin(angle),
                        Mathf.Sin(Mathf.Deg2Rad * v_angles[v_idx])
                    ));
                }
            }
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
