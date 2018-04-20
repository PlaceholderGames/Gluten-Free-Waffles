using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject MenuSystem;
    public GameObject character;
    public GameObject currency;

    private bool menuOpen = false;
    
    PlayerController pc;
    CamMouseLook cml;
    public 
	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        pc = character.GetComponent<PlayerController>();
        cml = character.GetComponentInChildren<CamMouseLook>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause") && !menuOpen)
        {
            if (!MenuSystem.activeSelf)
            {
                menuOpen = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                cml.enabled = false;
                pc.enabled = false;
                MenuSystem.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                MenuSystem.SetActive(false);
                cml.enabled = true;
                pc.enabled = true;
                Time.timeScale = 1.0f;
            }
        }

        if(menuOpen && MenuSystem.activeSelf)
        {
            menuOpen = false;
        }

        if(Input.GetButtonDown("Interact") && !menuOpen)
        {
            //turning on shop
            if (!currency.activeSelf)
            {
                //checking if a vendor was selected
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 5) && hit.transform.tag == "vendor")
                {
                    menuOpen = true;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    cml.enabled = false;
                    pc.enabled = false;
                    currency.SetActive(true);
                    Time.timeScale = 0f;
                    currency.GetComponent<currency>().readVendor(hit);
                }
            }      
        }
        if (currency.GetComponent<currency>().close && menuOpen)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            currency.SetActive(false);
            cml.enabled = true;
            pc.enabled = true;
            Time.timeScale = 1.0f;
            currency.GetComponent<currency>().resetAllBools();
            menuOpen = false;
        }
    }

    public bool ControllerCheck()
    {
        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();

        //Check whether array contains anything
        if (temp.Length > 0)
        {
            //Iterate over every element
            for (int i = 0; i < temp.Length; ++i)
            {
                //Check if the string is empty or not
                if (!string.IsNullOrEmpty(temp[i]))
                {
                    //Not empty, controller temp[i] is connected
                    return true;
                }
            }
        }
        return false;
    }
}
