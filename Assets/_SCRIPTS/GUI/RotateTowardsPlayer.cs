using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour {

    Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Character").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(player != null)
        {
            transform.LookAt(player.position);
        }
	}
}
