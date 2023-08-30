using System;
using Dreamteck.Splines;
using Sirenix.OdinInspector;
using UnityEngine;

namespace test11.EndlessRoadSystem
{
    public class SplineBuilder : MonoBehaviour
    {
        #region INSPECTOR PROPERTIES

        [SerializeField] private RoadEntity road;
        // [SerializeField] private Vector3 splineVector;
        // [SerializeField] private float splineStartAngle;
        // [SerializeField] private float splineEndAngle;
        //
        // [SerializeField] private float yaw;
        // [SerializeField] private float pitch;
        // [SerializeField] private float roll;

        #endregion

        #region PRIVATE PROPERTIES

        #endregion

        #region PUBLIC PROPERTIES

        public Vector3 StartPosition => road.Spline.GetPoint(0).position;

        public Vector3 EndPosition => road.Spline.GetPoint(road.Spline.pointCount - 1).position;
        public Vector3 StartRotationEuler => StartRotation.eulerAngles;
        public Vector3 EndRotationEuler => EndRotation.eulerAngles;

        public Quaternion StartRotation
        {
            get
            {
                SplinePoint firstPoint = road.Spline.GetPoint(0);
                Vector3 splineStartTangent = firstPoint.tangent;
                Vector3 splineStartTangent2 = firstPoint.tangent2;

                return Quaternion.LookRotation(splineStartTangent.normalized, splineStartTangent2.normalized);
            }
        }

        public Quaternion EndRotation
        {
            get
            {
                SplinePoint lastPoint = road.Spline.GetPoint(road.Spline.pointCount - 1);
                Vector3 splineEndTangent = lastPoint.tangent;
                Vector3 splineEndTangent2 = lastPoint.tangent2;

                return Quaternion.LookRotation(splineEndTangent.normalized, splineEndTangent2.normalized);
            }
        }

        #endregion

        #region UNITY METHODS



        #endregion

        #region PRIVATE METHODS

        [Button]
        private void GetStartEndPointProperties()
        {
            int splinePointCount = road.Spline.pointCount;
            SplinePoint firstPoint = road.Spline.GetPoint(0);
            SplinePoint lastPoint = road.Spline.GetPoint(splinePointCount - 1);

            Vector3 splineStartTangent = firstPoint.tangent;
            Vector3 splineStartTangent2 = firstPoint.tangent2;
            Vector3 splineEndTangent = lastPoint.tangent;
            Vector3 splineEndTangent2 = lastPoint.tangent2;


            // Calculate the start angle based on the first tangent points
            Quaternion startRot =
                Quaternion.LookRotation(splineStartTangent.normalized, splineStartTangent2.normalized);
            Vector3 startEuler = startRot.eulerAngles;

            // Calculate the end angle based on the last tangent points
            Quaternion endRot = Quaternion.LookRotation(splineEndTangent.normalized, splineEndTangent2.normalized);
            Vector3 endEuler = endRot.eulerAngles;

            // Display Euler angles in the debug log
            Debug.Log("Start Euler Angles: " + startEuler);
            Debug.Log("End Euler Angles: " + endEuler);
        }

        // [Button]
        // private void SetStartAngle(float yaw, float pitch, float roll)
        // {
        //     SplinePoint startSplinePoint = road.Spline.GetPoint(0);
        //
        //     TangentUtils.TangentsFromEulerAngles(yaw, pitch, roll, out Vector3 tangent, out Vector3 tangent2);
        //     
        //     SplinePoint newPoint = new SplinePoint(startSplinePoint.position, tangent, tangent2, startSplinePoint.size, startSplinePoint.color);
        //     
        //     road.Spline.SetPoint(0, newPoint); // Set the updated SplinePoint back to the spline
        //
        //     // Debug.Log("new angles: " + startSplinePoint.tangent + " " + startSplinePoint.tangent2);
        // }

        #endregion


        #region PUBLIC METHODS

        #endregion
    }
}