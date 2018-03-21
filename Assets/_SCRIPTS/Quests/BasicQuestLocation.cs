using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicQuestLocation : MonoBehaviour {

    public bool isFirst;

    public float questRadius = 10;

    public string questGiverName;

    [TextArea]
    public string questText;

    public GameObject nextQuestPoint;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, questRadius);

    }
    // Use this for initialization
    void Start () {
        if (isFirst)
        {
            gameObject.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);

            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.activeSelf)
        {
            Collider[] col = Physics.OverlapSphere(transform.position, questRadius, 1 << LayerMask.NameToLayer("Player"));
            if (col.Length != 0)
            {
                if (nextQuestPoint != null)
                {
                    if (!nextQuestPoint.activeSelf)
                    {
                        Debug.Log("The Quest Continues!!!");
                        nextQuestPoint.SetActive(true);
                        nextQuestPoint.transform.GetChild(0).gameObject.SetActive(true);

                        
                    }
                    
                }
                this.gameObject.SetActive(false);
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
