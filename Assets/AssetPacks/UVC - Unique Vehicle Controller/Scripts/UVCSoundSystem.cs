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
    public class UVCSoundSystem : MonoBehaviour
    {
        [Header("Audio Sources")]
        [Header("Engine")]
        public AudioSource EngineSound;
        public AudioSource EngineStartSound;
        public AudioSource EngineStopSound;
        public AudioSource EngineOutOfFuelSound;
        [Header("Parking Sensors")]
        public AudioSource Detected;
        public AudioSource Close;
        public AudioSource TooClose;
        [Header("Others")]
        public AudioSource BlinkerSound;
        public AudioSource HornSound;
        public AudioSource SkidSound;
        [Header("Audio Parameters")]
        public float EnginePitchBoost = 0.4f;
        public float EnginePitchRange = 1.4f;
        public float SkidPitchBoost = 0.45f;
        public float SkidPitchRange = 1f;

        bool firstSpeed;
        float GearShift;
        float Temp1;
        int Temp2;

        GameObject Car;

        void Start()
        {
            EngineSound.pitch = EnginePitchBoost;
            SkidSound.pitch = SkidPitchBoost;

            Car = GameObject.FindWithTag("Player");
        }

        public void StartEngine()
        {
            EngineStartSound.Play();
        }

        public void StopEngine()
        {
            EngineStopSound.Play();
        }

        public void EngineOut()
        {
            EngineOutOfFuelSound.Play();
        }

        public void EngineOutStop()
        {
            EngineOutOfFuelSound.Stop();
        }

        public void StartHorn()
        {
            HornSound.Play();
        }

        public void StopHorn()
        {
            HornSound.Stop();
        }

        public void StartBlinking()
        {
            BlinkerSound.Play();
        }

        public void StopBlinking()
        {
            BlinkerSound.Stop();
        }

        public void SensorDetected()
        {
            Detected.Play();
        }

        public void SensorUndetected()
        {
            Detected.Stop();
        }

        public void SensorClose()
        {
            Close.Play();
        }

        public void SensorFar()
        {
            Close.Stop();
        }

        public void SensorTooClose()
        {
            TooClose.Play();
        }

        public void SensorTooFar()
        {
            TooClose.Stop();
        }

        public IEnumerator GearShifting()
        {
            yield return new WaitForSeconds(0.05f);
            GearShift = Car.GetComponent<UVCUniqueVehicleController>().maxGearSpeed;
        }

        void Update()
        {
            if (!firstSpeed)
            {
                GearShift = Car.GetComponent<UVCUniqueVehicleController>().maxGearSpeed;
                firstSpeed = true;
            }

            if (gameObject.tag == "Audio")
            {
                float Speed = Mathf.Abs(Car.GetComponent<UVCUniqueVehicleController>().speedOnKmh);
                Temp1 = Speed / GearShift;
                Temp2 = (int)Temp1;
                float Diffrence = Temp1 - Temp2;

                if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == false)
                {
                    EngineSound.pitch = 0f;
                }
                else
                {
                    if (Car.GetComponent<UVCUniqueVehicleController>().isneutral && Car.GetComponent<UVCUniqueVehicleController>().isoutofFuel == false)
                    {
                        if (EngineSound.pitch < EnginePitchRange)
                        {
                            if (Car.GetComponent<UVCUniqueVehicleController>().isaccelerating)
                            {
                                EngineSound.pitch += 0.01f;
                            }
                        }

                        if (EngineSound.pitch > EnginePitchBoost)
                        {
                            if (Car.GetComponent<UVCUniqueVehicleController>().isaccelerating == false)
                            {
                                EngineSound.pitch -= 0.01f;
                            }
                        }

                        if (EngineSound.pitch <= EnginePitchBoost)
                        {
                            EngineSound.pitch = EnginePitchBoost;
                        }

                        if (EngineSound.pitch >= EnginePitchRange)
                        {
                            if (Car.GetComponent<UVCUniqueVehicleController>().isaccelerating)
                            {
                                EngineSound.pitch = EnginePitchRange;
                            }
                        }
                    }
                    else
                    {
                        if (Car.GetComponent<UVCUniqueVehicleController>().ismoving == false)
                        {
                            EngineSound.pitch = Mathf.Lerp(EngineSound.pitch, EnginePitchBoost, .03f);
                        }
                        else
                        {
                            if (Car.GetComponent<UVCUniqueVehicleController>().speedOnKmh >= Car.GetComponent<UVCUniqueVehicleController>().maxEngineSpeed - 3)
                            {
                                EngineSound.pitch = Mathf.Lerp(EngineSound.pitch, (EnginePitchRange * Temp2) + EnginePitchBoost, .02f);
                            }
                            else
                            {
                                EngineSound.pitch = Mathf.Lerp(EngineSound.pitch, (EnginePitchRange * Diffrence) + EnginePitchBoost, .082f);
                            }

                            if (Car.GetComponent<UVCUniqueVehicleController>().isaccelerating)
                            {
                                SkidSound.pitch = Mathf.Lerp(SkidSound.pitch, SkidPitchRange, .00035f);
                            }
                            else
                            {
                                SkidSound.pitch = Mathf.Lerp(SkidSound.pitch, SkidPitchBoost, .00035f);
                            }
                        }
                    }
                }
            }
        }
    }
}