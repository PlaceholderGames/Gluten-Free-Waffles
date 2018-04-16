﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputQuest : BaseQuest
{
    

    public string axis;
   
    // Use this for initialization
    void Start () {
        setup();
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.activeSelf)
        {
            if (!getActivateBool())
            {

                activateBool();
                updateQuest();
            }
            if (Input.GetAxis(axis) != 0)
            {
                if (nextQuestPoint.Length != 0)
                {
                    continueQuest();

                }
                else
                {
                    endQuest();
                }
                this.gameObject.SetActive(false);
            }
        }
    }
}
