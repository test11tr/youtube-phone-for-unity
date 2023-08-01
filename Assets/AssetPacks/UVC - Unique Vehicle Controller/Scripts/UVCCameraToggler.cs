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
	public class UVCCameraToggler : MonoBehaviour
	{
		public GameObject[] Cameras;
        UVCOrbitCamera OrbitCamera;
		int CurrentCamera;
	
 	   void Start()
	   {
            CurrentCamera = 0;
            OrbitCamera = FindObjectOfType<UVCOrbitCamera>();
	   }
	
		public void SetCameras(int idx)
		{
			if (!OrbitCamera.gameObject.activeSelf)
			{
                for (int i = 0; i < Cameras.Length; i++)
                {
                    if (i == idx)
                    {
                        Cameras[i].SetActive(true);
                    }
                    else
                    {
                        Cameras[i].SetActive(false);
                    }
                }
			}
		}
	
		public void ToggleCameras()
		{
			if (OrbitCamera.gameObject.activeSelf)
			{
                OrbitCamera.gameObject.SetActive(false);
			}

            if (!OrbitCamera.gameObject.activeSelf)
            {
                CurrentCamera++;

                if (CurrentCamera > Cameras.Length - 1)
                {
                    CurrentCamera = 0;
                    OrbitCamera.gameObject.SetActive(true);
                    for (int i = 0; i < Cameras.Length; i++)
					{
                        Cameras[i].SetActive(false);
					}
                }

                SetCameras(CurrentCamera - 1);
            }
		}
	}
}