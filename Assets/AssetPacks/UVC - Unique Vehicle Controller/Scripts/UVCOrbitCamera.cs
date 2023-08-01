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
    public class UVCOrbitCamera : MonoBehaviour
    {
        [Header("Distance")]
        public float Distance = 6.5f;
        public float minDistance = 6.5f;
        public float maxDistance = 12.25f;
        [Header("Speed")]
        public float Speed = 1;
        public float SpeedX = 175.0f;
        public float SpeedY = 75.0f;
        [HideInInspector]
        public float PinchSpeed = 0;
        [Header("Transform")]
        public int yMinLimit = 25;
        public int yMaxLimit = 50;
        public Vector3 OffSet;

        private float yOffset;
        private float x = 0.0f;
        private float y = 0.0f;
        public int camResetTime = 5;
        private float resetTimer;

        public bool Dragging;
        private bool notCourutine = true;

        private Touch Touch;

        Transform Car;
        Transform MainCameraPos;

        IEnumerator Start()
        {
            Vector3 angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;

            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().freezeRotation = true;
            }

            Car = GameObject.FindGameObjectWithTag("Player").transform;
            MainCameraPos = GameObject.FindGameObjectWithTag("MainCameraPosition").transform;
            resetTimer = camResetTime;
            yield return new WaitForSeconds(0.3f);
        }

        void Update()
        {
            //if(Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted){
                if (Dragging)
                {
                    if (Car && GetComponent<Camera>())
                    {
                        Distance = Mathf.Clamp(Distance, minDistance, maxDistance);

                        #if UNITY_ANDROID
                        {
                            if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved) 
                            {
                                Touch = Input.GetTouch (0);
                                x += Touch.deltaPosition.x * SpeedX * 0.02f;
                                y -= Touch.deltaPosition.y * SpeedY * 0.02f;
                            }

                            if (Input.GetMouseButton(0))
                            {
                                x += Input.GetAxis("Mouse X") * SpeedX * 0.02f;
                                y -= Input.GetAxis("Mouse Y") * SpeedY * 0.02f;
                            }
                        }
                        #else
                        {
                            if (Input.GetMouseButton(0))
                            {
                                x += Input.GetAxis("Mouse X") * SpeedX * 0.02f;
                                y -= Input.GetAxis("Mouse Y") * SpeedY * 0.02f;
                            }
                        }
                        #endif

                    
                    }
                }

                
            //}
        }

        void FixedUpdate()
        {
            //if(Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted){
                if (!Dragging)
                {
                    resetTimer += Time.deltaTime;
                    if(resetTimer > camResetTime){
                        Vector3 dPos = MainCameraPos.position + OffSet;
                        Vector3 sPos = Vector3.Lerp(transform.position, dPos, Speed * Time.deltaTime * 2);
                        transform.position = sPos;
                        transform.LookAt(Car.transform.position);
                        Vector3 angles = transform.eulerAngles;
                        x = angles.y;
                        y = angles.x;
                    }    
                }else{
                    resetTimer = 0;
                }

                y = ClampAngle(y, yMinLimit, yMaxLimit);
                Quaternion rotation = Quaternion.Euler(y, x, 0);
                Vector3 vTemp = new Vector3(0.0f, 0.0f, -Distance);
                Vector3 position = rotation * vTemp + new Vector3(Car.position.x, Car.position.y + yOffset, Car.position.z);
                transform.position = Vector3.Lerp(transform.position, position, Speed);
                transform.rotation = rotation;
            //}
        }

        static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360)
            {
                angle += 360;
            }

            if (angle > 360)
            {
                angle -= 360;
            }
            return Mathf.Clamp(angle, min, max);
        }
    }
}