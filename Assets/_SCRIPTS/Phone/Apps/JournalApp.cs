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
        List<Quest> quests = handler.getActiveQuestList();

        int numberOfQuests = quests.Count;
        int pages = (numberOfQuests / 4);

        int questCount = pages * 4;

        for (int i = 1; i < 5; i++)
        {
            Transform current = listPage.Find("Quest (" + i.ToString() + ")");

            current.Find("Heading").GetComponent<TextMesh>().text = quests[questCount].title;
            current.Find("Setter").GetComponent<TextMesh>().text = quests[questCount].giverName;
            current.Find("Current Instruction").GetComponent<TextMesh>().text = quests[questCount].directions;

            if (questCount < numberOfQuests)
                questCount++;
            if (numberOfQuests == 0)
                break;
        }

        listPage.Find("Page Number").GetComponent<TextMesh>().text = pageNumber.ToString();
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
