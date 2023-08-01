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
using test11.Managers;

namespace UniqueVehicleController
{
    public class UVCInputSystem : MonoBehaviour
    {
        [Header("Manager")]
        [SerializeField] private ParkingManager _parkingManager;

        [Header("Visuals")]
        public Text SpeedText;
        public RectTransform SpeedNeedle;
        public RectTransform FuelNeedle;
        public Text distanceTravelledText;
        public Text distanceRemainingText;
        public Image GearD;
        public Image GearN;
        public Image GearR;
        public Image GearP;
        public GameObject Gear;
        public GameObject DriveNotify;

        [Header("Buttons")]
        public GameObject SensorsUI;
        public GameObject EngineStartButton;
        public Image EngineStartFill;
        public GameObject EngineStopButton;
        public GameObject LightOFFButton;
        public GameObject LowBeamLightButton;
        public GameObject HighBeamLightButton;
        public GameObject FogLightButton;
        public GameObject HazardLightButton;
        public GameObject RightBlinkerButton;
        public GameObject LeftBlinkerButton;
        public GameObject SettingsButton;
        public GameObject CarSensorToggleButton;
        public Slider Accelerator;
        public Slider Brakes;
        public Dropdown SpeedUnitDrop;
        public Dropdown SteertingTypeDrop;
        public Dropdown PedalTypeDrop;
        public Dropdown GearBoxDrop;

        [Header("Components")]
        public Slider GearSlider;
        public GameObject SteeringArrows;
        public GameObject SteeringWheel;
        public GameObject ClickPedals;
        public GameObject SlidePedals;

        [Header("Sound Effects")]
        public AudioSource ButtonClick;
        public AudioSource LightButtonClick;
        public AudioSource GearStickDrive;
        public AudioSource GearStickParkNeutral;
        public AudioSource GearStickReverse;

        [Header("Parameters")]
        public bool Mobile;
        public float MaxSpeedMeter;
        public float minSpeedNeedleAngle;
        public float maxSpeedNeedleAngle;
        public float minFuelNeedleAngle;
        public float maxFuelNeedleAngle;
        public float AcceleratorReleaseSpeed = 0.02f;
        public float BrakesReleaseSpeed = 0.02f;

        [Header("Materials")]
        public Material[] carMaterials;

        float CarMaxSpeed;
        float CurrentSpeed;
        float MaxRevSpeed;
        float MaxFuel;
        float CurrentFuel;
        float StopSpeed;
        float StartingValue = 1;
        float EndValue;
        float MinValue;
        float currenBra;

        bool isHolding;
        bool isfoglights = false;
        bool islowbeam = false;
        bool ishighbeam = false;

        [HideInInspector]
        public bool FrontSensors;
        [HideInInspector]
        public bool RearSensors;
        [HideInInspector]
        public bool RB;
        [HideInInspector]
        public bool LB;
        [HideInInspector]
        public bool EngineOut;

        bool AccisReleased;
        bool BrakesisReleased;
        bool FirstGear;
        bool KMH;
        bool MPH;

        GameObject Car;
        GameObject Audio;
        GameObject Lights;
        GameObject Camera;

        public static UVCInputSystem UIS;

        void Start()
        {
            UIS = this;

            foreach (var material in carMaterials)
                {
                    material.DisableKeyword("_EMISSION");
                }

            if (GearD && GearN && GearR && GearP)
            {
                GearD.enabled = true;
                GearN.enabled = false;
                GearR.enabled = false;
                GearP.enabled = false;
            }
            if (EngineStopButton)
            {
                EngineStopButton.SetActive(false);
            }
            if (LightOFFButton)
            {
                LightOFFButton.SetActive(false);
            }
            if (FogLightButton)
            {
                FogLightButton.SetActive(false);
            }
            if (LowBeamLightButton)
            {
                LowBeamLightButton.SetActive(false);
            }
            if (HighBeamLightButton)
            {
                HighBeamLightButton.SetActive(false);
            }
            if (HazardLightButton)
            {
                HazardLightButton.SetActive(false);
            }
            if (RightBlinkerButton)
            {
                RightBlinkerButton.SetActive(false);
            }
            if (LeftBlinkerButton)
            {
                LeftBlinkerButton.SetActive(false);
            }
            if (HazardLightButton)
            {
                HazardLightButton.transform.parent.gameObject.SetActive(false);
            }
            if (RightBlinkerButton)
            {
                RightBlinkerButton.transform.parent.gameObject.SetActive(false);
            }
            if (LeftBlinkerButton)
            {
                LeftBlinkerButton.transform.parent.gameObject.SetActive(false);
            }

            EndValue = StartingValue * 10f;
            MinValue = 0;

            Car = GameObject.FindWithTag("Player");
            Audio = GameObject.FindWithTag("Audio");
            Lights = GameObject.FindWithTag("Lights");
            Camera = GameObject.FindWithTag("Camera");
            _parkingManager = GameObject.FindWithTag("parkingManager").GetComponent<ParkingManager>();

            ChangeSpeedUnit();
        }

