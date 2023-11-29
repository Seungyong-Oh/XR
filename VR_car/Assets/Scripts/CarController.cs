using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] WheelCollider frontLeftW, frontRightW;
    [SerializeField] GameObject steerParent;
    [SerializeField] GameObject tachPointer, speedPointer;
    [SerializeField] AudioSource engineSound;

    // Start is called before the first frame update
    void Start()
    {
        frontLeftW.motorTorque = 0f;
        frontRightW.motorTorque = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);
        foreach (var device in rightHandDevices)
        {
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 position))
            {
                if (position.y > 0)
                {
                    frontLeftW.brakeTorque = 0f;
                    frontRightW.brakeTorque = 0f;
                    frontLeftW.motorTorque = position.y * 100f;
                    frontRightW.motorTorque = position.y * 100f;
                    engineSound.pitch = 1f + position.y;
                }
                else if (position.y < 0)
                {
                    frontLeftW.brakeTorque = -position.y * 200f;
                    frontRightW.brakeTorque = -position.y * 200f;
                    frontLeftW.motorTorque = 0f;
                    frontRightW.motorTorque = 0f;
                    engineSound.pitch = 1f;
                }
                frontLeftW.steerAngle = position.x * 10f;
                frontRightW.steerAngle = position.x * 10f;
                steerParent.transform.localRotation = Quaternion.Euler(0, 0, -position.x * 100f);
                if (position.y >= 0)
                    tachPointer.transform.localRotation = Quaternion.Euler(0, 0, -position.y * 100f);
                var speed = frontLeftW.rpm * 2f * Mathf.PI * frontLeftW.radius * 60f / 1000f;
                speedPointer.transform.localRotation = Quaternion.Euler(0, 0, -speed * 2f);
            }
        }
    }
}
