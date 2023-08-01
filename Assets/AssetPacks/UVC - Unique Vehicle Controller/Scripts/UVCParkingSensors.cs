//--------------------------------------------------------------//
//                                                              //
//             Unique Vehicle Controller v1.0.0                 //
//                  STAY TUNED FOR UPDATES                      //
//           Contact me : imolegstudio@gmail.com                //
//                                                              //
//--------------------------------------------------------------//

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UniqueVehicleController;
using UnityEngine;

namespace UniqueVehicleController
{
    #if UNITY_EDITOR
    using UnityEditor;
    #endif

    public class UVCParkingSensors : MonoBehaviour
    {
        [Header("Sensors Length")]
        public float DetectionLength = 1.2f;
        public float CloseDetectionLenght = 0.85f;
        public float TooCloseDetectionLenght = 0.5f;
        [Header("Front Sensors Position")]
        public Vector3 frontSensorPosition = new Vector3(0f, -0.45f, 1.87f);
        public float frontSideSensorPosition = 0.33f;
        public float frontSensorAngle = 60f;
        [Header("Rear Sensors Position")]
        public Vector3 rearSensorPosition = new Vector3(0f, -0.45f, -2.13f);
        public float rearSideSensorPosition = 0.33f;
        public float rearSensorAngle = 60f;
        [HideInInspector]
        public bool isOnEditMode = false;

        GameObject Car;

        public void Start()
        {
            Car = GameObject.FindWithTag("Player");
        }

