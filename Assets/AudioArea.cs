using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioArea : MonoBehaviour {

    public AudioClip song;

    [SerializeField]
    AudioSource source;

	// Use this for initialization
	void Start () {
		if(source == null)
        {
            source = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger");
        if (other.gameObject.name == "Character")
        {
            source.clip = song;
            source.Play();
            source.loop = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Character")
        {
            source.clip = song;
        }
    }
}
