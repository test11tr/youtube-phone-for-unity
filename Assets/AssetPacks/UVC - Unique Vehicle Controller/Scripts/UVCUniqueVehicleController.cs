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
using UniqueVehicleController;
using UnityEngine.UI;

namespace UniqueVehicleController
{
    #if UNITY_EDITOR
    using UnityEditor;
    #endif
    public enum DriveType { FrontWheelDrive, RearWheelDrive, AllWheelDrive }
    public enum VehicleType { Custom, Compact, SUV, FullSizeSUV, Van }
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(UVCParkingSensors))]
    [RequireComponent(typeof(UVCFuelSystem))]

    public class UVCUniqueVehicleController : MonoBehaviour
    {
        [Header("Vehicle Setup")]
        public bool startEngineOnAwake;
        public bool ABSSystem = true;
        public int carID;
        public float engineStartDuration;
        public Vector3 centerOfMass;

        [Header("Engine Specifications")]
        public float maxEngineTorque;
        public float maxEngineSpeed = 100;
        public float maxBrakeTorque;

        [Header("Steering Setup")]
        public Transform SteeringWheel;
        public float minSteeringAngle = 5;
        public float maxSteeringAngle = 30;
        public float arrowSteeringSpeed = 0.01f;
        [Range(0.5f, 1.5f)]
        public float handling = 1;
        [Range(50f, 1000f)]
        public float AntiRoll = 100.0f;
        public bool PowerSteering = true;

        [Header("Wheels Setup")]
        public DriveType driveType;
        public WheelsCalipers[] WheelCalipers;
        public WheelsColliders[] WheelColliders;
        [Range(0.1f, 5f)]
        public float wheelRadius = 0.35f;
        [Range(5000f, 70000f)]
        public float suspensionSpring = 35000f;
        [Range(500f, 6500f)]
        public float Damper = 4500f;
        [Range(0f, 10f)]
        public float suspensionDistance = 0.1f;

        [Header("GearBox Setup")]
        public VehicleType vehicleType;

        [Header("Effects")]
        public ParticleSystem exhaustEffect;
        public ParticleSystem onCollisionEffect;

        [HideInInspector]
        public bool engineIsStarted = false;
        [HideInInspector]
        public bool isbraking;
        [HideInInspector]
        public bool isaccelerating;
        [HideInInspector]
        public bool ismoving;
        [HideInInspector]
        public bool isSlidePedals;
        [HideInInspector]
        public bool isreversing;
        [HideInInspector]
        public bool isneutral;
        [HideInInspector]
        public bool isparking;
        [HideInInspector]
        public bool goingright;
        [HideInInspector]
        public bool goingleft;
        [HideInInspector]
        public bool usingarrows;
        [HideInInspector]
        public bool isoutofFuel;
        [HideInInspector]
        public bool ranged;
        [HideInInspector]
        public float[] gearRatios;

        UVCWheelCollider WheelL;
        UVCWheelCollider WheelR;

        [HideInInspector]
        public float distanceTravelled = 0;
        [HideInInspector]
        public float speedOnKmh;
        [HideInInspector]
        public float speedOnMph;
        [HideInInspector]
        public float maxGearSpeed;
        [HideInInspector]
        public float speedFactor;
        [HideInInspector]
        public float steering;
        [HideInInspector]
        public float currentSteeringAngle;
        [HideInInspector]
        public float NonAbsRange;

        private float currentWheelsTorque;
        private float gearUptemp;
        private float gearDowntemp;
        private float currentBrakeTorque;
        private float Drive;
        private float radius;
        private float circumFerence;
        private float inputTemp;
        private float AbsRange;
        private float nonAbsSteering;
        private float OutRange;
        private float tempArrowSteeringSpeed;
        private float touchSteeringWheelValue;

        private int DriveWheelsNumber;
        private int currentGear = 0;
        private int topGear;
        private int totalRatios;

        Rigidbody rb;
        Vector3 lastPosition;
        GameObject Audio;

        public void Awake()
        {
            foreach (WheelsColliders wheelsColliders in WheelColliders)
            {
                JointSpringSource rightWheelSpring = wheelsColliders.rightWheel.SuspensionSpring;
                JointSpringSource leftWheelSpring = wheelsColliders.leftWheel.SuspensionSpring;

                rightWheelSpring.Spring = suspensionSpring;
                leftWheelSpring.Spring = suspensionSpring;
                rightWheelSpring.Damper = Damper;
                leftWheelSpring.Damper = Damper;
                wheelsColliders.rightWheel.SuspensionSpring = rightWheelSpring;
                wheelsColliders.leftWheel.SuspensionSpring = leftWheelSpring;

                wheelsColliders.rightWheel.SuspensionDistance = suspensionDistance;
                wheelsColliders.leftWheel.SuspensionDistance = suspensionDistance;
                wheelsColliders.rightWheel.WheelRadius = wheelRadius;
                wheelsColliders.leftWheel.WheelRadius = wheelRadius;
                wheelsColliders.rightWheel.Mass = 1;
                wheelsColliders.leftWheel.Mass = 1;

                wheelsColliders.rightWheel.ForwardFriction = new WheelFrictionCurveSource();
                wheelsColliders.leftWheel.ForwardFriction = new WheelFrictionCurveSource();
                wheelsColliders.rightWheel.ForwardFriction.ExtremumSlip = 3f;
                wheelsColliders.leftWheel.ForwardFriction.ExtremumSlip = 3f;
                wheelsColliders.rightWheel.ForwardFriction.ExtremumValue = 6000f;
                wheelsColliders.leftWheel.ForwardFriction.ExtremumValue = 6000f;
                wheelsColliders.rightWheel.ForwardFriction.AsymptoteSlip = 4f;
                wheelsColliders.leftWheel.ForwardFriction.AsymptoteSlip = 4f;
                wheelsColliders.rightWheel.ForwardFriction.AsymptoteValue = 6000f;
                wheelsColliders.leftWheel.ForwardFriction.AsymptoteValue = 6000f;
                wheelsColliders.rightWheel.ForwardFriction.Stiffness = 4f;
                wheelsColliders.leftWheel.ForwardFriction.Stiffness = 4f;

                wheelsColliders.rightWheel.SidewaysFriction = new WheelFrictionCurveSource();
                wheelsColliders.leftWheel.SidewaysFriction = new WheelFrictionCurveSource();
                wheelsColliders.rightWheel.SidewaysFriction.ExtremumSlip = 3f;
                wheelsColliders.leftWheel.SidewaysFriction.ExtremumSlip = 3f;
                wheelsColliders.rightWheel.SidewaysFriction.ExtremumValue = 4000f;
                wheelsColliders.leftWheel.SidewaysFriction.ExtremumValue = 4000f;
                wheelsColliders.rightWheel.SidewaysFriction.AsymptoteSlip = 4f;
                wheelsColliders.leftWheel.SidewaysFriction.AsymptoteSlip = 4f;
                wheelsColliders.rightWheel.SidewaysFriction.AsymptoteValue = 4000f;
                wheelsColliders.leftWheel.SidewaysFriction.AsymptoteValue = 4000f;
                wheelsColliders.rightWheel.SidewaysFriction.Stiffness = 4f;
                wheelsColliders.leftWheel.SidewaysFriction.Stiffness = 4f;
            }

            if (vehicleType == VehicleType.Compact)
            {
                gearRatios = new float[] { 2.714f, 1.551f, 1f, 0.679f };
            }

            if (vehicleType == VehicleType.SUV)
            {
                gearRatios = new float[] { 4.71f, 3.14f, 2.11f, 1.67f, 1.29f, 1f, 0.84f, 0.67f };
            }

            if (vehicleType == VehicleType.FullSizeSUV)
            {
                gearRatios = new float[] { 4.02f, 2.36f, 1.52f, 1.15f, 0.85f, 0.66f };
            }

            if (vehicleType == VehicleType.Van)
            {
                gearRatios = new float[] { 3.8f, 2.06f, 1.26f };
            }

            maxGearSpeed = maxEngineSpeed * gearRatios[currentGear] / (float)totalRatios;
        }

        public void Start()
        {
            for (int i = 0; i < gearRatios.Length; i++)
            {
                totalRatios += Mathf.RoundToInt(gearRatios[i]);
                topGear++;
            }

            Audio = GameObject.FindWithTag("Audio");

            foreach (WheelsColliders wheelsColliders in WheelColliders)
            {
                circumFerence = 2.0f * 3.14f * wheelsColliders.rightWheel.WheelRadius;
            }
            radius = maxSteeringAngle * 4.84f;
            rb = GetComponent<Rigidbody>();
            rb.centerOfMass = centerOfMass;
            inputTemp = 0f;
            lastPosition = transform.position;
            tempArrowSteeringSpeed = arrowSteeringSpeed;

            distanceTravelled = PlayerPrefs.GetFloat(carID.ToString() + "DT", distanceTravelled);

            if (driveType == DriveType.FrontWheelDrive)
            {
                foreach (WheelsColliders wheelsColliders in WheelColliders)
                {
                    if (wheelsColliders.steering)
                    {
                        wheelsColliders.Drive = true;
                    }
                }
            }

            if (driveType == DriveType.RearWheelDrive)
            {
                foreach (WheelsColliders wheelsColliders in WheelColliders)
                {
                    if (wheelsColliders.steering == false)
                    {
                        wheelsColliders.Drive = true;
                    }
                }
            }

            if (driveType == DriveType.AllWheelDrive)
            {
                foreach (WheelsColliders wheelsColliders in WheelColliders)
                {
                    wheelsColliders.Drive = true;
                }
            }

            if (startEngineOnAwake)
            {
                UVCInputSystem.UIS.StartEngine();
            }

            for (int i = 0; i < WheelColliders.Length; i++)
            {
                if (WheelColliders[i].Drive)
                {
                    if (DriveWheelsNumber < 100)
                    {
                        DriveWheelsNumber++;
                    }
                }
            }
        }

        public void FixedUpdate()
        {
            if (goingleft && !goingright)
            {
                if (inputTemp > -1.00f)
                {
                    inputTemp -= arrowSteeringSpeed;
                }
            }

            if (goingright && !goingleft)
            {
                if (inputTemp < 1.00f)
                {
                    inputTemp += arrowSteeringSpeed;
                }
            }

            if (!goingright && !goingleft)
            {
                if (ismoving)
                {
                    if (inputTemp > 0.05f)
                    {
                        inputTemp -= arrowSteeringSpeed;
                    }
                    else if (inputTemp < -0.05f)
                    {
                        inputTemp += arrowSteeringSpeed;
                    }
                    else
                    {
                        inputTemp = 0;
                    }
                }
            }

            if (isaccelerating && !isneutral && !isoutofFuel && !isparking && engineIsStarted)
            {
                if (isreversing)
                {
                    if (isSlidePedals)
                    {
                        Drive = (currentWheelsTorque - (currentWheelsTorque * UVCInputSystem.UIS.Accelerator.value)) * -1f;
                    }
                    else
                    {
                        Drive = currentWheelsTorque * -1f;
                    }
                }
                else
                {
                    if (isSlidePedals)
                    {
                        Drive = (currentWheelsTorque - (currentWheelsTorque * UVCInputSystem.UIS.Accelerator.value)) * 1f;
                    }
                    else
                    {
                        Drive = currentWheelsTorque * 1f;
                    }
                }
            }
            else
            {
                Drive = currentWheelsTorque * 0f;
            }

            if (UVCInputSystem.UIS.Mobile)
            {
                if (isSlidePedals)
                {
                    if (isparking)
                    {
                        currentBrakeTorque = maxBrakeTorque;
                    }
                    else
                    {
                        currentBrakeTorque = maxBrakeTorque - (maxBrakeTorque * UVCInputSystem.UIS.Brakes.value);
                    }
                }
                else
                {
                    currentBrakeTorque = maxBrakeTorque;
                }
            }
            else
            {
                currentBrakeTorque = maxBrakeTorque;
            }

            float MovingSpeed = rb.velocity.magnitude * 3.6f;

            if (MovingSpeed > 0.5f)
            {
                ismoving = true;
            }
            else
            {
                ismoving = false;
            }

            if (Mathf.Abs(speedOnKmh) > 0.5)
            {
                rb.constraints = RigidbodyConstraints.None;
            }
            else
            {
                if (isparking | isbraking)
                {
                    rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                    rb.freezeRotation = true;
                    if (!ABSSystem)
                    {
                        if (!ranged)
                        {
                            AbsRange = Random.Range(0.15f, 0.2f);
                            NonAbsRange = Random.Range(25, 45);
                            OutRange = Random.Range(0.2f, 0.4f);
                            ranged = true;
                        }
                    }
                }
                else
                {
                    rb.constraints = RigidbodyConstraints.None;
                    if (!ABSSystem)
                    {
                        if (ranged)
                        {
                            AbsRange = Random.Range(0.15f, 0.2f);
                            NonAbsRange = Random.Range(25, 45);
                            ranged = false;
                        }
                    }
                }
            }

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, ((circumFerence * maxEngineSpeed) * 20) / 10);

            if (gameObject.tag == "Player")
            {
                if (UVCSteeringWheel.UVCSW)
                {
                    touchSteeringWheelValue = UVCSteeringWheel.UVCSW.GetClampedValue();
                }
            }
            else
            {
                isbraking = true;
            }

            speedFactor = (rb.velocity.magnitude * 4.84f * handling / maxEngineSpeed) * (DriveWheelsNumber / 2);

            foreach (WheelsColliders wheelsColliders in WheelColliders)
            {
                speedOnKmh = ((circumFerence * wheelsColliders.rightWheel.RPM) * 20) / 1000;
            }
            speedOnMph = speedOnKmh * 0.62f;

            if (currentSteeringAngle >= minSteeringAngle)
            {
                currentSteeringAngle = Mathf.Lerp(maxSteeringAngle, minSteeringAngle, speedFactor);
            }
            else if (currentSteeringAngle <= minSteeringAngle)
            {
                currentSteeringAngle = minSteeringAngle;
            }

            if (touchSteeringWheelValue != 0)
            {
                steering = currentSteeringAngle * touchSteeringWheelValue;
            }
            else
            {
                steering = currentSteeringAngle * inputTemp;
            }

            if (isbraking || isparking)
            {
                Drive = 0;
            }
            else
            {
                nonAbsSteering = steering;
            }

            foreach (WheelsColliders wheelsColliders in WheelColliders)
            {
                if (wheelsColliders.steering)
                {
                    if (ABSSystem)
                    {
                        wheelsColliders.rightWheel.SteerAngle = Mathf.Rad2Deg * (Mathf.Atan(2.55f / (radius + (1.5f / 2))) * steering);
                        wheelsColliders.leftWheel.SteerAngle = Mathf.Rad2Deg * (Mathf.Atan(2.55f / (radius - (1.5f / 2))) * steering);
                    }
                    else
                    {
                        if (isbraking)
                        {
                            wheelsColliders.rightWheel.SteerAngle = Mathf.Rad2Deg * (Mathf.Atan(2.55f / (radius + (1.5f / 2))) * (Mathf.Lerp(nonAbsSteering, 0f, 0.6f)));
                            wheelsColliders.leftWheel.SteerAngle = Mathf.Rad2Deg * (Mathf.Atan(2.55f / (radius - (1.5f / 2))) * (Mathf.Lerp(nonAbsSteering, 0f, 0.6f)));
                            arrowSteeringSpeed = 0f;
                        }
                        else
                        {
                            wheelsColliders.rightWheel.SteerAngle = Mathf.Rad2Deg * (Mathf.Atan(2.55f / (radius + (1.5f / 2))) * steering);
                            wheelsColliders.leftWheel.SteerAngle = Mathf.Rad2Deg * (Mathf.Atan(2.55f / (radius - (1.5f / 2))) * steering);
                            arrowSteeringSpeed = tempArrowSteeringSpeed;
                        }
                    }
                    
                    if(engineIsStarted){
                        if (SteeringWheel)
                        {
                            if (isbraking && !ABSSystem)
                            {
                                SteeringWheel.rotation = transform.rotation * Quaternion.Euler(34f, 0, wheelsColliders.rightWheel.m_wheelSteerAngleNoABS * (UVCSteeringWheel.UVCSW.maxSteeringAngle / maxSteeringAngle) * -1);
                            }
                            else
                            {
                                SteeringWheel.rotation = transform.rotation * Quaternion.Euler(34f, 0, wheelsColliders.rightWheel.m_wheelSteerAngle * (UVCSteeringWheel.UVCSW.maxSteeringAngle / maxSteeringAngle) * -1);
                            }
                        }
                    }
                }
                if (wheelsColliders.Drive)
                {
                    if (isreversing)
                    {
                        if (Mathf.Abs(speedOnKmh) <= maxGearSpeed)
                        {
                            wheelsColliders.rightWheel.MotorTorque = Drive / DriveWheelsNumber;
                            wheelsColliders.leftWheel.MotorTorque = Drive / DriveWheelsNumber;
                        }
                        else
                        {
                            wheelsColliders.rightWheel.MotorTorque = 0;
                            wheelsColliders.leftWheel.MotorTorque = 0;
                        }
                    }
                    else
                    {
                        if (speedOnKmh <= maxEngineSpeed)
                        {
                            wheelsColliders.rightWheel.MotorTorque = Drive / DriveWheelsNumber;
                            wheelsColliders.leftWheel.MotorTorque = Drive / DriveWheelsNumber;
                        }
                        else
                        {
                            wheelsColliders.rightWheel.MotorTorque = 0;
                            wheelsColliders.leftWheel.MotorTorque = 0;
                        }
                    }
                }
                if (wheelsColliders.steering == false)
                {
                    WheelR = wheelsColliders.rightWheel;
                    WheelL = wheelsColliders.leftWheel;
                }

                if (isbraking || isparking)
                {
                    if (ABSSystem)
                    {
                        if (engineIsStarted)
                        {
                            wheelsColliders.rightWheel.BrakeTorque = currentBrakeTorque * 5 / DriveWheelsNumber;
                            wheelsColliders.leftWheel.BrakeTorque = currentBrakeTorque * 5 / DriveWheelsNumber;
                        }
                        else
                        {
                            wheelsColliders.rightWheel.BrakeTorque = ((currentBrakeTorque - (currentBrakeTorque * OutRange)) * 5 / DriveWheelsNumber) * 0.7f;
                            wheelsColliders.leftWheel.BrakeTorque = ((currentBrakeTorque - (currentBrakeTorque * OutRange)) * 5 / DriveWheelsNumber) * 0.7f;
                        }
                    }
                    else
                    {
                        if (engineIsStarted)
                        {
                            wheelsColliders.rightWheel.BrakeTorque = (currentBrakeTorque - (currentBrakeTorque * AbsRange)) * 5 / DriveWheelsNumber;
                            wheelsColliders.leftWheel.BrakeTorque = (currentBrakeTorque - (currentBrakeTorque * AbsRange)) * 5 / DriveWheelsNumber;
                        }
                        else
                        {
                            wheelsColliders.rightWheel.BrakeTorque = ((currentBrakeTorque - ((currentBrakeTorque * AbsRange) - (currentBrakeTorque * OutRange))) * 5 / DriveWheelsNumber) * 0.7f;
                            wheelsColliders.leftWheel.BrakeTorque = ((currentBrakeTorque - ((currentBrakeTorque * AbsRange) - (currentBrakeTorque * OutRange))) * 5 / DriveWheelsNumber) * 0.7f;
                        }
                    }
                }
                else
                {
                    wheelsColliders.rightWheel.BrakeTorque = 0;
                    wheelsColliders.leftWheel.BrakeTorque = 0;
                }
            }

            for (int i = 0; i < WheelCalipers.Length; i++)
            {
                if (WheelCalipers[i].steering)
                {
                    if (WheelCalipers[i].leftCaliper && WheelCalipers[i].rightCaliper)
                    {
                        WheelCalipers[i].leftCaliper.rotation = transform.rotation * Quaternion.Euler(-90f, steering, 0f);
                        WheelCalipers[i].rightCaliper.rotation = transform.rotation * Quaternion.Euler(-90f, steering, 0f);
                    }
                }
            }

            if (!isreversing)
            {
                if (speedOnKmh >= maxGearSpeed)
                {
                    if (currentGear < topGear)
                    {
                        currentGear++;
                    }

                    gearUptemp = maxGearSpeed;
                }

                if (speedOnKmh <= gearDowntemp)
                {
                    if (currentGear > 0)
                    {
                        currentGear--;
                    }
                }

                if (currentGear < topGear)
                {
                    if (currentGear > 0)
                    {
                        maxGearSpeed = maxEngineSpeed * gearRatios[currentGear] / (float)totalRatios + gearUptemp;
                        if (Audio)
                        {
                            StartCoroutine(Audio.GetComponent<UVCSoundSystem>().GearShifting());
                        }
                    }
                    else
                    {
                        maxGearSpeed = maxEngineSpeed * gearRatios[currentGear] / (float)totalRatios;
                    }
                }

                if (currentGear == 0)
                {
                    currentWheelsTorque = maxEngineTorque * totalRatios / (float)totalRatios;
                }

                if (currentGear > 0 && currentGear < topGear)
                {
                    if (currentGear > 0)
                    {
                        currentWheelsTorque = maxEngineTorque - (maxEngineTorque * gearRatios[Mathf.Abs(currentGear - topGear)] / (float)totalRatios);
                    }

                    gearDowntemp = gearUptemp - (maxEngineSpeed * gearRatios[currentGear - 1] / (float)totalRatios);
                }
            }

            distanceTravelled += Vector3.Distance(transform.position, lastPosition) / 60f;
            lastPosition = transform.position;
            PlayerPrefs.SetFloat(carID.ToString() + "DT", distanceTravelled);

            WheelHitSource hit;
            float travelL = 1.0f;
            float travelR = 1.0f;

            bool groundedL = WheelL.GetGroundHit(out hit);

            if (groundedL)
            {
                travelL = (-WheelL.transform.InverseTransformPoint(hit.Point).y - WheelL.WheelRadius) / WheelL.SuspensionDistance;
            }

            bool groundedR = WheelR.GetGroundHit(out hit);

            if (groundedR)
            {
                travelR = (-WheelR.transform.InverseTransformPoint(hit.Point).y - WheelR.WheelRadius) / WheelR.SuspensionDistance;
            }

            float antiRollForce = (travelL - travelR) * AntiRoll;

            if (speedOnKmh >= 50)
            {
                if (groundedL)
                {
                    rb.AddForceAtPosition(WheelL.transform.up * -antiRollForce, WheelL.transform.position);
                }

                if (groundedR)
                {
                    rb.AddForceAtPosition(WheelR.transform.up * antiRollForce, WheelR.transform.position);
                }
            }
        }

        public void Braking(bool isit)
        {
            if (isit)
            {
                isbraking = true;
            }
            else
            {
                isbraking = false;
            }
        }

        public void Accelerating(bool isacc)
        {
            if (isacc)
            {
                isaccelerating = true;
            }
            else
            {
                isaccelerating = false;
            }
        }

        public void ReverseToggle()
        {
            if (!isreversing)
            {
                isreversing = true;
            }
            else
            {
                isreversing = false;
            }
        }
    }

    [System.Serializable]
    public class WheelsColliders
    {
        public UVCWheelCollider leftWheel;
        public UVCWheelCollider rightWheel;
        [HideInInspector]
        public bool Drive = false;
        public bool steering;
    }
    [System.Serializable]
    public class WheelsCalipers
    {
        public Transform leftCaliper;
        public Transform rightCaliper;
        public bool steering;
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(UVCUniqueVehicleController))]
    class UVCUniqueVehicleControllerEditor : Editor
    {
    	public override void OnInspectorGUI ()
    	{
    		base.OnInspectorGUI();
    
    		UVCUniqueVehicleController UVCUVC = (UVCUniqueVehicleController)target;
    
    		if (UVCUVC.vehicleType == VehicleType.Custom)
    		{
    			EditorGUILayout.PropertyField (serializedObject.FindProperty ("gearRatios"),
    			new GUIContent ("Gear Ratios", "Drag your car prefabs"), true);
    		}  

    		serializedObject.ApplyModifiedProperties();
    	}
    }
    #endif
}