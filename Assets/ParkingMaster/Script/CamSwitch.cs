using System.Collections;
using System.Collections.Generic;
using UniqueVehicleController;
using UnityEngine;

namespace test11
{
    public class CamSwitch : MonoBehaviour
    {
        public Transform[] CameraPositions;
        public Camera _camera;
        public float transitionDuration = 1f;
        private bool isTransitioning = false;
        private Vector3 initialPosition;
        private Quaternion initialRotation;
        private float transitionStartTime;
        private int targetCam;

        void Start() {
            TriggerTransition(0);
        }

        void Update(){
            if (isTransitioning){
                float transitionProgress = (Time.time - transitionStartTime) / transitionDuration;
                transitionProgress = Mathf.Clamp01(transitionProgress);

                SmoothTransition(transitionProgress);

                if (transitionProgress >= 1f)
                {
                    isTransitioning = false;
                }
            }
        }

        public void TriggerTransition(int _target){
            initialPosition = _camera.transform.position;
            initialRotation = _camera.transform.rotation;
            transitionStartTime = Time.time;
            isTransitioning = true;
            targetCam = _target;
        }

        void SmoothTransition(float progress)
        {
            _camera.transform.position = Vector3.Lerp(initialPosition, CameraPositions[targetCam].position, progress);
            _camera.transform.rotation = Quaternion.Lerp(initialRotation, CameraPositions[targetCam].rotation, progress);
        }
    }
}
