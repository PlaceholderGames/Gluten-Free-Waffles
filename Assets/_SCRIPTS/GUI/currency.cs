using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currency : MonoBehaviour
{

    public static bool transactionScreen = false;
    public static bool transaction = false;
    public static bool buying = false;
    public GameObject transactionUI;
    public GameObject buyingScreen;

	// Use this for initialization
	void Start ()
    {
        GameObject player = GameObject.Find("Character");
        funds money = player.GetComponent<funds>();
	}
	
	// Update is called once per frame
	void Update ()
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
            }
        }
        if (transaction)
        {
            if (transactionScreen)
            {
                VendorScreen();
            }
            if (buying)
            {
                BuyingPanel();
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
    public void BuyingPanel()
    {
        buying = true;
        transactionScreen = false;
        transactionUI.SetActive(false);
        buyingScreen.SetActive(true);
    }

    public void ReturnToGame()
    {
        transactionUI.SetActive(false);
        transaction = false;
        //re-enabling time and returning cursor
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}
