using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currency : MonoBehaviour
{

    public static bool transactionScreen = false;
    public static bool transaction = false;
    public static bool buying = false;
    public static bool selling = false;
    public static bool confirm = false;
    //bool to prevent multiply vendor screens from being opened
    public static bool clicked = false;

    public GameObject confirmationScreen;
    public GameObject sellingScreen;
    public GameObject transactionUI;
    public GameObject buyingScreen;
    public Text[] itemButtons;

    //references to other game objects required
    private GameObject player;
    private funds money;
    private Inventory inventory;
    private VendorSupplies vendorSupplies;
    private ItemDatabase database;
    private Item itemRef;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Character");
        money = player.GetComponent<funds>();
        inventory = player.GetComponent<Inventory>();
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!clicked)
        {
            //lmb click
            if (Input.GetMouseButtonDown(0))
            {
                //checking if a vendor was selected
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 5) && hit.transform.tag == "vendor")
                {
                    transactionScreen = true;
                    transaction = true;
                    clicked = true;
                    //getting the supplies
                    vendorSupplies = hit.transform.GetComponent<VendorSupplies>();
                }
            }
        }
        if (transaction)
        {
            if (transactionScreen)
            {
                VendorScreen();
            }
            else if (buying)
            {
                BuyingUI();
            }
            else if (selling)
            {
                SellingUI();
            }
            else if (confirm)
            {
                ConfirmPurchaseUI();
            }
        }
    }

    void VendorScreen()
    {
        //bringing up the ui and pausing the game
        transactionUI.SetActive(true);
        Time.timeScale = 0f;
        //reallowing player to see and use their mouse cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //function to be used with on click for buying button
    public void BuyingUI()
    {
        buying = true;
        transactionScreen = false;
        transactionUI.SetActive(false);
        buyingScreen.SetActive(true);
        //setting the correct text for each of the items in the buttons
        for (int i = 0; i < 4; i++)
        {
            //checking if an item has been set in the vendor supplies
            if (vendorSupplies.supplies[i] > -1)
            {
                itemRef = database.items[vendorSupplies.supplies[i]];
                itemButtons[i].text = itemRef.itemName;
            }
        }
    }

    public void ReturnToGame()
    {
        transactionUI.SetActive(false);
        transaction = false;
        clicked = false;
        //re-enabling time and returning cursor
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void exitBuyingPanel()
    {
        //turning on the initial transaction screen back on
        //also turning off the buying screen
        buyingScreen.SetActive(false);
        transactionUI.SetActive(true);
        //setting transaction back to true so that only the transaction screen is showing
        buying = false;
        transactionScreen = true;
    }

    //SELLING SCREEN FUNCTIONS
    public void SellingUI()
    {
        selling = true;
        transactionScreen = false;
        transactionUI.SetActive(false);
        sellingScreen.SetActive(true);
    }

    public void exitSellingPanel()
    {
        sellingScreen.SetActive(false);
        transactionUI.SetActive(true);
        selling = false;
        transactionScreen = true;
    }

    //FUNCTIONS RELATED TO THE CONFIRMATION OF PURCHASE SCREEN
    public void ConfirmPurchaseUI()
    {
        buying = false;
        confirm = true;
        buyingScreen.SetActive(false);
        confirmationScreen.SetActive(true);
    }

    public void DeclinePurchase()
    {
        buyingScreen.SetActive(true);
        confirmationScreen.SetActive(false);
        confirm = false;
        transactionScreen = false;
        buying = true;     
    }
}