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
    public class UVCWheelEffects : MonoBehaviour
    {
        [Header("Effects Setup")]
        public GameObject WheelSkidPrefab;

        bool isSkiding;
        GameObject Marks;

        GameObject Car;
        GameObject Audio;

        void Start()
        {
            Car = GameObject.FindWithTag("Player");
            Audio = GameObject.FindWithTag("Audio");
        }

        void Update()
        {
            try
            {
                if (UVCWheelCollider.UVCWC.m_isGrounded)
                {
                    if (Mathf.Abs(Car.GetComponent<UVCUniqueVehicleController>().steering) >= Car.GetComponent<UVCUniqueVehicleController>().currentSteeringAngle * 0.85 && Mathf.Abs(Car.GetComponent<UVCUniqueVehicleController>().speedOnKmh) >= 81f)
                    {
                        if (!isSkiding)
                        {
                            Marks = (GameObject)Instantiate(WheelSkidPrefab, transform.position, transform.rotation);
                            Marks.name = "Marks";
                            Marks.transform.parent = gameObject.transform;
                            isSkiding = true;
                            if (Audio.GetComponent<UVCSoundSystem>().SkidSound.isPlaying == false)
                            {
                                Audio.GetComponent<UVCSoundSystem>().SkidSound.Play();
                            }
                        }
                        else
                        {
                            Marks.transform.parent = null;
                            Marks.transform.position = transform.position;
                        }
                    }
                    else if (Car.GetComponent<UVCUniqueVehicleController>().isbraking && Car.GetComponent<UVCUniqueVehicleController>().ABSSystem == false && Mathf.Abs(Car.GetComponent<UVCUniqueVehicleController>().speedOnKmh) > Car.GetComponent<UVCUniqueVehicleController>().NonAbsRange && Car.GetComponent<UVCUniqueVehicleController>().ismoving)
                    {
                        if (!isSkiding)
                        {
                            Marks = (GameObject)Instantiate(WheelSkidPrefab, transform.position, transform.rotation);
                            Marks.name = "Marks";
                            Marks.transform.parent = gameObject.transform;
                            isSkiding = true;
                            if (Audio.GetComponent<UVCSoundSystem>().SkidSound.isPlaying == false)
                            {
                                Audio.GetComponent<UVCSoundSystem>().SkidSound.Play();
                            }
                        }
                        else
                        {
                            Marks.transform.parent = null;
                            Marks.transform.position = transform.position;
                        }
                    }
                    else
                    {
                        if (isSkiding)
                        {
                            isSkiding = false;
                            Audio.GetComponent<UVCSoundSystem>().SkidSound.Stop();
                        }
                    }
                }
                else
                {
                    isSkiding = false;
                    Audio.GetComponent<UVCSoundSystem>().SkidSound.Stop();
                    Marks.transform.parent = null;
                    Marks.transform.position = transform.position;
                    Marks.GetComponent<UVCTimedDestroy>().Destroy();
                }
            }
            catch { }
        }
    }
}