using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHandler : MonoBehaviour {


    List<Quest> activeQuestList = new List<Quest>();
    List<Quest> inactiveQuestList = new List<Quest>();
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void addQuest(string title, Color colour, string giverName, string text, string directions)
    {
        Debug.Log("Adding Quest: " + title);
        bool alreadyExists = false;
        Quest questToAdd = new Quest(title, colour, giverName, text, directions);
        if(activeQuestList.Count != 0)
        {
            foreach (Quest q in activeQuestList)
            {
                if(q.title == questToAdd.title)
                {
                    alreadyExists = true;
                    q.giverName = questToAdd.giverName;
                    q.directions = questToAdd.directions;
                }
            }
            if (!alreadyExists)
            {
                activeQuestList.Add(questToAdd);
            }
        }
        else
        {
            activeQuestList.Add(questToAdd);
        }
    }

    public void switchQuest(string title)
    {
        if(activeQuestList.Count != 0)
        {
            foreach (Quest q in activeQuestList)
            {
                if (q.title == title)
                {
                    inactiveQuestList.Add(q);
                    activeQuestList.Remove(q);

                }
            }
        }
    }

    public List<Quest> getActiveQuestList()
    {
        return activeQuestList;
    }
    public List<Quest> getInactiveQuestList()
    {
        return inactiveQuestList;
    }
}
