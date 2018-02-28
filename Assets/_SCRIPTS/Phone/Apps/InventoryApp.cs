using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryApp : MonoBehaviour {

    public Material background;
    public Material homescreen;
    private bool inMenu = false;
    private GameObject phone;
    private bool backgroundUpdate = true;

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
    }

    void close()
    {
        phone.transform.GetChild(0).GetComponent<MeshRenderer>().material = homescreen;
        phone.GetComponent<MobilePhone>().appClosed = true;
        backgroundUpdate = true;
        gameObject.SetActive(false);
    }
}
