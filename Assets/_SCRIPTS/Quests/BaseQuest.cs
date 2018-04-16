using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseQuest : MonoBehaviour {
    public bool isFirst;

    public string questGiverName;

    public string questDirections;

    [TextArea]
    public string questText;

    public GameObject[] nextQuestPoint;
    // Use this for initialization

    private float startTime;

    private float endTime;

	// Update is called once per frame
	void Update () {
		
	}
    public void setup()
    {
        startTime = Time.time;
        if (isFirst)
        {
            this.transform.parent.GetComponent<QuestSettings>().startTimeQuest = startTime;
            gameObject.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.SetActive(false);
            
        }

        this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = questDirections;
    }
    public void endQuest()
    {
        endTime = Time.time;
        this.transform.parent.GetComponent<QuestSettings>().endTimeQuest = endTime;
        this.transform.parent.GetComponent<QuestSettings>().questCompleted = true;
    }

    public void continueQuest()
    {
        endTime = Time.time;
        foreach (GameObject g in nextQuestPoint)
        {
            if (!g.activeSelf)
            {
                g.SetActive(true);
                g.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    public float getStartTime()
    {
        return startTime;
    }

    public float getEndTime()
    {
        return endTime;
    }
}
