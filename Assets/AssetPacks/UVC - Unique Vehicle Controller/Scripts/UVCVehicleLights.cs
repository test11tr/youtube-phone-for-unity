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
    public class UVCVehicleLights : MonoBehaviour
    {
        [SerializeField]
        carfoglights[] CarFogLights;
        [SerializeField]
        carbeamlights[] CarBeamLights;
        [SerializeField]
        carbrakelights[] CarBrakeLights;
        [SerializeField]
        carreverselights[] CarReverseLights;
        [SerializeField]
        rightblinkerlight[] RightBlinkerLight;
        [SerializeField]
        leftblinkerlight[] LeftBlinkerLight;
        [HideInInspector]
        public bool isLowBeamLight;
        [HideInInspector]
        public bool isHighBeamLight;
        [HideInInspector]
        public bool isHazard;
        [HideInInspector]
        public bool isRightBlinker;
        [HideInInspector]
        public bool isLeftBlinker;
        public float BlinkingTime;

        float Timer;
        [HideInInspector]
        public float foglightsfound;

        GameObject Car;
        GameObject Audio;

        void Start()
        {
            Timer = BlinkingTime;

            isLowBeamLight = true;

            for (int i = 0; i < CarFogLights.Length; i++)
            {
                if (foglightsfound < 100)
                {
                    foglightsfound++;
                }
            }

            Car = GameObject.FindWithTag("Player");
            Audio = GameObject.FindWithTag("Audio");
        }

        public void EnableFogLight()
        {
            for (int i = 0; i < CarFogLights.Length; i++)
            {
                CarFogLights[i].FogLightsMesh.GetComponent<MeshRenderer>().material = CarFogLights[i].FogLightsEnabled;
                CarFogLights[i].RightFogLight.range = CarFogLights[i].FogLightRange;
                CarFogLights[i].LeftFogLight.range = CarFogLights[i].FogLightRange;
                CarFogLights[i].RightFogLight.enabled = true;
                CarFogLights[i].LeftFogLight.enabled = true;
                //Debug.Log("Fog Light On");
            }
        }

        public void DisableFogLight()
        {
            for (int i = 0; i < CarFogLights.Length; i++)
            {
                CarFogLights[i].FogLightsMesh.GetComponent<MeshRenderer>().material = CarFogLights[i].FogLightsDisabled;
                CarFogLights[i].RightFogLight.enabled = false;
                CarFogLights[i].LeftFogLight.enabled = false;
                //Debug.Log("Fog Light Off");
            }
        }

        public void EnableBeamLights()
        {
            for (int i = 0; i < CarBeamLights.Length; i++)
            {
                if (isLowBeamLight == true)
                {
                    CarBeamLights[i].BeamLightMesh.GetComponent<MeshRenderer>().material = CarBeamLights[i].BeamLightEnabled;
                    CarBeamLights[i].RightBeamLight.range = CarBeamLights[i].LowBeamRange;
                    CarBeamLights[i].LeftBeamLight.range = CarBeamLights[i].LowBeamRange;
                    CarBeamLights[i].RightBeamLight.enabled = true;
                    CarBeamLights[i].LeftBeamLight.enabled = true;
                    //Debug.Log("Low Beam Lights On");
                }
                if (isHighBeamLight == true)
                {
                    CarBeamLights[i].BeamLightMesh.GetComponent<MeshRenderer>().material = CarBeamLights[i].BeamLightEnabled;
                    if (CarBeamLights[i].HighBeamLightMesh)
                    {
                        CarBeamLights[i].HighBeamLightMesh.GetComponent<MeshRenderer>().material = CarBeamLights[i].BeamLightEnabled;
                    }
                    CarBeamLights[i].RightBeamLight.range = CarBeamLights[i].HighBeamRange;
                    CarBeamLights[i].LeftBeamLight.range = CarBeamLights[i].HighBeamRange;
                    CarBeamLights[i].RightBeamLight.enabled = true;
                    CarBeamLights[i].LeftBeamLight.enabled = true;
                    //Debug.Log("Hight Beam Lights On");
                }
            }
        }

        public void DisableBeamLights()
        {
            for (int i = 0; i < CarBeamLights.Length; i++)
            {
                CarBeamLights[i].BeamLightMesh.GetComponent<MeshRenderer>().material = CarBeamLights[i].BeamLightDisabled;
                if (CarBeamLights[i].HighBeamLightMesh)
                {
                    CarBeamLights[i].HighBeamLightMesh.GetComponent<MeshRenderer>().material = CarBeamLights[i].BeamLightDisabled;
                }
                CarBeamLights[i].RightBeamLight.range = CarBeamLights[i].LowBeamRange;
                CarBeamLights[i].LeftBeamLight.range = CarBeamLights[i].LowBeamRange;
                CarBeamLights[i].RightBeamLight.enabled = false;
                CarBeamLights[i].LeftBeamLight.enabled = false;
                DisableFogLight();
                //Debug.Log("Head Lights Off");
            }
        }

        public void EnableReverseLight()
        {
            if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted)
            {
                for (int i = 0; i < CarReverseLights.Length; i++)
                {
                    CarReverseLights[i].ReverseLightsMesh.GetComponent<MeshRenderer>().material = CarReverseLights[i].ReverseLightsEnabled;
                    CarReverseLights[i].RightReverseLight.enabled = true;
                    CarReverseLights[i].LeftReverseLight.enabled = true;
                    //Debug.Log("Reverse Light On");
                }
            }
        }

        public void DisableReverseLight()
        {
            for (int i = 0; i < CarReverseLights.Length; i++)
            {
                CarReverseLights[i].ReverseLightsMesh.GetComponent<MeshRenderer>().material = CarReverseLights[i].ReverseLightsDisabled;
                CarReverseLights[i].RightReverseLight.enabled = false;
                CarReverseLights[i].LeftReverseLight.enabled = false;
                //Debug.Log("Reverse Light Off");
            }
        }

        public void EnableBrakeLight()
        {
            if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted)
            {
                for (int i = 0; i < CarBrakeLights.Length; i++)
                {
                    CarBrakeLights[i].BrakeLightsMesh.GetComponent<MeshRenderer>().material = CarBrakeLights[i].BrakeLightsEnabled;
                    CarBrakeLights[i].RightBrakeLight.enabled = true;
                    CarBrakeLights[i].LeftBrakeLight.enabled = true;
                    //Debug.Log("BrakeLightsOn");
                }
            }
        }

        public void DisableBrakeLight()
        {
            for (int i = 0; i < CarBrakeLights.Length; i++)
            {
                CarBrakeLights[i].BrakeLightsMesh.GetComponent<MeshRenderer>().material = CarBrakeLights[i].BrakeLightsDisabled;
                CarBrakeLights[i].RightBrakeLight.enabled = false;
                CarBrakeLights[i].LeftBrakeLight.enabled = false;
                //Debug.Log("BrakeLightsOff");
            }
        }

        public void EnableHazardLights()
        {
            if (!isHazard)
            {
                if (Audio)
                {
                    Audio.GetComponent<UVCSoundSystem>().StartBlinking();
                }
                isHazard = true;
            }
            else if (isHazard)
            {
                if (Audio)
                {
                    Audio.GetComponent<UVCSoundSystem>().StopBlinking();
                }
                isHazard = false;
            }
        }

        public void EnableRightBlinkerLight()
        {
            if (Timer > 0)
            {
                DisableRightBlinkerLight();
                Timer -= Time.deltaTime;
            }
            if (Timer <= 0)
            {
                for (int i = 0; i < RightBlinkerLight.Length; i++)
                {
                    RightBlinkerLight[i].RightBlinkerMesh.GetComponent<MeshRenderer>().material = RightBlinkerLight[i].RightBlinkerLightEnabled;
                    RightBlinkerLight[i].FRBlinkerLight.enabled = true;
                    RightBlinkerLight[i].RRBlinkerLight.enabled = true;
                    if (UVCInputSystem.UIS.RightBlinkerButton)
                    {
                        UVCInputSystem.UIS.RightBlinkerButton.SetActive(true);
                    }
                    if (isHazard == true)
                    {
                        if (UVCInputSystem.UIS.HazardLightButton)
                        {
                            UVCInputSystem.UIS.HazardLightButton.SetActive(true);
                        }
                    }
                    StartCoroutine(Wait());
                    //Debug.Log("Right Blinkers Light Blink");
                }
            }
        }

        public void DisableRightBlinkerLight()
        {
            for (int i = 0; i < RightBlinkerLight.Length; i++)
            {
                RightBlinkerLight[i].RightBlinkerMesh.GetComponent<MeshRenderer>().material = RightBlinkerLight[i].RightBlinkerLightDisabled;
                RightBlinkerLight[i].FRBlinkerLight.enabled = false;
                RightBlinkerLight[i].RRBlinkerLight.enabled = false;
                //Debug.Log("Right Blinkers Light Off");
            }
        }

        public void EnableLeftBlinkerLight()
        {
            if (Timer > 0)
            {
                DisableLeftBlinkerLight();
                Timer -= Time.deltaTime;
            }
            if (Timer <= 0)
            {
                for (int i = 0; i < LeftBlinkerLight.Length; i++)
                {
                    LeftBlinkerLight[i].LeftBlinkerMesh.GetComponent<MeshRenderer>().material = LeftBlinkerLight[i].LeftBlinkerLightEnabled;
                    LeftBlinkerLight[i].FLBlinkerLight.enabled = true;
                    LeftBlinkerLight[i].RLBlinkerLight.enabled = true;
                    if (UVCInputSystem.UIS.LeftBlinkerButton)
                    {
                        UVCInputSystem.UIS.LeftBlinkerButton.SetActive(true);
                    }
                    StartCoroutine(Wait());
                    //Debug.Log("Left Blinkers Light Blink");
                }
            }
        }

        public void DisableLeftBlinkerLight()
        {
            for (int i = 0; i < LeftBlinkerLight.Length; i++)
            {
                LeftBlinkerLight[i].LeftBlinkerMesh.GetComponent<MeshRenderer>().material = LeftBlinkerLight[i].LeftBlinkerLightDisabled;
                LeftBlinkerLight[i].FLBlinkerLight.enabled = false;
                LeftBlinkerLight[i].RLBlinkerLight.enabled = false;
                //Debug.Log("Left Blinkers Light Off");
            }
        }

        void Update()
        {
            if (isHazard)
            {
                isRightBlinker = true;
                isLeftBlinker = true;
                UVCInputSystem.UIS.RB = false;
                UVCInputSystem.UIS.LB = false;
            }
            if (!isHazard && UVCInputSystem.UIS.RB == false && UVCInputSystem.UIS.LB == false)
            {
                isRightBlinker = false;
                isLeftBlinker = false;
            }

            if (isRightBlinker)
            {
                EnableRightBlinkerLight();
            }

            if (isLeftBlinker)
            {
                EnableLeftBlinkerLight();
            }
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(BlinkingTime);
            if (UVCInputSystem.UIS.RightBlinkerButton)
            {
                UVCInputSystem.UIS.RightBlinkerButton.SetActive(false);
            }
            if (UVCInputSystem.UIS.LeftBlinkerButton)
            {
                UVCInputSystem.UIS.LeftBlinkerButton.SetActive(false);
            }
            if (UVCInputSystem.UIS.HazardLightButton)
            {
                UVCInputSystem.UIS.HazardLightButton.SetActive(false);
            }
            DisableRightBlinkerLight();
            DisableLeftBlinkerLight();
            if (isHazard == true)
            {
                if (UVCInputSystem.UIS.HazardLightButton)
                {
                    UVCInputSystem.UIS.HazardLightButton.SetActive(false);
                }
            }
            Timer = BlinkingTime;
        }

        [System.Serializable]
        public class carfoglights
        {
            // For Car Fog Lights
            public GameObject FogLightsMesh;
            public float FogLightRange;
            public Light RightFogLight;
            public Light LeftFogLight;
            public Material FogLightsEnabled;
            public Material FogLightsDisabled;
        }
        [System.Serializable]
        public class carbrakelights
        {
            // For Car Brake Lights
            public GameObject BrakeLightsMesh;
            public Light RightBrakeLight;
            public Light LeftBrakeLight;
            public Material BrakeLightsEnabled;
            public Material BrakeLightsDisabled;
        }
        [System.Serializable]
        public class carreverselights
        {
            // For Car Reverse Lights
            public GameObject ReverseLightsMesh;
            public Light RightReverseLight;
            public Light LeftReverseLight;
            public Material ReverseLightsEnabled;
            public Material ReverseLightsDisabled;
        }
        [System.Serializable]
        public class carbeamlights
        {
            // For Car Beam Lights
            public GameObject BeamLightMesh;
            public GameObject HighBeamLightMesh;
            public float LowBeamRange;
            public float HighBeamRange;
            public Light RightBeamLight;
            public Light LeftBeamLight;
            public Material BeamLightEnabled;
            public Material BeamLightDisabled;
        }

        [System.Serializable]
        public class rightblinkerlight
        {
            // For Right Blinker Light
            public GameObject RightBlinkerMesh;
            public Light FRBlinkerLight;
            public Light RRBlinkerLight;
            public Material RightBlinkerLightEnabled;
            public Material RightBlinkerLightDisabled;
        }
        [System.Serializable]
        public class leftblinkerlight
        {
            // For Left Blinker Light
            public GameObject LeftBlinkerMesh;
            public Light FLBlinkerLight;
            public Light RLBlinkerLight;
            public Material LeftBlinkerLightEnabled;
            public Material LeftBlinkerLightDisabled;
        }
    }
}