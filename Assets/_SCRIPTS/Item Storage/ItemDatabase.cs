using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Awake()        //Awake used to fix execution order issue.
    {
        //Add all items in the game here. Format:
        //"Name",ID,"Description",Item.ItemType.ENTERITEMTYPE,destroyWhenUsed, quantity stored in inventory already (should be 1)
        //LIST OF SPECIFIERS: SNACKS, BEVERAGES, TOP, BOTTOM, SHOES, KEY
        items.Add(new Item("Energy Drink", 0, "Increases your speed for a short amount of time.", Item.ItemType.Drink, true, 1));
        items.Add(new Item("Energy Drink - Frozen", 1, "Frozen. Increases your speed for a short amount of time.", Item.ItemType.Drink, true, 1));
        items.Add(new Item("Police Hat", 2, "Definitely should not have this.", Item.ItemType.Drink, false, 1));
        items.Add(new Item("Mobile Phone", 3, "Mobile Phone", Item.ItemType.Drink, false, 1));
    }

}
