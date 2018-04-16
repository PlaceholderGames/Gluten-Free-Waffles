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
            if (objectheld.beingHeld)
            {
                if (nextQuestPoint != null)
                {
                    if (!nextQuestPoint.activeSelf)
                    {
                        nextQuestPoint.SetActive(true);
                        nextQuestPoint.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
                else
                {
                    endQuest();
                }
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.GetComponent<ItemQuestPickup>().enabled = false;
            }
        }
    }
}
