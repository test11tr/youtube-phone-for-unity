using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test11
{
    public class ActivateDeactivateGameObject : MonoBehaviour
    {
        #region INSPECTOR PROPERTIES

        [SerializeField] private GameObject[] gameObjectToDeactivate;
        [SerializeField] private GameObject[] gameObjectToActivate;

        #endregion



        #region PUBLIC METHODS

        public void ActivateGameObjects()
        {
            foreach (var gameObject in gameObjectToActivate)
                gameObject.SetActive(true);
            foreach (var gameObject in gameObjectToDeactivate)
                gameObject.SetActive(false);
        }

        #endregion
    }
}
