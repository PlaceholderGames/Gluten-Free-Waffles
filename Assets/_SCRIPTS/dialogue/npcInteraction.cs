using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcInteraction : MonoBehaviour
{
    //stores the range at which the npc can interact with the player
    public float npcRange;
    public string inputText;

    public GUIStyle dialogueStyle;

    private bool GUIShowing = false;
    private Transform player;
    private GameObject dialogueCanvas;

    // Use this for initialization
    void Start ()
    {
        dialogueCanvas = transform.GetChild(1).gameObject;
        player = GameObject.Find("Character").transform; //Stores a reference to the actual player

        dialogueCanvas.SetActive(GUIShowing);

        dialogueStyle.alignment = TextAnchor.MiddleCenter;
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

                toggleGUI();
            }
        }

        //Updates the speech bubble position so it always faces the player
        dialogueCanvas.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, player.position - transform.position, 10.0f, 0.0f));
        dialogueCanvas.transform.rotation = Quaternion.Euler(0.0f, dialogueCanvas.transform.eulerAngles.y + 180, dialogueCanvas.transform.eulerAngles.z);
    }

    void OnGUI()
    {
        dialogueCanvas.GetComponent<TextMesh>().text = inputText;

        GUI.skin.box = dialogueStyle;

        GUILayout.Box("THIS IS A BOX");
        //GUI.Box(new Rect(0, Screen.height -100, Screen.width, 100), "This is a box", dialogueStyle);

    }

    void toggleGUI() {
        GUIShowing = !GUIShowing;

        dialogueCanvas.SetActive(GUIShowing);
    }
}