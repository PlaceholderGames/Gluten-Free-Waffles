using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalApp : MonoBehaviour {

    private GameObject phone;
    private bool backgroundUpdate = true;
    public Material backgroundList;
    public Material backgroundFull;
    public Material homescreen;

    // Use this for initialization
    void Start()
    {
        phone = GameObject.FindGameObjectWithTag("Phone").transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the background if it is still the home screen.
        if (backgroundUpdate == true)
        {
            phone.transform.GetChild(0).GetComponent<MeshRenderer>().material = backgroundList;
            backgroundUpdate = false;
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Backspace))
        {
            close();
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
