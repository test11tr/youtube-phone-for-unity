using System;
using Dreamteck.Splines;
using Sirenix.OdinInspector;
using UnityEngine;

namespace test11.EndlessRoadSystem
{
    public class SplineBuilder : MonoBehaviour
    {
        #region INSPECTOR PROPERTIES

        [SerializeField] private SplineComputer roadSpline;
        [SerializeField] private PathGenerator pathGenerator;

        [SerializeField] private Vector3 splineVector;
        [SerializeField] private float splineStartAngle;
        [SerializeField] private float splineEndAngle;

        [SerializeField] private float yaw;
        [SerializeField] private float pitch;
        [SerializeField] private float roll;

        #endregion

        #region PRIVATE PROPERTIES

        #endregion

        #region UNITY METHODS



        private void Start()
        {
        }

        #endregion

        #region PRIVATE METHODS

        [Button]
        private void GetStartEndPointAngles()
        {
            int splinePointCount = roadSpline.pointCount;
            SplinePoint firstPoint = roadSpline.GetPoint(0);
            SplinePoint lastPoint = roadSpline.GetPoint(splinePointCount - 1);

            Vector3 splineStartTangent = firstPoint.tangent;
            Vector3 splineStartTangent2 = firstPoint.tangent2;
            Vector3 splineEndTangent = lastPoint.tangent;
            Vector3 splineEndTangent2 = lastPoint.tangent2;


            // Calculate the start angle based on the first tangent points
            Quaternion startRotation =
                Quaternion.LookRotation(splineStartTangent.normalized, splineStartTangent2.normalized);
            Vector3 startEulerAngles = startRotation.eulerAngles;

            // Calculate the end angle based on the last tangent points
            Quaternion endRotation = Quaternion.LookRotation(splineEndTangent.normalized, splineEndTangent2.normalized);
            Vector3 endEulerAngles = endRotation.eulerAngles;

            // Display Euler angles in the debug log
            Debug.Log("Start Euler Angles: " + startEulerAngles);
            Debug.Log("End Euler Angles: " + endEulerAngles);
        }

        [Button]
        private void SetStartAngle(float yaw, float pitch, float roll)
        {
            SplinePoint startSplinePoint = roadSpline.GetPoint(0);

            TangentUtils.TangentsFromEulerAngles(yaw, pitch, roll, out Vector3 tangent, out Vector3 tangent2);
            
            SplinePoint newPoint = new SplinePoint(startSplinePoint.position, tangent, tangent2, startSplinePoint.size, startSplinePoint.color);
            
            roadSpline.SetPoint(0, newPoint); // Set the updated SplinePoint back to the spline

            // Debug.Log("new angles: " + startSplinePoint.tangent + " " + startSplinePoint.tangent2);
        }

        #endregion


        #region PUBLIC METHODS

        #endregion
    }
}