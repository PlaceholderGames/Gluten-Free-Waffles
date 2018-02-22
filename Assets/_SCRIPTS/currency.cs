﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currency : MonoBehaviour
{

    public static bool transaction = false;
    public GameObject transactionUI;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //lmb click
        if (Input.GetMouseButtonDown(0))
        {
            //checking if a vendor was selected
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 5) && hit.transform.tag == "vendor")
            {
                transaction = true;
                if (transaction)
                {
                    vendorScreen();
                }
            }
        }	
	}

    void vendorScreen()
    {
        //bringing up the ui and pausing the game
        transactionUI.SetActive(true);
        Time.timeScale = 0f;
        //reallowing player to see and use their mouse cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}