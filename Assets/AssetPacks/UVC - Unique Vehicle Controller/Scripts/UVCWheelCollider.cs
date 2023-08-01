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
    public class UVCWheelCollider : MonoBehaviour
    {
        private Transform m_dummyWheel;
        private Rigidbody m_rigidbody;
        private WheelFrictionCurveSource m_forwardFriction;
        private WheelFrictionCurveSource m_sidewaysFriction;
        private float m_forwardSlip;
        private float m_sidewaysSlip;
        private Vector3 m_totalForce;
        private Vector3 m_center;
        private Vector3 m_prevPosition;
        [HideInInspector]
        public bool m_isGrounded;
        private float m_wheelMotorTorque;
        private float m_wheelBrakeTorque;
        [HideInInspector]
        public float m_wheelSteerAngle;
        [HideInInspector]
        public float m_wheelSteerAngleNoABS;
        private float m_wheelAngularVelocity;
        private float m_wheelRotationAngle;
        private float m_wheelRotationAngleNoABS;
        private float m_wheelRadius;
        private float m_wheelMass;
        private RaycastHit m_raycastHit;
        private float m_suspensionDistance;
        private float m_suspensionCompression;
        private float m_suspensionCompressionPrev;
        private JointSpringSource m_suspensionSpring;
        private Color GizmoColor;
        GameObject Car;

        public static UVCWheelCollider UVCWC;

        public Vector3 Center
        {
            set
            {
                m_center = value;
                m_dummyWheel.localPosition = transform.localPosition + m_center;
                m_prevPosition = m_dummyWheel.localPosition;
            }
            get
            {
                return m_center;
            }
        }
        public float WheelRadius
        {
            set
            {
                m_wheelRadius = value;
            }
            get
            {
                return m_wheelRadius;
            }
        }

        public float SuspensionDistance
        {
            set
            {
                m_suspensionDistance = value;
            }
            get
            {
                return m_suspensionDistance;
            }
        }
        public JointSpringSource SuspensionSpring
        {
            set
            {
                m_suspensionSpring = value;
            }
            get
            {
                return m_suspensionSpring;
            }
        }
        public float Mass
        {
            set
            {
                m_wheelMass = Mathf.Max(value, 0.0001f);
            }
            get
            {
                return m_wheelMass;
            }
        }
        public WheelFrictionCurveSource ForwardFriction
        {
            set
            {
                m_forwardFriction = value;
            }
            get
            {
                return m_forwardFriction;
            }
        }
        public WheelFrictionCurveSource SidewaysFriction
        {
            set
            {
                m_sidewaysFriction = value;
            }
            get
            {
                return m_sidewaysFriction;
            }
        }
        public float MotorTorque
        {
            set
            {
                m_wheelMotorTorque = value;
            }
            get
            {
                return m_wheelMotorTorque;
            }
        }
        public float BrakeTorque
        {
            set
            {
                m_wheelBrakeTorque = value;
            }
            get
            {
                return m_wheelBrakeTorque;
            }
        }
        public float SteerAngle
        {
            set
            {
                m_wheelSteerAngle = value;
            }
            get
            {
                return m_wheelSteerAngle;
            }
        }
        public bool IsGrounded
        {
            get
            {
                return m_isGrounded;
            }
        }
        public float RPM
        {
            get
            {
                return m_wheelAngularVelocity;
            }
        }

        public void Awake()
        {
            m_forwardFriction = new WheelFrictionCurveSource();
            m_sidewaysFriction = new WheelFrictionCurveSource();
            m_dummyWheel = new GameObject("DummyWheel").transform;
            m_dummyWheel.transform.parent = this.transform.parent;
            Center = Vector3.zero;
        }

        public void Start()
        {
            UVCWC = this;

            GameObject parent = this.gameObject;
            while (parent != null)
            {
                if (parent.GetComponent<Rigidbody>() != null)
                {
                    m_rigidbody = parent.GetComponent<Rigidbody>();
                    break;
                }
                parent = parent.transform.parent.gameObject;
            }
            if (m_rigidbody == null)
            {
                Debug.LogError("WheelCollider: Unable to find associated Rigidbody.");
            }

            Car = GameObject.FindWithTag("Player");
        }

        public void FixedUpdate()
        {
            UpdateSuspension();
            UpdateWheel();
            if (m_isGrounded)
            {
                CalculateSlips();

                CalculateForcesFromSlips();

                m_rigidbody.AddForceAtPosition(m_totalForce, transform.position);
            }
            WheelHitSource CorrespondingGroundHit;
            GetGroundHit(out CorrespondingGroundHit);
        }

        public void OnDrawGizmosSelected()
        {
            if (!m_dummyWheel) return;
            Gizmos.color = GizmoColor;
            Gizmos.DrawLine(
                transform.position - m_dummyWheel.up * m_wheelRadius,
                transform.position + (m_dummyWheel.up * (m_suspensionDistance - m_suspensionCompression))
            );
            Vector3 point1;
            Vector3 point0 = transform.TransformPoint(m_wheelRadius * new Vector3(0, Mathf.Sin(0), Mathf.Cos(0)));
            for (int i = 1; i <= 20; ++i)
            {
                point1 = transform.TransformPoint(m_wheelRadius * new Vector3(0, Mathf.Sin(i / 20.0f * Mathf.PI * 2.0f), Mathf.Cos(i / 20.0f * Mathf.PI * 2.0f)));
                Gizmos.DrawLine(point0, point1);
                point0 = point1;

            }
            Gizmos.color = Color.white;
        }

        public bool GetGroundHit(out WheelHitSource wheelHit)
        {
            wheelHit = new WheelHitSource();
            if (m_isGrounded)
            {
                wheelHit.Collider = m_raycastHit.collider;
                wheelHit.Point = m_raycastHit.point;
                wheelHit.Normal = m_raycastHit.normal;
                wheelHit.ForwardDir = m_dummyWheel.forward;
                wheelHit.SidewaysDir = m_dummyWheel.right;
                wheelHit.Force = m_totalForce;
                wheelHit.ForwardSlip = m_forwardSlip;
                wheelHit.SidewaysSlip = m_sidewaysSlip;
            }

            return m_isGrounded;
        }

        private void UpdateSuspension()
        {
            bool result = Physics.Raycast(new Ray(m_dummyWheel.position, -m_dummyWheel.up), out m_raycastHit, m_wheelRadius + m_suspensionDistance);
            if (result)
            {
                if (!m_isGrounded)
                {
                    m_prevPosition = m_dummyWheel.position;
                }
                GizmoColor = Color.green;
                m_isGrounded = true;
                m_suspensionCompressionPrev = m_suspensionCompression;
                m_suspensionCompression = m_suspensionDistance + m_wheelRadius - (m_raycastHit.point - m_dummyWheel.position).magnitude;
                if (m_suspensionCompression > m_suspensionDistance)
                {
                    GizmoColor = Color.red;
                }
            }
            else
            {
                m_suspensionCompression = 0;
                GizmoColor = Color.blue;
                m_isGrounded = false;
            }
        }

        private void UpdateWheel()
        {
            m_dummyWheel.localEulerAngles = new Vector3(0, m_wheelSteerAngle, 0);
            m_wheelRotationAngle += m_wheelAngularVelocity * Time.fixedDeltaTime;


            if (Car.GetComponent<UVCUniqueVehicleController>().ABSSystem)
            {
                this.transform.localEulerAngles = new Vector3(m_wheelRotationAngle, m_wheelSteerAngle, 0);
            }
            else
            {
                this.transform.localEulerAngles = new Vector3(m_wheelRotationAngle, m_wheelSteerAngle, 0);

                if (Car.GetComponent<UVCUniqueVehicleController>().isbraking == false)
                {
                    m_wheelRotationAngleNoABS = m_wheelRotationAngle;
                    m_wheelSteerAngleNoABS = m_wheelSteerAngle;
                }
            }

            transform.localPosition = m_dummyWheel.localPosition - Vector3.up * (m_suspensionDistance - m_suspensionCompression);

            if (m_isGrounded && m_wheelMotorTorque == 0)
            {
                m_wheelAngularVelocity -= Mathf.Sign(m_forwardSlip) * m_forwardFriction.Evaluate(m_forwardSlip) / (Mathf.PI * 2.0f * m_wheelRadius) / m_wheelMass * Time.fixedDeltaTime;
            }

            m_wheelAngularVelocity += m_wheelMotorTorque / m_wheelRadius / m_wheelMass * Time.fixedDeltaTime;

            if (Car.GetComponent<UVCUniqueVehicleController>().ABSSystem)
            {
                m_wheelAngularVelocity -= Mathf.Sign(m_wheelAngularVelocity) * Mathf.Min(Mathf.Abs(m_wheelAngularVelocity), m_wheelBrakeTorque * m_wheelRadius / m_wheelMass * Time.fixedDeltaTime);
            }
            else
            {
                m_wheelAngularVelocity -= Mathf.Sign(m_wheelAngularVelocity) * Mathf.Min(Mathf.Abs(m_wheelAngularVelocity), m_wheelBrakeTorque * m_wheelRadius / m_wheelMass * Time.fixedDeltaTime);
                this.transform.localEulerAngles = new Vector3(m_wheelRotationAngleNoABS, m_wheelSteerAngleNoABS, 0);
            }
        }

        private void CalculateSlips()
        {
            Vector3 velocity = (m_dummyWheel.position - m_prevPosition) / Time.fixedDeltaTime;
            m_prevPosition = m_dummyWheel.position;
            Vector3 forward = m_dummyWheel.forward;
            Vector3 sideways = -m_dummyWheel.right;
            Vector3 forwardVelocity = Vector3.Dot(velocity, forward) * forward;
            Vector3 sidewaysVelocity = Vector3.Dot(velocity, sideways) * sideways;
            m_forwardSlip = -Mathf.Sign(Vector3.Dot(forward, forwardVelocity)) * forwardVelocity.magnitude + (m_wheelAngularVelocity * Mathf.PI / 180.0f * m_wheelRadius);
            m_sidewaysSlip = -Mathf.Sign(Vector3.Dot(sideways, sidewaysVelocity)) * sidewaysVelocity.magnitude;
        }

        private void CalculateForcesFromSlips()
        {
            m_totalForce = m_dummyWheel.forward * Mathf.Sign(m_forwardSlip) * m_forwardFriction.Evaluate(m_forwardSlip);
            m_totalForce -= m_dummyWheel.right * Mathf.Sign(m_sidewaysSlip) * m_sidewaysFriction.Evaluate(m_sidewaysSlip);
            m_totalForce += m_dummyWheel.up * (m_suspensionCompression - m_suspensionDistance * (m_suspensionSpring.TargetPosition)) * m_suspensionSpring.Spring;
            m_totalForce += m_dummyWheel.up * (m_suspensionCompression - m_suspensionCompressionPrev) / Time.fixedDeltaTime * m_suspensionSpring.Damper;
        }
    }

    public struct JointSpringSource
    {
        private float m_spring;
        private float m_damper;
        private float m_targetPosition;

        public float Spring
        {
            get
            {
                return m_spring;
            }
            set
            {
                m_spring = Mathf.Max(0, value);
            }
        }
        public float Damper
        {
            get
            {
                return m_damper;
            }
            set
            {
                m_damper = Mathf.Max(0, value);
            }
        }
        public float TargetPosition
        {
            get
            {
                return m_targetPosition;
            }
            set
            {
                m_targetPosition = Mathf.Clamp01(value);
            }
        }
    }

    public class WheelFrictionCurveSource
    {
        private struct WheelFrictionCurvePoint
        {
            public float TValue;
            public Vector2 SlipForcePoint;
        }
        private float m_extremumSlip;
        private float m_extremumValue;
        private float m_asymptoteSlip;
        private float m_asymptoteValue;
        private float m_stiffness;

        private int m_arraySize;
        private WheelFrictionCurvePoint[] m_extremePoints;
        private WheelFrictionCurvePoint[] m_asymptotePoints;

        public float ExtremumSlip
        {
            get
            {
                return m_extremumSlip;
            }
            set
            {
                m_extremumSlip = value;
                UpdateArrays();
            }
        }
        public float ExtremumValue
        {
            get
            {
                return m_extremumValue;
            }
            set
            {
                m_extremumValue = value;
                UpdateArrays();
            }
        }
        public float AsymptoteSlip
        {
            get
            {
                return m_asymptoteSlip;
            }
            set
            {
                m_asymptoteSlip = value;
                UpdateArrays();
            }
        }
        public float AsymptoteValue
        {
            get
            {
                return m_asymptoteValue;
            }
            set
            {
                m_asymptoteValue = value;
                UpdateArrays();
            }
        }
        public float Stiffness
        {
            get
            {
                return m_stiffness;
            }
            set
            {
                m_stiffness = value;
            }
        }

        public WheelFrictionCurveSource()
        {
            m_extremumSlip = 3;
            m_extremumValue = 6000;
            m_asymptoteSlip = 4;
            m_asymptoteValue = 5500;
            m_stiffness = 4;
            m_arraySize = 50;
            m_extremePoints = new WheelFrictionCurvePoint[m_arraySize];
            m_asymptotePoints = new WheelFrictionCurvePoint[m_arraySize];
            UpdateArrays();
        }

        private void UpdateArrays()
        {
            for (int i = 0; i < m_arraySize; ++i)
            {
                m_extremePoints[i].TValue = (float)i / (float)m_arraySize;
                m_extremePoints[i].SlipForcePoint = Hermite(
                        (float)i / (float)m_arraySize,
                        Vector2.zero,
                        new Vector2(m_extremumSlip, m_extremumValue),
                        Vector2.zero,
                        new Vector2(m_extremumSlip * 0.5f + 1, 0)
                    );

                m_asymptotePoints[i].TValue = (float)i / (float)m_arraySize;
                m_asymptotePoints[i].SlipForcePoint = Hermite(
                    (float)i / (float)m_arraySize,
                        new Vector2(m_extremumSlip, m_extremumValue),
                        new Vector2(m_asymptoteSlip, m_asymptoteValue),
                        new Vector2((m_asymptoteSlip - m_extremumSlip) * 0.5f + 1, 0),
                        new Vector2((m_asymptoteSlip - m_extremumSlip) * 0.5f + 1, 0)
                    );
            }
        }

        public float Evaluate(float slip)
        {
            slip = Mathf.Abs(slip);
            if (slip < m_extremumSlip)
            {
                return Evaluate(slip, m_extremePoints) * m_stiffness;
            }
            else if (slip < m_asymptoteSlip)
            {
                return Evaluate(slip, m_asymptotePoints) * m_stiffness;
            }
            else
            {
                return m_asymptoteValue * m_stiffness;
            }
        }

        private float Evaluate(float slip, WheelFrictionCurvePoint[] curvePoints)
        {
            int top = m_arraySize - 1;
            int bottom = 0;
            int index = (int)((top + bottom) * 0.5f);
            WheelFrictionCurvePoint result = curvePoints[index];
            while ((top != bottom && top - bottom > 1))
            {
                if (result.SlipForcePoint.x <= slip)
                {
                    bottom = index;
                }
                else if (result.SlipForcePoint.x >= slip)
                {
                    top = index;
                }

                index = (int)((top + bottom) * 0.5f);
                result = curvePoints[index];
            }
            float slip1 = curvePoints[bottom].SlipForcePoint.x;
            float slip2 = curvePoints[top].SlipForcePoint.x;
            float force1 = curvePoints[bottom].SlipForcePoint.y;
            float force2 = curvePoints[top].SlipForcePoint.y;
            float slipFraction = (slip - slip1) / (slip2 - slip1);

            return force1 * (1 - slipFraction) + force2 * (slipFraction); ;
        }

        private Vector2 Hermite(float t, Vector2 p0, Vector2 p1, Vector2 m0, Vector2 m1)
        {
            float t2 = t * t;
            float t3 = t2 * t;

            return
                (2 * t3 - 3 * t2 + 1) * p0 +
                (t3 - 2 * t2 + t) * m0 +
                (-2 * t3 + 3 * t2) * p1 +
                (t3 - t2) * m1;
        }
    }

    public struct WheelHitSource
    {
        public Collider Collider;
        public Vector3 Point;
        public Vector3 Normal;
        public Vector3 ForwardDir;
        public Vector3 SidewaysDir;
        public Vector3 Force;
        public float ForwardSlip;
        public float SidewaysSlip;
    }
}