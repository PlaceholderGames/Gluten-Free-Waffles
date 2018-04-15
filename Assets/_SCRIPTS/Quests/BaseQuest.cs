using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseQuest : MonoBehaviour {
    public bool isFirst;

    public string questGiverName;

    public string questDirections;

    [TextArea]
    public string questText;

    public GameObject nextQuestPoint;
    // Use this for initialization

	
	// Update is called once per frame
	void Update () {
		
	}
    public void setup()
    {
        if (isFirst)
        {
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
        Debug.Log("Quest is Over!");
    }
}
