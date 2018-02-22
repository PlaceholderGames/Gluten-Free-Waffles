using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject MenuSystem;
    public GameObject character;

    Camera cam;

    PlayerController pc;
    Behaviour GUICrosshair;
    CamMouseLook cml;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        pc = character.GetComponent<PlayerController>();
        cml = character.GetComponentInChildren<CamMouseLook>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("Key Pressed");
            if (!MenuSystem.activeSelf)
            {
                
                Cursor.lockState = CursorLockMode.None;
                cml.enabled = false;
                pc.enabled = false;
                MenuSystem.SetActive(true);
                Time.timeScale = 0.00001f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                MenuSystem.SetActive(false);
                cml.enabled = true;
                pc.enabled = true;
                Time.timeScale = 1.0f;
            }
        }
    }
}
