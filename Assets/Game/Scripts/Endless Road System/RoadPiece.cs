using UnityEngine;

namespace test11.EndlessRoadSystem
{
    public class RoadPiece : MonoBehaviour
    {
        #region INSPECTOR PROPERTIES

        [SerializeField] private GameObject roadPrefab;

        [SerializeField] private Vector3 startOffset;
        [SerializeField] private Vector3 endOffset;

        #endregion

        #region PUBLIC PROPERTIES

        public Vector3 StartOffset
        {
            get { return startOffset; }
        }

        public Vector3 EndOffset
        {
            get { return endOffset; }
        }

        #endregion
    }
}