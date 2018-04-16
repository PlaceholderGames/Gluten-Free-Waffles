using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicQuestLocation : BaseQuest
{

    public float questRadius = 10;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, questRadius);

    }
    // Use this for initialization
    void Start () {
        setup();
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
                    continueQuest();


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
