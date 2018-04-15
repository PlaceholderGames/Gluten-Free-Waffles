using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemQuestPickup : MonoBehaviour
{
    public GameObject itemToPickup;

    public bool isFirst;

    public string questGiverName;

    public string questDirections;

    [TextArea]
    public string questText;

    public GameObject nextQuestPoint;

    private ItemHeldBool objectheld;

    // Use this for initialization
    void Start()
    {
        objectheld = itemToPickup.GetComponent<ItemHeldBool>();

        if(objectheld == null)
        {
            objectheld = itemToPickup.AddComponent<ItemHeldBool>();
        }
        if (isFirst)
        {
            gameObject.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text = questDirections;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (objectheld.beingHeld)
            {
                Debug.Log("Item being held");
                if (nextQuestPoint != null)
                {
                    Debug.Log("Next Quest is not null!");
                    if (!nextQuestPoint.activeSelf)
                    {
                        nextQuestPoint.SetActive(true);
                        nextQuestPoint.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.GetComponent<ItemQuestPickup>().enabled = false;
            }
        }
    }
}
