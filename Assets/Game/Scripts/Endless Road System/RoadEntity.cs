using Dreamteck.Splines;
using UnityEngine;

namespace test11.EndlessRoadSystem
{
    public class RoadEntity : MonoBehaviour
    {
        #region

        [SerializeField] private SplineComputer spline;
        [SerializeField] private PathGenerator path;
        [SerializeField] private SplineBuilder splineBuilder;
        
        [SerializeField] private float endRotationEulerY;

        #endregion

        #region PUBLIC PROPERTIES

        public SplineComputer Spline => spline;
        public PathGenerator Path => path;
        
        public Vector3 StartPosition => splineBuilder.StartPosition;
        public Vector3 EndPosition => splineBuilder.EndPosition;
        public Quaternion StartRotation => splineBuilder.StartRotation;
        public Quaternion EndRotation => splineBuilder.EndRotation;
        
        public Vector3 StartRotationEuler => splineBuilder.StartRotationEuler;
        public Vector3 EndRotationEuler => new Vector3(0, endRotationEulerY, 0);



        #endregion
        
        #region PUBLIC METHODS
        
        public void RebuildRoad()
        {
            path.RebuildImmediate();
            spline.RebuildImmediate();
        }
        
        #endregion
    }
}