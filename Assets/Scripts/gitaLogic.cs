﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GitaLogic : MonoBehaviour {

    [Header("------------Editable variables------------")]
    [Range(0f, 650f)]
    public float maxMotorTorque;
    public float offsetMaximum = 14f;
    public float CenterOfMassY = 1f;
    public float CenterOfMassZ = 1.03f;

    [Header("------------Hookups------------")]
    public Transform wheelAxle;
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public GameObject CenterMassSphere;

    private Vector3 wheelsPosition;
    private Vector3 defaultAxlePosition;
    private Vector3 defaultWheelLPosition;
    private Vector3 defaultWheelRPosition;
    private float motorL;
    private float motorR;
    private Rigidbody rigBody;
    private float wheelOffset;

    //--------------------------------------------------------------------------------------------------------------------
    void Start() {
        rigBody = GetComponent<Rigidbody>();
        rigBody.centerOfMass = new Vector3(0, 1f, 1.03f);
        wheelsPosition = new Vector3(0, 0, 0);

        defaultAxlePosition = wheelAxle.transform.localPosition;
        defaultWheelLPosition = leftWheel.center;
        defaultWheelRPosition = rightWheel.center;
    }

    //--------------------------------------------------------------------------------------------------------------------
    private void FixedUpdate() {
        CenterMassSphere.transform.localPosition = rigBody.centerOfMass;
        rigBody.centerOfMass = new Vector3(0, CenterOfMassY, CenterOfMassZ);

        if (wheelOffset > offsetMaximum - (0.2f * offsetMaximum) || wheelOffset < -(offsetMaximum - (0.2f * offsetMaximum))) {
            wheelsPosition.y = Mathf.Lerp(wheelsPosition.y, (Mathf.Abs(wheelOffset) - offsetMaximum - (0.2f * offsetMaximum)) / 4f, Time.deltaTime);
        } else {
            wheelsPosition.y = 0;
        }

        //new Motor input
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float turnDirection = Input.GetAxis("Horizontal");

        if (Input.GetAxis("Horizontal") < 0.01f && Input.GetAxis("Horizontal") > -0.01f) {
            turnDirection = 0;
        }

        if (turnDirection != 0) {
            motorL = maxMotorTorque * -turnDirection;
            motorR = maxMotorTorque * turnDirection;
        } else {
            motorL = motor;
            motorR = motor;
        }

        //wheel displacement
        Quaternion bodyTilt = new Quaternion (transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z, Quaternion.identity.w);
        if (bodyTilt.x > 60) {
            wheelOffset = 360 - bodyTilt.x;
        } else {
            wheelOffset = -bodyTilt.x;
        }
        wheelOffset = Mathf.Clamp(wheelOffset, -offsetMaximum, offsetMaximum);
        wheelsPosition.z = wheelOffset;

        wheelAxle.localPosition = defaultAxlePosition - wheelsPosition;
        leftWheel.center = defaultWheelLPosition - wheelsPosition;
        rightWheel.center = defaultWheelRPosition - wheelsPosition;

        leftWheel.motorTorque = motorL;
        rightWheel.motorTorque = motorR;

        wheelAxle.transform.Rotate(leftWheel.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    }
}