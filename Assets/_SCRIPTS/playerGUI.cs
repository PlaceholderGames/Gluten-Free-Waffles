﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGUI : MonoBehaviour {
    public DayNightCycle dayNightCycle;

    private string txtTime = "TIME TEST";

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), dayNightCycle.getTime().ToString());
        GUI.Label(new Rect(10, 30, 100, 20), dayNightCycle.getDay().ToString());
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
    }
}