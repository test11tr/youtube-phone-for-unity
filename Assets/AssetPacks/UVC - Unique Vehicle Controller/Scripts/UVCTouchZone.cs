//--------------------------------------------------------------//
//                                                              //
//             Unique Vehicle Controller v1.0.0                 //
//                  STAY TUNED FOR UPDATES                      //
//           Contact me : imolegstudio@gmail.com                //
//                                                              //
//--------------------------------------------------------------//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UniqueVehicleController
{
    public class UVCTouchZone : MonoBehaviour
    {
        UVCOrbitCamera OrbitCamera;
        
        void Start()
        {
            OrbitCamera = FindObjectOfType<UVCOrbitCamera>();
        }

        public void Drag(bool state)
        {
            if (state)
            {
                OrbitCamera.GetComponent<UVCOrbitCamera>().Dragging = true;
            }
            else
            {
                OrbitCamera.GetComponent<UVCOrbitCamera>().Dragging = false;
            }
        }
    }
}