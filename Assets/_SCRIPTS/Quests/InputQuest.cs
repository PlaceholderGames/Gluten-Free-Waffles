using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputQuest : BaseQuest
{
    

    public string axis;
   
    // Use this for initialization
    void Start () {
        setup();
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.activeSelf)
        {
            if (Input.GetAxis(axis) != 0)
            {
                if (nextQuestPoint != null)
                {
                    if (!nextQuestPoint.activeSelf)
                    {
                        nextQuestPoint.SetActive(true);
                        nextQuestPoint.transform.GetChild(0).gameObject.SetActive(true);


                    }

                }
                else
                {
                    endQuest();
                }
                this.gameObject.SetActive(false);
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
