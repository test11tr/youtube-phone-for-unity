//--------------------------------------------------------------//
//                                                              //
//             Unique Vehicle Controller v1.0.0                 //
//                  STAY TUNED FOR UPDATES                      //
//           Contact me : imolegstudio@gmail.com                //
//                                                              //
//--------------------------------------------------------------//

using System.Collections;
using System.Collections.Generic;
using UniqueVehicleController;
using UnityEngine;

namespace UniqueVehicleController
{
    public class UVCParkingSensorsConfig : MonoBehaviour
    {
        [Header("UI Properties")]
        [Header("Front Parent")]
        public GameObject FrontSensors;
        [Header("Rear Parent")]
        public GameObject RearSensors;
        [Header("Car Image")]
        public GameObject CarImage;
        [Header("Front Right")]
        public GameObject DetectedRight;
        public GameObject CloseRight;
        public GameObject TooCloseRight;
        [Header("Front Left")]
        public GameObject DetectedLeft;
        public GameObject CloseLeft;
        public GameObject TooCloseLeft;
        [Header("Front Right Side")]
        public GameObject DetectedSideRight;
        public GameObject CloseSideRight;
        public GameObject TooCloseSideRight;
        [Header("Front Left Side")]
        public GameObject DetectedSideLeft;
        public GameObject CloseSideLeft;
        public GameObject TooCloseSideLeft;
        [Header("Rear Right")]
        public GameObject DetectedRearRight;
        public GameObject CloseRearRight;
        public GameObject TooCloseRearRight;
        [Header("Rear Left")]
        public GameObject DetectedRearLeft;
        public GameObject CloseRearLeft;
        public GameObject TooCloseRearLeft;
        [Header("Rear Right Side")]
        public GameObject DetectedRearSideRight;
        public GameObject CloseRearSideRight;
        public GameObject TooCloseRearSideRight;
        [Header("Rear Left Side")]
        public GameObject DetectedRearSideLeft;
        public GameObject CloseRearSideLeft;
        public GameObject TooCloseRearSideLeft;
        [HideInInspector]
        public bool Detection;
        [HideInInspector]
        public bool SoundDetection;
        [HideInInspector]
        public bool CloseDetection;
        [HideInInspector]
        public bool SoundCloseDetection;
        [HideInInspector]
        public bool TooCloseDetection;
        [HideInInspector]
        public bool SoundTooCloseDetection;

        GameObject Car;
        GameObject Audio;

        public static UVCParkingSensorsConfig PSUI;

        void Start()
        {
            PSUI = this;
            Detection = false;
            CloseDetection = false;
            TooCloseDetection = false;

            Car = GameObject.FindWithTag("Player");
            Audio = GameObject.FindWithTag("Audio");
        }

        // Front Right Sensor
        public void DetectedRightSensor()
        {
            if (Detection == true)
            {
                DetectedRight.SetActive(true);
            }

            if (Detection == false)
            {
                DetectedRight.SetActive(false);
            }
        }

        public void CloseRightSensor()
        {
            if (CloseDetection == true)
            {
                CloseRight.SetActive(true);
            }

            if (CloseDetection == false)
            {
                CloseRight.SetActive(false);
            }
        }

        public void TooCloseRightSensor()
        {
            if (TooCloseDetection == true)
            {
                TooCloseRight.SetActive(true);
            }

            if (TooCloseDetection == false)
            {
                TooCloseRight.SetActive(false);
            }
        }

        // Front Left Sensor
        public void DetectedLeftSensor()
        {
            if (Detection == true)
            {
                DetectedLeft.SetActive(true);
            }

            if (Detection == false)
            {
                DetectedLeft.SetActive(false);
            }
        }

        public void CloseLeftSensor()
        {
            if (CloseDetection == true)
            {
                CloseLeft.SetActive(true);
            }

            if (CloseDetection == false)
            {
                CloseLeft.SetActive(false);
            }
        }

        public void TooCloseLeftSensor()
        {
            if (TooCloseDetection == true)
            {
                TooCloseLeft.SetActive(true);
            }

            if (TooCloseDetection == false)
            {
                TooCloseLeft.SetActive(false);
            }
        }

