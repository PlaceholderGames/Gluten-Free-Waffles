using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pee : MonoBehaviour
{
    public float changeBladderSpeed = 30.0f; //how fast the bladder var will fill up in seconds

    private GameObject player;
    private Vitals vitals;
    private CamMouseLook camMouseLook;

    // Use this for initialization
    void Start ()
    {
        //get some components & objects
        player = GameObject.Find("Character");
        vitals = player.GetComponent<Vitals>();
        camMouseLook = player.transform.Find("FPPCamera").GetComponent<CamMouseLook>();

        vitals.setBladderState(true);

        //set the initial position to the player position
        transform.position = player.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //each frame ensure the pee position is on the player
        transform.position = player.transform.position;
        transform.localRotation = camMouseLook.transform.localRotation;
       // transform.localRotation.eulerAngles.Set(0.0f, camMouseLook.transform.localRotation.y, 0.0f);
    }
}
