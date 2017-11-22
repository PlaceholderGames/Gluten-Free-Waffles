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
    public int itemQuantity;
    public ItemType itemType;
    public string specifier;


    public enum ItemType
    {
        Consumable,
        KeyItem,
        Collectable,
        Holdable,
        Equipable
    }

    public Item()
    {
        //Used to create empty inventory slots.
    }

    public Item(string name, int id, string desc, int quant, ItemType type, string specType) //If equipable or consumable effects are added, create new constructor.
    {
        itemName = name;
        itemID = id;
        itemDesc = desc;
        itemQuantity = quant;
        itemType = type;
        itemIcon = Resources.Load<Texture2D>("ItemIcons/" + itemName);
        specifier = specType;
    }
}
