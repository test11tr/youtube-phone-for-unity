﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PG
{
    /// <summary>
    /// It differs from the basic type only in the display of WheelView, the wheel of the bike uses a different logic.
    /// </summary>
    public class BikeWheel :Wheel
    {
        Vector3 StartLocalPos;

        private void Start ()
        {
            StartLocalPos = WheelView.transform.localPosition;
        }

        public override void LateUpdate ()
        {
            if (Vehicle.VehicleIsVisible)
            {
                WheelCollider.GetWorldPose (out Position, out Rotation);
                CurrentRotateAngle += RPM * Time.deltaTime * 6.28f;
                WheelView.localRotation = Quaternion.AngleAxis (CurrentRotateAngle, Vector3.right);
            }
        }

        public override void RestoreObject ()
        {
            base.RestoreObject ();
            WheelView.transform.localPosition = StartLocalPos;
        }

        /// <summary>
        /// Moving WheelView on impact because WheelView is not a child of Wheel
        /// </summary>
        public override void MoveObject (Vector3 damageVelocity)
        {
            base.MoveObject (damageVelocity);

            WheelView.localPosition += damageVelocity;
        }
    }
}
