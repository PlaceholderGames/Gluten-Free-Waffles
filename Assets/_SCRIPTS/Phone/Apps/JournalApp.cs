using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalApp : MonoBehaviour {

    private GameObject phone;
    private bool backgroundUpdate = true;
    public Material backgroundList;
    public Material backgroundFull;
    public Material homescreen;

    int questSelection = 0;
    int oldSelection = 1;
    int pageNumber = 1;

    Transform listPage;
    Transform fullPage;
    Transform completedPage;

    QuestHandler handler;

    // Use this for initialization
    void Start()
    {
        phone = GameObject.FindGameObjectWithTag("Phone").transform.GetChild(0).gameObject;
        listPage = transform.Find("List Screen");
        fullPage = transform.Find("Full Screen Quest");
        handler = GameObject.Find("GameManager").GetComponent<QuestHandler>();
        Clear();
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the background if it is still the home screen.
        if (backgroundUpdate == true)
        {
            phone.transform.GetChild(0).GetComponent<MeshRenderer>().material = backgroundList;
            backgroundUpdate = false;
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Backspace))
        {
            close();
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            OpenFull(questSelection);
        }

        LoadQuests(pageNumber);

    }

    void LoadQuests(int pageNumber)
    {
        listPage.Find("Page Number").GetComponent<TextMesh>().text = pageNumber.ToString();

        List<Quest> quests = handler.getActiveQuestList();

        //Gets the number of quests stored in the quest list
        int numberOfQuests = quests.Count;

        if (numberOfQuests != 0)
        {
            //How many pages of quests are there (+1 as <4 quests needs 1 page not 0)
            int pages = (numberOfQuests / 4) + 1;

            //Which 4 quests should be loaded onto the current page (Will be multiples of 4 starting with 0)
            int questCount = (pageNumber - 1) * 4;

            //Cycles through Quest (1-4) game objects on phone screen 
            for (int i = 1; i < 5; i++)
            {
                //Gets the current game object
                Transform current = listPage.Find("Quest (" + i.ToString() + ")");

                //Reformats colour so that textmesh will display it
                Color colour = quests[questCount].colour;
                Color colourOutput = new Color(colour.r, colour.g, colour.b);

                //Sets heading text and colour
                current.Find("Heading").GetComponent<TextMesh>().text = quests[questCount].title;
                current.Find("Heading").GetComponent<TextMesh>().color = colourOutput;

                //Sets quest giver text
                current.Find("Setter").GetComponent<TextMesh>().text = quests[questCount].giverName;

                //Ensures text doesn't go off the edge of the screen
                string instruction = quests[questCount].directions;
                if (instruction.Length > 23)
                    instruction = instruction.Substring(0, 23) + "...";

                current.Find("Current Instruction").GetComponent<TextMesh>().text = instruction;

                //If there are still quests to be displayed
                if (questCount < numberOfQuests)
                    questCount++;

                //If all the quests have been shown
                if (questCount == numberOfQuests)
                    break;
            }
        }
        else
        {
            Clear();
        }
        
    }


    void OpenFull(int selection)
    {

    }

    void Clear()
    {
        for (int i = 1; i < 5; i++)
        {
            Transform current = listPage.Find("Quest (" + i.ToString() + ")");

            current.Find("Heading").GetComponent<TextMesh>().text = "";
            current.Find("Setter").GetComponent<TextMesh>().text = "";
            current.Find("Current Instruction").GetComponent<TextMesh>().text = "";
        }
    }

    void close()
    {
        //Resets phone to home screen to close the app
        phone.transform.GetChild(0).GetComponent<MeshRenderer>().material = homescreen;
        phone.GetComponent<MobilePhone>().appClosed = true;
        backgroundUpdate = true;
        gameObject.SetActive(false);
    }
}
