using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour {
    Vector3 fwd = Vector3.zero;
    private bool interact = false;
    
	// Use this for initialization
	void Start () {
        
	}

    private void Update()
    {
    }

    public void OnMouseUp()
    {
        Debug.Log("Mouse Clicked");
        StartGame("PlaceholderCity");
    }

    // Update is called once per frame
    void FixedUpdate () {
        
    }
    
    public void StartGame(string name)
    {
        SceneManager.LoadScene(name);
    }
}
