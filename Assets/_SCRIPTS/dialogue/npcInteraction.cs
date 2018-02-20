using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcInteraction : MonoBehaviour
{
    //stores the range at which the npc can interact with the player
    public float npcRange;

    public string inputText;

    private bool showGUI = false;


    //var font : Font;


    // Use this for initialization
    void Start ()
    {
        var Text = new GameObject();
        var DialogueTextMesh = gameObject.AddComponent<TextMesh>();
       // DialogueTextMesh.font = font;
       // DialogueTextMesh.GetComponent<Renderer>().material = font.material;
        DialogueTextMesh.text = "Hello World!";
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
       // GUI.Label(new Rect(10, Screen.height - 10, 150, 130), "HELLO WORLD!!!!");
    }
}