using System.Collections;
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
            
            if (holdingItem)
            {
                dropItem();
                
            }
            else if (!holdingItem)
            {
                pickupItem();
            }
        }
    }
    void dropItem()
    {
        itemInHand.isKinematic = false;
        itemInHand.detectCollisions = true;
        itemInHand.useGravity = true;
        itemInHand.constraints = RigidbodyConstraints.None;
        itemInHand.transform.parent = null;
        holdingItem = false;
        daInventoryMan.GetComponent<Inventory>().setItemHolding(-1);
    }
    void pickupItem()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, itemRange) && hit.transform.tag == "item")    //checking that item trying to be picked up is tagged to be held
        {
            if (hit.transform.GetComponent<ItemID>().itemID == 3)
            {
                daInventoryMan.GetComponent<Inventory>().CollectedCollectable(hit.transform.GetComponent<ItemID>().itemID);
                Destroy(hit.rigidbody.gameObject);
            }
            else
            {
                //setting object as a child and giving new position
                hit.transform.SetParent(player);
                //changing the items position so that it is in a set position when picked up
                hit.transform.position = hit.transform.parent.position + Camera.main.transform.right * 0.8f + Camera.main.transform.forward - Camera.main.transform.up * 0.08f;
                //getting the rotation of the player to base item rotation off of
                Quaternion playerRotation = player.transform.rotation;
                //adjusting the rotation of the item to a prefered alignment
                hit.transform.rotation = playerRotation;
                hit.transform.Rotate(Vector3.right, -90);
                hit.transform.Rotate(Vector3.forward, 180);
                daInventoryMan.GetComponent<Inventory>().setItemHolding(hit.transform.GetComponent<ItemID>().itemID);
                holdingItem = true;

                //setting the objects rigid body and turning off collisions
                itemInHand = hit.transform.GetComponent<Rigidbody>();
                itemInHand.isKinematic = true;
                itemInHand.detectCollisions = false;
                itemInHand.useGravity = false;
                itemInHand.constraints = RigidbodyConstraints.FreezeAll;
            }

        }
    }
}

//else
//{
//    //ITEMS BEING DROPPED
//    //looking to drop a stick
//    //string ssdasd;
//    if (itemOut == 1)
//    {
//        //setting the currently held item to false
//        //ssdasd = arraywtvtf[itemOut];
//        transform.Find("stick").gameObject.SetActive(false);
//        //spawning in the dropped item
//        dropItem(itemOut);
//        //reseting item held to 0
//        itemOut = 0;
//    }
//    //looking to drop a rock
//    else if (itemOut == 2)
//    {
//        //setting the currently held item to false
//        transform.Find("rock").gameObject.SetActive(false);
//        //spawning in the dropped item
//        dropItem(itemOut);
//        //reseting item held to 0
//        itemOut = 0;
//    }
//}
//}
//}
//player looking to drop an item