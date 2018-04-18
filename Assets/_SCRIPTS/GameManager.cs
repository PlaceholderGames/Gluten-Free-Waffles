using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject MenuSystem;
    public GameObject character;
    public GameObject currency;
    
    PlayerController pc;
    CamMouseLook cml;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        pc = character.GetComponent<PlayerController>();
        cml = character.GetComponentInChildren<CamMouseLook>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            if (!MenuSystem.activeSelf)
            {
                
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
        if(Input.GetButtonDown("Interact"))
        {
            //turning on shop
            if (!currency.activeSelf)
            {
                //checking if a vendor was selected
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 5) && hit.transform.tag == "vendor")
                {
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
        if (currency.GetComponent<currency>().close)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            currency.SetActive(false);
            cml.enabled = true;
            pc.enabled = true;
            Time.timeScale = 1.0f;
            currency.GetComponent<currency>().resetAllBools();
        }
    }
}
