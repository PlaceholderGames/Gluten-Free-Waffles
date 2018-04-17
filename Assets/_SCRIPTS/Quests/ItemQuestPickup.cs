﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemQuestPickup : BaseQuest
{
    public GameObject itemToPickup;

    private ItemHeldBool objectheld;

    // Use this for initialization
    void Start()
    {
        objectheld = itemToPickup.GetComponent<ItemHeldBool>();

        if(objectheld == null)
        {
            objectheld = itemToPickup.AddComponent<ItemHeldBool>();
        }

        setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (!getActivateBool())
            {
                activateBool();
                updateQuest();
            }
            if (objectheld.beingHeld)
            {
                if (transform.parent.GetChild(transform.parent.childCount - 1) != this.transform)
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
