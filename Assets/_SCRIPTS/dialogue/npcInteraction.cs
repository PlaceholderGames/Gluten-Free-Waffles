using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcInteraction : MonoBehaviour
{
    //stores the range at which the npc can interact with the player
    public float npcRange;
    public string inputText;

    private bool showGUI = false;

    public GameObject player;

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
        transform.GetChild(1).GetComponent<TextMesh>().text = inputText;
    }
}