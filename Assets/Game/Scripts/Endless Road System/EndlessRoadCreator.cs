using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace test11.EndlessRoadSystem
{
    public class EndlessRoadCreator : MonoBehaviour
    {
        #region INSPECTOR PROPERTIES

        [SerializeField] private RoadEntity[] roadPieces;

        #endregion

        #region PRIVATE PROPERTIES

        [SerializeField]private List<RoadEntity> roadPiecesList = new List<RoadEntity>();

        [SerializeField]private Vector3 currentEndPoint;
        [SerializeField] private Vector3 currentEndRotationEuler;
        private int roadPieceIndex = 0;

        #endregion

        #region UNITY EVENTS

        private void Start()
        {
            currentEndPoint = Vector3.zero;
            roadPieceIndex = 0;
            for (int i = 0; i < 5; i++)
            {
                CreateRoadPiece();
            }
        }



        #endregion


        #region PRIVATE METHODS

        [Button]
        private void CreateRoadPiece()
        {
            int randomRoadPieceIndex = Random.Range(0, roadPieces.Length);
            RoadEntity roadPiece = Instantiate(roadPieces[randomRoadPieceIndex], transform);
            roadPiecesList.Add(roadPiece);
            StartCoroutine(Co_PositionRoadPiece(roadPiece));


        }

        [Button]
        private void DeleteLatestRoadPiece()
        {
            RoadEntity roadPiece = roadPiecesList[roadPiecesList.Count - 1];
            roadPiecesList.Remove(roadPiece);
            DestroyImmediate(roadPiece.gameObject);
            currentEndPoint -= roadPiece.EndPosition;
            // currentPiecePoint--;
        }

        [Button]
        private void DeleteFirstRoadPiece()
        {
            RoadEntity roadPiece = roadPiecesList[0];
            roadPiecesList.Remove(roadPiece);
            DestroyImmediate(roadPiece.gameObject);
            currentEndPoint -= roadPiece.EndPosition;
            // currentPiecePoint--;
            
        }
        
        private IEnumerator Co_PositionRoadPiece(RoadEntity roadPiece)
        {
            yield return null;
            roadPiece.transform.position = currentEndPoint;
            
            // roadPiece.transform.rotation = Quaternion.Euler(currentEndRotationEuler);
            currentEndPoint += new Vector3(roadPiece.EndPosition.x, 0, roadPiece.EndPosition.z);
            currentEndRotationEuler -= new Vector3(0, roadPiece.EndRotationEuler.y, 0);
            
            Debug.Log("road piece end position: "+roadPiece.EndPosition);
            
            // randomize road piece index
            roadPieceIndex = Random.Range(0, roadPieces.Length);
            // currentPiecePoint++;
        }

        #endregion
    }
}