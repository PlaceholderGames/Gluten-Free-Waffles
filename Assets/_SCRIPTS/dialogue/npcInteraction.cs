using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class npcInteraction : MonoBehaviour
{
    //stores values to reference against the dialogue database
    public int characterID;

    //stores the range at which the npc can interact with the player
    public float npcRange;

    private bool GUIShowing = false;
    private byte optionSelected = 1;
    private Transform player;

    //stores the various dialogue text that will be displayed
    private string[] dbDialogue = new string[6];
    //Stores the currently active dialogue and response options for the NPC
    private int currentDialogue = 0;

    private GameObject dialoguePopup;
    private GameObject dialogueOverlay;

    // Use this for initialization
    void Start()
    {
        //Stores a reference to the actual player transformation
        player = GameObject.Find("Character").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && !GUIShowing) {
            RaycastHit hit;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //checks that the player is near an npc when trying to interact
            if (Physics.Raycast(ray, out hit, npcRange) && hit.transform.tag == "npc")
            {
                print("OW! YOU TOUCHED ME?!");

                //establish a connection to the database and find the correct data
                dbConnect();

                //now create a GUI to display the data
                spawnGUI();
            }
        }

        //Check if the popup that shows above NPC heads exists
        if (dialoguePopup) {
            //Updates the popup position so it always faces the player
            dialoguePopup.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, player.position - transform.position, 10.0f, 0.0f));
            dialoguePopup.transform.rotation = Quaternion.Euler(0.0f, dialoguePopup.transform.eulerAngles.y + 180, dialoguePopup.transform.eulerAngles.z);

            //checks whether the player has pressed any key to change their option selected and then will update the GUI
            checkSelectedOption();

            //gets the distance between the player and the npc each frame
            float distToPlayer = Vector3.Distance(player.position, transform.position);

            //if the players moves too far away from the npc, remove the dialogue GUI
            if (distToPlayer > (npcRange * 2) && GUIShowing) {
                destroyGUI();
            }

            //check if the player clicked on the mouse which means they confirmed their dialogue option
            //Configured to use the left mouse btn and the return key
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)) {
                print("YOU SELECTED OPTION " + optionSelected);
                dbConnect();
            }
        }
    }

    void spawnGUI()
    {
        GUIShowing = true;

        //Spawns an instance of the dialogue popup above the NPC head and set its text
        dialoguePopup = Instantiate(Resources.Load("DialogueSystem/DialoguePopup"), new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity) as GameObject;
        
        dialoguePopup.GetComponent<TextMesh>().text = dbDialogue[0];

        //spawns an instance of the large GUI popup that allows the player to choose responses
        dialogueOverlay = Instantiate(Resources.Load("DialogueSystem/DialogueOverlay"), Vector3.zero, Quaternion.identity) as GameObject;
        Transform overlay = dialogueOverlay.transform.Find("DialoguePanel");
        overlay.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, 200);
        overlay.position = new Vector2(Screen.width * 0.5f, 0);

        //Find the question text box within the canvas and sets its text
        GameObject question = dialogueOverlay.transform.Find("DialoguePanel/Question").gameObject;
        question.GetComponent<Text>().text = dbDialogue[0];

        //Finds the children of the canvas and sets their text to the correct responses
        for (int i = 1; i <= 3; i++) {
            GameObject option = dialogueOverlay.transform.Find("DialoguePanel/Option" + i).gameObject;
            option.GetComponent<Text>().text = dbDialogue[i];
        }

        //Calls this at the start to highlight a default option
        changeSelectedOption();
    }

    void destroyGUI()
    {
        GUIShowing = false;

        //destory both the popup above the NPC head and the dialogue GUI
        Destroy(dialoguePopup);
        Destroy(dialogueOverlay);
    }

    void checkSelectedOption()
    {
        //checks the inputs to see whether the player has pressed a button to change the currently selected option
        if (Input.GetButtonDown("Dialogue 1")) {
            clearPreviousOption(optionSelected);
            optionSelected = 1;
        } else if (Input.GetButtonDown("Dialogue 2")) {
            clearPreviousOption(optionSelected);
            optionSelected = 2;
        } else if (Input.GetButtonDown("Dialogue 3")) {
            clearPreviousOption(optionSelected);
            optionSelected = 3;
        }

        //Handles the scroll wheel with scrolling up and down
        var scrollValue = Input.GetAxis("Mouse ScrollWheel");
        if (scrollValue > 0f)
        {
            // scroll up
            clearPreviousOption(optionSelected);
            if (optionSelected > 1)
                optionSelected--;
            else
                optionSelected = 3;
        } 
        else if (scrollValue < 0f)
        {
            // scroll down
            clearPreviousOption(optionSelected);
            if (optionSelected < 3)
                optionSelected++;
            else
                optionSelected = 1;
        }

        //Selects the new dialogue option
        changeSelectedOption();
    }

    void clearPreviousOption(byte previousOption)  
    {
        //disables the highlighting for the previously selected option
        GameObject OptionSelected = dialogueOverlay.transform.Find("DialoguePanel/Selected" + previousOption).gameObject;
        OptionSelected.GetComponent<Image>().enabled = false;
    }

    void changeSelectedOption()
    {
        //Finds and then highlists the selected response option that is selected
        GameObject OptionSelected = dialogueOverlay.transform.Find("DialoguePanel/Selected" + optionSelected).gameObject;
        OptionSelected.GetComponent<Image>().enabled = true;
    }

    void dbConnect()
    {
        //Path to database
        string conn = "URI=file:" + Application.dataPath + "/Resources/DialogueSystem/dialogueDB.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection) new SqliteConnection(conn);

        //Open connection to the database
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();

        string sqlQuery = "SELECT D.Dialogue, D.ResponseTop, D.ResponseMiddle, D.ResponseBottom, D.PreviousDialogue, C.Name " + "FROM Dialogue AS D " + "INNER JOIN Character AS C " + "ON D.CharacterID = C.CharacterID " +
                          "WHERE D.CharacterID = " + characterID + " AND D.PreviousDialogue = " + currentDialogue.ToString();


        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read()) {
            dbDialogue[0] = reader.GetString(0);
            dbDialogue[1] = reader.GetString(1);
            dbDialogue[2] = reader.GetString(2);
            dbDialogue[3] = reader.GetString(3);
            dbDialogue[4] = reader.GetInt32(4).ToString();
            dbDialogue[5] = reader.GetString(5);
        }

        //close the connection to the database once done
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        //once the database has been closed, increment the currentDialogue by one
        //currentDialogue++;
    }
}