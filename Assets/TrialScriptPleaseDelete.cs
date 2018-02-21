using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialScriptPleaseDelete : MonoBehaviour {

    public Component comp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.U))
        {
            GetComponent<AIRig>();
        }
	}
}
