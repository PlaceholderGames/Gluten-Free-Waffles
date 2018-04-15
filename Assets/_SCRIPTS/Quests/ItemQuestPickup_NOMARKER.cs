using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemQuestPickup_NOMARKER : MonoBehaviour {

    public bool isFirst;

    public string questGiverName;

    public string questDirections;

    [TextArea]
    public string questText;

    public GameObject nextQuestPoint;

    // Use this for initialization
    void Start()
    {
        if (isFirst)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (GetComponent<ItemHeldBool>().beingHeld)
            {
                Debug.Log("Item being held");
                if (nextQuestPoint != null)
                {
                    Debug.Log("Next Quest is not null!");
                    if (!nextQuestPoint.activeSelf)
                    {
                        nextQuestPoint.SetActive(true);
                        if(nextQuestPoint.transform.GetChild(0) != null)
                            nextQuestPoint.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
                gameObject.GetComponent<ItemQuestPickup>().enabled = false;
            }
        }
    }
}
