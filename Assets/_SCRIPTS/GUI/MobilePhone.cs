using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePhone : MonoBehaviour {

    public Transform player;
    public Transform playerCamera;

	// Use this for initialization
	void Start () {
        
        transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.84f, Screen.height * 0.12f, 3));
        transform.position = worldPoint;

        transform.Rotate(Vector3.right, -90);
        transform.Rotate(Vector3.up, -110);

        //transform.GetChild(1).transform.GetComponent<Canvas>().transform.
	}
	
	// Update is called once per frame
	void Update () {

	}
}
