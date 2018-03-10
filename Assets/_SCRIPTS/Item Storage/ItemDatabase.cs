﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Awake()        //Awake used to fix execution order issue.
    {
        //Add all items in the game here
        items.Add(new Item(name: "Energy Drink", id: 0, desc: "Increases your speed for a short amount of time.", type: Item.ItemType.Drink, destroy: true, quantity: 0, cost: 100));
        items.Add(new Item(name: "Energy Drink - Frozen", id: 1, desc: "Frozen. Increases your speed for a short amount of time.", type: Item.ItemType.Drink, destroy: true, quantity: 0, cost: 100));
        items.Add(new Item(name: "Police Hat", id: 2, desc: "Definitely should not have this.", type: Item.ItemType.Drink, destroy: false, quantity: 0, cost: 100));
        items.Add(new Item(name: "Mobile Phone", id: 3, desc: "Mobile Phone", type: Item.ItemType.Drink, destroy: false, quantity: 0, cost: 100));
    }

}
