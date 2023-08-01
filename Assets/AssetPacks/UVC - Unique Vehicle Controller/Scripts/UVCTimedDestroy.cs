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
    public class UVCTimedDestroy : MonoBehaviour
    {
        public float DestroyingTime = 7f;

        public static UVCTimedDestroy TD;

        IEnumerator Start()
        {
            TD = this;

            yield return new WaitForSeconds(DestroyingTime);
            Destroy(gameObject);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}