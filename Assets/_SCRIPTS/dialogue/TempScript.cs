using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScript : MonoBehaviour {

    Transform player;
    RectTransform rect;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Character").transform;
        rect = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        rect.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, player.position - transform.position, 10f, 0.0f));
        rect.rotation = Quaternion.Euler(0, rect.eulerAngles.y + 180, rect.eulerAngles.z);

	}
}
