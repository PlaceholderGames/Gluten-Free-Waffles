using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDrop : MonoBehaviour
{
    //setting max distance player can pick up items from
    public float itemRange;
    public Transform player;
    //used to reference which item if any should be activated on the camera
    bool holdingItem = false; //0=no item, 1=stick, 2-rock, etc 
    //used to store name of which item is currently being held
    int currentItem;
    //distance dropped objects drop from player
    public float spawnDistance;
    // Use this for initialization
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
            if (Physics.Raycast(ray, out hit, itemRange) && hit.transform.tag == "item")
            {
                Rigidbody rb;
                //checking that item trying to be picked up is tagged to be held
                if (!holdingItem)
                {
                    //setting the objects rigid body and turning off collisions
                    rb = hit.transform.GetComponent<Rigidbody>();
                    rb.isKinematic = false;
                    rb.detectCollisions = false;
                    rb.useGravity = false;
                    //setting object as a child and giving new position
                    hit.transform.SetParent(player);
                    hit.transform.position = new Vector3(1f, 0.5f, 1f) + hit.transform.parent.position;
                    currentItem = hit.transform.GetComponent<ItemID>().itemID;
                    holdingItem = true;
                    Debug.Log(GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>().items[currentItem].itemName);
                    
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