        // Front Right Side Sensor
        public void DetectedSideRightSensor()
        {
            if (Detection == true)
            {
                DetectedSideRight.SetActive(true);
            }

            if (Detection == false)
            {
                DetectedSideRight.SetActive(false);
            }
        }

        public void CloseSideRightSensor()
        {
            if (CloseDetection == true)
            {
                CloseSideRight.SetActive(true);
            }

            if (CloseDetection == false)
            {
                CloseSideRight.SetActive(false);
            }
        }

        public void TooCloseSideRightSensor()
        {
            if (TooCloseDetection == true)
            {
                TooCloseSideRight.SetActive(true);
            }

            if (TooCloseDetection == false)
            {
                TooCloseSideRight.SetActive(false);
            }
        }

        // Front Left Side Sensor
        public void DetectedSideLeftSensor()
        {
            if (Detection == true)
            {
                DetectedSideLeft.SetActive(true);
            }

            if (Detection == false)
            {
                DetectedSideLeft.SetActive(false);
            }
        }

        public void CloseSideLeftSensor()
        {
            if (CloseDetection == true)
            {
                CloseSideLeft.SetActive(true);
            }

            if (CloseDetection == false)
            {
                CloseSideLeft.SetActive(false);
            }
        }

        public void TooCloseSideLeftSensor()
        {
            if (TooCloseDetection == true)
            {
                TooCloseSideLeft.SetActive(true);
            }

            if (TooCloseDetection == false)
            {
                TooCloseSideLeft.SetActive(false);
            }
        }

        // Rear Right Sensor
        public void DetectedRearRightSensor()
        {
            if (Detection == true)
            {
                DetectedRearRight.SetActive(true);
            }

            if (Detection == false)
            {
                DetectedRearRight.SetActive(false);
            }
        }

        public void CloseRearRightSensor()
        {
            if (CloseDetection == true)
            {
                CloseRearRight.SetActive(true);
            }

            if (CloseDetection == false)
            {
                CloseRearRight.SetActive(false);
            }
        }

        public void TooCloseRearRightSensor()
        {
            if (TooCloseDetection == true)
            {
                TooCloseRearRight.SetActive(true);
            }

            if (TooCloseDetection == false)
            {
                TooCloseRearRight.SetActive(false);
            }
        }

        // Rear Side Right Sensor
        public void DetectedRearSideRightSensor()
        {
            if (Detection == true)
            {
                DetectedRearSideRight.SetActive(true);
            }

            if (Detection == false)
            {
                DetectedRearSideRight.SetActive(false);
            }
        }

        public void CloseRearSideRightSensor()
        {
            if (CloseDetection == true)
            {
                CloseRearSideRight.SetActive(true);
            }

            if (CloseDetection == false)
            {
                CloseRearSideRight.SetActive(false);
            }
        }

        public void TooCloseRearSideRightSensor()
        {
            if (TooCloseDetection == true)
            {
                TooCloseRearSideRight.SetActive(true);
            }

            if (TooCloseDetection == false)
            {
                TooCloseRearSideRight.SetActive(false);
            }
        }
        // Rear Left Sensor
        public void DetectedRearLeftSensor()
        {
            if (Detection == true)
            {
                DetectedRearLeft.SetActive(true);
            }

            if (Detection == false)
            {
                DetectedRearLeft.SetActive(false);
            }
        }

        public void CloseRearLeftSensor()
        {
            if (CloseDetection == true)
            {
                CloseRearLeft.SetActive(true);
            }

            if (CloseDetection == false)
            {
                CloseRearLeft.SetActive(false);
            }
        }

        public void TooCloseRearLeftSensor()
        {
            if (TooCloseDetection == true)
            {
                TooCloseRearLeft.SetActive(true);
            }

            if (TooCloseDetection == false)
            {
                TooCloseRearLeft.SetActive(false);
            }
        }

