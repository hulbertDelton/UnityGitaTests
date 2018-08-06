using UnityEngine;
using System.Collections;

public class CameraLogic : MonoBehaviour {
    //nathanwentworth.co
    public Transform target;
    public float speed = 3f;
    public float xOffset = 0f;
    public float yOffset = 0f;
    public float zOffset = -3f;
    public float rotationSpeed = 3.0f;
    private Vector3 targetVector;

    void FixedUpdate() {
        if (!target) return;

        transform.position = new Vector3(
        Mathf.Lerp(transform.position.x, target.transform.position.x + xOffset, Time.deltaTime * speed),
        Mathf.Lerp(transform.position.y, target.transform.position.y + yOffset, Time.deltaTime * speed),
        Mathf.Lerp(transform.position.z, target.transform.position.z + zOffset, Time.deltaTime * speed));

        float wantedRotationAngle = target.eulerAngles.y;
        float currentRotationAngle = transform.eulerAngles.y;
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationSpeed * Time.deltaTime);

        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        transform.rotation = currentRotation;
        transform.position += currentRotation * Vector3.forward * zOffset;
    }    //delton's attempt at mouse control    //public float height = 4.0f;
    //private float heightGoal;
    //public float followDistance = 3.0f;
    //public float risingSpeed = 2.0f;
    //public Transform cam;

    //void Start() {
    //    heightGoal = height;

    //}

    //private void FixedUpdate() {
    //    if (!cam) return;
    //    GetInput();

    //        float wantedRotationAngle = transform.eulerAngles.y;
    //        float currentRotationAngle = cam.eulerAngles.y;
    //        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, Input.GetAxis("Mouse X") * Time.deltaTime);

    //        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
    //        cam.rotation = currentRotation;
    //        cam.position += currentRotation * Vector3.forward * followDistance;
    //}

    //private void GetInput() {
    //    float xChange;
    //    float yChange;

    //    if (Input.GetAxis ("Mouse X") != 0) {
    //        xChange = Input.GetAxis("Mouse X");
    //    } else { xChange = 0.0f; }

    //    if (Input.GetAxis("Mouse Y") != 0) {
    //        yChange = Input.GetAxis("Mouse Y");
    //        height += yChange;

    //    } else { yChange = 0.0f; }

    //    cam.transform.position = new Vector3(xChange, height, Mathf.Lerp(cam.position.z, transform.position.z + followDistance, Time.deltaTime * risingSpeed));

    //}
}