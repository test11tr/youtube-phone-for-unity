using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using test11.Managers;
using UniqueVehicleController;

namespace test11
{
    public class onCollision : MonoBehaviour
    {
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] public ParkingManager _parkingManager;
        [SerializeField] private UVCUniqueVehicleController _carController;
        bool CanCollid;
        void Start()
        {
            if (_levelManager == null)
            {
                _levelManager = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelManager>();
            }
            if (_parkingManager == null)
            {
                _parkingManager = GameObject.FindGameObjectWithTag("parkingManager").GetComponent<ParkingManager>();
            }
            if (_carController == null)
            {
                _carController = _levelManager.SpawnedPlayerVehicle.GetComponent<UVCUniqueVehicleController>();
            }
        }

        private void OnCollisionEnter(Collision other) {
            if(!CanCollid){
                if(other.gameObject.tag == "Player"){
                    //print("Collided with obstacle");
                    _parkingManager.CollisionCount++;
                    PlayerPrefs.SetInt("TotalCollisions", PlayerPrefs.GetInt("TotalCollisions" + 1));
                    // _parkingManager.AlarmSound.Play();
                    CanCollid = true;
                    _parkingManager.updateVisualStar();
                    _carController.onCollisionEffect.Play();
                    StartCoroutine(CanCollidCounter());
                    //_parkingManager.CollisionCountText.text = _parkingManager.collisionLimit + "/" + _parkingManager.CollisionCount.ToString();
                    /*if (_parkingManager.CollisionCount > _parkingManager.collisionLimit){
                        _parkingManager.FailedMenu.SetActive(true);
                        _levelManager.SpawnedPlayerVehicle.GetComponent<UVCUniqueVehicleController>().engineIsStarted = false;
                        _levelManager.SpawnedPlayerVehicle.GetComponent<Rigidbody>().isKinematic = true;
                        PlayerPrefs.SetInt("TotalFailed", PlayerPrefs.GetInt("TotalFailed") + 1);
                        Destroy(_parkingManager);
                    }*/
                }
            }
        }

        IEnumerator CanCollidCounter(){
            yield return new WaitForSeconds(3f);
            CanCollid = false;
            //print("cancollid to false");
        }
    }
}
