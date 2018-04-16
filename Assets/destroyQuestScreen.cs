using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyQuestScreen : MonoBehaviour {
    [SerializeField]
    GameManager gameManager;

    PlayerController pc;
    CamMouseLook cml;
    // Use this for initialization
    void Start () {
        if (gameManager == null)
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pc = gameManager.character.GetComponent<PlayerController>();
        cml = gameManager.character.GetComponentInChildren<CamMouseLook>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void removeQuestScreenCanvas()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
        cml.enabled = true;
        pc.enabled = true;
        Time.timeScale = 1.0f;
    }
}
