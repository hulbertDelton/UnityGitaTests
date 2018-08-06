using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flicker : MonoBehaviour {

    public Light flickerLight;
    public float random;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        random = Random.value;

        if (random < 0.1f) {
            flickerLight.enabled = false;
        } else {
            flickerLight.enabled = true;
        } 

	}
}
