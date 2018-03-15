using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockedOut : MonoBehaviour
{
    private Texture2D fade;

    // Use this for initialization
    public void Start ()
    {
        Debug.Log("hello there");
        //Load the fade overlay texture
        fade = Resources.Load("Textures/fade", typeof(Texture2D)) as Texture2D;
    }
	
	// Update is called once per frame
	private void Update ()
    {
		
	}

    private void OnGUI() {
        int w = Screen.width, h = Screen.height;
        int halfW = w / 2, halfH = h / 2;

        //draws the fade overlay on the screen
        GUI.DrawTexture(new Rect(halfW, halfH, Screen.width, fade.height), fade, ScaleMode.StretchToFill, true, 0.0f);
    }
}
