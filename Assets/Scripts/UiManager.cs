using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

    public GitaLogic gitaLog;

    public Text speedText;
    public Text torqueText;
    public RawImage speedSlider;
    public float barMover;
    // Use this for initialization
    void Start () {
        barMover = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {
        speedText.text = Mathf.Round(gitaLog.speed * 10) / 10 + " m/s";
        torqueText.text = Mathf.Round(gitaLog.rightWheel.motorTorque * 10) / 10 + " N⋅m";

        barMover += Input.GetAxis("Mouse Y") * Time.deltaTime * 200;
        barMover = Mathf.Clamp(barMover, -720, 720);
        Vector3 speedPos = new Vector3(-50, barMover, 0);
        speedSlider.rectTransform.localPosition = speedPos;
	}
}
