using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePhone : MonoBehaviour {

    public Transform player;
    public GameObject inventory;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Phone"))
        {
            inventory.GetComponent<Inventory>().setPhoneOut(false);
            Destroy(GameObject.FindGameObjectWithTag("Phone"));
        }

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.8f, Screen.height * 0.1f, 3));
        transform.position = worldPoint;
        Quaternion playerRotation = player.transform.rotation;
        transform.rotation = playerRotation;
	}
}