        private void FixedUpdate()
        {
            if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted)
            {
                Sensors();
            }
            else
            {
                if (UVCParkingSensorsConfig.PSUI)
                {
                    UVCParkingSensorsConfig.PSUI.Detection = false;
                    UVCParkingSensorsConfig.PSUI.CloseDetection = false;
                    UVCParkingSensorsConfig.PSUI.TooCloseDetection = false;
                    UVCParkingSensorsConfig.PSUI.DetectedRightSensor();
                    UVCParkingSensorsConfig.PSUI.CloseRightSensor();
                    UVCParkingSensorsConfig.PSUI.TooCloseRightSensor();
                    UVCParkingSensorsConfig.PSUI.DetectedLeftSensor();
                    UVCParkingSensorsConfig.PSUI.CloseLeftSensor();
                    UVCParkingSensorsConfig.PSUI.TooCloseLeftSensor();
                    UVCParkingSensorsConfig.PSUI.DetectedSideRightSensor();
                    UVCParkingSensorsConfig.PSUI.CloseSideRightSensor();
                    UVCParkingSensorsConfig.PSUI.TooCloseSideRightSensor();
                    UVCParkingSensorsConfig.PSUI.DetectedSideLeftSensor();
                    UVCParkingSensorsConfig.PSUI.CloseSideLeftSensor();
                    UVCParkingSensorsConfig.PSUI.TooCloseSideLeftSensor();
                    UVCParkingSensorsConfig.PSUI.DetectedRearRightSensor();
                    UVCParkingSensorsConfig.PSUI.CloseRearRightSensor();
                    UVCParkingSensorsConfig.PSUI.TooCloseRearRightSensor();
                    UVCParkingSensorsConfig.PSUI.DetectedRearSideRightSensor();
                    UVCParkingSensorsConfig.PSUI.CloseRearSideRightSensor();
                    UVCParkingSensorsConfig.PSUI.TooCloseRearSideRightSensor();
                    UVCParkingSensorsConfig.PSUI.DetectedRearLeftSensor();
                    UVCParkingSensorsConfig.PSUI.CloseRearLeftSensor();
                    UVCParkingSensorsConfig.PSUI.TooCloseRearLeftSensor();
                    UVCParkingSensorsConfig.PSUI.DetectedRearSideLeftSensor();
                    UVCParkingSensorsConfig.PSUI.CloseRearSideLeftSensor();
                    UVCParkingSensorsConfig.PSUI.TooCloseRearSideLeftSensor();
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (isOnEditMode == true)
            {
                //RaycastHit hit;
                Vector3 sensorStartPos = transform.position;
                Vector3 rearsensorStartPos = transform.position;
                sensorStartPos += transform.forward * frontSensorPosition.z;
                sensorStartPos += transform.up * frontSensorPosition.y;
                rearsensorStartPos += transform.forward * rearSensorPosition.z;
                rearsensorStartPos += transform.up * rearSensorPosition.y;
                Gizmos.color = Color.red;
                Vector3 direction = transform.TransformDirection(Vector3.forward) * DetectionLength;
                Vector3 right = Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward;
                Vector3 rearRight = Quaternion.AngleAxis(-rearSensorAngle, transform.up) * transform.forward * -1;
                Vector3 left = Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward;
                Vector3 rearLeft = Quaternion.AngleAxis(rearSensorAngle, transform.up) * transform.forward * -1;
                sensorStartPos += transform.right * frontSideSensorPosition;
                Gizmos.DrawRay(sensorStartPos, direction);
                Gizmos.DrawRay(sensorStartPos, right * DetectionLength);
                sensorStartPos -= transform.right * frontSideSensorPosition * 2;
                Gizmos.DrawRay(sensorStartPos, direction);
                Gizmos.DrawRay(sensorStartPos, left * DetectionLength);
                rearsensorStartPos += transform.right * rearSideSensorPosition;
                Gizmos.DrawRay(rearsensorStartPos, -direction);
                Gizmos.DrawRay(rearsensorStartPos, rearRight * DetectionLength);
                rearsensorStartPos -= transform.right * rearSideSensorPosition * 2;
                Gizmos.DrawRay(rearsensorStartPos, -direction);
                Gizmos.DrawRay(rearsensorStartPos, rearLeft * DetectionLength);
            }
        }

        private void Sensors()
        {
            RaycastHit hit;
            Vector3 sensorStartPos = transform.position;
            Vector3 rearsensorStartPos = transform.position;
            sensorStartPos += transform.forward * frontSensorPosition.z;
            sensorStartPos += transform.up * frontSensorPosition.y;
            rearsensorStartPos += transform.forward * rearSensorPosition.z;
            rearsensorStartPos += transform.up * rearSensorPosition.y;
            if (UVCParkingSensorsConfig.PSUI)
            {
                UVCParkingSensorsConfig.PSUI.Detection = false;
                UVCParkingSensorsConfig.PSUI.CloseDetection = false;
                UVCParkingSensorsConfig.PSUI.TooCloseDetection = false;
                UVCParkingSensorsConfig.PSUI.DetectedRightSensor();
                UVCParkingSensorsConfig.PSUI.CloseRightSensor();
                UVCParkingSensorsConfig.PSUI.TooCloseRightSensor();
                UVCParkingSensorsConfig.PSUI.DetectedLeftSensor();
                UVCParkingSensorsConfig.PSUI.CloseLeftSensor();
                UVCParkingSensorsConfig.PSUI.TooCloseLeftSensor();
                UVCParkingSensorsConfig.PSUI.DetectedSideRightSensor();
                UVCParkingSensorsConfig.PSUI.CloseSideRightSensor();
                UVCParkingSensorsConfig.PSUI.TooCloseSideRightSensor();
                UVCParkingSensorsConfig.PSUI.DetectedSideLeftSensor();
                UVCParkingSensorsConfig.PSUI.CloseSideLeftSensor();
                UVCParkingSensorsConfig.PSUI.TooCloseSideLeftSensor();
                UVCParkingSensorsConfig.PSUI.DetectedRearRightSensor();
                UVCParkingSensorsConfig.PSUI.CloseRearRightSensor();
                UVCParkingSensorsConfig.PSUI.TooCloseRearRightSensor();
                UVCParkingSensorsConfig.PSUI.DetectedRearSideRightSensor();
                UVCParkingSensorsConfig.PSUI.CloseRearSideRightSensor();
                UVCParkingSensorsConfig.PSUI.TooCloseRearSideRightSensor();
                UVCParkingSensorsConfig.PSUI.DetectedRearLeftSensor();
                UVCParkingSensorsConfig.PSUI.CloseRearLeftSensor();
                UVCParkingSensorsConfig.PSUI.TooCloseRearLeftSensor();
                UVCParkingSensorsConfig.PSUI.DetectedRearSideLeftSensor();
                UVCParkingSensorsConfig.PSUI.CloseRearSideLeftSensor();
                UVCParkingSensorsConfig.PSUI.TooCloseRearSideLeftSensor();
            }

            if (UVCInputSystem.UIS.FrontSensors == true && Car.GetComponent<UVCUniqueVehicleController>().speedOnKmh <= 25)
            {
                //Front Right Sensor
                sensorStartPos += transform.right * frontSideSensorPosition;
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, DetectionLength))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(sensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.Detection = true;
                        UVCParkingSensorsConfig.PSUI.DetectedRightSensor();
                    }
                }
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, CloseDetectionLenght))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(sensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.CloseDetection = true;
                        UVCParkingSensorsConfig.PSUI.CloseRightSensor();
                    }
                }
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, TooCloseDetectionLenght))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(sensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.TooCloseDetection = true;
                        UVCParkingSensorsConfig.PSUI.TooCloseRightSensor();
                    }
                }
                //Front Right Side Sensor
                else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, DetectionLength))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(sensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.Detection = true;
                        UVCParkingSensorsConfig.PSUI.DetectedSideRightSensor();
                    }
                }
                if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, CloseDetectionLenght))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(sensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.CloseDetection = true;
                        UVCParkingSensorsConfig.PSUI.CloseSideRightSensor();
                    }
                }
                if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, TooCloseDetectionLenght))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(sensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.TooCloseDetection = true;
                        UVCParkingSensorsConfig.PSUI.TooCloseSideRightSensor();
                    }
                }

                //Front Left Sensor
                sensorStartPos -= transform.right * frontSideSensorPosition * 2;
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, DetectionLength))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(sensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.Detection = true;
                        UVCParkingSensorsConfig.PSUI.DetectedLeftSensor();
                    }
                }
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, CloseDetectionLenght))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(sensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.CloseDetection = true;
                        UVCParkingSensorsConfig.PSUI.CloseLeftSensor();
                    }
                }
                if (Physics.Raycast(sensorStartPos, transform.forward, out hit, TooCloseDetectionLenght))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(sensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.TooCloseDetection = true;
                        UVCParkingSensorsConfig.PSUI.TooCloseLeftSensor();
                    }
                }
                //Front Left Side Sensor
                else if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, DetectionLength))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(sensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.Detection = true;
                        UVCParkingSensorsConfig.PSUI.DetectedSideLeftSensor();
                    }
                }
                if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, CloseDetectionLenght))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(sensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.CloseDetection = true;
                        UVCParkingSensorsConfig.PSUI.CloseSideLeftSensor();
                    }
                }
                if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, TooCloseDetectionLenght))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(sensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.TooCloseDetection = true;
                        UVCParkingSensorsConfig.PSUI.TooCloseSideLeftSensor();
                    }
                }
            }

            if (UVCInputSystem.UIS.RearSensors == true && Car.GetComponent<UVCUniqueVehicleController>().speedOnKmh <= 25)
            {
                //Rear Right Sensor
                rearsensorStartPos += transform.right * frontSideSensorPosition;
                if (Physics.Raycast(rearsensorStartPos, transform.forward * -1, out hit, DetectionLength))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(rearsensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.Detection = true;
                        UVCParkingSensorsConfig.PSUI.DetectedRearRightSensor();
                    }
                }
                if (Physics.Raycast(rearsensorStartPos, transform.forward * -1, out hit, CloseDetectionLenght))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(rearsensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.CloseDetection = true;
                        UVCParkingSensorsConfig.PSUI.CloseRearRightSensor();
                    }
                }
                if (Physics.Raycast(rearsensorStartPos, transform.forward * -1, out hit, TooCloseDetectionLenght))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(rearsensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.TooCloseDetection = true;
                        UVCParkingSensorsConfig.PSUI.TooCloseRearRightSensor();
                    }
                }
                //Rear Right Side Sensor
                else if (Physics.Raycast(rearsensorStartPos, Quaternion.AngleAxis(-rearSensorAngle, transform.up) * transform.forward * -1, out hit, DetectionLength))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(rearsensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.Detection = true;
                        UVCParkingSensorsConfig.PSUI.DetectedRearSideRightSensor();
                    }
                }
                if (Physics.Raycast(rearsensorStartPos, Quaternion.AngleAxis(-rearSensorAngle, transform.up) * transform.forward * -1, out hit, CloseDetectionLenght))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(rearsensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.CloseDetection = true;
                        UVCParkingSensorsConfig.PSUI.CloseRearSideRightSensor();
                    }
                }
                if (Physics.Raycast(rearsensorStartPos, Quaternion.AngleAxis(-rearSensorAngle, transform.up) * transform.forward * -1, out hit, TooCloseDetectionLenght))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(rearsensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.TooCloseDetection = true;
                        UVCParkingSensorsConfig.PSUI.TooCloseRearSideRightSensor();
                    }
                }
                //Rear Left Sensor
                rearsensorStartPos -= transform.right * frontSideSensorPosition * 2;
                if (Physics.Raycast(rearsensorStartPos, transform.forward * -1, out hit, DetectionLength))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(rearsensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.Detection = true;
                        UVCParkingSensorsConfig.PSUI.DetectedRearLeftSensor();
                    }
                }
                if (Physics.Raycast(rearsensorStartPos, transform.forward * -1, out hit, CloseDetectionLenght))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(rearsensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.CloseDetection = true;
                        UVCParkingSensorsConfig.PSUI.CloseRearLeftSensor();
                    }
                }
                if (Physics.Raycast(rearsensorStartPos, transform.forward * -1, out hit, TooCloseDetectionLenght))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(rearsensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.TooCloseDetection = true;
                        UVCParkingSensorsConfig.PSUI.TooCloseRearLeftSensor();
                    }
                }
                //Rear Left Side Sensor
                else if (Physics.Raycast(rearsensorStartPos, Quaternion.AngleAxis(rearSensorAngle, transform.up) * transform.forward * -1, out hit, DetectionLength))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(rearsensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.Detection = true;
                        UVCParkingSensorsConfig.PSUI.DetectedRearSideLeftSensor();
                    }
                }
                if (Physics.Raycast(rearsensorStartPos, Quaternion.AngleAxis(rearSensorAngle, transform.up) * transform.forward * -1, out hit, CloseDetectionLenght))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(rearsensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.CloseDetection = true;
                        UVCParkingSensorsConfig.PSUI.CloseRearSideLeftSensor();
                    }
                }
                if (Physics.Raycast(rearsensorStartPos, Quaternion.AngleAxis(rearSensorAngle, transform.up) * transform.forward * -1, out hit, TooCloseDetectionLenght))
                {
                    if (!hit.collider.CompareTag("Ground") && Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                    {
                        Debug.DrawLine(rearsensorStartPos, hit.point);
                        UVCParkingSensorsConfig.PSUI.TooCloseDetection = true;
                        UVCParkingSensorsConfig.PSUI.TooCloseRearSideLeftSensor();
                    }
                }
            }
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(UVCParkingSensors))]
    class UVCParkingSensorsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            UVCParkingSensors parkingsensors = (UVCParkingSensors)target;
            if (GUILayout.Button("Edit Mode"))
            {
                if (parkingsensors.isOnEditMode == false)
                {
                    parkingsensors.isOnEditMode = true;
                }
                else if (parkingsensors.isOnEditMode == true)
                {
                    parkingsensors.isOnEditMode = false;
                }
            }
        }
    }
    #endif
}