        public void StartEngine()
        {
            if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == false && Car.GetComponent<UVCUniqueVehicleController>().isoutofFuel == false)
            {
                Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted = true;
                SwitchGear();
                GearStickDrive.Stop();
                GearStickReverse.Stop();
                GearStickParkNeutral.Stop();

                Car.GetComponent<UVCUniqueVehicleController>().exhaustEffect.Play();
                foreach (var material in carMaterials)
                {
                    material.EnableKeyword("_EMISSION");
                }

                if (Mobile)
                {
                    if (EngineStartButton)
                    {
                        EngineStartButton.SetActive(false);
                    }
                    if (Car.GetComponent<UVCUniqueVehicleController>().startEngineOnAwake == false)
                    {
                        EngineStopButton.SetActive(true);
                    }
                    if (LightOFFButton)
                    {
                        LightOFFButton.SetActive(true);
                    }
                    if (HazardLightButton)
                    {
                        HazardLightButton.transform.parent.gameObject.SetActive(true);
                    }
                    if (RightBlinkerButton)
                    {
                        RightBlinkerButton.transform.parent.gameObject.SetActive(true);
                    }
                    if (LeftBlinkerButton)
                    {
                        LeftBlinkerButton.transform.parent.gameObject.SetActive(true);
                    }
                    /*if (SettingsButton)
                    {
                        SettingsButton.SetActive(false);
                    }*/
                }
                if (Audio)
                {
                    Audio.GetComponent<UVCSoundSystem>().StartEngine();
                }
            }
        }

        public void StopEngine()
        {
            if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
            {
                Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted = false;
                isfoglights = false;
                islowbeam = false;
                ishighbeam = false;

                Car.GetComponent<UVCUniqueVehicleController>().exhaustEffect.Stop();
                foreach (var material in carMaterials)
                {
                    material.DisableKeyword("_EMISSION");
                }

                if (Mobile)
                {
                    if (EngineStartButton)
                    {
                        EngineStartButton.SetActive(true);
                    }
                    if (EngineStopButton)
                    {
                        EngineStopButton.SetActive(false);
                    }
                    if (LightOFFButton)
                    {
                        LightOFFButton.SetActive(false);
                    }
                    if (FogLightButton)
                    {
                        FogLightButton.SetActive(false);
                    }
                    if (LowBeamLightButton)
                    {
                        LowBeamLightButton.SetActive(false);
                    }
                    if (HighBeamLightButton)
                    {
                        HighBeamLightButton.SetActive(false);
                    }
                    if (HazardLightButton)
                    {
                        HazardLightButton.transform.parent.gameObject.SetActive(false);
                    }
                    if (RightBlinkerButton)
                    {
                        RightBlinkerButton.transform.parent.gameObject.SetActive(false);
                    }
                    if (LeftBlinkerButton)
                    {
                        LeftBlinkerButton.transform.parent.gameObject.SetActive(false);
                    }
                    /*if (SettingsButton)
                    {
                        SettingsButton.SetActive(true);
                    }*/
                }
                if (Lights)
                {
                    Lights.GetComponent<UVCVehicleLights>().DisableBeamLights();
                    Lights.GetComponent<UVCVehicleLights>().DisableBrakeLight();
                    Lights.GetComponent<UVCVehicleLights>().DisableFogLight();
                    Lights.GetComponent<UVCVehicleLights>().DisableLeftBlinkerLight();
                    Lights.GetComponent<UVCVehicleLights>().isLowBeamLight = true;
                    Lights.GetComponent<UVCVehicleLights>().isHighBeamLight = false;
                    Lights.GetComponent<UVCVehicleLights>().DisableRightBlinkerLight();
                    Lights.GetComponent<UVCVehicleLights>().DisableReverseLight();
                    DisableRightBlinker();
                    DisableLeftBlinker();
                }
                if (Audio)
                {
                    Audio.GetComponent<UVCSoundSystem>().StopEngine();
                }
            }
        }

        public void StartHorn()
        {
            if (Audio)
            {
                Audio.GetComponent<UVCSoundSystem>().StartHorn();
            }
        }

        public void StopHorn()
        {
            if (Audio)
            {
                Audio.GetComponent<UVCSoundSystem>().StopHorn();
            }
        }

        public void ButtonClickSound()
        {
            if (ButtonClick)
            {
                ButtonClick.Play();
            }
        }

        public void LightButtonClickSound()
        {
            if (LightButtonClick)
            {
                LightButtonClick.Play();
            }
        }

