//--------------------------------------------------------------//
//                                                              //
//             Unique Vehicle Controller v1.0.0                 //
//                  STAY TUNED FOR UPDATES                      //
//           Contact me : imolegstudio@gmail.com                //
//                                                              //
//--------------------------------------------------------------//

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using System.Collections;
using UniqueVehicleController;
using UnityEngine.UI;

namespace UniqueVehicleController
{
    public class UVCFuelSystem : MonoBehaviour
    {
        [Header("Parameters")]
        public int carID;
        public float maxFuel;
        public float consumption = 1f;
    
        [HideInInspector]
        public float DistanceRemaining;
        [HideInInspector]
        public float usedFuel;
        [HideInInspector]
        public float currentFuel;

        Image FuelIndicator;

        Text GasolinePrice;
        Text fuelDisplay;

        float countDown;
        float usedFuelPrice;
        float OneLittrePrice;

        GameObject Car;
    
        void Start()
        {
            countDown = consumption;
            currentFuel = PlayerPrefs.GetFloat(carID.ToString()+"FSys", maxFuel);
            if (FuelIndicator)
            {
                FuelIndicator.fillAmount = currentFuel / maxFuel;
            }

            if (gameObject.tag == "Player")
            {
                Car = GameObject.FindWithTag("Player");
            }

            // Calculating Fuel Price
            //usedFuelPrice = usedFuel * OneLittrePrice;
            //GasolinePrice.text = usedFuelPrice.ToString() + "$";
            //Maybe Next Update ;)
        }

        void Update()
        {
            if (gameObject.tag == "Player")
            {
                DistanceRemaining = currentFuel * consumption / 2.4f;
                if (Car.GetComponent<UVCUniqueVehicleController>().isaccelerating)
                {
                    if (countDown > 0)
                    {
                        countDown -= Time.deltaTime;
                    }
                    else
                    {
                        countDown = consumption;
                        currentFuel -= 1f;
                        Car.GetComponent<UVCUniqueVehicleController>().isoutofFuel = false;
                        PlayerPrefs.SetFloat(carID.ToString()+"FSys", currentFuel);
                        if (FuelIndicator)
                        {
                        FuelIndicator.fillAmount = currentFuel / maxFuel;
                        }
                    }
                }

                if (currentFuel <= 0)
                {
                    UVCInputSystem.UIS.StopEngine();
                    Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted = false;
                    Car.GetComponent<UVCUniqueVehicleController>().isoutofFuel = true;
                    currentFuel = 0;
                    PlayerPrefs.SetFloat(carID.ToString()+"FSys", currentFuel);
                    if (FuelIndicator)
                    {
                        FuelIndicator.fillAmount = currentFuel / maxFuel;
                    }
                }

                if (currentFuel > 0)
                {
                    Car.GetComponent<UVCUniqueVehicleController>().isoutofFuel = false;
                }

                if (fuelDisplay)
                {
                    fuelDisplay.text = currentFuel + "/" + maxFuel + "LT";
                }
    
                usedFuel = maxFuel - currentFuel;   
            }
        }

        public void UpdateFuel()
        {
            PlayerPrefs.SetFloat(carID.ToString()+"FSys", currentFuel);
        }

        public void UpdateIndicator()
        {
            if (FuelIndicator)
            {
                FuelIndicator.fillAmount = currentFuel / maxFuel;
            }
        }
    }
}