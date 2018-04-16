using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pee : MonoBehaviour
{
    public float changeBladderSpeed = 30.0f; //how fast the bladder var will fill up in seconds

    private Vitals vitals;

    // Use this for initialization
    void Start ()
    {
        //get some components & objects
        vitals = GameObject.Find("Character").GetComponent<Vitals>();
        vitals.setBladderState(true);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