        public void GearShiftSound()
        {
            if (GearStickDrive && GearStickParkNeutral && GearStickReverse)
            {
                if (GearSlider.value == 0)
                {
                    GearStickDrive.Play();
                }

                if (GearSlider.value == 1)
                {
                    GearStickParkNeutral.Play();
                }

                if (GearSlider.value == 2)
                {
                    GearStickReverse.Play();
                }

                if (GearSlider.value == 3)
                {
                    GearStickParkNeutral.Play();
                }
            }
        }

        public void HoldStart()
        {
            if (isHolding == false)
            {
                isHolding = true;
            }
            else if (isHolding == true)
            {
                isHolding = false;
            }
        }

        public void Throttle()
        {
            if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true && Car.GetComponent<UVCUniqueVehicleController>().isoutofFuel == false)
            {
                Car.GetComponent<UVCUniqueVehicleController>().Accelerating(true);
            }

            AccisReleased = false;
        }

        public void ThrottleRelease()
        {
            if (Car)
            {
                Car.GetComponent<UVCUniqueVehicleController>().Accelerating(false);
            }

            AccisReleased = true;
        }

        public void Brake()
        {
            if (Car)
            {
                Car.GetComponent<UVCUniqueVehicleController>().Braking(true);
            }
            if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted)
            {
                if (Lights)
                {
                    Lights.GetComponent<UVCVehicleLights>().EnableBrakeLight();
                }
            }

            BrakesisReleased = false;
        }

        public void BrakeRelease()
        {
            if (Car)
            {
                Car.GetComponent<UVCUniqueVehicleController>().Braking(false);
            }
            if (Car.GetComponent<UVCUniqueVehicleController>().isparking == false)
            {
                if (Lights)
                {
                    Lights.GetComponent<UVCVehicleLights>().DisableBrakeLight();
                }
            }

            BrakesisReleased = true;
        }

        public void ToggleCamera()
        {
            if (Camera)
            {
                Camera.GetComponent<UVCCameraToggler>().ToggleCameras();
            }

            ButtonClickSound();
        }

        public void SwitchSteering()
        {
            if (SteertingTypeDrop.value == 0)
            {
                if (Car)
                {
                    Car.GetComponent<UVCUniqueVehicleController>().usingarrows = false;
                }
                if (SteeringArrows)
                {
                    SteeringArrows.SetActive(false);
                }
                if (SteeringWheel)
                {
                    SteeringWheel.SetActive(true);
                }
            }

            if (SteertingTypeDrop.value == 1)
            {
                if (Car)
                {
                    Car.GetComponent<UVCUniqueVehicleController>().usingarrows = true;
                }
                if (SteeringArrows)
                {
                    SteeringArrows.SetActive(true);
                }
                if (SteeringWheel)
                {
                    SteeringWheel.SetActive(false);
                }
            }
        }

        public void SwitchSteeringToArrows()
        {
            if(PlayerPrefs.GetInt("ControllerType") == 0){
                SteeringArrows.SetActive(true);
                SteeringWheel.SetActive(false);
                PlayerPrefs.SetInt("ControllerType", 1);
            }   
        }

        public void SwitchSteeringToWheel()
        {
            if(PlayerPrefs.GetInt("ControllerType") == 1){
                SteeringArrows.SetActive(false);
                SteeringWheel.SetActive(true);
                PlayerPrefs.SetInt("ControllerType", 0);
            }
        }

        public void SwitchPedals()
        {
            if (PedalTypeDrop.value == 0)
            {
                if (Car)
                {
                    Car.GetComponent<UVCUniqueVehicleController>().isSlidePedals = false;
                }
                if (ClickPedals)
                {
                    ClickPedals.SetActive(true);
                }
                if (SlidePedals)
                {
                    SlidePedals.SetActive(false);
                }
            }

            if (PedalTypeDrop.value == 1)
            {
                if (Car)
                {
                    Car.GetComponent<UVCUniqueVehicleController>().isSlidePedals = true;
                }
                if (ClickPedals)
                {
                    ClickPedals.SetActive(false);
                }
                if (SlidePedals)
                {
                    SlidePedals.SetActive(true);
                }
            }
        }

        public void TurnOffLight()
        {
            LightButtonClickSound();
            if (LightOFFButton)
            {
                LightOFFButton.SetActive(true);
            }
            if (HighBeamLightButton)
            {
                HighBeamLightButton.SetActive(false);
            }
            if (FogLightButton)
            {
                FogLightButton.SetActive(false);
            }
            if (Lights)
            {
                Lights.GetComponent<UVCVehicleLights>().isLowBeamLight = true;
                Lights.GetComponent<UVCVehicleLights>().isHighBeamLight = false;
                Lights.GetComponent<UVCVehicleLights>().DisableBeamLights();
            }
        }

        public void EnableLowBeamLights()
        {
            LightButtonClickSound();
            if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted)
            {
                if (LightOFFButton)
                {
                    LightOFFButton.SetActive(false);
                }
                if (LowBeamLightButton)
                {
                    LowBeamLightButton.SetActive(true);
                }
                if (Lights)
                {
                    Lights.GetComponent<UVCVehicleLights>().EnableBeamLights();
                    Lights.GetComponent<UVCVehicleLights>().isLowBeamLight = true;
                    Lights.GetComponent<UVCVehicleLights>().isHighBeamLight = false;
                }
            }
        }

        public void EnableHighBeamLights()
        {
            LightButtonClickSound();
            if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted)
            {
                if (LowBeamLightButton)
                {
                    LowBeamLightButton.SetActive(false);
                }
                if (HighBeamLightButton)
                {
                    HighBeamLightButton.SetActive(true);
                }
                if (Lights)
                {
                    Lights.GetComponent<UVCVehicleLights>().isLowBeamLight = false;
                    Lights.GetComponent<UVCVehicleLights>().isHighBeamLight = true;
                    Lights.GetComponent<UVCVehicleLights>().EnableBeamLights();
                }
            }
        }

        public void EnableFogLights()
        {
            LightButtonClickSound();
            if (Lights.GetComponent<UVCVehicleLights>().foglightsfound > 0)
            {
                if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted)
                {
                    if (HighBeamLightButton)
                    {
                        HighBeamLightButton.SetActive(false);
                    }
                    if (FogLightButton)
                    {
                        FogLightButton.SetActive(true);
                    }
                    if (Lights)
                    {
                        Lights.GetComponent<UVCVehicleLights>().EnableFogLight();
                    }
                }
            }

            if (Lights.GetComponent<UVCVehicleLights>().foglightsfound == 0)
            {
                TurnOffLight();
            }
        }

        public void EnableHazardLights()
        {
            LightButtonClickSound();
            if (Lights)
            {
                Lights.GetComponent<UVCVehicleLights>().EnableHazardLights();
            }
        }

        public void EnableRightBlinker()
        {
            LightButtonClickSound();
            if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted)
            {
                if (!RB && !LB)
                {
                    if (Lights)
                    {
                        Lights.GetComponent<UVCVehicleLights>().isRightBlinker = true;
                    }
                    if (Audio)
                    {
                        Audio.GetComponent<UVCSoundSystem>().StartBlinking();
                    }
                    RB = true;
                }
                else if (RB)
                {
                    if (Lights)
                    {
                        Lights.GetComponent<UVCVehicleLights>().isRightBlinker = false;
                    }
                    if (Audio)
                    {
                        Audio.GetComponent<UVCSoundSystem>().StopBlinking();
                    }
                    RB = false;
                }

                if (LB)
                {
                    LB = false;
                    if (Audio)
                    {
                        Audio.GetComponent<UVCSoundSystem>().StopBlinking();
                    }
                }
            }
        }

        public void DisableRightBlinker()
        {
            LightButtonClickSound();
            if (Lights)
            {
                Lights.GetComponent<UVCVehicleLights>().isRightBlinker = false;
            }
            if (Audio)
            {
                Audio.GetComponent<UVCSoundSystem>().StopBlinking();
            }
        }

        public void EnableLeftBlinker()
        {
            LightButtonClickSound();
            if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted)
            {
                if (!LB && !RB)
                {
                    if (Lights)
                    {
                        Lights.GetComponent<UVCVehicleLights>().isLeftBlinker = true;
                    }
                    if (Audio)
                    {
                        Audio.GetComponent<UVCSoundSystem>().StartBlinking();
                    }
                    LB = true;
                }
                else if (LB)
                {
                    if (Lights)
                    {
                        Lights.GetComponent<UVCVehicleLights>().isLeftBlinker = false;
                    }
                    if (Audio)
                    {
                        Audio.GetComponent<UVCSoundSystem>().StopBlinking();
                    }
                    LB = false;
                }

                if (RB)
                {
                    RB = false;
                    if (Audio)
                    {
                        Audio.GetComponent<UVCSoundSystem>().StopBlinking();
                    }
                }
            }
        }

        public void DisableLeftBlinker()
        {
            LightButtonClickSound();
            if (Lights)
            {
                Lights.GetComponent<UVCVehicleLights>().isLeftBlinker = false;
            }
            if (Audio)
            {
                Audio.GetComponent<UVCSoundSystem>().StopBlinking();
            }
        }

        public void AddOneLittre()
        {
            ButtonClickSound();
            if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == false && Car.GetComponent<UVCUniqueVehicleController>().ismoving == false)
            {
                if (Car.GetComponent<UVCFuelSystem>().currentFuel < Car.GetComponent<UVCFuelSystem>().maxFuel)
                {
                    Car.GetComponent<UVCFuelSystem>().currentFuel += 1;
                    Car.GetComponent<UVCFuelSystem>().UpdateFuel();
                }
                if (Car.GetComponent<UVCFuelSystem>().currentFuel >= Car.GetComponent<UVCFuelSystem>().maxFuel)
                {
                    Car.GetComponent<UVCFuelSystem>().currentFuel = Car.GetComponent<UVCFuelSystem>().maxFuel;
                }
            }
        }

        public void AddFullTank()
        {
            ButtonClickSound();
            if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == false && Car.GetComponent<UVCUniqueVehicleController>().ismoving == false)
            {
                if (Car.GetComponent<UVCFuelSystem>().currentFuel < Car.GetComponent<UVCFuelSystem>().maxFuel)
                {
                    Car.GetComponent<UVCFuelSystem>().currentFuel += Car.GetComponent<UVCFuelSystem>().usedFuel;
                    Car.GetComponent<UVCFuelSystem>().UpdateFuel();
                }
                if (Car.GetComponent<UVCFuelSystem>().currentFuel >= Car.GetComponent<UVCFuelSystem>().maxFuel)
                {
                    Car.GetComponent<UVCFuelSystem>().currentFuel = Car.GetComponent<UVCFuelSystem>().maxFuel;
                }
            }
        }

        public void CarIsGoingRight(bool isit)
        {
            if (isit)
            {
                Car.GetComponent<UVCUniqueVehicleController>().goingright = true;
            }
            else
            {
                Car.GetComponent<UVCUniqueVehicleController>().goingright = false;
            }
        }

        public void CarIsGoingLeft(bool isit)
        {
            if (isit)
            {
                Car.GetComponent<UVCUniqueVehicleController>().goingleft = true;
            }
            else
            {
                Car.GetComponent<UVCUniqueVehicleController>().goingleft = false;
            }
        }

        public void EnableFrontSensors()
        {
            ButtonClickSound();
            if (FrontSensors == false)
            {
                FrontSensors = true;
            }
            else if (FrontSensors == true)
            {
                FrontSensors = false;
            }
        }

        public void EnableRearSensors()
        {
            ButtonClickSound();
            if (RearSensors == false)
            {
                RearSensors = true;
            }
            else if (RearSensors == true)
            {
                RearSensors = false;
            }
        }

        public void EnableVehicleSensors(){
            ButtonClickSound();
            switch (FrontSensors){
                case false:
                    CarSensorToggleButton.SetActive(true);
                    SensorsUI.SetActive(true);
                    FrontSensors = true;
                    RearSensors = true;
                    break;
                case true:
                    CarSensorToggleButton.SetActive(false);
                    SensorsUI.SetActive(false);
                    FrontSensors = false;
                    RearSensors = false;
                    break;
            }
        }

        public void ChangeSpeedUnit()
        {
            if (SpeedUnitDrop)
            {
                if (SpeedUnitDrop.value == 0)
                {
                    KMH = true;
                    MPH = false;
                }

                if (SpeedUnitDrop.value == 1)
                {
                    KMH = false;
                    MPH = true;
                }
            }
        }

        public void EnableABSSystem()
        {
            if (Car.GetComponent<UVCUniqueVehicleController>().ABSSystem == false)
            {
                Car.GetComponent<UVCUniqueVehicleController>().ABSSystem = true;
            }
            else
            {
                Car.GetComponent<UVCUniqueVehicleController>().ABSSystem = false;
            }
        }

        public void EnablePowerSteering()
        {
            if (Car.GetComponent<UVCUniqueVehicleController>().PowerSteering == false)
            {
                Car.GetComponent<UVCUniqueVehicleController>().PowerSteering = true;
            }
            else
            {
                Car.GetComponent<UVCUniqueVehicleController>().PowerSteering = false;
            }
        }

        public void SwitchGear()
        {
            if (GearSlider)
            {
                if (GearSlider.value == 0)
                {
                    if (GearD)
                    {
                        GearD.enabled = true;
                    }

                    GearShiftSound();
                    Car.GetComponent<UVCUniqueVehicleController>().isreversing = false;
                    Car.GetComponent<UVCUniqueVehicleController>().isbraking = false;
                    Car.GetComponent<UVCUniqueVehicleController>().isparking = false;
                }
                else
                {
                    if (GearD)
                    {
                        GearD.enabled = false;
                    }
                }

                if (GearSlider.value == 1)
                {
                    if (GearN)
                    {
                        GearN.enabled = true;
                    }

                    GearShiftSound();
                    Car.GetComponent<UVCUniqueVehicleController>().isneutral = true;
                }
                else
                {
                    if (GearN)
                    {
                        GearN.enabled = false;
                    }

                    Car.GetComponent<UVCUniqueVehicleController>().isneutral = false;
                }

                if (GearSlider.value == 2)
                {
                    if (GearR)
                    {
                        GearR.enabled = true;
                    }

                    GearShiftSound();
                    Car.GetComponent<UVCUniqueVehicleController>().isreversing = true;
                    Car.GetComponent<UVCUniqueVehicleController>().isbraking = false;
                    Car.GetComponent<UVCUniqueVehicleController>().isparking = false;
                    if (Lights)
                    {
                        Lights.GetComponent<UVCVehicleLights>().EnableReverseLight();
                    }
                }
                else
                {
                    if (GearR)
                    {
                        GearR.enabled = false;
                    }

                    Car.GetComponent<UVCUniqueVehicleController>().isreversing = false;
                    if (Lights)
                    {
                        Lights.GetComponent<UVCVehicleLights>().DisableReverseLight();
                    }
                }

                if (GearSlider.value == 3)
                {
                    if (GearP)
                    {
                        GearP.enabled = true;
                    }

                    GearShiftSound();
                    Car.GetComponent<UVCUniqueVehicleController>().isreversing = false;
                    Car.GetComponent<UVCUniqueVehicleController>().isbraking = true;
                    Car.GetComponent<UVCUniqueVehicleController>().isparking = true;
                    if (Lights)
                    {
                        Lights.GetComponent<UVCVehicleLights>().EnableBrakeLight();
                    }
                }
                else
                {
                    if (GearP)
                    {
                        GearP.enabled = false;
                    }
                    if (Lights)
                    {
                        Lights.GetComponent<UVCVehicleLights>().DisableBrakeLight();
                    }
                }
            }
        }

        private void Update()
        {
            CarMaxSpeed = Car.GetComponent<UVCUniqueVehicleController>().maxEngineSpeed;
            MaxRevSpeed = CarMaxSpeed / 4;
            MaxFuel = Car.GetComponent<UVCFuelSystem>().maxFuel;
            CurrentFuel = Car.GetComponent<UVCFuelSystem>().currentFuel;

            if (KMH)
            {
                CurrentSpeed = Mathf.Abs(Car.GetComponent<UVCUniqueVehicleController>().speedOnKmh);
            }
            if (MPH)
            {
                CurrentSpeed = Mathf.Abs(Car.GetComponent<UVCUniqueVehicleController>().speedOnMph);
            }

            if (distanceTravelledText)
            {
                distanceTravelledText.text = Car.GetComponent<UVCUniqueVehicleController>().distanceTravelled.ToString("0 0 0 0 0 0");
            }
            if (distanceRemainingText)
            {
                distanceRemainingText.text = Car.GetComponent<UVCFuelSystem>().DistanceRemaining.ToString("0 0 0 0");
            }

            if (SpeedText)
            {
                if (KMH)
                {
                    SpeedText.text = ((int)CurrentSpeed) + "\n" + "KM/H";
                }
                if (MPH)
                {
                    SpeedText.text = ((int)CurrentSpeed) + "\n" + "MPH";
                }
            }

            if (SpeedNeedle)
            {
                if (Car.GetComponent<UVCUniqueVehicleController>().isreversing)
                {
                    SpeedNeedle.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedNeedleAngle, maxSpeedNeedleAngle, CurrentSpeed / MaxSpeedMeter));
                }
                else
                {
                    SpeedNeedle.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedNeedleAngle, maxSpeedNeedleAngle, CurrentSpeed / MaxSpeedMeter));
                }
            }

            if (FuelNeedle)
            {
                FuelNeedle.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minFuelNeedleAngle, maxFuelNeedleAngle, CurrentFuel / MaxFuel));
            }

            if (Input.GetKey(KeyCode.E) || isHolding == true)
            {
                if (Mobile)
                {
                    if (StartingValue < EndValue)
                    {
                        StartingValue = Mathf.MoveTowards(StartingValue, EndValue, Car.GetComponent<UVCUniqueVehicleController>().engineStartDuration * Time.deltaTime);
                        if (EngineStartFill)
                        {
                            EngineStartFill.fillAmount = Mathf.MoveTowards(StartingValue, EndValue, Car.GetComponent<UVCUniqueVehicleController>().engineStartDuration * Time.deltaTime) / EndValue;
                        }
                    }
                }
                else
                {
                    if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == false && Car.GetComponent<UVCUniqueVehicleController>().isoutofFuel == false)
                    {
                        StartEngine();
                    }

                    if (Car.GetComponent<UVCUniqueVehicleController>().isoutofFuel == true)
                    {
                        Audio.GetComponent<UVCSoundSystem>().EngineOut();
                        EngineOut = true;
                    }
                }
            }
            else
            {
                if (Mobile)
                {
                    if (StartingValue < EndValue)
                    {
                        StartingValue = Mathf.MoveTowards(StartingValue, MinValue, 4f * Time.deltaTime);
                        if (EngineStartFill)
                        {
                            EngineStartFill.fillAmount = Mathf.MoveTowards(StartingValue, MinValue, Car.GetComponent<UVCUniqueVehicleController>().engineStartDuration * 2 * Time.deltaTime) / EndValue;
                        }
                    }
                }
            }

            if (StartingValue == EndValue)
            {
                if (Car.GetComponent<UVCUniqueVehicleController>().isoutofFuel == true)
                {
                    if (Mobile)
                    {
                        if (Input.GetKey(KeyCode.E) || isHolding == true)
                        {
                            if (!EngineOut)
                            {
                                Audio.GetComponent<UVCSoundSystem>().EngineOut();
                                EngineOut = true;
                            }
                        }
                        else
                        {
                            LightButtonClickSound();
                            Audio.GetComponent<UVCSoundSystem>().EngineOutStop();
                            EngineOut = false;
                            StartingValue = Mathf.MoveTowards(StartingValue, MinValue, 4f * Time.deltaTime);
                            if (EngineStartFill)
                            {
                                EngineStartFill.fillAmount = Mathf.MoveTowards(StartingValue, MinValue, Car.GetComponent<UVCUniqueVehicleController>().engineStartDuration * 2 * Time.deltaTime) / EndValue;
                            }
                        }
                    }
                }

                if (Car.GetComponent<UVCUniqueVehicleController>().isoutofFuel == false)
                {
                    if (Mobile)
                    {
                        EngineOut = false;
                        isHolding = false;
                        if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == false)
                        {
                            StartEngine();

                            if (EngineStartButton)
                            {
                                EngineStartButton.SetActive(false);
                            }

                            if (EngineStopButton)
                            {
                                EngineStopButton.SetActive(true);
                            }
                        }
                        if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
                        {
                            StartingValue = 1;
                        }
                    }
                }
            }

            if (Mobile)
            {
                if (AccisReleased)
                {
                    float currentA = 0;
                    float currenAcc = Mathf.SmoothDamp(Accelerator.value, Accelerator.maxValue, ref currentA, AcceleratorReleaseSpeed);
                    Accelerator.value = currenAcc;
                }

                if (BrakesisReleased)
                {
                    float currentB = 0;
                    if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted)
                    {
                        currenBra = Mathf.SmoothDamp(Brakes.value, Brakes.maxValue, ref currentB, BrakesReleaseSpeed);
                    }
                    else
                    {
                        currenBra = Mathf.SmoothDamp(Brakes.value, Brakes.maxValue, ref currentB, BrakesReleaseSpeed / 4);
                    }
                    Brakes.value = currenBra;
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true && Car.GetComponent<UVCUniqueVehicleController>().isoutofFuel == false)
                {
                    Car.GetComponent<UVCUniqueVehicleController>().isaccelerating = true;
                }
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true && Car.GetComponent<UVCUniqueVehicleController>().isoutofFuel == false)
                {
                    Car.GetComponent<UVCUniqueVehicleController>().isaccelerating = false;
                }
            }

            if (Car.GetComponent<UVCUniqueVehicleController>().usingarrows == false)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    Car.GetComponent<UVCUniqueVehicleController>().goingleft = true;
                }
                else
                {
                    Car.GetComponent<UVCUniqueVehicleController>().goingleft = false;
                }

                if (Input.GetKeyUp(KeyCode.A))
                {
                    Car.GetComponent<UVCUniqueVehicleController>().goingleft = false;
                }

                if (Input.GetKey(KeyCode.D))
                {
                    Car.GetComponent<UVCUniqueVehicleController>().goingright = true;
                }
                else
                {
                    Car.GetComponent<UVCUniqueVehicleController>().goingright = false;
                }

                if (Input.GetKeyUp(KeyCode.D))
                {
                    Car.GetComponent<UVCUniqueVehicleController>().goingright = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Brake();
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                BrakeRelease();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                ToggleCamera();
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                EnableFrontSensors();
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                EnableRearSensors();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                AddOneLittre();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                AddFullTank();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                StopEngine();
            }

            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                GearSlider.value += 1;
                SwitchGear();
            }

            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                GearSlider.value -= 1;
                SwitchGear();
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                StartHorn();
            }
            else if (Input.GetKeyUp(KeyCode.H))
            {
                StopHorn();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                EnableLeftBlinker();
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                EnableHazardLights();
            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                EnableRightBlinker();
            }

            if (Car.GetComponent<UVCUniqueVehicleController>().engineIsStarted == true)
            {
                if(Car.GetComponent<UVCUniqueVehicleController>().isparking == true && !_parkingManager.front && !_parkingManager.rear)
                {
                    DriveNotify.SetActive(true);
                }else{
                    DriveNotify.SetActive(false);
                }

                Gear.SetActive(true);
                ClickPedals.SetActive(true);

                if(PlayerPrefs.GetInt("ControllerType") == 1){
                    SteeringArrows.SetActive(true);
                }else{
                    SteeringWheel.SetActive(true);
                }
                if (Lights)
                {
                    if (Lights.GetComponent<UVCVehicleLights>().foglightsfound > 0)
                    {
                        if (Input.GetKeyDown(KeyCode.L))
                        {
                            if (!isfoglights && !islowbeam && !ishighbeam)
                            {
                                isfoglights = false;
                                islowbeam = true;
                                ishighbeam = false;
                                if (Lights)
                                {
                                    Lights.GetComponent<UVCVehicleLights>().DisableFogLight();
                                    Lights.GetComponent<UVCVehicleLights>().isLowBeamLight = true;
                                    Lights.GetComponent<UVCVehicleLights>().EnableBeamLights();
                                }

                                LightButtonClickSound();
                                //Debug.Log("Low Beam ON");
                            }
                            else if (!isfoglights && islowbeam && !ishighbeam)
                            {
                                isfoglights = false;
                                islowbeam = false;
                                ishighbeam = true;
                                if (Lights)
                                {
                                    Lights.GetComponent<UVCVehicleLights>().isLowBeamLight = false;
                                    Lights.GetComponent<UVCVehicleLights>().isHighBeamLight = true;
                                    Lights.GetComponent<UVCVehicleLights>().EnableBeamLights();
                                }
                                LightButtonClickSound();
                                //Debug.Log("High Beam ON");
                            }
                            else if (!isfoglights && !islowbeam && ishighbeam)
                            {
                                isfoglights = true;
                                islowbeam = false;
                                ishighbeam = false;
                                if (Lights)
                                {
                                    Lights.GetComponent<UVCVehicleLights>().EnableFogLight();
                                }
                                LightButtonClickSound();
                                //Debug.Log("Fog ON");
                            }
                            else if (isfoglights && !islowbeam && !ishighbeam)
                            {
                                isfoglights = false;
                                islowbeam = false;
                                ishighbeam = false;
                                if (Lights)
                                {
                                    Lights.GetComponent<UVCVehicleLights>().isLowBeamLight = false;
                                    Lights.GetComponent<UVCVehicleLights>().isHighBeamLight = false;
                                    Lights.GetComponent<UVCVehicleLights>().DisableFogLight();
                                    Lights.GetComponent<UVCVehicleLights>().DisableBeamLights();
                                }
                                LightButtonClickSound();
                                //Debug.Log("All OFF");
                            }
                        }
                    }

                    if (Lights.GetComponent<UVCVehicleLights>().foglightsfound == 0)
                    {
                        if (Input.GetKeyDown(KeyCode.L))
                        {
                            if (!isfoglights && !islowbeam && !ishighbeam)
                            {
                                isfoglights = false;
                                islowbeam = true;
                                ishighbeam = false;
                                if (Lights)
                                {
                                    Lights.GetComponent<UVCVehicleLights>().DisableFogLight();
                                    Lights.GetComponent<UVCVehicleLights>().isLowBeamLight = true;
                                    Lights.GetComponent<UVCVehicleLights>().EnableBeamLights();
                                }
                                LightButtonClickSound();
                                //Debug.Log("Low Beam ON");
                            }
                            else if (!isfoglights && islowbeam && !ishighbeam)
                            {
                                isfoglights = false;
                                islowbeam = false;
                                ishighbeam = true;
                                if (Lights)
                                {
                                    Lights.GetComponent<UVCVehicleLights>().isLowBeamLight = false;
                                    Lights.GetComponent<UVCVehicleLights>().isHighBeamLight = true;
                                    Lights.GetComponent<UVCVehicleLights>().EnableBeamLights();
                                }
                                LightButtonClickSound();
                                //Debug.Log("High Beam ON");
                            }
                            else if (!isfoglights && !islowbeam && ishighbeam)
                            {
                                isfoglights = false;
                                islowbeam = false;
                                ishighbeam = false;
                                if (Lights)
                                {
                                    Lights.GetComponent<UVCVehicleLights>().isLowBeamLight = false;
                                    Lights.GetComponent<UVCVehicleLights>().isHighBeamLight = false;
                                    Lights.GetComponent<UVCVehicleLights>().DisableFogLight();
                                    Lights.GetComponent<UVCVehicleLights>().DisableBeamLights();
                                }
                                LightButtonClickSound();
                                //Debug.Log("All OFF");
                            }
                        }
                    }
                }
            }else{
                Gear.SetActive(false);
            }

            if (!FirstGear)
            {
                SwitchGear();
                FirstGear = true;
                GearStickDrive.Stop();
                GearStickReverse.Stop();
                GearStickParkNeutral.Stop();
            }
        }
    }
}