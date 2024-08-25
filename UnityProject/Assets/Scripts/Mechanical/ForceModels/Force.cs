using UnityEngine;

namespace SimuNEX.Mechanical
{
    public static class Force
    {
        public static Vector6DOF ApplyAtPosition(RBModel RB, Vector3 force, Vector3 pos)
        {
            return new()
            {
                linear = force,
                angular = Vector3.Cross(force, RB.transform.InverseTransformPoint(pos))
            };
        }
    }
}
