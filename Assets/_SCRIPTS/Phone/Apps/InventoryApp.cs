using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryApp : MonoBehaviour {

    public Material background;
    public Material homescreen;
    public Inventory inventory;
    public Font normal;
    public Font bold;

    private GameObject phone;
    private bool backgroundUpdate = true;
    private int categorySelection = 0;
    private int oldCategorySelection = 1;
    private int itemSelection = 0;
    private int oldItemSelection = 1;


    // Use this for initialization
    void Start () {
        phone = GameObject.FindGameObjectWithTag("Phone");
    }
	
	// Update is called once per frame
	void Update () {

        //Updates the background if it is still the home screen.
        if (backgroundUpdate == true)
        {
            phone.transform.GetChild(0).GetComponent<MeshRenderer>().material = background;
            backgroundUpdate = false;
        }

        //Closes the app
        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Backspace))
        {
            close();
        }

        //Gets user navigation
        navigateCategory();
    }

    void navigateCategory()
    {

        //Updates selection based on user input
        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.LeftArrow))
            categorySelection--;
        if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.RightArrow))
            categorySelection++;

        //Ensures the user doesn't exceed number of categories
        if (categorySelection < 0)
            categorySelection = 4;
        if (categorySelection > 4)
            categorySelection = 0;

        //Updates colour of icon and title to represent selection
        if (oldCategorySelection != categorySelection)
        {
            transform.GetChild(oldCategorySelection).gameObject.GetComponent<RawImage>().color = Color.white;
            transform.GetChild(categorySelection).gameObject.GetComponent<RawImage>().color = Color.gray;

            transform.Find("Category").GetComponent<Text>().text = transform.GetChild(categorySelection).name;
        }

        //Loads the current category into the list
        load(categorySelection);
        
        //Resets for colour change to function correctly
        oldCategorySelection = categorySelection;
    }

    void load (int categoryLoaded)
    {
        List<Item> itemList = new List<Item>();
        int counter = 0;

        

        //Determines which list to load from function parameter
        switch (categoryLoaded)
        {
            case 0:
                {
                    itemList = inventory.foodList;
                    break;
                }
            case 1:
                {
                    itemList = inventory.drinkList;
                    break;
                }
            case 2:
                {
                    itemList = inventory.clothesList;
                    break;
                }
            case 3:
                {
                    itemList = inventory.questList;
                    break;
                }
            case 4:
                {
                    itemList = inventory.miscList;
                    break;
                }
        }

        //If the current list isn't empty...
        if (itemList.Count != 0)
        {
            //Moves the list up and down
            if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.UpArrow))
                itemSelection--;
            if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.DownArrow))
                itemSelection++;
        
            //Prevents user from going out of the end of the list
            if (itemSelection < 0)
                itemSelection = itemList.Count - 1;
            if (itemSelection == itemList.Count)
                itemSelection = 0;

            //Updates the item selected with bold text and makes the previous one normal
            if (oldItemSelection != itemSelection)
            {
                transform.Find("Lists").Find("Items").GetChild(oldItemSelection).gameObject.GetComponent<Text>().font = normal;
                transform.Find("Lists").Find("Items").GetChild(itemSelection).gameObject.GetComponent<Text>().font = bold;
            }

            //Updates the item description
            transform.Find("Description").GetComponent<Text>().text = itemList[itemSelection].itemDesc;

            transform.Find("Item Icon").gameObject.SetActive(true);
            transform.Find("Item Icon").GetComponent<RawImage>().texture = Resources.Load<Texture2D>("ItemIcons/" + itemList[itemSelection].itemName);

            oldItemSelection = itemSelection;

            while (counter < itemList.Count)
            {
                //Displays items and quantites in current list
                transform.Find("Lists").Find("Items").GetChild(counter).GetComponent<Text>().text = itemList[counter].itemName;
                transform.Find("Lists").Find("Quantities").GetChild(counter).GetComponent<Text>().text = itemList[counter].itemQuantity.ToString();
                counter++;

                //Stops overflow
                if (counter == 12)
                    counter = itemList.Count;
            }

            //Fills unfilled slots with blanks
            while(counter < 12)
            {
                transform.Find("Lists").Find("Items").GetChild(counter).GetComponent<Text>().text = "";
                transform.Find("Lists").Find("Quantities").GetChild(counter).GetComponent<Text>().text = "";
                counter++;
            }

        }

        //Clears the list when it is empty and displays messages to the player
        else
        {
            for (int i = 0; i < 12; i++)
            {
                transform.Find("Lists").Find("Items").GetChild(i).GetComponent<Text>().text = "";
                transform.Find("Lists").Find("Quantities").GetChild(i).GetComponent<Text>().text = "";
            }

            transform.Find("Lists").Find("Items").GetChild(0).GetComponent<Text>().text = "Empty";
            transform.Find("Description").GetComponent<Text>().text = "No item to show. Go find some!";
            transform.Find("Item Icon").gameObject.SetActive(false);
        }
        
        if (Input.GetButtonDown("Use"))
        {
            inventory.GetComponent<Inventory>().useItem(itemList[itemSelection], false);
        }

        if (Input.GetButtonDown("Interact"))
        {
            print("Move item to hand here in pickupdrop");
        }

    }

    void close()
    {
        //Resets phone to home screen to close the app
        phone.transform.GetChild(0).GetComponent<MeshRenderer>().material = homescreen;
        phone.GetComponent<MobilePhone>().appClosed = true;
        backgroundUpdate = true;
        gameObject.SetActive(false);
    }
}
