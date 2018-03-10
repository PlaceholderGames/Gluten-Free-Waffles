using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Item
{
    public string itemName;
    public int itemID;
    public string itemDesc;
    public Texture2D itemIcon;
    public ItemType itemType;
<<<<<<< HEAD
    public string specifier;
    bool destroyWhenUsed;
    public int price;
=======
    public bool destroyWhenUsed;
    public int itemQuantity;

>>>>>>> master

    public enum ItemType
    {
        Food,
        Drink,
        Clothes,
        Quest,
        Misc
    }

    public Item()
    {
        //Used to create empty inventory slots.
    }

<<<<<<< HEAD
    public Item(string name, int id, string desc, int quant, ItemType type, string specType, bool destroy, int cost) //If equipable or consumable effects are added, create new constructor.
=======
    public Item(string name, int id, string desc, ItemType type, bool destroy, int quantity) //If equipable or consumable effects are added, create new constructor.
>>>>>>> master
    {
        itemName = name;
        itemID = id;
        itemDesc = desc;
        itemType = type;
        itemIcon = Resources.Load<Texture2D>("ItemIcons/" + itemName);
        destroyWhenUsed = destroy;
<<<<<<< HEAD
        price = cost;
=======
        itemQuantity = quantity;
>>>>>>> master
    }

    public bool getDestroy()
    {
        return destroyWhenUsed;
    }
}
