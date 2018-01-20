using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Awake()        //Awake used to fix execution order issue.
    {
        //Add all items in the game here. Format:
        //"Name",ID,"Description",Quantity,Item.ItemType.ENTERITEMTYPE,specifier,destroyWhenUsed
        //LIST OF SPECIFIERS: SNACKS, BEVERAGES, TOP, BOTTOM, SHOES, KEY
        items.Add(new Item("Energy Drink", 0, "Increases your speed for a short amount of time.", 0, Item.ItemType.Consumable, "BEVERAGES", true));
        items.Add(new Item("Energy Drink", 1, "Frozen. Increases your speed for a short amount of time.", 0, Item.ItemType.Consumable, "SNACKS", true));
        items.Add(new Item("Police Hat", 2, "Definitely should not have this.", 0, Item.ItemType.KeyItem, "KEY", false));
        items.Add(new Item("Mobile Phone", 3, "Mobile Phone", 0, Item.ItemType.Collectable, "PHONE", false));
    }

}
