using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private ItemDatabase database;

    public GUISkin skin;

    public List<Item> inventory = new List<Item>();
    public List<Item> SNACKS = new List<Item>();
    public List<Item> BEVERAGES = new List<Item>();
    public List<Item> KEY = new List<Item>();
    public List<Item> HOLD = new List<Item>();
    public List<Item> TOP = new List<Item>();
    public List<Item> BOTTOM = new List<Item>();
    public List<Item> SHOES = new List<Item>();

    private int itemHolding = -1;        //IDs start at 0. -1 indicated no item present.

    private bool showInventory;
    private bool showStats;
    private string stats;
    private int selectedPage = 0;

    public void setItemHolding(int i)
    {
        itemHolding = i;
    }

    void Start()
    {
        
        int maxSnacks = 10;
        int maxBevs = 10;
        int maxKey = 20;
        int maxHold = 20;
        int maxTop = 10;
        int maxBottom = 10;
        int maxShoes = 10;

        int totalInv = maxSnacks + maxBevs + maxKey + maxHold + maxTop + maxBottom + maxShoes;

        Populate(inventory, totalInv);             //This number is the max number of items across the whole inventory that the player can hold.

        Populate(SNACKS, maxSnacks);              //Initialises lists for slot creation
        Populate(BEVERAGES, maxBevs);
        Populate(KEY, maxKey);
        Populate(HOLD, maxHold);
        Populate(TOP, maxTop);
        Populate(BOTTOM, maxBottom);
        Populate(SHOES, maxShoes);

        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        /*
        //Pass the ID of the item that is picked up.
        addItem(0);
        addItem(1);
        addItem(2);

        updateSections();
        */
    }

    void Populate(List<Item> section, int amount)
    {
        for (int i = 0; i < amount; i++)
            section.Add(new Item());
    }

    void updateSections()
    {
        int snack = 0;
        int bev = 0;
        int key = 0;
        int hold = 0;
        int top = 0;
        int bottom = 0;
        int shoe = 0;

        for (int i = 0; i < inventory.Count; i++)
        {
            switch(inventory[i].specifier)
            {
                case "SNACKS":
                    {
                        SNACKS[snack] = inventory[i];
                        snack++;
                        break;
                    }
                case "BEVERAGES":
                    {
                        BEVERAGES[bev] = inventory[i];
                        bev++;
                        break;
                    }
                case "KEY":
                    {
                        KEY[key] = inventory[i];
                        key++;
                        break;
                    }
                case "HOLD":
                    {
                        HOLD[hold] = inventory[i];
                        hold++;
                        break;
                    }
                case "TOP":
                    {
                        TOP[top] = inventory[i];
                        top++;
                        break;
                    }
                case "BOTTOM":
                    {
                        BOTTOM[bottom] = inventory[i];
                        bottom++;
                        break;
                    }
                case "SHOES":
                    {
                        SHOES[shoe] = inventory[i];
                        shoe++;
                        break;
                    }

            }
        }

    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))       //When the i button is pressed, shows the inventory. Uses trigger in editor.
            showInventory = !showInventory;

        showStats = false;            //Fixes issue where tooltip remains on screen if inventory is closed while tt is open.
        
        if (itemHolding != -1)
        {
            if (Input.GetButtonDown("Store"))
            {
                addItem(itemHolding);
                updateSections();
                itemHolding = -1;
            }
        }
    }


    void OnGUI()
    {
        stats = "";               //Clears tooltip data.
        GUI.skin = skin;            //Sets the GUI skin to the skin configurated in the inspector.

        if (showInventory)
        {
            DrawInventory();
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
        
        if (showStats)        //Draws the tooltip if the mouse is hovering over it.
            GUI.Box(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 150, 100), stats, skin.GetStyle("Stats"));
    }

    void DrawInventory()
    {
        int w = 500;                //Width and height of inventory background.
        int h = 500;
        int boxWidth = (Screen.width - w) / 2;
        int boxHeight = (Screen.height - h) / 2;

        int buttonWidth = 50;       //Tab dimentions.
        int buttonLength = 250;


        Event currentEvent = Event.current;

        //Draws tabs and updates selected page based on tab selection.
        if (GUI.Button(new Rect((Screen.width / 2) - (buttonLength / 2), (Screen.height / 2) + (h / 2), buttonLength, buttonWidth), "", skin.GetStyle("Home")))
            selectedPage = 0;
        if (GUI.Button(new Rect(boxWidth - buttonWidth, boxHeight, buttonWidth, buttonLength),"", skin.GetStyle("Consumable")))
            selectedPage = 1;
        if (GUI.Button(new Rect(boxWidth - buttonWidth, boxHeight + (h - buttonLength), buttonWidth, buttonLength), "", skin.GetStyle("Key")))
            selectedPage = 2;
        if (GUI.Button(new Rect(boxWidth + w, boxHeight, buttonWidth, buttonLength),"", skin.GetStyle("Holdable")))
            selectedPage = 3;                                          
        if (GUI.Button(new Rect(boxWidth + w, boxHeight + (h - buttonLength), buttonWidth, buttonLength), "", skin.GetStyle("Equipable")))
            selectedPage = 4;


        switch (selectedPage)
        {
            case 0: //Home
                {
                    Rect background = new Rect(boxWidth, boxHeight, w, h);          //Draws the main inventory screen.
                    GUI.Box(background, "", skin.GetStyle("Main"));
                    break;
                }
            case 1: //Consumables
                {

                    Rect background = new Rect(boxWidth, boxHeight, w, h);          //Draws the background for the tab
                    GUI.Box(background, "", skin.GetStyle("Consumable Background"));

                    GUI.Box(new Rect(boxWidth, boxHeight + 15, w, buttonWidth), "<color=#ffffff>Snacks</color>\n", skin.GetStyle("Label"));
                    GUI.Box(new Rect(boxWidth, boxHeight + 240, w, buttonWidth), "<color=#ffffff>Beverages</color>\n", skin.GetStyle("Label"));

                    boxHeight += 45;        //Pushes the boxes down
                    int snackIDX = 0;
                    int bevIDX = 0;

                    for (int y = 0; y < 4; y++)
                    {
                        for (int x = 0; x < 5; x++)
                        {
                            int xspacing = 95;
                            int yspacing = 95;
                            int slotSize = 90;           //Variables for slot creation
                            int padding = 15;

                            if (y == 2)
                                yspacing += 20;
                            if (y == 3)
                                yspacing += 13;

                            Rect slotRect = new Rect(x * xspacing + boxWidth + padding, y * yspacing + boxHeight + padding, slotSize, slotSize);
                            GUI.Box(slotRect, "", skin.GetStyle("Slot"));           //Draws current slot on the screen.

                            if (y < 2)
                            {

                                Item item = SNACKS[snackIDX];

                                if (SNACKS[snackIDX].itemName != null && SNACKS[snackIDX].itemType == Item.ItemType.Consumable)
                                {
                                    GUI.DrawTexture(slotRect, item.itemIcon);
                                    if (slotRect.Contains(currentEvent.mousePosition))
                                    {
                                        loadStats(item);
                                        showStats = true;

                                        if (currentEvent.isMouse && currentEvent.type == EventType.mouseDown && currentEvent.button == 0)
                                        {
                                            if (item.itemType == Item.ItemType.Consumable)
                                                useConsumable(item, snackIDX, true);
                                        }
                                    }
                                }
                                snackIDX++;
                            }
                            else
                            {
                                Item item = BEVERAGES[bevIDX];

                                if (BEVERAGES[bevIDX].itemName != null && BEVERAGES[bevIDX].itemType == Item.ItemType.Consumable)
                                {
                                    GUI.DrawTexture(slotRect, item.itemIcon);
                                    if (slotRect.Contains(currentEvent.mousePosition))
                                    {
                                        loadStats(item);
                                        showStats = true;

                                        if (currentEvent.isMouse && currentEvent.type == EventType.mouseDown && currentEvent.button == 0)
                                        {
                                            if (item.itemType == Item.ItemType.Consumable)
                                                useConsumable(item, bevIDX, true);
                                        }
                                    }
                                }
                                bevIDX++;
                            }

                            if (stats == "")
                                showStats = false;

                        }
                    }
                    break;
                }
            case 2: //Key
                {
                    Rect background = new Rect(boxWidth, boxHeight, w, h);          //Draws the background for the tab
                    GUI.Box(background, "", skin.GetStyle("Key Background"));

                    GUI.Box(new Rect(boxWidth, boxHeight + 13, w, buttonWidth), "<color=#ffffff>Key Items</color>\n", skin.GetStyle("Label"));

                    boxHeight += 65;        //Pushes the boxes down
                    int idx = 0;

                    for (int y = 0; y < 4; y++)
                    {
                        for (int x = 0; x < 5; x++)
                        {
                            int xspacing = 95;
                            int yspacing = 95;
                            int slotSize = 90;           //Variables for slot creation
                            int padding = 15;

                            Rect slotRect = new Rect(x * xspacing + boxWidth + padding, y * yspacing + boxHeight + padding, slotSize, slotSize);
                            GUI.Box(slotRect, "", skin.GetStyle("Slot"));           //Draws current slot on the screen.

                            Item item = KEY[idx];

                            if (KEY[idx].itemName != null && KEY[idx].itemType == Item.ItemType.KeyItem)
                            {
                                GUI.DrawTexture(slotRect, item.itemIcon);
                                if (slotRect.Contains(currentEvent.mousePosition))
                                {
                                    loadStats(item);
                                    showStats = true;
                                }
                            }
                            idx++;
                        }

                        if (stats == "")
                            showStats = false;
                    }
                    break;
                }
            case 3: //Holdable
                {
                    Rect background = new Rect(boxWidth, boxHeight, w, h);          //Draws the background for the tab
                    GUI.Box(background, "", skin.GetStyle("Holdable Background"));

                    GUI.Box(new Rect(boxWidth, boxHeight + 13, w, buttonWidth), "<color=#ffffff>Holdable Items</color>\n", skin.GetStyle("Label"));

                    boxHeight += 65;        //Pushes the boxes down
                    int idx = 0;

                    for (int y = 0; y < 3; y++)
                    {
                        for (int x = 0; x < 5; x++)
                        {
                            int xspacing = 95;
                            int yspacing = 95;
                            int slotSize = 90;           //Variables for slot creation
                            int padding = 15;

                            Rect slotRect = new Rect(x * xspacing + boxWidth + padding, y * yspacing + boxHeight + padding, slotSize, slotSize);
                            GUI.Box(slotRect, "", skin.GetStyle("Slot"));           //Draws current slot on the screen.

                            Item item = HOLD[idx];

                            if (HOLD[idx].itemName != null && HOLD[idx].itemType == Item.ItemType.Holdable)
                            {
                                GUI.DrawTexture(slotRect, item.itemIcon);
                                if (slotRect.Contains(currentEvent.mousePosition))
                                {
                                    loadStats(item);
                                    showStats = true;
                                }
                            }
                            idx++;
                        }

                        if (stats == "")
                            showStats = false;
                    }
                    break;
                }
            case 4: //Equipable
                {
                    Rect background = new Rect(boxWidth, boxHeight, w, h);          //Draws the background for the tab
                    GUI.Box(background, "", skin.GetStyle("Equipable Background"));

                    GUI.Box(new Rect(boxWidth, boxHeight + 15, w, buttonWidth), "<color=#ffffff>Tops</color>\n", skin.GetStyle("Label"));
                    GUI.Box(new Rect(boxWidth, boxHeight + 165, w, buttonWidth), "<color=#ffffff>Bottoms</color>\n", skin.GetStyle("Label"));
                    GUI.Box(new Rect(boxWidth, boxHeight + 315, w, buttonWidth), "<color=#ffffff>Shoes</color>\n", skin.GetStyle("Label"));

                    boxHeight += 45;        //Pushes the boxes down
                    int topIDX = 0;
                    int bottomIDX = 0;
                    int shoeIDX = 0;

                    for (int y = 0; y < 6; y++)
                    {
                        for (int x = 0; x < 5; x++)
                        {
                            int xspacing = 55;
                            int yspacing = 55;
                            int slotSize = 50;           //Variables for slot creation
                            int padding = 15;

                            if (y == 2)
                                yspacing += 20;
                            if (y == 3)
                                yspacing += 13;
                            if (y == 4)
                                yspacing += 20;
                            if (y == 5)
                                yspacing += 16;

                            Rect slotRect = new Rect(x * xspacing + boxWidth + padding, y * yspacing + boxHeight + padding, slotSize, slotSize);
                            GUI.Box(slotRect, "", skin.GetStyle("Slot"));           //Draws current slot on the screen.

                            if (y < 2)
                            {

                                Item item = TOP[topIDX];

                                if (TOP[topIDX].itemName != null && TOP[topIDX].itemType == Item.ItemType.Equipable)
                                {
                                    GUI.DrawTexture(slotRect, item.itemIcon);
                                    if (slotRect.Contains(currentEvent.mousePosition))
                                    {
                                        loadStats(item);
                                        showStats = true;
                                    }
                                }
                                topIDX++;
                            }
                            if (y >= 2 && y < 4)
                            {

                                Item item = BOTTOM[bottomIDX];

                                if (BOTTOM[bottomIDX].itemName != null && BOTTOM[bottomIDX].itemType == Item.ItemType.Equipable)
                                {
                                    GUI.DrawTexture(slotRect, item.itemIcon);
                                    if (slotRect.Contains(currentEvent.mousePosition))
                                    {
                                        loadStats(item);
                                        showStats = true;
                                    }
                                }
                                bottomIDX++;
                            }
                            if (y >= 4 && y < 6)
                            {
                                Item item = SHOES[shoeIDX];

                                if (SHOES[shoeIDX].itemName != null && SHOES[shoeIDX].itemType == Item.ItemType.Equipable)
                                {
                                    GUI.DrawTexture(slotRect, item.itemIcon);
                                    if (slotRect.Contains(currentEvent.mousePosition))
                                    {
                                        loadStats(item);
                                        showStats = true;
                                    }
                                }
                                shoeIDX++;
                            }


                            if (stats == "")
                                showStats = false;

                        }
                    }
                    break;
                }
        }
    }

    string loadStats(Item item)
    {
        string green = "<color=#aed581>";
        string yellow = "<color=#e8f22e>";
        string lightBlue = "<color=#189ea5>";
        string red = "<color=#fc4961>";
        string closeColour = "</color>\n";
        string colourUsed = "";

        string type = item.itemType.ToString();
        stats = "<color=#ffffff>" + item.itemName + closeColour;
        stats += "<color=#bababa>" + item.itemDesc + closeColour;
        switch(type)
        {
            case "Consumable":
                {
                    colourUsed = green;
                    break;
                }
            case "KeyItem":
                {
                    colourUsed = yellow;
                    break;
                }
            case "Holdable":
                {
                    colourUsed = lightBlue;
                    break;
                }
            case "Equipable":
                {
                    colourUsed = red;
                    break;
                }
        }
        stats += colourUsed + type + closeColour;        
        return stats;
    }

   
    public void addItem(int id)
    {
        bool isFull = false;
        int consumableCount = 0;
        int keyItemCount = 0;
        int holdableCount = 0;
        int equipableCount = 0;
        string type = database.items[id].itemType.ToString();
        int idx = 0;
        while (idx < inventory.Count)
        {
            if(inventory[idx].itemName != null)
            {
                string currentType = inventory[idx].itemType.ToString();
                switch (currentType)
                {
                    case "Consumable":
                        {
                            consumableCount++;
                            break;
                        }
                    case "KeyItem":
                        {
                            keyItemCount++;
                            break;
                        }
                    case "Holdable":
                        {
                            holdableCount++;
                            break;
                        }
                    case "Equipable":
                        {
                            equipableCount++;
                            break;
                        }
                }
            }
            idx++;
        }

        switch(type)
        {
            case "Consumable":
                {
                    consumableCount++;
                    if (consumableCount >= 10)
                    {
                        isFull = true;
                        //return isFull;
                    }
                    break;
                }
            case "KeyItem":
                {
                    keyItemCount++;
                    if (keyItemCount >= 10)
                    {
                        isFull = true;
                        //return isFull;
                    }
                    break;  
                }
            case "Holdable":
                {
                    holdableCount++;
                    if (holdableCount >= 10)
                    {
                        isFull = true;
                        //return isFull;
                    }
                    break;  
                }
            case "Equipable":
                {
                    equipableCount++;
                    if (equipableCount >= 10)
                    {
                        isFull = true;
                        //return isFull;
                    }
                    break;  
                }
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemName == null)
            {
                for(int j = 0; j < database.items.Count; j++)
                {
                    if (database.items[j].itemID == id)
                    {
                        inventory[i] = database.items[j];
                    }    
                }
                break;
            }
            if (inventory[i].itemID == id)
            {
                inventory[i].itemQuantity++;
                
            }
        }
        //return isFull;
    }

    private void useConsumable(Item item, int slot, bool deleteItem)
    {
        switch (item.itemID)
        {
            case 0:
                {
                    print("Drunk dat drink");
                    //Add stat increases here
                    break;
                }
            case 1:
                {
                    print("Ate dat slush");
                    break;
                }
        }

        if (deleteItem)
        {
            for(int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].itemID == item.itemID)
                {
                    inventory[i] = new Item();
                }
            }
            switch(item.specifier.ToString())
            {
                case "SNACKS":
                    {
                        SNACKS[slot] = new Item();
                        break;
                    }
                case "BEVERAGES":
                    {
                        BEVERAGES[slot] = new Item();
                        break;
                    }
            }
        }
    }
}
