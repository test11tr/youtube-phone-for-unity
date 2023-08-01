//--------------------------------------------------------------//
//                                                              //
//             Unique Vehicle Controller v1.0.0                 //
//                  STAY TUNED FOR UPDATES                      //
//           Contact me : imolegstudio@gmail.com                //
//                                                              //
//--------------------------------------------------------------//

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UniqueVehicleController;
using System.Collections;

namespace UniqueVehicleController
{
    public class UVCSteeringWheel : MonoBehaviour
    {
        [Header("Visuals")]
        public Graphic SteeringWheel;

        [Header("Parameters")]
        public float minSteeringAngle = 15;
        public float maxSteeringAngle = 360;
        public float releaseSpeed = 200f;

        GameObject Car;
        RectTransform rectT;
        Vector2 centerPoint;
        Rigidbody CarRb;

        float currentSteeringAngle;
        [HideInInspector]
        public float wheelAngle = 0f;
        float wheelAngleNoAbs;
        float wheelPrevAngle = 0f;

        bool HoldWheel = false;

        public static UVCSteeringWheel UVCSW;

        public float GetClampedValue()
        {
            return wheelAngle / maxSteeringAngle;
        }

        public float GetAngle()
        {
            return wheelAngle;
        }

        void Start()
        {
            UVCSW = this;
            CarRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
            rectT = SteeringWheel.rectTransform;
            InitEventsSystem();

            Car = GameObject.FindWithTag("Player");
        }

        void Update()
        {
            if (Car.GetComponent<UVCUniqueVehicleController>().PowerSteering == false)
            {
                float speedFactor = Car.GetComponent<UVCUniqueVehicleController>().speedFactor;

                if (currentSteeringAngle >= minSteeringAngle)
                {
                    currentSteeringAngle = Mathf.Lerp(maxSteeringAngle, minSteeringAngle, speedFactor);
                }
                else if (currentSteeringAngle <= minSteeringAngle)
                {
                    currentSteeringAngle = minSteeringAngle;
                }
            }

            if (!HoldWheel && !Mathf.Approximately(0f, wheelAngle))
            {
                float deltaAngle = releaseSpeed * Time.deltaTime;
                if (Mathf.Abs(deltaAngle) > Mathf.Abs(wheelAngle))
                {
                    wheelAngle = 0f;
                }
                else if (wheelAngle > 0f)
                {
                    if (Car.GetComponent<UVCUniqueVehicleController>().ismoving == true && Car.GetComponent<UVCUniqueVehicleController>().isbraking == false)
                    {
                        wheelAngle -= deltaAngle;
                    }
                }
                else
                {
                    if (Car.GetComponent<UVCUniqueVehicleController>().ismoving == true && Car.GetComponent<UVCUniqueVehicleController>().isbraking == false)
                    {
                        wheelAngle += deltaAngle;
                    }
                }
            }
            rectT.localEulerAngles = Vector3.back * wheelAngle;
        }

        void InitEventsSystem()
        {
            EventTrigger events = SteeringWheel.gameObject.GetComponent<EventTrigger>();
            if (events == null)
            {
                events = SteeringWheel.gameObject.AddComponent<EventTrigger>();
            }

            if (events.triggers == null)
            {
                events.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();
            }

            EventTrigger.Entry entry = new EventTrigger.Entry();
            EventTrigger.TriggerEvent callback = new EventTrigger.TriggerEvent();
            UnityAction<BaseEventData> functionCall = new UnityAction<BaseEventData>(PressEvent);
            callback.AddListener(functionCall);
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback = callback;
            events.triggers.Add(entry);
            entry = new EventTrigger.Entry();
            callback = new EventTrigger.TriggerEvent();
            functionCall = new UnityAction<BaseEventData>(DragEvent);
            callback.AddListener(functionCall);
            entry.eventID = EventTriggerType.Drag;
            entry.callback = callback;
            events.triggers.Add(entry);
            entry = new EventTrigger.Entry();
            callback = new EventTrigger.TriggerEvent();
            functionCall = new UnityAction<BaseEventData>(ReleaseEvent);
            callback.AddListener(functionCall);
            entry.eventID = EventTriggerType.PointerUp;
            entry.callback = callback;
            events.triggers.Add(entry);
        }

        public void PressEvent(BaseEventData eventData)
        {
            Vector2 pointerPos = ((PointerEventData)eventData).position;
            HoldWheel = true;
            centerPoint = RectTransformUtility.WorldToScreenPoint(((PointerEventData)eventData).pressEventCamera, rectT.position);
            wheelPrevAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);
        }

        public void DragEvent(BaseEventData eventData)
        {
            Vector2 pointerPos = ((PointerEventData)eventData).position;
            float wheelNewAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);
            if (Vector2.Distance(pointerPos, centerPoint) > 20f)
            {
                if (pointerPos.x > centerPoint.x)
                    wheelAngle += wheelNewAngle - wheelPrevAngle;
                else
                    wheelAngle -= wheelNewAngle - wheelPrevAngle;
            }
            if (Car.GetComponent<UVCUniqueVehicleController>().PowerSteering == false)
            {
                if (Car.GetComponent<UVCUniqueVehicleController>().ABSSystem)
                {
                    wheelAngle = Mathf.Clamp(wheelAngle, -currentSteeringAngle, currentSteeringAngle);
                }
                else
                {
                    if (Car.GetComponent<UVCUniqueVehicleController>().isbraking)
                    {
                        wheelAngle = wheelAngleNoAbs;
                    }
                    else
                    {
                        wheelAngleNoAbs = wheelAngle;
                        wheelAngle = Mathf.Clamp(wheelAngle, -currentSteeringAngle, currentSteeringAngle);
                    }
                }
            }
            else
            {
                if (Car.GetComponent<UVCUniqueVehicleController>().ABSSystem)
                {
                    wheelAngle = Mathf.Clamp(wheelAngle, -maxSteeringAngle, maxSteeringAngle);
                }
                else
                {
                    if (Car.GetComponent<UVCUniqueVehicleController>().isbraking)
                    {
                        wheelAngle = wheelAngleNoAbs;
                    }
                    else
                    {
                        wheelAngleNoAbs = wheelAngle;
                        wheelAngle = Mathf.Clamp(wheelAngle, -maxSteeringAngle, maxSteeringAngle);
                    }
                }
            }
            wheelPrevAngle = wheelNewAngle;
        }

        public void ReleaseEvent(BaseEventData eventData)
        {
            DragEvent(eventData);
            HoldWheel = false;
        }
    }
}