        // Rear Side Left Sensor
        public void DetectedRearSideLeftSensor()
        {
            if (Detection == true)
            {
                DetectedRearSideLeft.SetActive(true);
            }

            if (Detection == false)
            {
                DetectedRearSideLeft.SetActive(false);
            }
        }

        public void CloseRearSideLeftSensor()
        {
            if (CloseDetection == true)
            {
                CloseRearSideLeft.SetActive(true);
            }

            if (CloseDetection == false)
            {
                CloseRearSideLeft.SetActive(false);
            }
        }

        public void TooCloseRearSideLeftSensor()
        {
            if (TooCloseDetection == true)
            {
                TooCloseRearSideLeft.SetActive(true);
            }

            if (TooCloseDetection == false)
            {
                TooCloseRearSideLeft.SetActive(false);
            }
        }

        void Update()
        {
            if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted && UVCInputSystem.UIS.FrontSensors == true || UVCInputSystem.UIS.RearSensors == true)
            {
                if (!Detection && !CloseDetection && !TooCloseDetection)
                {
                    if (Audio)
                    {
                        Audio.GetComponent<UVCSoundSystem>().SensorUndetected();
                        Audio.GetComponent<UVCSoundSystem>().SensorFar();
                        Audio.GetComponent<UVCSoundSystem>().SensorTooFar();
                    }
                    SoundDetection = false;
                    SoundCloseDetection = false;
                    SoundTooCloseDetection = false;
                }

                if (Detection && !CloseDetection && !TooCloseDetection)
                {
                    if (!SoundDetection)
                    {
                        if (Audio)
                        {
                            Audio.GetComponent<UVCSoundSystem>().SensorDetected();
                            Audio.GetComponent<UVCSoundSystem>().SensorFar();
                            Audio.GetComponent<UVCSoundSystem>().SensorTooFar();
                        }
                        SoundDetection = true;
                        SoundCloseDetection = false;
                        SoundTooCloseDetection = false;
                    }
                }

                if (Detection && CloseDetection && !TooCloseDetection)
                {
                    if (!SoundCloseDetection)
                    {
                        if (Audio)
                        {
                            Audio.GetComponent<UVCSoundSystem>().SensorUndetected();
                            Audio.GetComponent<UVCSoundSystem>().SensorClose();
                            Audio.GetComponent<UVCSoundSystem>().SensorTooFar();
                        }
                        SoundDetection = false;
                        SoundCloseDetection = true;
                        SoundTooCloseDetection = false;
                    }
                }

                if (Detection && CloseDetection && TooCloseDetection)
                {
                    if (!SoundTooCloseDetection)
                    {
                        if (Audio)
                        {
                            Audio.GetComponent<UVCSoundSystem>().SensorUndetected();
                            Audio.GetComponent<UVCSoundSystem>().SensorFar();
                            Audio.GetComponent<UVCSoundSystem>().SensorTooClose();
                        }
                        SoundDetection = false;
                        SoundCloseDetection = false;
                        SoundTooCloseDetection = true;
                    }
                }
            }
            else
            {
                if (Audio)
                {
                    Audio.GetComponent<UVCSoundSystem>().SensorUndetected();
                    Audio.GetComponent<UVCSoundSystem>().SensorFar();
                    Audio.GetComponent<UVCSoundSystem>().SensorTooFar();
                }
                SoundDetection = false;
                SoundCloseDetection = false;
                SoundTooCloseDetection = false;
            }

            if (UVCInputSystem.UIS.FrontSensors == false)
            {
                FrontSensors.SetActive(false);
            }
            else
            {
                FrontSensors.SetActive(true);
            }

            if (UVCInputSystem.UIS.RearSensors == false)
            {
                RearSensors.SetActive(false);
            }
            else
            {
                RearSensors.SetActive(true);
            }

            if (UVCInputSystem.UIS.FrontSensors == false && UVCInputSystem.UIS.RearSensors == false)
            {
                CarImage.SetActive(false);
                Detection = false;
                CloseDetection = false;
                TooCloseDetection = false;
            }
            else
            {
                CarImage.SetActive(true);
            }
        }
    }
}