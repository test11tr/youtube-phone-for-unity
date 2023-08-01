using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using test11.Managers;
using UniqueVehicleController;

namespace test11.Managers
{
    public class WheelParkingTrigger : MonoBehaviour
    {
        [Header("Important References - Don't Assign")]
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private UVCUniqueVehicleController _carController;
        [SerializeField] private ParkingManager _parkingManager;
        
        [Header("Setup Settings")]
        [SerializeField] private colliderChecker _colliderChecker;
        [SerializeField] private enum colliderChecker{
            FL,
            FR,
            RL,
            RR,
            VehicleFront,
            VehicleRear,
        }

        void Start()
        {
            if (_levelManager == null)
            {
                _levelManager = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelManager>();
            }
            if (_carController == null)
            {
                _carController = _levelManager.SpawnedPlayerVehicle.GetComponent<UVCUniqueVehicleController>();
            }
            if (_parkingManager == null)
            {
                _parkingManager = GetComponentInParent<ParkingManager>();
            }
        }

        void OnTriggerStay(Collider other) {
            if(other.tag == "Player"){
                if(_colliderChecker == colliderChecker.FL){
                    _parkingManager.fl = true;
                    //print("fl = true");
                }else if (_colliderChecker == colliderChecker.FR){
                    _parkingManager.fr = true;
                    //print("fr = true");
                }else if (_colliderChecker == colliderChecker.RL){
                    _parkingManager.rl = true;
                    //print("rl = true");
                }else if (_colliderChecker == colliderChecker.RR){
                    _parkingManager.rr = true;
                    //print("rr = true");
                }
            }    
        
            if(_colliderChecker == colliderChecker.VehicleFront){
                if(other.tag == "p_vehicle_front"){
                    _parkingManager.front = true;
                    //print("front = true");
                }
            }else if (_colliderChecker == colliderChecker.VehicleRear){
                if(other.tag == "p_vehicle_rear"){
                    _parkingManager.rear = true;
                    //print("rear = true");
                }
            }   
        }

        void OnTriggerExit(Collider other){
            if(other.tag == "Player"){
                if(_colliderChecker == colliderChecker.FL){
                    _parkingManager.fl = false;
                    //print("fl = false");
                }else if (_colliderChecker == colliderChecker.FR){
                    _parkingManager.fr = false;
                     //print("fr = false");
                }else if (_colliderChecker == colliderChecker.RL){
                    _parkingManager.rl = false;
                     //print("rl = false");
                }else if (_colliderChecker == colliderChecker.RR){
                    _parkingManager.rr = false;
                     //print("rr = false");
                }
            }   

            if(_colliderChecker == colliderChecker.VehicleFront){
                if(other.tag == "p_vehicle_front"){
                    _parkingManager.front = false;
                     //print("front = false");
                }
            }else if (_colliderChecker == colliderChecker.VehicleRear){
                if(other.tag == "p_vehicle_rear"){
                    _parkingManager.rear = false;
                     //print("rear = false");
                }
            }  
        }
    }
}
