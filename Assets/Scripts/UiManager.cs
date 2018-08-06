using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

    public GitaLogic gitaLog;

    public Text speedText;
    public Text torqueText;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        speedText.text = Mathf.Round(gitaLog.rightWheel.motorTorque * 10) / 10 + " m/s";
        torqueText.text = Mathf.Round(gitaLog.rightWheel.motorTorque * 10) / 10 + " N⋅m";
	}
}
