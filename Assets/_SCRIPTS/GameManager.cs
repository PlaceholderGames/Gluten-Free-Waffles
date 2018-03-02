using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject MenuSystem;
    public GameObject character;
    
    PlayerController pc;
    CamMouseLook cml;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
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
    }
}
