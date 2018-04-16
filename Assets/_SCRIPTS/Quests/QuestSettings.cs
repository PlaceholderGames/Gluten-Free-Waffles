using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSettings : MonoBehaviour {

    public Color questColor;

    private float questCompletion = 0;
    private float childrenIndex;

    public string questTitle = "Please give me a title.";

	// Use this for initialization
	void Start () {
        childrenIndex = transform.childCount;
        //sets the color for each quad of the children
        for(int i = 0; i < childrenIndex; i++)
        {
            Transform child = transform.GetChild(i).GetChild(0);
            if(child != null)
            {
                if (child.name == "Quad")
                {
                    Renderer rend = child.GetComponent<Renderer>();
                    rend.material.color = questColor;
                    rend.material.SetColor("_EmissionColor", questColor);
                }
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
	}
}
