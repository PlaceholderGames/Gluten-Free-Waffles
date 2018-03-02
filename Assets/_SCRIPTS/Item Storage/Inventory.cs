﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    private ItemDatabase database;

    public GameObject playerPhone;

    public List<Item> foodList = new List<Item>();
    public List<Item> drinkList = new List<Item>();
    public List<Item> clothesList = new List<Item>();
    public List<Item> questList = new List<Item>();
    public List<Item> miscList = new List<Item>();

    //IDs start at 0. -1 indicated no item present.
    private int itemHolding = -1;      

    //Identifiers to know if player has certain items
    private bool hasPhone;

    public bool phoneOut;

    private void Start()
    {
        //Hides the phone as it hasn't been found at the start of the game.
        playerPhone = GameObject.FindGameObjectWithTag("Phone");
        playerPhone.SetActive(false);

        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
    }

    private void Update()
    {
        //Updates the current status of the phone if the player has found the phone
        if (Input.GetButtonDown("Phone") && hasPhone)
        {
            updatePhone();
        }
        
        //If the player is holding an item...
        if (itemHolding != -1)
        {
            //If the player wishes to store the item...
            if (Input.GetButtonDown("Store"))
            {
                updateItems(itemHolding, true);
                itemHolding = -1;
                GameObject.Find("FPPCamera").GetComponent<PickupDrop>().holdingItem = false;
                Destroy(GameObject.Find("FPPCamera").GetComponent<PickupDrop>().itemInHand.gameObject);
            }

            //If the player wishes to use the item...
            if (Input.GetButtonDown("Use"))
            {
                useItem(GameObject.Find("ItemDatabase").GetComponent<ItemDatabase>().items[itemHolding],true);
                GameObject.Find("FPPCamera").GetComponent<PickupDrop>().holdingItem = false;
            }

        }
    }

    //Shows/hides the phone
    public void updatePhone()
    {
        phoneOut = !phoneOut;
        playerPhone.SetActive(phoneOut);
    }

    //Adds or removes items into the system based on bool (true = add)
    public void updateItems(int id, bool addOrRemove)
    { 
        Item storedItem = database.items[id];
        List<Item> typeList = new List<Item>();
        string type = storedItem.itemType.ToString();

        switch (type)
        {
            case "Food":
                {
                    updateList(ref foodList, storedItem, addOrRemove);
                    break;
                }
            case "Drink":
                {
                    updateList(ref drinkList, storedItem, addOrRemove);
                    break;
                }
            case "Clothes":
                {
                    updateList(ref clothesList, storedItem, addOrRemove);
                    break;
                }
            case "Quest":
                {
                    updateList(ref questList, storedItem, addOrRemove);
                    break;
                }
            case "Misc":
                {
                    updateList(ref miscList, storedItem, addOrRemove);
                    break;
                }
        }
    }

    public void updateList (ref List<Item> list, Item current, bool aor)
    {
        bool add = true;
        bool remove = true;
        if (aor == true)
        {
            current.itemQuantity++;
            for (int i = 0; i < list.Count; i++)
            {
                //If the player already has the item, increases the quantity
                if (list[i] == current)
                {
                    list[i].itemQuantity++;
                    add = false;
                }
            }

            //Adds the item to the appropriate list
            if (add == true)
            {
                list.Add(current);
                list = list.OrderBy(g => g.itemName).ToList();
            }
        }
        else
        {
            for (int i = 0; i < list.Count; i++)
            {
                //If the player already has the item, decreases the quantity
                if (list[i] == current)
                {
                    list[i].itemQuantity--;
                    remove = false;

                    if (list[i].itemQuantity < 1)
                        remove = true;
                }
            }

            //Adds the item to the appropriate list
            if (remove == true)
            {
                list.Remove(current);
            }
        }

    }

    //Setters and Getters for pickupDrop
    public void setItemHolding(int i)
    {
        itemHolding = i;
    }
    public int getItemHolding()
    {
        return itemHolding;
    }

    public void CollectedCollectable(int itemCollected)
    {
        switch (itemCollected)
        {
            //Add ID's and bools for collectable items eg. wallet, shirt etc.
            case 3:
                {
                    hasPhone = true;
                    phoneOut = false;
                    print("You found your phone!!");
                    break;
                    //Display message to user saying they found phone here

                }
        }
    }

    public void useItem(Item item, bool fromHand)
    {
        switch (item.itemID)
        {
            //Add cases for each item ID that has an effect when consumed.
            case 0:
                {
                    print("Drunk dat drink");
                    //Add stat increases here
                    break;
                }
            case 1:
                {
                    print("Drunk dat slush");
                    //Add stat increases here
                    break;
                }
            case 2:
                {
                    print("Where did you get this?");
                    //Add stat increases here
                    break;
                }
        }

        //When the item is used, and is 'Destroyed when used', destroys the item
        if (item.destroyWhenUsed == true)
        {
            if (fromHand == true)
            {
                Destroy(GameObject.Find("FPPCamera").GetComponent<PickupDrop>().itemInHand.gameObject);
                itemHolding = -1;
            }
            else
            {
                updateItems(item.itemID, false);
            }
        }
    }
}
