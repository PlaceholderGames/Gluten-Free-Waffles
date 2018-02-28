using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryApp : MonoBehaviour {

    public Material background;
    public Material homescreen;
    private bool inMenu = false;
    private GameObject phone;
    private bool backgroundUpdate = true;
    private int selection = 0;
    private int oldSelection = 1;

	// Use this for initialization
	void Start () {
        print("Loading Inventory...");
        phone = GameObject.FindGameObjectWithTag("Phone");
    }
	
	// Update is called once per frame
	void Update () {
        if (backgroundUpdate == true)
        {
            phone.transform.GetChild(0).GetComponent<MeshRenderer>().material = background;
            backgroundUpdate = false;
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Backspace))
        {
            close();
        }

        navigate();

    }

    void navigate()
    {

        if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.LeftArrow))
            selection--;
        if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.RightArrow))
            selection++;

        if (selection < 0)
            selection = 4;
        if (selection > 4)
            selection = 0;

        if (oldSelection != selection)
        {
            transform.GetChild(oldSelection).gameObject.GetComponent<RawImage>().color = Color.white;
            transform.GetChild(selection).gameObject.GetComponent<RawImage>().color = Color.gray;

            transform.Find("Category").GetComponent<Text>().text = transform.GetChild(selection).name;
        }

        load(selection);
        oldSelection = selection;
    }

    void load (int selection)
    {

    }

    void close()
    {
        phone.transform.GetChild(0).GetComponent<MeshRenderer>().material = homescreen;
        phone.GetComponent<MobilePhone>().appClosed = true;
        backgroundUpdate = true;
        gameObject.SetActive(false);
    }
}
