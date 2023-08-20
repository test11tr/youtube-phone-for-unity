using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test11.EndlessRoadSystem
{
    public class EndlessRoadCreator : MonoBehaviour
    {
        #region INSPECTOR PROPERTIES

        [SerializeField] private GameObject roadPrefab;
        [SerializeField] private GameObject roadParent;
        [SerializeField] private GameObject roadStartPoint;
        [SerializeField] private GameObject roadEndPoint;
        [SerializeField] private int roadCount;
        


        #endregion
        

        #region PRIVATE VARIABLES

        private List<GameObject> roadList = new List<GameObject>();

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            InitializeRoad();
        }

        #endregion

        #region PRIVATE METHODS

        private void InitializeRoad()
        {
            for (int i = 0; i < roadCount; i++)
            {

            }
        }

        private void CreateRoad()
        {
            GameObject road = Instantiate(roadPrefab, roadParent.transform.position, Quaternion.identity, roadParent.transform);
            roadList.Add(road);
        }
        
        
        
        #endregion
        
        #region PUBLIC METHODS
        
        
        
        #endregion
        
    }
}
