using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPhone : MonoBehaviour {
	// Use this for initialization
	void Start () {
        GameObject phone = Instantiate(Resources.Load("Phone"), Vector3.zero, Quaternion.identity) as GameObject;
    }
}
