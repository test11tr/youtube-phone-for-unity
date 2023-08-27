using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace test11.EndlessRoadSystem
{
    public class EndlessRoadCreator : MonoBehaviour
    {
        #region INSPECTOR PROPERTIES

        [SerializeField] private RoadPiece[] roadPieces;

        #endregion

        #region PRIVATE PROPERTIES

        [SerializeField]private List<RoadPiece> roadPiecesList = new List<RoadPiece>();

        [SerializeField]private Vector3 currentEndPoint;
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
            RoadPiece roadPiece = Instantiate(roadPieces[roadPieceIndex], transform);
            roadPiecesList.Add(roadPiece);
            roadPiece.transform.position = currentEndPoint;
            currentEndPoint += roadPiece.EndOffset;
            
            // randomize road piece index
            roadPieceIndex = Random.Range(0, roadPieces.Length);
            // currentPiecePoint++;

        }

        [Button]
        private void DeleteLatestRoadPiece()
        {
            RoadPiece roadPiece = roadPiecesList[roadPiecesList.Count - 1];
            roadPiecesList.Remove(roadPiece);
            DestroyImmediate(roadPiece.gameObject);
            currentEndPoint -= roadPiece.EndOffset;
            // currentPiecePoint--;
        }

        [Button]
        private void DeleteFirstRoadPiece()
        {
            RoadPiece roadPiece = roadPiecesList[0];
            roadPiecesList.Remove(roadPiece);
            DestroyImmediate(roadPiece.gameObject);
            currentEndPoint -= roadPiece.EndOffset;
            // currentPiecePoint--;
            
        }

        #endregion
    }
}