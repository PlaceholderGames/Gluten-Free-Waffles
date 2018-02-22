using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueGUI : MonoBehaviour
{
    //GUIStyle dialogueStyle = new GUIStyle();

    public GUIStyle dialogueStyle;

    // Use this for initialization
    void Start ()
    {
        //dialogueStyle.normal.background = MakeText(2, 2, new Color(0f, 1f, 0f, 0.5f));
        // style.alignment = TextAnchor.LowerLeft;

        //dialogueStyle.Co
        dialogueStyle.alignment = TextAnchor.MiddleCenter;

    }

    void OnGUI()
    {
        GUI.skin.box = dialogueStyle;

        GUILayout.Box("THIS IS A BOX");
        //GUI.Box(new Rect(0, Screen.height -100, Screen.width, 100), "This is a box", dialogueStyle);
    }
	
	// Update is called once per frame
	void Update ()
    {	
	}
}
