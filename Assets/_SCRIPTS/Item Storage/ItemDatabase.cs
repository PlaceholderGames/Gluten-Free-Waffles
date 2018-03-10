using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Awake()        //Awake used to fix execution order issue.
    {
<<<<<<< HEAD
        //Add all items in the game here. Format:
        //"Name",ID,"Description",Quantity,Item.ItemType.ENTERITEMTYPE,specifier,destroyWhenUsed, itemPrice
        //LIST OF SPECIFIERS: SNACKS, BEVERAGES, TOP, BOTTOM, SHOES, KEY
        items.Add(new Item("Energy Drink", 0, "Increases your speed for a short amount of time.", 0, Item.ItemType.Consumable, "BEVERAGES", true, 100));
        items.Add(new Item("Energy Drink", 1, "Frozen. Increases your speed for a short amount of time.", 0, Item.ItemType.Consumable, "SNACKS", true, 100));
        items.Add(new Item("Police Hat", 2, "Definitely should not have this.", 0, Item.ItemType.KeyItem, "KEY", false, 100));
        items.Add(new Item("Mobile Phone", 3, "Mobile Phone", 0, Item.ItemType.Collectable, "PHONE", false, 100));
=======
        //Add all items in the game here
        items.Add(new Item(name: "Energy Drink", id: 0, desc: "Increases your speed for a short amount of time.", type: Item.ItemType.Drink, destroy: true, quantity: 0));
        items.Add(new Item(name: "Energy Drink - Frozen", id: 1, desc: "Frozen. Increases your speed for a short amount of time.", type: Item.ItemType.Drink, destroy: true, quantity: 0));
        items.Add(new Item(name: "Police Hat", id: 2, desc: "Definitely should not have this.", type: Item.ItemType.Drink, destroy: false, quantity: 0));
        items.Add(new Item(name: "Mobile Phone", id: 3, desc: "Mobile Phone", type: Item.ItemType.Drink, destroy: false, quantity: 0));
>>>>>>> master
    }

}
