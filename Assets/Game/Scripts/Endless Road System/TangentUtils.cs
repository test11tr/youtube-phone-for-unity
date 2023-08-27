using Dreamteck.Splines;
using UnityEngine;

namespace test11.EndlessRoadSystem
{
    public static class TangentUtils
    {
        public static void TangentsFromEulerAngles(float yaw, float pitch, float roll, out Vector3 tangent,
            out Vector3 tangent2)
        {
            // Convert Euler angles to radians
            float yawRad = yaw * Mathf.Deg2Rad;
            float pitchRad = pitch * Mathf.Deg2Rad;
            float rollRad = roll * Mathf.Deg2Rad;

            // Calculate direction vectors based on trigonometry
            tangent = new Vector3(
                Mathf.Cos(yawRad) * Mathf.Cos(pitchRad),
                Mathf.Sin(pitchRad),
                Mathf.Sin(yawRad) * Mathf.Cos(pitchRad)
            );

            // Apply roll rotation
            tangent = Quaternion.Euler(0f, roll, 0f) * tangent;

            tangent2 = new Vector3(-tangent.x,-tangent.y,-tangent.z); // Calculate the perpendicular tangent2
        }
        // public static void CalculateTangentsFromEulerAngles(ref SplinePoint point, float yaw, float pitch, float roll)
        // {
        //     float yawRad = yaw * Mathf.Deg2Rad;
        //     float pitchRad = pitch * Mathf.Deg2Rad;
        //     float rollRad = roll * Mathf.Deg2Rad;
        //
        //     Vector3 tangent = new Vector3(
        //         Mathf.Cos(yawRad) * Mathf.Cos(pitchRad),
        //         Mathf.Sin(pitchRad),
        //         Mathf.Sin(yawRad) * Mathf.Cos(pitchRad)
        //     );
        //
        //     tangent = Quaternion.Euler(0f, roll, 0f) * tangent;
        //     Vector3 tangent2 = new Vector3(-tangent.x,-tangent.y,-tangent.z); // Calculate the perpendicular tangent2
        //         
        //     point.tangent = tangent;
        //     point.tangent2 = tangent2;
        // }
    }
    
}