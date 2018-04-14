﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemQuestPickup : MonoBehaviour
{

    public bool isFirst;

    public string questGiverName;

    public string questDirections;

    [TextArea]
    public string questText;

    public GameObject nextQuestPoint;

    // Use this for initialization
    void Start()
    {
        if (isFirst)
        {
            gameObject.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = questDirections;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (GetComponent<ItemHeldBool>().beingHeld)
            {
                Debug.Log("Item being held");
                if (nextQuestPoint != null)
                {
                    Debug.Log("Next Quest is not null!");
                    if (!nextQuestPoint.activeSelf)
                    {
                        nextQuestPoint.SetActive(true);
                        nextQuestPoint.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.GetComponent<ItemQuestPickup>().enabled = false;
            }
        }
    }
}