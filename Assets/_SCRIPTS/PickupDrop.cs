﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDrop : MonoBehaviour
{
    public GUISkin Messages;
    //setting max distance player can pick up items from
    public float itemRange;
    public Transform player;
    //used to reference which item if any should be activated on the camera
    bool holdingItem = false;
    //distance dropped objects drop from player
    public float spawnDistance;
    public GameObject daInventoryMan;
    // Use this for initialization
    public Rigidbody itemInHand;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //player input to try and pick up an item
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, itemRange) && hit.transform.tag == "item")    //checking that item trying to be picked up is tagged to be held
            {
                if (!holdingItem)
                {
                    //setting object as a child and giving new position
                    hit.transform.SetParent(player);
                    hit.transform.position = new Vector3(2.0f, 0.0f, 0.8f) + hit.transform.parent.position;
                    daInventoryMan.GetComponent<Inventory>().setItemHolding(hit.transform.GetComponent<ItemID>().itemID);
                    holdingItem = true;
                    
                    //setting the objects rigid body and turning off collisions
                    itemInHand = hit.transform.GetComponent<Rigidbody>();
                    itemInHand.isKinematic = true;
                    itemInHand.detectCollisions = false;
                    itemInHand.useGravity = false;
                    itemInHand.constraints = RigidbodyConstraints.FreezeAll;
                }
                else if(holdingItem)
                {
                    //todo
                }
                //if player is trying to drop item raycast should return as null
            }
        }
    }
}
/*else
{
    //ITEMS BEING DROPPED
    //looking to drop a stick
    //string ssdasd;
    if (itemOut == 1)
    {
        //setting the currently held item to false
        //ssdasd = arraywtvtf[itemOut];
        transform.Find("stick").gameObject.SetActive(false);
        //spawning in the dropped item
        dropItem(itemOut);
        //reseting item held to 0
        itemOut = 0;
    }
    //looking to drop a rock
    else if (itemOut == 2)
    {
        //setting the currently held item to false
        transform.Find("rock").gameObject.SetActive(false);
        //spawning in the dropped item
        dropItem(itemOut);
        //reseting item held to 0
        itemOut = 0;
    }
}
}
}
//player looking to drop an item
void dropItem(int itemRef)
{
Vector3 playerPos = player.transform.position;
Vector3 playerDirection = player.transform.forward;
Quaternion playerRotation = player.transform.rotation;
Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
//dropping a stick
if (itemRef == 1)
{
Instantiate(stick, spawnPos, playerRotation);
}
//dropping a rock
else if (itemRef == 2)
{
Instantiate(rock, spawnPos, playerRotation);
}
}
}*/
