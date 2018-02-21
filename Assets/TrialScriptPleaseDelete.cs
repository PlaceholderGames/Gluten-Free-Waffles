using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;

public class TrialScriptPleaseDelete : MonoBehaviour {
    // Use this for initialization

    bool active = true;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.U))
        {
            active = !active;
            GetComponent<AIRig>().enabled = active ;
        }
	}
}
