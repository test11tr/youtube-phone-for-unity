using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace test11.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Car[] _playerVehicles;
        [SerializeField] private Transform SpawnPoint;

        private GameObject p_spawnedPlayerVehicle;
        public GameObject SpawnedPlayerVehicle => p_spawnedPlayerVehicle;

        [SerializeField] bool drawGizmos = true;
        private int currentCarIndex;

        private void Awake()
        {
            //Pool Vehicle
            currentCarIndex = PlayerPrefs.GetInt("CurrentCar");
            if(SceneManager.GetActiveScene().name == "00-MainMenu"){
                p_spawnedPlayerVehicle = Instantiate(_playerVehicles[currentCarIndex].carVisualPrefab, SpawnPoint.position, Quaternion.identity, SpawnPoint);
            }else{
                p_spawnedPlayerVehicle = Instantiate(_playerVehicles[currentCarIndex].carPlayablePrefab, SpawnPoint.position, Quaternion.identity);
            }
            
            //p_spawnedPlayerVehicle.SetActive(false);
        }
        
        private void Start()
        {
            p_spawnedPlayerVehicle.transform.position = SpawnPoint.position;
            p_spawnedPlayerVehicle.transform.rotation = SpawnPoint.rotation;
            p_spawnedPlayerVehicle.SetActive(true);
        }

        private void ReloadScene(){
            string currentScene = SceneManager.GetActiveScene ().name; 
            SceneManager.LoadScene(currentScene);
        }

        public void PlayerCarInstantiate(){
            currentCarIndex = PlayerPrefs.GetInt("CurrentCar");
            if(SpawnPoint.childCount > 0){
                for (int i = 0; i < SpawnPoint.childCount; i++)
                {
                    Destroy(SpawnPoint.GetChild(i).gameObject);
                }
                p_spawnedPlayerVehicle = Instantiate(_playerVehicles[currentCarIndex].carVisualPrefab, SpawnPoint.position, Quaternion.identity, SpawnPoint);
            }
        }

        void OnDrawGizmos()
        {
            if(drawGizmos){
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(SpawnPoint.position, 1);
            }
            
        }
    }
}
