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

        LoadQuests();

    }

    void LoadQuests()
    {
        List<Quest> quests = handler.getActiveQuestList();

        int questCount = quests.Count;
        print(questCount);





        for (int i = 1; i < 5; i++)
        {

        }
    }


    void OpenFull(int selection)
    {

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
