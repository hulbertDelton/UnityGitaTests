using System.Collections;
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

    public float speed;
    private Vector3 wheelsPosition;
    private Vector3 defaultAxlePosition;
    private Vector3 defaultWheelLPosition;
    private Vector3 defaultWheelRPosition;
    private float motorL;
    private float motorR;
    private Rigidbody rigBody;
    private float wheelOffset;

    void Start() {
        rigBody = GetComponent<Rigidbody>();
        rigBody.centerOfMass = new Vector3(0, 1f, 1.03f);
        wheelsPosition = new Vector3(0, 0, 0);

        defaultAxlePosition = wheelAxle.transform.localPosition;
        defaultWheelLPosition = leftWheel.center;
        defaultWheelRPosition = rightWheel.center;
    }
    
    private void FixedUpdate() {
        CenterMassSphere.transform.localPosition = rigBody.centerOfMass;
        rigBody.centerOfMass = new Vector3(0, CenterOfMassY, CenterOfMassZ);

        if (wheelOffset > offsetMaximum - (0.2f * offsetMaximum) || wheelOffset < -(offsetMaximum - (0.2f * offsetMaximum))) {
            wheelsPosition.y = Mathf.Lerp(wheelsPosition.y, (Mathf.Abs(wheelOffset) - offsetMaximum - (0.2f * offsetMaximum)) / 4f, Time.deltaTime);
        } else {
            wheelsPosition.y = 0;
        }

        GetMotorInput();
        DisplaceWheels();
        ChooseSpeed();
    }

    //moves the wheels to always be under the chassis
    private void DisplaceWheels() { 
        Quaternion bodyTilt = new Quaternion(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z, Quaternion.identity.w);
        if (bodyTilt.x > 60) {
            wheelOffset = 360 - bodyTilt.x;
        } else {
            wheelOffset = -bodyTilt.x;
        }

        //TODO Damp rocking, it's extreme and unreasonable
        //TODO Reduce body tilt somehow

        wheelOffset = Mathf.Clamp(wheelOffset, -offsetMaximum, offsetMaximum);
        wheelsPosition.z = wheelOffset;

        wheelAxle.localPosition = defaultAxlePosition - wheelsPosition;
        leftWheel.center = defaultWheelLPosition - wheelsPosition;
        rightWheel.center = defaultWheelRPosition - wheelsPosition;

        leftWheel.motorTorque = motorL;
        rightWheel.motorTorque = motorR;

        wheelAxle.transform.Rotate(leftWheel.rpm / 60 * 360 * Time.deltaTime, 0, 0);
    }

    private float ChooseSpeed() {
        Vector3 veloc = transform.GetComponent<Rigidbody>().velocity;
        speed = veloc.magnitude;


        return speed;
    }
    //gets input and applies torque to the wheels
    private void GetMotorInput() {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float turnDirection = Input.GetAxis("Horizontal");

        //TODO make a motion selection UI item that switches between movement systems
        //TODO make a "carrot" movement system, where gita chases a carrot
        //TODO make a "spline" movement system, where gia follows a set path
        //TODO make a torque curve that applies a lot of torque when at a standstill, then reduces to maintain speed when desired speed is reached.
        //TODO make vertical mouse input dictate desired speed
         
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
    }
}