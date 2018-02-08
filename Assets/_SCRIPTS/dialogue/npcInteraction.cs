using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcInteraction : MonoBehaviour
{
    //stores the range at which the npc can interact with the player
    public float npcRange;
    public GUISkin skin;

    private bool showGUI = false;

    // Use this for initialization
    void Start ()
    {	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, npcRange) && hit.transform.tag == "npc")    //checks that the player is near an npc when trying to interact
            {
                print("YOU TOUCHED THE NPC!");
                showGUI = true;
            }
        }
    }

    void OnGUI()
    {
        GUI.skin = skin; //Sets the GUI skin to the skin configurated in the inspector.

        if (showGUI) {
            GUI.Box(new Rect(0, 100, Screen.width, 100), "<color=#ffffff>Snacks</color>\n", skin.GetStyle("Label"));
        }
    }

    void drawQuad(Rect position, Color color) {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }
}